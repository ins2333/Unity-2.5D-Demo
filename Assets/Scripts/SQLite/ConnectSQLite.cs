using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using Unity.VisualScripting;

public class ConnectSQLite : MonoBehaviour
{
    /// <summary>
    /// 数据库连接管理器，设置单例，负责处理与SQLite数据库的连接、数据的保存和读取等功能
    /// </summary>
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


    public void SaveScore(int score) {
        PlayerScore playerScore = new PlayerScore();
        if (score > 0)
        {
            playerScore.Score = score;
            playerScore.Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Connection.Insert(playerScore);
            Debug.Log($"{score}分数保存成功");
        }
        else { 
            //Debug.Log($"分数不能为0！");
        }
    }

    public PlayerScore GetSaveById(int id)
    {
        return Connection.Find<PlayerScore>(id);
    }

    public List<PlayerScore> GetAllSaves() {
        string sql = "SELECT * FROM PlayerScore ORDER BY Date DESC;";
        return Connection.Query<PlayerScore>(sql);
    }
}
