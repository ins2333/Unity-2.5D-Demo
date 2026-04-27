using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private Animator enemyAnimator;
    private bool playerInRange;
    public int EnemyAttackDamage =10;
    private float timer = 0;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!playerHealth.playerIsDead && playerInRange && timer >=2.5f &&!enemyHealth.IsDead) {
            Attack();
        }
        if (playerHealth.playerIsDead) {
            enemyAnimator.SetTrigger("PlayerDead");
        }
    }

    private void Attack()
    {
        timer = 0;
        playerHealth.TakeDamage(EnemyAttackDamage);
    }
    public void ResetAttackState()
    {
        playerInRange = false;
        timer = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject==player)
        {
            playerInRange = false;
        }
    }
}
