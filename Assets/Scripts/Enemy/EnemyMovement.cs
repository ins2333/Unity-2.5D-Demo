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
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyNavi = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    void Update ()
    {
        if (!enemyHealth.IsDead && !playerHealth.playerIsDead)
        {
            enemyNavi.SetDestination(player.transform.position);
        }
        else { 
            enemyNavi.enabled = false;
        }
    }
    
}
