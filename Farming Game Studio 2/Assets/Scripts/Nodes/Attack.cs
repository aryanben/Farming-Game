﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BaseNode
{
    public override RESULTS UpdateBehavior(EnemyBehaviorTree bt)
    {
        bt.attackCountdown -= Time.deltaTime;

        if (bt.attackCountdown <= 0)
        {
            bt.anim.SetTrigger("Attack");

            Health.Instance.TakeDamage(10);

            bt.attackCountdown = bt.attackWaitTime;
        }
        current = RESULTS.SUCCEED;
        return current;
    }
}
