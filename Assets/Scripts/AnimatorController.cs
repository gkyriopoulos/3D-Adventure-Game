using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerFunctions playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerFunctions>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        Vector3 playerDirection = playerMovement.GetMovementDirection();

        animator.SetFloat("xInput", playerDirection.x, 0.1f, Time.deltaTime);
        animator.SetFloat("zInput", playerDirection.z, 0.1f, Time.deltaTime);

        animator.SetBool("isRunning", playerDirection != Vector3.zero);

        animator.SetBool("isDashing", playerMovement.IsDashing());

        animator.SetBool("isSprinting", playerMovement.IsSprinting());

        animator.SetBool("isGrounded", playerMovement.IsGrounded());

        animator.SetBool("isAttackingWithWeapon", playerMovement.IsAttackingWithWeapon());

        animator.SetBool("isAttackingWithoutWeapon", playerMovement.IsAttackingWithoutWeapon());
    }
}
