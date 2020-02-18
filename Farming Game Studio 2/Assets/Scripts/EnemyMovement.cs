using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    Animator anim;
    float timeToReleasePlayerCanBlock = 1;
    float timeToStopKickBack = 0.5f;
    public float minDist;
    public float maxDist;
    public float moveSpeed;
    public float moveBackSpeedForAttack = 30;
    public float moveBackSpeedForBlock = 20;
    public static float attackPeriod = 1;
    public static bool playerCanBlock = false;
    public static bool canAttackAnim;
    bool allowedTimeToAttack;
    bool startTimeToStopKB;
    bool boolforTimeToReleasePlayerCanBlock;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        EnemyAttack();
        EnemyMovementFunction();
    }
   
    void EnemyMovementFunction()
    {
        if (Vector3.Distance(transform.position, player.position) <= minDist)
        {
            Vector3 lookAt = player.position;
            lookAt.y = transform.position.y;
            transform.LookAt(lookAt);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            anim.SetBool("isMoving", true);
        }
    }
    void EnemyAttack()
    {
        if (allowedTimeToAttack)
        {
            attackPeriod -= Time.deltaTime;
            canAttackAnim = true;

            if (canAttackAnim)
            {
                if (canAttackAnim && PlayerMovement.blockAnimation)
                {
                    EnemyJumpBackAfterBlock();
                    playerCanBlock = true;
                    boolforTimeToReleasePlayerCanBlock = true;                 
                }
                if (attackPeriod <= 0)
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isMoving", false);

                    attackPeriod = 1;
                }
                else anim.SetBool("isAttacking", false);

                canAttackAnim = false;
            }
            allowedTimeToAttack = false;
        }
        TimeBasedStuff();        
    } 
    void TimeBasedStuff()
    {
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

        if (boolforTimeToReleasePlayerCanBlock)
        {
            timeToReleasePlayerCanBlock -= Time.deltaTime;
        }

        if (timeToReleasePlayerCanBlock <= 0)
        {
            boolforTimeToReleasePlayerCanBlock = false;
            playerCanBlock = false;
            timeToReleasePlayerCanBlock = 1;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("AttackTrigger"))
        {
            transform.position += transform.forward;
            anim.SetBool("isAttacking", true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AttackTrigger"))
        {
            allowedTimeToAttack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AttackTrigger"))
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isMoving", true);
        }
    }
    public void EnemyJumpBackAfterAttack()
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        transform.GetComponent<Rigidbody>().velocity = direction * moveBackSpeedForAttack;
        startTimeToStopKB = true;
    }
    public void EnemyJumpBackAfterBlock()
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        transform.GetComponent<Rigidbody>().velocity = direction * moveBackSpeedForBlock;
        startTimeToStopKB = true;
    }
}