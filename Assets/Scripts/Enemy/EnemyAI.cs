using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyAttack EnemyAttack;
    private EnemyHealth EnemyHealth;
    private EnemyMovement EnemyMovement;
    private GameObject Player;
    private PlayerHealth PlayerHealth;
    private float timer=0f;
    public enum State {
        Patrol,Chase,Attack,Dead
    }

    public State currentState = State.Patrol;
    private void Awake()
    {
        EnemyHealth = GetComponent<EnemyHealth>();
        EnemyAttack = GetComponent<EnemyAttack>();
        EnemyMovement = GetComponent<EnemyMovement>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = Player.GetComponent<PlayerHealth>();
    }
    void Update()
    {
        timer += Time.deltaTime; 
        if (timer >=0.2f)
        {
            UpdateState();
            timer = 0f;
            EnemyState();
        }
        //EnemyState(); 
    }
    public void UpdateState() {
        if (EnemyHealth.IsDead)
        {
            currentState = State.Dead;
        }
        else if (EnemyAttack.playerInRange)
        {
            currentState = State.Attack;
        }
        else if (PlayerHealth.playerIsDead)
        {
            currentState = State.Patrol;   
        }
        else
        {
            currentState = State.Chase;
        }

    }
    public void EnemyState() {
        switch (currentState) {
            case State.Patrol:
                EnemyMovement.EnemyStopMove();
                break;
            case State.Chase:
                EnemyMovement.EnemyChasePlayer();
                break;
            case State.Attack:
                EnemyAttack.Attack();
                break;
            case State.Dead:
                EnemyHealth.Death();
                break;
        }
    }
}
