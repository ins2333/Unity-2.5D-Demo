using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

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
