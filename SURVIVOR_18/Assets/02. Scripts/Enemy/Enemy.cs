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
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f); // 회전 속도를 조절
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
        if (other.gameObject.tag == "무기")
        {
            Attacked(other);
        }
    }

    public void Hit(float damage)
    {
        curHealth -= (int)damage;
        Debug.Log("HITTest");

    }

    private void Attack(Collision other)
    {
        
        anim.SetTrigger("attack");
        //플레이어의 체력을 깍아야함 => 아마 싱글톤으로 플레이어 체력을 받아와야할듯함
        //공격사운드호출
        isAttacking = true;
        Invoke("ResetAttack", enemyAttackCoolTime);
    }
    private void ResetAttack() { isAttacking = false; }

    private void Attacked(Collision other)
    {
        //curHealth -= "무기데미지" => 아마 other의 데미지를 받아와야함
        if(curHealth > 0)
        {
            anim.SetTrigger("damage");
            // 맞는 사운드 호출
        }
        else
        {
            anim.SetTrigger("death");
            // 죽는 사운드 호출
            Invoke("DestroyObject", 3f); // 3초뒤에 사라짐
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
