using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : BaseNode
{

    public override RESULTS UpdateBehavior(EnemyBehaviorTree bt)
    {
        if (bt.playerDetect == true)
        {
            current = RESULTS.SUCCEED;
            return current;
        }
        else
        {
            current = RESULTS.FAILED;
            return current;
        }
    }
}
