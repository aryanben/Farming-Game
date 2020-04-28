using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BaseNode
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
            else if(current == RESULTS.SUCCEED)
            {
                return RESULTS.SUCCEED;
            }
        }
        return RESULTS.FAILED;
    }
}
