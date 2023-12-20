using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiegirl : Enemy
{
    protected override void Start()
    {
        base.Start();
        enemyAttackCoolTime = 2;
        enemyPower = 20;
        maxHealth = 60;
        curHealth = maxHealth;
    }
}
