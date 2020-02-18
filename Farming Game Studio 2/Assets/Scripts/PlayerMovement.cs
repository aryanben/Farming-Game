using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
            if (EnemyMovement.playerCanBlock == false)
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
        if (EnemyMovement.playerCanBlock)
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
                isMoving = true;
            }
            else if (Input.GetAxis("Vertical") < -0.001)
            {
                isMoving = true;
            }

            else if (Input.GetAxis("Horizontal") > 0.001)
            {
                isMoving = true;
            }
            else if (Input.GetAxis("Horizontal") < -0.001)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            if (isMoving)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spherePos.position, 1);
    }
}
