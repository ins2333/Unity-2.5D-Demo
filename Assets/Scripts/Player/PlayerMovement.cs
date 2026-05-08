using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// 玩家移动脚本组件，负责处理玩家的移动、视角旋转和动画切换等功能，使用Rigidbody组件实现物理移动，通过射线检测实现视角旋转，并根据玩家的输入切换行走动画
    /// </summary>
    //public LayerMask FloorMask;
    public float speed = 6f;
    private Rigidbody playerRigidbody; 
    private Animator playerAnimator;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //玩家移动
        Move(h, v);

        //视角旋转
        Turning();

        //动画切换
        Animate(h,v);
    }
    void Move(float h, float v) {
        Vector3 movementV3 = new Vector3(h, 0, v);
        movementV3 = movementV3.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movementV3);
    }
    void Turning() {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;
        bool isTouchFloor = Physics.Raycast(cameraRay, out floorHit,100,LayerMask.GetMask("Floor"));
        if (isTouchFloor) {
            Vector3 v3 = floorHit.point - transform.position;
            v3.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(v3);
            playerRigidbody.MoveRotation(newRotation);
        }   
    }
    void Animate(float h,float v) { 
        bool isWalking = false;
        if (h!=0||v!=0) { 
            isWalking = true;
        }
        playerAnimator.SetBool("IsWalking",isWalking);
       
    }
}
