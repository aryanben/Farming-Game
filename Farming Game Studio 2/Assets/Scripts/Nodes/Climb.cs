using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : BaseNode
{
    public override RESULTS UpdateBehavior(EnemyBehaviorTree bt)
    {
        Vector3 dir = bt.target.transform.position - bt.transform.position;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            bt.transform.rotation = Quaternion.Slerp(bt.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        bt.transform.Translate(dir.normalized * bt.moveSpeed * Time.deltaTime, Space.World);
        bt.anim.SetBool("IsMoving", true);

        if (Vector3.Distance(bt.transform.position, bt.target.position) <= 3f)
        {
            GetNextWaypoint(bt);
        }

        current = RESULTS.SUCCEED;
        return current;

    }

    void GetNextWaypoint(EnemyBehaviorTree bt)
    {
        if (bt.wavepointIndex >= bt.nodePoints.Length - 1)
        {
            bt.wavepointIndex = 0;
            return;
        }

        bt.wavepointIndex++;
        bt.target = bt.nodePoints[bt.wavepointIndex].transform;
    }
}
