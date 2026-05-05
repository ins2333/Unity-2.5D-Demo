//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerScoreManager : MonoBehaviour
//{
//    public static PlayerScoreManager Instance;
//    public Text PlayerScoreUI;
//    public int playerScore = 0;
//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else { 
//            Destroy(gameObject);
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //PlayerScoreUI.text = $"score: {playerScore}";
//    }
//    public void AddScore (int score){
//        PlayerScoreUI.text = $"score: {playerScore += score}";
//    }

//}


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScoreManager : MonoBehaviour
{
    public static PlayerScoreManager Instance;
    public Text PlayerScoreTextUI;   // 不再手动拖拽，改为代码查找
    public int playerScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 监听场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 每次场景加载完成后，重新查找 UI 元素
        // 假设你的分数的 Text 对象叫 "PlayerScoreText"
        GameObject scoreTextObj = GameObject.Find("PlayerScoreText");
        if (scoreTextObj != null)
        {
            PlayerScoreTextUI = scoreTextObj.GetComponent<Text>();
            // 立即刷新显示
            if (PlayerScoreTextUI != null)
                PlayerScoreTextUI.text = $"score: {playerScore}";
        }
        else
        {
            Debug.LogWarning("当前场景中没有找到 PlayerScoreText 对象");
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
        if (PlayerScoreTextUI != null)
            PlayerScoreTextUI.text = $"score: {playerScore}";
        else
            Debug.LogError("PlayerScoreUI 为空，无法更新分数显示");
    }
}
