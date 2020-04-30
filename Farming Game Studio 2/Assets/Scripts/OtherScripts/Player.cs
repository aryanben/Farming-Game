using UnityEngine;

public class Player : MonoBehaviour
{
    float moveLimiter = .85f;
    Rigidbody rb;
    public Animator anim;
    public LayerMask enemyMask;
    public float speed;
    public float jumpBackSpeed;
    public static bool hasAttacked;
    public static bool blockAnimation;
    bool enemyLocated;
    public float removeBlock = 1f;
    bool canRemoveBlock;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    float imboredAnimation = 25f;
    public GameObject weapon;
    bool weaponEnabled;
    float currentSpeed;
    float acceleration = 100f;
    float maxSpeed = 280f;
    float movementX;
    float movementY;
    bool canSprint;
    EnemyBehaviorTree bt;
    public static int enemySeen; //For Dialogue
    void Start()
    {
        bt = FindObjectOfType<EnemyBehaviorTree>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = speed;
    }

    private void Update()
    {
        
        Attack();
        Block();

        if (Energy.Instance.destroyPlayer)
        {
            Energy.Instance.destroyPlayerTime -= Time.deltaTime;
            Energy.Instance.canMove = false;

            if (Energy.Instance.destroyPlayerTime <= 0)
            {
                Destroy(gameObject);

                Energy.Instance.destroyPlayer = false;
            }
        }

        if (bt.attackedPlayer)
        {
            print("hi");

            bt.attackedPlayer = false;
        }

        if (Health.Instance.triggerAnimation)
        {
            anim.SetTrigger("Death");
            Health.Instance.triggerAnimation = false;
        }
    }
    void FixedUpdate()
    {
        Movement();
    }

    void EnableColliderOnWeapon()
    {
        weapon.GetComponent<Collider>().enabled = true;
    }
    void DisableColliderOnWeapon()
    {
        weapon.GetComponent<Collider>().enabled = false;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && weaponEnabled) 
        {
            anim.SetBool("FirstHit", true);
        }
        else anim.SetBool("FirstHit", false);

        if (Input.GetMouseButtonDown(1) && weaponEnabled) 
        {
            anim.SetBool("SecondHit", true);
        }
        else anim.SetBool("SecondHit", false);
    }
    void Block()
    {
      
        if (!canSprint)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Enemy.playerCanBlock == false)
                {
                    blockAnimation = true;
                    canRemoveBlock = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                blockAnimation = false;
            }
            if (blockAnimation && weaponEnabled)
            {
                Energy.Instance.canMove = false;
                anim.SetBool("Block", true);
            }
            else if (!blockAnimation)
            {
                Energy.Instance.canMove = true;
                anim.SetBool("Block", false);
            }
            if (Enemy.playerCanBlock)
            {
                blockAnimation = false;
            }
            if (canRemoveBlock)
            {
                removeBlock -= Time.deltaTime;

                if (removeBlock <= 0)
                {
                    blockAnimation = false;
                    removeBlock = 1f;
                }
            }
        }
    }

    public void AnimationFalse()
    {
        anim.SetBool("Dodge", false);
    }

    public void BackToIdle()
    {
        anim.SetBool("idleStay", true);
        anim.SetBool("idleBored", false);
    }

    public void SecondHitFalse()
    {
        anim.SetBool("SecondHit", false);
    }

    void Sprint(float x, float y)
    {
        anim.SetFloat("SprintX", x);
        anim.SetFloat("SprintY", y);
    }

    void Move(float x, float y)
    {
        anim.SetFloat("X", x);
        anim.SetFloat("Y", y);

        if (x > 0 || y > 0)
        {
            anim.SetBool("idleBored", false);
        }

        if (x == 0 && y == 0)
        {
            imboredAnimation -= Time.deltaTime;

            if (imboredAnimation <= 0)
            {
                anim.SetBool("idleBored", true);
                imboredAnimation = 25f;
            }
        }

        if (x == 1 || x == -1 || y == 1 || y == -1)
        {
            Energy.Instance.DecreaseEnergy(Energy.Instance.energyPoint);
        }
        else Energy.Instance.IncreaseEnergy(Energy.Instance.energyPoint);
    }
    void Movement()
    {
        if (Energy.Instance.canMove)
        {
            movementX = Input.GetAxis("Horizontal");
            movementY = Input.GetAxis("Vertical");
            Vector3 movement = Camera.main.transform.TransformDirection(movementX, 0, movementY);
            rb.velocity = movement * speed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

            if (movementX != 0 && movementY != 0) // Check for diagonal movement
            {
                movementX *= moveLimiter;
                movementY *= moveLimiter;
            }

            if (!canSprint)
            {
                anim.SetFloat("SprintX", 0);
                anim.SetFloat("SprintY", 0);
                Move(movementX, movementY);
            }


            if (enemyLocated)
            {
                if (Input.GetKey(KeyCode.S) && Time.time > nextFire)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        transform.position -= transform.forward * Time.deltaTime * 70f;
                        nextFire = Time.time + fireRate;
                        anim.SetBool("Dodge", true);
                    }
                }
            }

            for (int i = 0; i < TargetController.instance.enemiesInRange.Count; i++)
            {
                enemyLocated = true;
            }


            if (!weaponEnabled)
            {
                if (speed <= currentSpeed)
                {
                    speed = currentSpeed;
                }

                if (speed >= maxSpeed)
                {
                    speed = maxSpeed;
                }

                if (Input.GetKey(KeyCode.C))
                {   
                    anim.SetBool("EquipSword", true);
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    canSprint = true;
                    if (canSprint)
                    {
                        Energy.Instance.DecreaseEnergy(Energy.Instance.energyPoint);

                        anim.SetFloat("X", 0);
                        anim.SetFloat("Y", 0);

                        anim.SetBool("Run", true);

                        Sprint(PlayerFollow.instance.camTurnAngle.x, PlayerFollow.instance.camTurnAngle.y);
                        Sprint(movementX, movementY);
                    }
                    speed += acceleration * Time.deltaTime;
                }
                else
                {
                    anim.SetBool("Run", false);
                    canSprint = false;
                    speed -= acceleration * Time.deltaTime;
                    Energy.Instance.IncreaseEnergy(Energy.Instance.energyPoint);
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    anim.SetBool("Run", false);
                }
            }

            if (weaponEnabled)
            {
                if (Input.GetKey(KeyCode.C))
                {
                    anim.SetBool("UnEquipSword", true);
                }
            }

        }

        if (Energy.Instance.cantWalk)
        {
            Energy.Instance.cantWalkFor5Seconds -= Time.deltaTime;

            if (Energy.Instance.cantWalkFor5Seconds <= 0)
            {
                Energy.Instance.cantWalk = false;
                Energy.Instance.canMove = true;
            }
        }
    }

    public void EquipWeapon()
    {
        weaponEnabled = true;

        if (weaponEnabled)
        {
            weapon.SetActive(true);
        }
    }

    public void WeaponEnabled()
    {
        anim.SetBool("EquipSword", false);
    }

    public void UnEquipWeapon()
    {
        weaponEnabled = false;

        if (!weaponEnabled)
        {
            weapon.SetActive(false);
            anim.SetBool("UnEquipSword", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BewareOfEnemy"))
        {
            enemySeen = 1;
        }
    }
}

