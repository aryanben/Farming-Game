using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public Animator anim;
    bool isMoving = false;
    int attackedOnce;
    public static bool hasAttacked;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        LookToMouse();       
    }
    void FixedUpdate()
    {
       Movement();
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

        if (GameManager.isAllowedToAttack)
        {
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("isAttacking", true);
                attackedOnce++;
            }
            else anim.SetBool("isAttacking", false);

            if (attackedOnce >= 2)
            {
                anim.SetBool("isAttacking2", true);
                attackedOnce = 0;
            }
            else anim.SetBool("isAttacking2", false);
        }       

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {          
            for (int i = 0; i < TargetController.instance.enemiesInRange.Count; i++)
            {
                anim.SetBool("isJumping", true);

                Vector3 enemyPos = TargetController.instance.enemiesInRange[i].transform.position;

                Vector3 direction = (transform.position - enemyPos).normalized;
                transform.GetComponent<Rigidbody>().velocity = direction * speed;
            }     
        }   
        else anim.SetBool("isJumping", false);
    }

    //Used For the Animation Events
    void FirstSkill()
    {
        hasAttacked = true;
    }

    void SecondSkill()
    {
        hasAttacked = true;
    }
}
