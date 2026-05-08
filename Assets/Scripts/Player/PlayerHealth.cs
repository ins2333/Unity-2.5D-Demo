using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// 玩家血量脚本组件，负责处理玩家的血量管理、受伤反馈和死亡逻辑等功能，使用AudioSource组件播放受伤和死亡音效，通过Animator组件控制受伤和死亡动画，并在玩家死亡后禁用玩家的移动和射击功能，同时提供一个方法来重启游戏场景
    /// </summary>
    private AudioSource playerSource;
    public AudioClip deathClip;

    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private CapsuleCollider playerCapsule;

    public bool playerIsDead;

    public int health = 100;

    public Text PlayerHealthUI;

    public Image PlayerHurtImage;
    public Color PlayerHurtColor = new Color(1f, 0f, 0f, 0f);
    private bool playerHurt;
    private void Start()
    {
        playerSource = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerCapsule = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        //受伤红屏
        if (playerHurt)
        {
            PlayerHurtImage.color = PlayerHurtColor;
        }
        else
        {
            PlayerHurtImage.color = Color.Lerp(PlayerHurtImage.color, Color.clear, 0.5f * Time.deltaTime);
        }
        playerHurt = false;
    }
    public void TakeDamage(int amount)
    {
        if (playerIsDead)
        {
            return;
        }

        playerHurt = true;

        playerSource.Play();

        health -= amount;
        PlayerHealthUI.text = health.ToString();

        if (health <= 0)
        {
            Death();
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }
    }
    void Death()
    {
        //判断死亡
        playerIsDead = true;
        //播放死亡音效
        playerSource.clip = deathClip;
        playerSource.Play();

        playerAnimator.SetTrigger("Death");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);        
    }
}
