using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitable
{

    [SerializeField] protected int maxHealth;
    [SerializeField] protected int curHealth;
    [SerializeField] protected int enemyAttackCoolTime;
    [SerializeField] protected int enemyPower;

    public Transform target;
    private bool isAttacking;
    private bool isDead;
    NavMeshAgent nav;
    Animator anim;

    protected virtual void Start()
    {
        defaultSetting();
        FindPlayerTarget();
    }
    private void defaultSetting()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isAttacking = false;
        isDead = false;
    }
    private void FindPlayerTarget()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in the scene!");
        }
    }
    void Update()
    {
        if (!isDead)
        {
            nav.SetDestination(target.position);
            if (nav.hasPath)
            {
                RotateTowardsTarget(nav.steeringTarget);
            }
        }
    }
    void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f); // 5f=> 회전속도조절
    }
    private void OnCollisionEnter(Collision other) 
    {
        CollisionProcess(other);
    }
    private void OnCollisionStay(Collision other)
    {
        CollisionProcess(other);
    }
    private void CollisionProcess(Collision other)
    {
        if (other.gameObject.tag == "Player" && !isAttacking)
        {
            Attack(other);
        }
        /*if (other.gameObject.tag == "무기")
        {
            Attacked();
        }*/
    }
    public void Hit(float damage) // 몬스터가 데미지 받는부분
    {
        damage = 100;
        curHealth -= (int)damage;
        if (!isDead)
        {
            Attacked();
        }
        Debug.Log("HITTest");
    }
    private void Attacked()
    {
        if (curHealth > 0)
        {
            // 맞는 사운드 호출
            anim.SetTrigger("damage");
        }
        else
        {
            // 죽는 사운드 호출
            goLittleDown();
            isDead = true;
            anim.SetTrigger("death");
            Invoke("DestroyObject", 2f);
        }
    }

    private void goLittleDown()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.5f; // 원하는 만큼 y좌표를 조정
        transform.position = newPosition;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    private void Attack(Collision other)
    {
        //공격사운드호출
        anim.SetTrigger("attack");
        other.gameObject.GetComponent<PlayerStatHandler>()?.Hit((float)enemyPower); // 캐릭터한테 데미지를 주는 부분
        isAttacking = true;
        Invoke("ResetAttack", enemyAttackCoolTime);
    }
    private void ResetAttack() 
    { 
        isAttacking = false; 
    }
}
