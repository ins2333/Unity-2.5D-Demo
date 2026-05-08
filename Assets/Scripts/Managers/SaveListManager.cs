using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveListManager : MonoBehaviour
{
    public GameObject SavesPrefab;
    public Transform ContentParent;
    void Start()
    {
        
    }

    public void ShowSaveList() {
        foreach (Transform child in ContentParent) { 
            Destroy(child.gameObject);
        }

        List<PlayerScore> playerSave = ConnectSQLite.Instance.GetAllSaves();

        foreach (PlayerScore record in playerSave)
        {
            GameObject savePrefab = Instantiate(SavesPrefab,ContentParent);
            Text scoreText = savePrefab.transform.Find("ScoreText").GetComponent<Text>();
            Text saveTimeText = savePrefab.transform.Find("SaveTimeText").GetComponent<Text>();

            if (scoreText != null) {
                scoreText.text = $"SCORE:{record.Score}";
            }
            if (saveTimeText != null) {
                saveTimeText.text = $"SAVETIME:{record.Date}";
            }

            Button btn = savePrefab.GetComponent<Button>();
            if (btn != null) {
                int id = record.Id;  // 捕获当前记录的 id，避免闭包问题
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => LoadSaveById(id));
            }
        }

    }
    private void LoadSaveById(int id)
    {
        PlayerScore selected = ConnectSQLite.Instance.GetSaveById(id);
        if (selected != null)
        {           
            // 将分数加载到当前游戏
            PlayerScoreManager.Instance.playerScore = selected.Score;
            //Debug.Log($"加载存档 Id={id}, 分数={selected.Score}");

            SceneManager.LoadScene(1);
            // 关闭存档面板
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError($"找不到存档 {id}");
        }
    }
}
