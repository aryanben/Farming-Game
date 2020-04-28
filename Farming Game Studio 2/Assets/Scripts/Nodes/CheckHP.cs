using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHP : BaseNode
{
    public override RESULTS UpdateBehavior(EnemyBehaviorTree bt)
    {
        if (bt.currHealth >= bt.lowHealth)
        {
            current = RESULTS.SUCCEED;
            return current;
        }

        if (bt.currHealth <= 0)
        {
            current = RESULTS.FAILED;
            return current;
        }

        return current;
    }
}
