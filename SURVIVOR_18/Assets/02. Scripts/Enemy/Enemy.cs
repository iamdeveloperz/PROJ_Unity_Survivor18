using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;

    //Rigidbody rigid;
    //CapsuleCollider capsuleCollider;
    //Material mat;
    NavMeshAgent nav;
    Animator anim;


    void Awake()
    {
        //rigid = GetComponent<Rigidbody>();
        //capsuleCollider = GetComponent<CapsuleCollider>();
        //mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        nav.SetDestination(target.position);
        if (nav.hasPath)
        {
            RotateTowardsTarget(nav.steeringTarget);
        }
    }


    // 목표 지점 방향으로 회전하는 메서드
    void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f); // 회전 속도를 조절
    }
}
