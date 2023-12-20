public class Zombiegirl : Enemy
{
    protected override void Start()
    {
        base.Start();
        enemyAttackCoolTime = 2;
        enemyPower = 5;
        maxHealth = 60;
        curHealth = maxHealth;
    }
}
