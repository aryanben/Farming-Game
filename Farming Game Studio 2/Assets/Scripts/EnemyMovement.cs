using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float minDist;
    public float maxDist;
    public float moveSpeed;

    Animator anim;

    public float attackPeriod = 1;

    public float moveBackSpeed = 5;
    bool startTimeToStopKB;
    float timeToStopKickBack = .5f;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        EnemyMovementFunction();
    }

    void EnemyMovementFunction()
    {
        if (Vector3.Distance(transform.position, player.position) >= minDist)
        {
            anim.SetBool("isMoving", false);
        }

        if (Vector3.Distance(transform.position, player.position) <= minDist)
        {
            Vector3 lookAt = player.position;
            lookAt.y = transform.position.y;
            transform.LookAt(lookAt);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            anim.SetBool("isMoving", true);

            if (Vector3.Distance(transform.position, player.position) <= maxDist)
            {
                attackPeriod -= Time.deltaTime;

                if (attackPeriod <= 0)
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isMoving", false);
                    attackPeriod = 2;
                }
                else anim.SetBool("isAttacking", false);
            }

            if (startTimeToStopKB)
            {
                timeToStopKickBack -= Time.deltaTime;
            }
            if (timeToStopKickBack <= 0)
            {
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                startTimeToStopKB = false;
                timeToStopKickBack = 0.5f;
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player") && other.collider.transform.GetComponent<PlayerMovement>() !=null && PlayerMovement.hasAttacked)
        {
            Vector3 direction = (transform.position - other.transform.position).normalized;
            transform.GetComponent<Rigidbody>().velocity = direction * moveBackSpeed;
            startTimeToStopKB = true;
            PlayerMovement.hasAttacked = false;
        }        
    }
}


    