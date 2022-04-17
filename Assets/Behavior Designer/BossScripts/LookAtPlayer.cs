using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class LookAtPlayer : Action
{
    public GameObject targetGameObject;
    private int bossShield;


    public override void OnStart()
    {
        //GlobalVariables.Instance.SetVariable("BossShield", (SharedInt)bossShield);
    }
    public override TaskStatus OnUpdate()
    {
        if (targetGameObject == null) 
        {
            return TaskStatus.Failure;
        }
        if (targetGameObject != null)
        {

            AxisLookAt(transform, targetGameObject.transform.position, Vector3.up);
            //if ((Mathf.Abs(transform.position.y - 20) >= 0) || (Mathf.Abs(transform.position.x - 36) >= 0))
            //{
            //    BossSpeed = 20;
            //    BackNearPos();
            //}
            //else { BossSpeed = 0; } 
        }
        return TaskStatus.Success;
        //return TaskStatus.Running;
    }
    //void BackNearPos()
    //{
    //    if (transform.position.y >= 20)
    //    {
    //        Vector3 dirTop = new Vector3(0, 20, 0) - transform.position;
    //        AxisLookAt(transform, new Vector3(0, 20, 0), Vector3.up);
    //        Debug.Log("top");
    //        //transform.position -= transform.up * Time.deltaTime * BossSpeed;
    //    }
    //    else if(transform.position.y <= 20)
    //    {
    //        Vector3 dirBottom = new Vector3(0, -20, 0) - transform.position;
    //        AxisLookAt(transform, new Vector3(0, -20, 0), Vector3.up);
    //        //transform.position -= transform.up * Time.deltaTime * BossSpeed;
    //    }
    //    else if (transform.position.x <= -36)
    //    {
    //        Vector3 dirLeft = new Vector3(36, 0, 0) - transform.position;
    //        AxisLookAt(transform, new Vector3(36, 0, 0), Vector3.up);
    //        //transform.position -= transform.up * Time.deltaTime * BossSpeed;
    //    }
    //    else if (transform.position.x >= 36)
    //    {
    //        Vector3 dirRight = new Vector3(-36, 0, 0) - transform.position;
    //        AxisLookAt(transform, new Vector3(-36, 0, 0), Vector3.up);
    //        //transform.position -= transform.up * Time.deltaTime * BossSpeed;
    //    }
    //    else
    //    {
    //        BossSpeed = 0;
    //    }
    //}

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
        tr_self.rotation = Quaternion.AngleAxis(angle, axis) * rotation;
        //tr_self.localEulerAngles = new Vector3(0, tr_self.localEulerAngles.y, 90);//後來調試增加的，因為我想讓x，z軸向不會有任何變化
    }
}
