using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour
{
    /// <summary>
    /// 分数管理器，设置单例，负责处理玩家分数的增加、显示和保存等功能，在场景切换时保持分数数据不丢失，并在每个场景中正确显示当前分数
    /// </summary>
    public static PlayerScoreManager Instance;
    public Text PlayerScoreTextUI;
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
        GameObject scoreTextObj = GameObject.Find("PlayerScoreText");
        if (scoreTextObj != null)
        {
            PlayerScoreTextUI = scoreTextObj.GetComponent<Text>();
            if (PlayerScoreTextUI != null)
                PlayerScoreTextUI.text = $"score: {playerScore}";
        }
        else
        {
            //Debug.Log("当前场景中没有找到 PlayerScoreText 对象");
        }
    }

    public void AddScore(int score)
    {
        // 增加分数并更新 UI 显示
        playerScore += score;
        if (PlayerScoreTextUI != null)
        {
            PlayerScoreTextUI.text = $"score: {playerScore}";
        }
        else
        {
            //Debug.Log("PlayerScoreUI 为空，无法更新分数显示");
        }
    }
}
