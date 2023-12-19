using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiegirl : Enemy
{
    protected override void Start()
    {
        base.Start();
        enemyAttackCoolTime = 1;
        enemyPower = 20;
    }
}
