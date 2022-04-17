using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MilkShake;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject player;  
    public GameObject stageCanvasPrefab;

    public int currentStage;
    public int enemyDeathCount;
    public int targetEnemy;
    bool nextStage;

    [Header("Set Enemy Prefab")]
    //敵プレハブ
    public GameObject enemyPrefab;
    Enemy enemy;
    [Header("Set Interval Min and Max")]
    [Range(1f, 5f),Tooltip("時間間隔の最小値")]
    public float minTime;
    [Range(5f, 20f), Tooltip("時間間隔の最大値")]
    public float maxTime;

    [Header("Set X Position Min and Max")]
    [Range(-35f, 0f), Tooltip("X座標の最小値")]
    public float xMinPosition = -35f;
    [Range(0f, 35f), Tooltip("X座標の最大値")]
    public float xMaxPosition = 35f;

    [Header("Set Y Position Min and Max")]
    [Range(-18f, 0f), Tooltip("Y座標の最小値")]
    public float yMinPosition = -18f;
    [Range(0f, 20f), Tooltip("Y座標の最大値")]
    public float yMaxPosition = 20f;

    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    Vector3 playerPos;
    [Header("player附近的敵人生成範圍限制")]
    [Range(0f, 34f)]
    public float playerLimitAroundX;
    [Range(0f, 20f)]
    public float playerLimitAroundY;

    [SerializeField]
    private Shaker shakeManager;
    [SerializeField]
    private ShakeParameters shakeCam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = enemyPrefab.GetComponent<Enemy>();
        //時間間隔を決定する
        interval = GetRandomTime();
        nextStage = false;
        currentStage = 0;
        targetEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;
        if (player != null)
        {
            playerPos = player.transform.position;
        }


        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            //enemyをインスタンス化する(生成する)
            GameObject enemy = Instantiate(enemyPrefab);

            //生成した敵の位置をランダムに設定する
            enemy.transform.position = GetRandomPosition();
            
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
            //次に発生する時間間隔を決定する
            interval = GetRandomTime();           
        }
        //nextStage = false;
        //Debug.Log("stage : " + stage + "," + nextStage);
        if (currentStage == 0 && enemyDeathCount == 0)
        {
            nextStage = true;
            targetEnemy = 5;
            currentStage++;
            stageCanvasPrefab.GetComponentInChildren<TMP_Text>().text = "STAGE" + currentStage + "\n" + "TargetEnemy" + "[" + targetEnemy + "]";
        }
        else if (currentStage == 1 && enemyDeathCount == 5)
        {
            shakeManager.Shake(shakeCam);
            nextStage = true;
            currentStage++;
            targetEnemy = 15;
            minTime = 5f;
            maxTime = 15f;
            stageCanvasPrefab.GetComponentInChildren<TMP_Text>().text = "STAGE" + currentStage + "\n" + "TargetEnemy" + "[" + targetEnemy + "]";
        }
        else if(currentStage == 2 && enemyDeathCount == 15)
        {
            shakeManager.Shake(shakeCam);
            nextStage = true;
            currentStage++;
            targetEnemy = 40;
            minTime = 5f;
            maxTime = 10f;
            enemy.enemySpeed = 5.0f;
            stageCanvasPrefab.GetComponentInChildren<TMP_Text>().text = "STAGE" + currentStage + "\n" + "TargetEnemy" + "[" + targetEnemy + "]";
        }
        else if (currentStage == 3 && enemyDeathCount == 40)
        {
            shakeManager.Shake(shakeCam);
            nextStage = true;
            currentStage++;
            targetEnemy = 100;
            minTime = 2f;
            maxTime = 7f;
            enemy.enemySpeed = 10.0f;
            stageCanvasPrefab.GetComponentInChildren<TMP_Text>().text = "STAGE" + currentStage + "\n" + "TargetEnemy" + "[" + targetEnemy + "]";
        }
        else if (currentStage == 4 && enemyDeathCount == 100)
        {
            shakeManager.Shake(shakeCam);
            nextStage = true;
            currentStage++;
            minTime = 1f;
            maxTime = 5f;
            enemy.enemySpeed = 15.0f;
            player.GetComponent<Player>().playerWin = true;
        }
        else
        {
            nextStage = false;
            if (player != null)
            {
                player.GetComponent<Player>().playerWin = false;
            }
        }
        
        if (nextStage)
        {
            stageCanvasPrefab.GetComponentInChildren<TMP_Text>().CrossFadeAlpha(1, 0f, true);//通關時顯示目標數量
        }
        else
        {
            stageCanvasPrefab.GetComponentInChildren<TMP_Text>().CrossFadeAlpha(0, 1.0f, true);//文字淡出
        }
    }
    //ランダムな時間を生成する関数
    private float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = 0;
        //if ((Mathf.Abs(playerPos.x - x) < playerLimitAroundX))
        //{
        //    x += playerLimitAroundX;
        //}
        //if ((Mathf.Abs(playerPos.y - y) < playerLimitAroundY))
        //{
        //    y += playerLimitAroundY;
        //}

        //TODO 佔效能
        while ((Mathf.Abs(playerPos.x - x) < playerLimitAroundX) && (Mathf.Abs(playerPos.y - y) < playerLimitAroundY))//敵人生成範圍限制(不在player周圍生成，範圍過大計算多時會卡頓)
        {
            x = Random.Range(xMinPosition, xMaxPosition);
            y = Random.Range(yMinPosition, yMaxPosition);
        }

        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(new Vector3(playerPos.x, playerPos.y, 0), new Vector3(playerLimitAroundX, playerLimitAroundY, 0));
        Gizmos.DrawWireCube(new Vector3(playerPos.x, playerPos.y, 0), new Vector3(playerLimitAroundX, playerLimitAroundY, 0));//描繪出Player範圍
    }

}
