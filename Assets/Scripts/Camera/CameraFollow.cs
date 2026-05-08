using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// 相机跟随脚本，负责使相机跟随玩家移动，保持一定的距离和角度，使用Lerp方法实现平滑过渡
    /// </summary>
    //public GameObject player;
    private GameObject player;
    private Vector3 offset;
    private float smoothSpeed = 6f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        offset = transform.position - player.transform.position;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset,smoothSpeed*Time.deltaTime); 
    }
}
