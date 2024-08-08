using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyRadius = 5f;
    private GameObject player;
    private NavMeshAgent agent;
    private float lastAttackTime = -Mathf.Infinity;
    private float attackCooldown = 1.5f;
    private bool attackingWithWeapon = false;
    private bool walking = false;
    private float health = 50;
    void Start()
    {   
        player = GameManager.player;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(player == null){
            player = GameManager.player;
        }
        if(player != null){
             MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        walking = agent.velocity.magnitude > 0;
        if(distance <= enemyRadius)
        {
            agent.SetDestination(player.transform.position);
            if(distance <= agent.stoppingDistance)
            {   
                walking = false;
                HandleAttack(distance);
                if(player.GetComponent<PlayerFunctions>().IsAttackingWithWeapon() || player.GetComponent<PlayerFunctions>().IsAttackingWithoutWeapon()){
                    //Couldnt fix a bug so I scaled down the attack value I know it's wrong. :(
                    TakeDamage(player.GetComponent<PlayerFunctions>().GetPlayerAttack()/120f);
                }
                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void HandleAttack(float distance){
        bool attackOnCooldown = Time.time < lastAttackTime + attackCooldown;
            if (!attackOnCooldown && !attackingWithWeapon){
                    attackingWithWeapon = true;
                    lastAttackTime = Time.time;
                    StartCoroutine(AttackWithWeapon());
                    if(distance <= agent.stoppingDistance){
                        player.GetComponent<PlayerFunctions>().TakeDamage(10);
                    }
            }
        
    }

    private IEnumerator AttackWithWeapon(){
        while(Time.time < lastAttackTime + attackCooldown){
            //PlayAttackSound();
            yield return new WaitForSeconds(0.55f);
        }
        attackingWithWeapon = false;
    }

    public bool IsAttackingWithWeapon(){
        return attackingWithWeapon;
    }
    
    public bool IsWalking(){
        return walking;
    }

    public void TakeDamage(float damage)
    {   
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

     void Die()
    {
        Destroy(gameObject);
    }
    
}
