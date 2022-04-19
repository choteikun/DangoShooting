using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject pauseMenu;
    public GameObject bulletPrefab;
    public float moveSpeed = 10;
    public float smoothRotateSpeed;
    public static bool GameIsPaused = false;
    public bool playerDead;
    public bool playerWin;
    public Vector3 targetDir;

    InputActions inputActions;
    Rigidbody rb;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClip;
    Transform muzzle;
    Transform dangoBodyTransform;
    MeshRenderer meshRenderer;
    SphereCollider playerCollider;
    Material mat;
    float hitShieldTime;
    bool shield;
    int playerHealth = 1;


    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }
    private void Start()
    {   
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        mat = GetComponent<Renderer>().material;
        meshRenderer = GetComponent<MeshRenderer>();
        playerCollider = GetComponent<SphereCollider>();
        playerInput = GetComponent<PlayerInput>();
        dangoBodyTransform = GameObject.Find("dan_n_go_body").transform;
        muzzle = GameObject.Find("Muzzle").transform;

        shield = false;
        playerDead = false;
        playerWin = false;
    }

    private void Update()
    {
        PlayerDead();
        if (hitShieldTime > 0)
        {
            hitShieldTime -= Time.deltaTime * 1000;
            if (hitShieldTime < 0)
            {
                hitShieldTime = 0;
            }
            mat.SetFloat("_HitShieldTime", hitShieldTime);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        Vector2 vector2d = inputActions.Player.Movement.ReadValue<Vector2>();
        if (vector2d != Vector2.zero)
        {
            Vector3 vector3d = new Vector3(vector2d.x, vector2d.y, 0);
            rb.velocity = vector3d * moveSpeed;     
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        targetDir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - transform.position;
        dangoBodyTransform.rotation = Quaternion.RotateTowards(dangoBodyTransform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * smoothRotateSpeed);
    }
    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }

        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
    public void OnFire(InputAction.CallbackContext callback)
    {
        if (muzzle != null && callback.started) 
        {
            PoolManager.Release(bulletPrefab, new Vector3(muzzle.position.x, muzzle.position.y, 0), Quaternion.identity);
        }
        //Instantiate(bulletPrefab, new Vector3(muzzle.transform.position.x, muzzle.transform.position.y, 0), Quaternion.identity);
    }
    public void Shield(InputAction.CallbackContext callback)
    {
        shield = !shield;
        if (shield)
        {
            meshRenderer.enabled = true;
            playerCollider.radius = 3.0f;
        }
        else
        {
            meshRenderer.enabled = false;
            playerCollider.radius = 1.5f;
        }
    }
    public void PauseMenu(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Started && !GameIsPaused)
        {
            Pause();
        }
               
        //if (v == Time.timeScale) { Time.timeScale = 1; } else { Time.timeScale = 0; }
        //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }


    public void Pause()
    {
        //inputActions.Player.Disable();
        playerInput.actions.Disable();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Enemy") && meshRenderer.enabled == false)
        {
            playerHealth--;
            if (playerHealth <= 0 && !playerWin)
            {
                playerHealth = 0;
                playerDead = true;
            }
        }
        foreach (ContactPoint contact in collision.contacts)
        {

            mat.SetVector("_HitPosition", transform.InverseTransformPoint(contact.point));
            hitShieldTime = 500;
            mat.SetFloat("_HitShieldTime", hitShieldTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Bullet" || other.gameObject.tag == "Boss") && meshRenderer.enabled == false)
        {
            playerHealth--;
            if (playerHealth <= 0 && !playerWin)
            {
                playerHealth = 0;
                playerDead = true;
            }
        }        
    }
    void PlayerDead()
    {
        if (playerDead)
        {
            Destroy(gameObject);
            playerDead = false;
        }
    }
    //private void FollowMouseRotate()//物體跟隨鼠標旋轉 
    //{
    //    //獲取鼠標的坐標，鼠標是螢幕坐標，Z軸為0，這裡不做轉換  
    //    Vector3 mouse = Input.mousePosition;
    //    //獲取物體坐標，物體坐標是世界坐標，將其轉換成螢幕坐標
    //    Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);
    //    //螢幕坐標向量相減，得到指向鼠標點的目標向量
    //    Vector3 direction = mouse - obj;
    //    //將Z軸置0,保持在2D平面內  
    //    direction.z = 0f;
    //    //將目標向量長度變成1，即單位向量，這裡的目的是只使用向量的方向，不需要長度，所以變成1  
    //    direction = direction.normalized;
    //    //物體自身的Y軸和目標向量保持一直線，這個過程XY軸都會變化數值  
    //    transform.up = direction;
    //}

}
