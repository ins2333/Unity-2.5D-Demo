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
    public AudioClip enemyHurtClip;
    private Animator enemyAnimator;
    private CapsuleCollider enemyCapsuleCollider;
    private SphereCollider enemySphereCollider;
    private EnemyAttack enemyAttack;
    private NavMeshAgent enemyNavi;
    public bool IsDead;
    //private bool IsSink;

    public int EnemyDeathScore;
    private ObjectPool<EnemyHealth> _enemyPool;
  
    private void Start()
    {
        initialHealth = health;

        enemyParticles = GetComponentInChildren<ParticleSystem>();
        enemySound = GetComponentInChildren<AudioSource>();
        enemyAnimator = GetComponent<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider>();
        enemySphereCollider = GetComponent<SphereCollider>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyNavi = GetComponent<NavMeshAgent>();
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

    public void Death()
    {
        if (IsDead) {
            return;
        }

        IsDead = true;
        enemyAnimator.SetTrigger("Death");
        enemySound.clip = enemyDeathClip;
        enemySound.Play();
        enemyAttack.enabled = false;
        PlayerScoreManager.Instance.AddScore(EnemyDeathScore);
        
        StartCoroutine(SinkAndRecover());
    }
    IEnumerator SinkAndRecover() {
        float sinkTime = 2.5f;
        float elapsed = 0f;
        while (elapsed < sinkTime) {
            transform.Translate(-transform.up * 0.3f * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(RecoverToPool());
    }

    IEnumerator RecoverToPool() {
        yield return null;
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
        //IsSink = true;
        //enemyCapsuleCollider.isTrigger = true;//怪物死亡仍受子弹影响
        enemyCapsuleCollider.enabled = false;
        enemySphereCollider.enabled = false;

        enemyNavi.enabled = false;
        //Destroy(gameObject,2f);
    }

    void ResetEnemy() { 

        IsDead= false;
        //IsSink = false;
        health = initialHealth;

        enemySound.clip=enemyHurtClip;
        enemyAttack.ResetAttackState();
        //enemyCapsuleCollider.isTrigger = false;
        enemyCapsuleCollider.enabled = true;
        enemySphereCollider.enabled = true;
        enemyAttack.enabled = true;
        enemyNavi.enabled = true;
    }
}
