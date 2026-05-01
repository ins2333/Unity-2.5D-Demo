using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private GameObject player;
    private NavMeshAgent enemyNavi;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;
    private Animator animator;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyNavi = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }


    void Update ()
    {
        //if (!enemyHealth.IsDead && !playerHealth.playerIsDead)
        //{
        //    enemyNavi.SetDestination(player.transform.position);
        //}
        //else { 
        //    enemyNavi.enabled = false;
        //}
    }
    public void EnemyChasePlayer() {
        if (!enemyHealth.IsDead && !playerHealth.playerIsDead)
        {
            enemyNavi.enabled = true;
            enemyNavi.SetDestination(player.transform.position);
        }
    }

    public void EnemyStopMove() {
        if (playerHealth.playerIsDead) {
            animator.SetTrigger("PlayerDead");
            enemyNavi.enabled = false;
        } else if (enemyHealth.IsDead) {
            enemyNavi.enabled = false;
        }
    }
}
