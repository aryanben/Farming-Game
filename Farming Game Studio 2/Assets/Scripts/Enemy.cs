using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
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

    public int attackDamage = 50;
    public int startingHealth = 20;
    public static float currentHealth;
    //public Slider healthSlider;

    Player playerHealth;
    Rigidbody rb;

    private void Start()
    {
        currentHealth = startingHealth;
        //healthSlider.maxValue = currentHealth;
        //healthSlider.value = currentHealth;

        playerHealth = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        EnemyAttack();
        EnemyMovementFunction();


    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        //healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void EnemyMovementFunction()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) <= minDist)
            {
                Vector3 lookAt = player.position;
                lookAt.y = transform.position.y;
                transform.LookAt(lookAt);

                Vector3 direction = (player.position - transform.position).normalized;
                rb.velocity += (direction * moveSpeed * Time.deltaTime);

                anim.SetBool("isMoving", true);
            }
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
                if (canAttackAnim && Player.blockAnimation)
                {
                    EnemyJumpBackAfterBlock();
                    playerCanBlock = true;
                    boolforTimeToReleasePlayerCanBlock = true;                 
                }
                if (attackPeriod <= 0)
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isMoving", false);

                    if (Player.currentHealth > 0)
                    {
                        playerHealth.TakeDamage(attackDamage);
                    }

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
        if(player != null)
        {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            transform.GetComponent<Rigidbody>().velocity = direction * moveBackSpeedForAttack;
            startTimeToStopKB = true;
        }
    }
    public void EnemyJumpBackAfterBlock()
    {
        if (player != null)
        {
            Vector3 direction = (transform.position - player.transform.position).normalized;
            transform.GetComponent<Rigidbody>().velocity = direction * moveBackSpeedForBlock;
            startTimeToStopKB = true;
        }
    }

    //public void EnemyAttacked()
    //{
    //    if (currentHealth > 0)
    //    {
    //       TakeDamage(attackDamage);
    //    }
    //    if (currentHealth <= 0)
    //    {
    //        currentHealth = startingHealth;
    //    }
    //}
}