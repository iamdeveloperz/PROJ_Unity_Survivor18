using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;

    Rigidbody rigid;
    CapsuleCollider capsuleCollider;
    Material mat;
    NavMeshAgent nav;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.SetDestination(target.position);
        // 에이전트가 이동 중인 경우
        if (nav.hasPath)
        {
            // 이동 방향으로 회전
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {



        }
    }
}
