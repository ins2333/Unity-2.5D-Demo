using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private Animator enemyAnimator;
    public bool playerInRange;
    public int EnemyAttackDamage = 10;
    public float AttackCooldown = 1.5f;
    private float lastAttackTime = -100f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAnimator = GetComponent<Animator>();
    }


    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (!playerHealth.playerIsDead && playerInRange && timer >=2.5f &&!enemyHealth.IsDead) {
    //        Attack();
    //    }
    //    if (playerHealth.playerIsDead) {
    //        enemyAnimator.SetTrigger("PlayerDead");
    //    }
    //}

    public void Attack()
    {
        if (Time.time - lastAttackTime >= AttackCooldown)
        {
            lastAttackTime = Time.time;
            playerHealth.TakeDamage(EnemyAttackDamage);
        }
    }
    public void ResetAttackState()
    {
        playerInRange = false;
        lastAttackTime = -100f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
