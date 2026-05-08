using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// 怪物AI脚本，基于枚举类型的FSM有限状态机，负责控制怪物的行为状态，包括巡逻、追逐、攻击和死亡，根据玩家的位置和状态切换不同的行为，使用枚举来定义状态，并在Update方法中根据当前状态执行相应的行为逻辑
    /// </summary>
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
        if (timer >=0.2f)//每0.2秒更新一次状态和行为，避免每帧都进行判断，提高性能
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
