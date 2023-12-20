public class Zombieman : Enemy
{
    protected override void Start()
    {
        base.Start();
        enemyAttackCoolTime = 2;
        enemyPower = 10;
        maxHealth = 100;
        curHealth = maxHealth;
    }
}
