using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Collider[] enemiesInContact;
    Rigidbody rb;
    public Transform spherePos;
    public Animator anim;
    public LayerMask enemyMask;
    public float speed;
    public float jumpBackSpeed;
    public static bool hasAttacked;
    public static bool blockAnimation;
    bool isMoving = false;
    bool canMove;

    //PlayerHealth
    public int startingHealth = 100;                            
    public static float currentHealth;                          
    public Slider healthSlider;
    public int attackDamage = 20;

    //PlayerEnergy
    public int startingEnergy = 100;
    public static float currentEnergy;
    public Slider EnergySlider;
    float cantWalkFor5Seconds;
    bool cantWalk;
    float energyPoint = 0.01f;

    private void Awake()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;

        currentEnergy = startingEnergy;
        EnergySlider.maxValue = currentEnergy;
        EnergySlider.value = currentEnergy;
    }
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        LookToMouse();
        Attack();
        Block();
    }
    void FixedUpdate()
    {
        Movement();
    }

    
    void Attack()
    {     
        if (Input.GetMouseButtonDown(0) && GameManager.isAllowedToAttack)
        {
            anim.SetTrigger("AttackTrigger");

            enemiesInContact = Physics.OverlapSphere(spherePos.position, 1, enemyMask);

            for (int i = 0; i < enemiesInContact.Length; i++)
            {
                Debug.Log(enemiesInContact[i].name);
                enemiesInContact[i].SendMessage("EnemyJumpBackAfterAttack");
            }          
        }       
    }
    void Block()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Enemy.playerCanBlock == false)
            {
                blockAnimation = true;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            blockAnimation = false;
        }
        if (blockAnimation)
        {
            canMove = false;

            anim.SetBool("canBlock", true);
        }  
        else if (!blockAnimation)
        {
            canMove = true;
            anim.SetBool("canBlock", false);
        }
        if (Enemy.playerCanBlock)
        {
            blockAnimation = false;
        }
    }

    void LookToMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane invisiblePlane = new Plane(Vector3.up, Vector3.zero);

        if (invisiblePlane.Raycast(cameraRay, out float rayPoint))
        {
            Vector3 lookAt = cameraRay.GetPoint(rayPoint);

            transform.LookAt(lookAt);
        }
    }
    void Movement()
    {
        if (canMove)
        {  

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0, vertical);
            rb.velocity = movement * speed * Time.deltaTime;

            if (Input.GetAxis("Vertical") > 0.001)
            {
                DecreaseEnergy(energyPoint);
                isMoving = true;
            }
            else if (Input.GetAxis("Vertical") < -0.001)
            {
                DecreaseEnergy(energyPoint);
                isMoving = true;
            }

            else if (Input.GetAxis("Horizontal") > 0.001)
            {
                DecreaseEnergy(energyPoint);
                isMoving = true;
            }
            else if (Input.GetAxis("Horizontal") < -0.001)
            {
                DecreaseEnergy(energyPoint);
                isMoving = true;
            }
            else
            {
                isMoving = false;
                IncreaseEnergy(energyPoint);
            }

            if (isMoving)
            {
                DecreaseEnergy(energyPoint);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                IncreaseEnergy(energyPoint);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                for (int i = 0; i < TargetController.instance.enemiesInRange.Count; i++)
                {
                    anim.SetBool("isJumping", true);

                    Vector3 enemyPos = TargetController.instance.enemiesInRange[i].transform.position;

                    Vector3 direction = (transform.position - enemyPos).normalized;
                    transform.GetComponent<Rigidbody>().velocity = direction * jumpBackSpeed;
                }
            }
            else anim.SetBool("isJumping", false);
        }

        if (cantWalk)
        {
            cantWalkFor5Seconds -= Time.deltaTime;

            if(cantWalkFor5Seconds <= 0)
            {
                cantWalk = false;
                canMove = true;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DecreaseEnergy(float amount)
    {
        currentEnergy -= amount;

        EnergySlider.value = currentEnergy;

        if (currentEnergy <= 0)
        {
            canMove = false;
            cantWalk = true;
        }
    }

    public void IncreaseEnergy(float amount)
    {
        currentEnergy += amount;

        EnergySlider.value = currentEnergy;

        if (currentEnergy >= 100)
        {
            currentEnergy = 100;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spherePos.position, 1);
    }
}
