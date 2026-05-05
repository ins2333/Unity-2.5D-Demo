using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using Unity.VisualScripting;

public class ConnectSQLite : MonoBehaviour
{
    public static ConnectSQLite Instance;
    private void Awake()
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
    }
    //连接数据库
    private SQLiteConnection Connection;
    void Start()
    {
        //路径及命名
        //string databasePath  = Application.persistentDataPath + "/PlayerDatabase.db";
        string databasePath  = Application.streamingAssetsPath + "/PlayerDatabase.db";
        //创建数据库及读写权限
        Connection = new SQLiteConnection(databasePath,SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log($"数据库连接成功,路径{databasePath}");

        Connection.CreateTable<PlayerScore>();
    }

    public void OnSaveButtonClick() {
        int score = PlayerScoreManager.Instance.playerScore;
        SaveScore(score);
    }

    public void SaveScore(int score) {
        PlayerScore playerScore = new PlayerScore();
        if (score > 0)
        {
            playerScore.Score = score;
            playerScore.Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Connection.Insert(playerScore);
            //Debug.Log($"{score}分数保存成功");
        }
        else { 
            //Debug.Log($"分数不能为0！");
        }
    }

}
