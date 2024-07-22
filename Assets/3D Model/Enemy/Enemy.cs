using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent enemy;
    private bool isAttacking;
    [SerializeField] private Animator animator;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }
    void Update()
    {
        HandleAttack();
        HandleMove();
    }

    void HandleMove()
    {
        animator.SetBool("b_idle", false);
        animator.SetTrigger("t_run");
        enemy.SetDestination(player.transform.position);
    }

    void HandleAttack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2 && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("t_attacking");
            Invoke("HandleAttackFinish", 1f);
            Debug.Log("Atacou");
        }
    }

    void HandleAttackFinish()
    {
        isAttacking = false;
        animator.SetBool("b_idle", true);
    }
}
