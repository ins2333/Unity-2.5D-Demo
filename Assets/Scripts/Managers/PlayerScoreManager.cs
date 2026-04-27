using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour
{
    public static PlayerScoreManager Instance;
    public Text PlayerScoreUI;
    public int playerScore = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { 
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerScoreUI.text = $"score: {playerScore}";
    }
    public void AddScore (int score){
        PlayerScoreUI.text = $"score: {playerScore += score}";
    }
  
}
