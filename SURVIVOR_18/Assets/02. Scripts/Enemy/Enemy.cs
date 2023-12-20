using System.Collections;
using System.Collections.Generic;
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

    NavMeshAgent nav;
    Animator anim;

    protected virtual void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isAttacking = false;
    }

    void Update()
    {
        nav.SetDestination(target.position);
        if (nav.hasPath)
        {
            RotateTowardsTarget(nav.steeringTarget);
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
        if (other.gameObject.tag == "Player")
        {
            Attack(other);
        }
        /*if (other.gameObject.tag == "무기")
        {
            Attacked();
        }*/
    }

    public void Hit(float damage)
    {
        curHealth -= (int)damage;
        Attacked();
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
            anim.SetTrigger("death");
            Invoke("DestroyObject", 3f);
        }
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    private void Attack(Collision other)
    {
        //공격사운드호출
        anim.SetTrigger("attack");
        if (isAttacking != true)
        {
            other.gameObject.GetComponent<PlayerStatHandler>()?.Hit((float)enemyPower);
        }
        isAttacking = true;
        Invoke("ResetAttack", enemyAttackCoolTime);
    }
    private void ResetAttack() 
    { 
        isAttacking = false; 
    }
}
