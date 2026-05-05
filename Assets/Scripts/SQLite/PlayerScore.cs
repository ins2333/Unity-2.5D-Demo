using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

/// <summary>
/// 玩家分数数据库表
/// </summary>
public class PlayerScore {
    [PrimaryKey,AutoIncrement]
    public int Id { get; set; }
    //设置主键，自增

    public int Score { get;set; }

    public string Date { get; set; }    
}

