using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayerHealth : MonoBehaviour
{
    private AudioSource playerSource;
    public AudioClip deathClip;

    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;

    public bool playerIsDead;

    public int health = 100;

    public Text PlayerHealthUI;

    public Image PlayerHurtImage;
    public Color PlayerHurtColor = new Color(1f, 0f, 0f, 0f);
    private bool playerHurt;
    void Awake()
    {
        playerSource = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
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
