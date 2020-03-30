using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
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
    public float removeBlock = 1f;
    bool canRemoveBlock;
    public Animator quickAnim;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    Vector3 slideDir;
    Vector3 slideDir2;
    public float rollSpeed = 3000f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationClip clip)
    {
        //RuntimeAnimatorController.
        //quickAnim.runtimeAnimatorController;
        //quickAnim.clip = clip;
        //quickAnim.Play();
    }

    private void Update()
    {

        LookToMouse();
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
                canRemoveBlock = true;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            blockAnimation = false;
        }
        if (blockAnimation)
        {
            Energy.Instance.canMove = false;

            anim.SetBool("canBlock", true);
        }
        else if (!blockAnimation)
        {
            Energy.Instance.canMove = true;
            anim.SetBool("canBlock", false);
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
        if (Energy.Instance.canMove)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement = Camera.main.transform.TransformDirection(movement);
            rb.velocity = movement * speed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);


            if (Input.GetAxis("Vertical") > 0.001)
            {
                Energy.Instance.DecreaseEnergy(Energy.Instance.energyPoint);
                isMoving = true;
            }
            else if (Input.GetAxis("Vertical") < -0.001)
            {
                Energy.Instance.DecreaseEnergy(Energy.Instance.energyPoint);
                isMoving = true;
            }

            else if (Input.GetAxis("Horizontal") > 0.001)
            {
                Energy.Instance.DecreaseEnergy(Energy.Instance.energyPoint);
                isMoving = true;
            }
            else if (Input.GetAxis("Horizontal") < -0.001)
            {
                Energy.Instance.DecreaseEnergy(Energy.Instance.energyPoint);
                isMoving = true;
            }
            else
            {
                isMoving = false;
                Energy.Instance.IncreaseEnergy(Energy.Instance.energyPoint);
            }

            if (isMoving)
            {
                Energy.Instance.DecreaseEnergy(Energy.Instance.energyPoint);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                Energy.Instance.IncreaseEnergy(Energy.Instance.energyPoint);
            }

            if (Input.GetKey(KeyCode.LeftShift) && Time.time > nextFire)
            {
               
                    anim.SetTrigger("DodgeBack");
                    anim.SetBool("isIdle", false);
                    nextFire = Time.time + fireRate;

                   

                    //slideDir = movement;
                    //GetComponent<Rigidbody>().velocity = slideDir * rollSpeed * Time.deltaTime;
                
            }
            else
            {
                anim.ResetTrigger("DodgeBack");
                anim.SetBool("isIdle", true);
            }
            //for (int i = 0; i < TargetController.instance.enemiesInRange.Count; i++)
            //{
            //    anim.SetBool("isJumping", true);

            //    Vector3 enemyPos = TargetController.instance.enemiesInRange[i].transform.position;

            //    Vector3 direction = (transform.position - enemyPos).normalized;
            //    transform.GetComponent<Rigidbody>().velocity = direction * jumpBackSpeed;
            //}
            //else anim.SetBool("isJumping", false);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spherePos.position, 1);
    }

    void AnimationCall()
    {
        for (int i = 0; i < TargetController.instance.enemiesInRange.Count; i++)
        {
            Vector3 enemyPos = TargetController.instance.enemiesInRange[i].transform.position;

            Vector3 direction = (transform.position - enemyPos).normalized;
            transform.GetComponent<Rigidbody>().velocity = direction * jumpBackSpeed;
        }
    }
}
