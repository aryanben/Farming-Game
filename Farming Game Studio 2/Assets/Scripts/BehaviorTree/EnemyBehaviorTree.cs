using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviorTree : MonoBehaviour
{
    public bool canAttackPlayer;
    public bool attackedPlayer = false;
    public BaseNode root;
    public BaseNode current;    
    public Animator anim;
    public BoxCollider boxCollider;
    public Rigidbody rb;
    public float maxVelocity = 3.5f;
    public float maxAcceleration = 10f;
    public float slowRadius = 1f;
    public GameObject[] nodePoints;
    public GameObject player;
    public Transform target;

    public bool isDead;
    public float healWaitTime;
    public float moveSpeed;
    public float detectRange;
    public float attackRange;

    public float attackWaitTime;
    public float enemyReach;
    public bool playerDetect;

    [HideInInspector]
    public float healthCountdown;
    public float attackCountdown;

    public int wavepointIndex;
    public int maxHealth;
    public float currHealth;
    public int lowHealth;
    public int damage;

    public Slider healthBar;

    Selector selector1;
    Sequence sequence1;
    Sequence sequence2;

    CheckRange checkRange;
    CheckHP checkHp;
    Chase chase;
    Attack attack;
    Climb climb;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();

        
        selector1 = new Selector();
        sequence1 = new Sequence();
        sequence2 = new Sequence();
        climb = new Climb();
        checkRange = new CheckRange();
        checkHp = new CheckHP();
        chase = new Chase();
        attack = new Attack();

        root = selector1;

        selector1.childNodes.Add(sequence1);
        selector1.childNodes.Add(climb);

        sequence1.childNodes.Add(sequence2);
        sequence1.childNodes.Add(chase);
        sequence1.childNodes.Add(attack);

        sequence2.childNodes.Add(checkHp);
        sequence2.childNodes.Add(checkRange);
        
        target = nodePoints[0].transform;

        player = GameObject.FindGameObjectWithTag("Player");

        currHealth = maxHealth;
        healthBar.maxValue = currHealth;
        healthBar.value = currHealth;

        healthCountdown = healWaitTime;
    }

    public virtual void Update()
    {
        root.UpdateBehavior(this);
        
        if (currHealth <= 0)
        {
            currHealth = 0; 
            moveSpeed = 0;
            anim.SetBool("IsMoving", false);
            rb.isKinematic = true;
            anim.SetBool("isDead", true);
        }

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

    
    }    

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetect = true;
        }

        if (other.CompareTag("Sword"))
        {
            transform.position -= transform.forward * Time.deltaTime * 90f;
            TakeDamage(Random.Range(3, 10));
        }
        if (other.CompareTag("Sword") && attackedPlayer)
        {
            transform.position -= transform.forward * Time.deltaTime * 120f;
        }
    }

    public void TakeDamage(float amount)
    {
        currHealth -= amount;

        healthBar.value = currHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            canAttackPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            canAttackPlayer = false;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetect = false;
        }
    }
}
