using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BaseNode
{
    public override RESULTS UpdateBehavior(EnemyBehaviorTree bt)
    {
        for (int i = 0; i < childNodes.Count; i++)
        {
            current = childNodes[i].UpdateBehavior(bt);

            if(current == RESULTS.RUNNING)
            {
                return RESULTS.RUNNING;
            }
            else if(current == RESULTS.FAILED)
            {
                return RESULTS.FAILED;
            }
        }
        return RESULTS.SUCCEED;
    }
}
