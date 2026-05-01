using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public EnemyHealth EnemyPrefab;

    public float FirstCreatEnemyTime = 0f;
    public float CreatEnemyTime = 3f;

    public GameObject EnemyCreatPoint;

    private ObjectPool<EnemyHealth> enemyPool;
    //void Start()
    //{
    //    InvokeRepeating("Spwan",FirstCreatEnemyTime,CreatEnemyTime);
    //}
    //private void Spwan() {
    //    Instantiate(Enemy,EnemyCreatPoint.transform.position,EnemyCreatPoint.transform.rotation);
    //}

    private void Awake()
    {
        enemyPool = new ObjectPool<EnemyHealth>(
            OnCreateEnemy,       // 创建时的逻辑
            OnGetEnemy,          // 取出时的逻辑
            OnReleaseEnemy,      // 回收时的逻辑
            OnDestroyEnemy,      // 彻底销毁时的逻辑
            collectionCheck: true, // 检查重复回收（安全检查）
            defaultCapacity: 1,  // 默认初始容量
            maxSize: 20           // 池子最大容量
        );
    }
    private void Start()
    {
        InvokeRepeating("enemyPoolGet", FirstCreatEnemyTime, CreatEnemyTime);
    }
    void enemyPoolGet()
    {
        if (enemyPool.CountActive < 3)
        {
            enemyPool.Get();
        }
       
    }
    private void Update()
    {
        
        
    }

    private EnemyHealth OnCreateEnemy() { 
        EnemyHealth enemy = Instantiate(EnemyPrefab);
        enemy.SetPool(enemyPool);
        return enemy;
    }
    private void OnGetEnemy(EnemyHealth enemy) {
        enemy.gameObject.SetActive(true);
        enemy.gameObject.transform.position = EnemyCreatPoint.transform.position;
    }
    private void OnReleaseEnemy(EnemyHealth enemy) {
        enemy.gameObject.SetActive(false);
    }
    private void OnDestroyEnemy(EnemyHealth enemy) { 
        Destroy(enemy.gameObject);
    }
}
