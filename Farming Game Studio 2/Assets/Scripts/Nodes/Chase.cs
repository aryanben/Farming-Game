using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseNode
{
    public override RESULTS UpdateBehavior(EnemyBehaviorTree bt)
    {
        if (bt.player != null)
        {
            Vector3 targetVelocity = bt.player.transform.position - bt.transform.position;
            targetVelocity.y = 0;

            if (targetVelocity != Vector3.zero)
            {
                bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(targetVelocity), 0.1f);
            }

            float dist = targetVelocity.magnitude;

            if (dist < bt.attackRange)
            {
                current = RESULTS.SUCCEED;
                return current;
            }

            float targetSpeed;

            if (dist > bt.slowRadius)
            {
                targetSpeed = bt.maxVelocity;
            }
            else
            {
                targetSpeed = bt.maxVelocity * (dist / bt.slowRadius);
            }

            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            Vector3 acceleration = targetVelocity - bt.rb.velocity;

            if (acceleration.magnitude > bt.maxAcceleration)
            {
                acceleration.Normalize();
                acceleration *= bt.maxAcceleration;
            }

            bt.anim.SetBool("IsMoving", true);
            if (bt.playerDetect)
            {
                bt.rb.velocity += acceleration * Time.deltaTime;
            }
            

            if (bt.rb.velocity.magnitude > bt.maxVelocity)
            {
                bt.rb.velocity = bt.rb.velocity.normalized * bt.maxVelocity;
            }

            current = RESULTS.RUNNING;
            return current;
        }
        else current = RESULTS.FAILED;
        return current;        
    }
}