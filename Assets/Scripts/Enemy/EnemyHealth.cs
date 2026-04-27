using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.AI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    private int initialHealth;

    private ParticleSystem enemyParticles;
    private AudioSource enemySound;
    public AudioClip enemyDeathClip;
    private Animator enemyAnimator;
    private CapsuleCollider enemyCapsuleCollider;
    private EnemyAttack enemyAttack;
    private NavMeshAgent enemyNavi;
    public bool IsDead;
    private bool IsSink;

    public int EnemyDeathScore;
    private ObjectPool<EnemyHealth> _enemyPool;
    void Awake ()
    {
        initialHealth = health;

        enemyParticles = GetComponentInChildren<ParticleSystem>();
        enemySound = GetComponentInChildren<AudioSource>();
        enemyAnimator = GetComponent<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyNavi = GetComponent<NavMeshAgent>();
    }


    void Update ()
    {
        if (IsSink) {
            transform.Translate(-transform.up*0.3f*Time.deltaTime);
        }
    }
    public void SetPool(ObjectPool<EnemyHealth> enemyPool) {
        _enemyPool = enemyPool;
    }

    public void TakeDamage(int amount,Vector3 hitPoint) {
        if (IsDead) {
            return;
        }
        enemySound.Play();
        enemyParticles.transform.position = hitPoint;
        enemyParticles.Play();
        health -= amount;
        if (health<=0 ) {
            Death();
        }
        
    }

    private void Death()
    {
        IsDead = true;
        enemyAnimator.SetTrigger("Death");
        enemySound.clip = enemyDeathClip;
        enemySound.Play();
        enemyAttack.enabled = false;
        PlayerScoreManager.Instance.AddScore(EnemyDeathScore);

        StartCoroutine(RecoverToPool());
    }

    IEnumerator RecoverToPool() {
        yield return new WaitForSeconds(2f);
        if (_enemyPool != null)
        {
            ResetEnemy();
            _enemyPool.Release(this);
        }
        else
        {
            Destroy(this);
        }
    }
    public void StartSinking() { 
        IsSink = true;
        enemyCapsuleCollider.isTrigger = true;
        enemyNavi.enabled = false;
        //Destroy(gameObject,2f);
    }
    void ResetEnemy() { 
        IsDead= false;
        IsSink = false;
        health = initialHealth;

        enemyAttack.ResetAttackState();
        enemyCapsuleCollider.isTrigger = false;
        enemyAttack.enabled = true;
        enemyNavi.enabled = true;
    }
}
