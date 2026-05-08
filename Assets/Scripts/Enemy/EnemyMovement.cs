using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    /// <summary>
    /// 怪物移动脚本，负责控制怪物的移动行为，使用NavMeshAgent组件实现寻路功能，使怪物能够智能地追踪玩家的位置，并在玩家死亡或怪物死亡时停止移动，使用Animator组件控制动画状态的切换
    /// </summary>
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
