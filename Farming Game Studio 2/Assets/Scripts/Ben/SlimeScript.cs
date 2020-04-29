using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public float checkRangeAmount;
    public Vector3 towardsPlayer;
    public float jumpForce;
    public float maxJump;
    public float minJump;
    float jumptimer;
    public float jumpTimeLimit;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        towardsPlayer = ((PlayerPos.instance.posVector - transform.position).normalized + Vector3.up).normalized;
        towardsPlayer *=  (Vector3.Distance(transform.position, PlayerPos.instance.posVector * jumpForce));
        
        if (towardsPlayer.magnitude> maxJump)
        {
            towardsPlayer.Normalize();
            towardsPlayer *= maxJump;

        }
        if (towardsPlayer.magnitude < minJump)
        {
            towardsPlayer.Normalize();
            towardsPlayer *= minJump;

        }
        Debug.Log(towardsPlayer.magnitude);
        if (Vector3.Distance(transform.position, PlayerPos.instance.posVector) < checkRangeAmount)
        {
           
            if (jumptimer<=0)
            {
                rb.AddForce(towardsPlayer, ForceMode.VelocityChange);
                jumptimer = jumpTimeLimit;
            }
        }
        jumptimer -= Time.deltaTime;
       
    }
}
