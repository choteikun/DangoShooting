using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public float bulletSpeed;
    public float lostSpeed;
    public bool shootingState;

    public Vector3 direction;
    Vector3 mousePos;
    Vector3 playerPos;
    Player player;
    [SerializeField]GameObject bulletClone;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos = player.transform.position;
        shootingState = true;
        moveSpeed = 1;
    }

    
    void Update()
    {        
        if (gameObject.activeInHierarchy)//判斷物件是否啟用中
        {
            if (!shootingState)//再次擊發的狀態，初始化mousePos,playerPos
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                playerPos = player.transform.position;
            }
            direction = new Vector3(mousePos.x, mousePos.y, 0) - playerPos;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            shootingState = true;
        }
        if (shootingState)//擊發時的狀態
        {
            moveSpeed -= bulletSpeed * lostSpeed * Time.deltaTime;
            if (moveSpeed <= 0)//當子彈速度等於0
            {
                //moveSpeed = 0;
                moveSpeed = 1;
                BulletSplit();//分裂彈
                gameObject.SetActive(false);
                shootingState = false;//子彈回收
            }
        }
    }
    void BulletSplit()
    {
        for (int i = 0; i <= 6; i++)
        {
            float splitAngle = 360 / 6;
            PoolManager.Release(bulletClone, transform.position, Quaternion.Euler(0, 0, splitAngle * i));
            //Instantiate(bulletClone, transform.position, Quaternion.Euler(0, 0, splitAngle * i));          
        }
    }

}
