using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Tank : MonoBehaviour
{
    public BossState curState;

    Rigidbody rb;
    Animator anim;
    Transform muzzleTransform;
    [SerializeField] Transform[] wallTransform;
    
    Player player;

    public int BossHp;
    public int dashSpeed;
    public int OnFireAtkPercent;
    public int DashAtkPercent;
    public int BossShield;
    

    bool invincibly;//無敵
    bool BossActive;
    bool getCurPosInfo;//暫存當下位置

    Vector3 wallTopPos;
    Vector3 wallBottomPos;
    Vector3 wallLeftPos;
    Vector3 wallRightPos;
    Vector3 curPlayerPos;
    Vector3 curBossPos;

    public BehaviorTree behaviorTree;

    public enum BossState
    {
        Idle,
        Ready,
        OpenAss,
        OnFire,
        DashAttack,
        Dead
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        muzzleTransform = GameObject.Find("TankFriend_Canon1_end").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        curState = BossState.OpenAss;
        BossShield = 1;

        invincibly = true;
        BossActive = false;

        wallTopPos = wallTransform[0].position;
        wallBottomPos = wallTransform[1].position;
        wallLeftPos = wallTransform[2].position;
        wallRightPos = wallTransform[3].position;
    }

    
    void Update()
    {
        switch (curState)
        {
            case BossState.Idle:
                UpdateIdle();
                break;
            case BossState.Ready:
                UpdateReady();
                break;
            case BossState.OpenAss:
                UpdateOpenAss();
                break;
            case BossState.OnFire:
                UpdateOnFire();
                break;
            case BossState.DashAttack:
                UpdateDashAttack();
                break;
            case BossState.Dead:
                UpdateDead();
                break;
        }
        if (BossShield <= 0)
        {
            if (!BossActive)
            {   
                curState = BossState.Idle;
                Invoke("BossShieldCharge", 0);
                BossActive = true;
                return;
            }
            Invoke("BossShieldCharge", 5f);
            curState = BossState.OpenAss;
        }
        
    }
    void UpdateIdle()
    {
        AxisLookAt(transform, player.transform.position, Vector3.up);
        Invoke("DelayToReady", 5f);
    }
    void UpdateReady()
    {
        BackNearPos();
        AxisLookAt(transform, player.transform.position, Vector3.up);
    }
    void UpdateOpenAss()
    {
        if (transform.position.y == wallTopPos.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.05f);
        }
        else if (transform.position.y == wallBottomPos.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 180), 0.05f);
        }
        else if (transform.position.x == wallLeftPos.x)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.05f);
        }
        else if (transform.position.x == wallRightPos.x)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 270), 0.05f);
        }

    }
    void UpdateOnFire()
    {
        AxisLookAt(transform, player.transform.position, Vector3.up);
        anim.SetBool("NormalAtk", true);
    }
    void UpdateDashAttack()
    {
        if (getCurPosInfo)//暫存位置
        {
            curPlayerPos = player.transform.position;
            curBossPos = transform.position;
            getCurPosInfo = !getCurPosInfo;
        }
        Vector3 dashCurDir = (curPlayerPos - curBossPos).normalized;
        transform.position += dashCurDir * dashSpeed * Time.deltaTime;
        if ((transform.position.y >= 40) || (transform.position.y <= -40) || (transform.position.x >= 66) || (transform.position.x <= -66))
        {
            curState = BossState.Ready;
        }
    }
    void UpdateDead()
    {

    }



    public float GetHP()
    {
        return BossHp;
    }

    void DelayToReady()
    {
        curState = BossState.Ready;
    }

    void AxisLookAt(Transform tr_self, Vector3 lookPos, Vector3 directionAxis)
    {
        var rotation = tr_self.rotation;
        var targetDir = lookPos - tr_self.position;
        //指定哪根軸朝向目標,自行修改Vector3的方向
        var fromDir = tr_self.rotation * directionAxis;
        //計算垂直於當前方向和目標方向的軸
        var axis = Vector3.Cross(fromDir, targetDir).normalized;
        //計算當前方向和目標方向的夾角
        var angle = Vector3.Angle(fromDir, targetDir);
        //將當前朝向向目標方向旋轉一定角度，這個角度值可以做插值
        tr_self.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.AngleAxis(angle, axis) * rotation, Time.deltaTime * 3);
        //tr_self.localEulerAngles = new Vector3(0, tr_self.localEulerAngles.y, 90);//後來調試增加的，因為我想讓x，z軸向不會有任何變化
    }


    void BossShieldCharge()
    {
        BossShield = 1000;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            BossShield--;
        }
    }
    void BackNearPos()
    {
        if (transform.position.y > wallTopPos.y)
        {
            StartCoroutine(MoveToWallTop());
        }
        else if (transform.position.y < wallBottomPos.y)
        {
            StartCoroutine(MoveToWallBottom());
        }
        else if (transform.position.x < wallLeftPos.x)
        {
            StartCoroutine(MoveToWallLeft());
        }
        else if (transform.position.x > wallRightPos.x)
        {
            StartCoroutine(MoveToWallRight());
        }
        else
        {        
            if (Probability(OnFireAtkPercent))
            {
                curState = BossState.OnFire;
            }
            else if (Probability(DashAtkPercent))
            {
                getCurPosInfo = true;
                curState = BossState.DashAttack;
            }
        }
    }
    IEnumerator MoveToWallTop()
    {
        AxisLookAt(transform, wallTopPos, Vector3.up);
        while (transform.localPosition != wallTopPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, wallTopPos, 0.1f * Time.deltaTime);
            yield return 0;
        }
    }
    IEnumerator MoveToWallBottom()
    {
        AxisLookAt(transform, wallBottomPos, Vector3.up);
        while (transform.localPosition != wallBottomPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, wallBottomPos, 0.1f * Time.deltaTime);
            yield return 0;
        }
    }
    IEnumerator MoveToWallLeft()
    {
        AxisLookAt(transform, wallLeftPos, Vector3.up);
        while (transform.localPosition != wallLeftPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, wallLeftPos, 0.1f * Time.deltaTime);
            yield return 0;
        }
    }
    IEnumerator MoveToWallRight()
    {
        AxisLookAt(transform, wallRightPos, Vector3.up);
        while (transform.localPosition != wallRightPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, wallRightPos, 0.1f * Time.deltaTime);
            yield return 0;
        }
    }

    public bool Probability(float percent)//確率
    {
        float probabilityRate = Random.value * 100.0f;
        if (percent == 100.0f && probabilityRate == percent)
        {
            return true;
        }
        else if (probabilityRate < percent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
