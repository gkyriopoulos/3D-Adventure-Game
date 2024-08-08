using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private EnemyController enemy;
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", enemy.IsWalking());
        animator.SetBool("isAttacking", enemy.IsAttackingWithWeapon());
    }
}
