using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    float time = 0f;
    float dealtTime = 0.15f;
    float displayTime = 0.2f;

    private AudioSource gunShoot;
    private Light gunLight;
    private LineRenderer gunLine;
    private ParticleSystem gunParticle;

    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootMask;

    public int PlayerDamage = 15;
    private void Awake()
    {
        
    }
    private void Start()
    {
        gunShoot = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunParticle = GetComponent<ParticleSystem>();
        shootMask = LayerMask.GetMask("Shootable");
    }
    private void Update()
    {
        //时间间隔
        time = time + Time.deltaTime;

        if (Time.timeScale == 0f)
        {
            return;
        }
        else
        {
            //获取鼠标按键
            if (Input.GetButton("Fire1") && time > dealtTime)
            {
                Shoot();
            }
            if (time >= dealtTime * displayTime)
            {
                gunLight.enabled = false;
                gunLine.enabled = false;
            }
        }
    }

    void Shoot()
    {
        //时间归0
        time = 0f;

        gunLight.enabled = true;

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        //gunLine.SetPosition(1,transform.position + transform.forward *100);

        gunParticle.Play();

        gunShoot.Play();

        //发射射线有没有命中
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        if (Physics.Raycast(shootRay,out shootHit,100f,shootMask))
        {
            gunLine.SetPosition(1, shootHit.point);
            EnemyHealth enemyHealth =  shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null) { 
                enemyHealth.TakeDamage(PlayerDamage,shootHit.point);
            }
        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * 100);
        }
    }
}
