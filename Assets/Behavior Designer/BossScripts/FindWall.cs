using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class FindWall : Action
{
    [SerializeField] GameObject wallTOP;
    [SerializeField] GameObject wallBottom;
    [SerializeField] GameObject wallLeft;
    [SerializeField] GameObject wallRight;
    

    public override TaskStatus OnUpdate()
    {
        //if ((Mathf.Abs(transform.position.y - 20) >= 0) || (Mathf.Abs(transform.position.x - 36) >= 0))
        //{
        //    BackNearPos();
        //    Debug.Log("walllllllllllll");
        //    return TaskStatus.Running;
        //}
        BackNearPos();
        if (transform.position.x == wallRight.transform.position.x
                || transform.position.x == wallLeft.transform.position.x
                || transform.position.y == wallTOP.transform.position.y
                || transform.position.y == wallBottom.transform.position.y)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
    void BackNearPos()
    {
        if (transform.position.y >= wallTOP.transform.position.y)
        {
            StartCoroutine(MoveToWallTop());
        }
        else if(transform.position.y <= wallBottom.transform.position.y)
        {
            StartCoroutine(MoveToWallBottom());
        }
        else if (transform.position.x <= wallLeft.transform.position.x)
        {
            StartCoroutine(MoveToWallLeft());
        }
        else if (transform.position.x >= wallRight.transform.position.x)
        {
            StartCoroutine(MoveToWallRight());
        }

        if (transform.position.x == wallRight.transform.position.x
         || transform.position.x == wallLeft.transform.position.x
         || transform.position.y == wallTOP.transform.position.y
         || transform.position.y == wallBottom.transform.position.y
            )
        {
            GlobalVariables.Instance.SetVariable("BossReady", (SharedBool)true);
            GlobalVariables.Instance.SetVariable("LookAtPlayer", (SharedBool)true);
        }
        else
        {
            GlobalVariables.Instance.SetVariable("BossReady", (SharedBool)false);
        }
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
    IEnumerator MoveToWallTop()
    {
        AxisLookAt(transform, wallTOP.transform.position, Vector3.up);
        while (transform.localPosition != wallTOP.transform.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, wallTOP.transform.position, 0.1f * Time.deltaTime);
            yield return 0;
        }
    }
    IEnumerator MoveToWallBottom()
    {
        AxisLookAt(transform, wallBottom.transform.position, Vector3.up);
        while (transform.localPosition != wallBottom.transform.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, wallBottom.transform.position, 0.1f * Time.deltaTime);
            yield return 0;
        }
    }
    IEnumerator MoveToWallLeft()
    {
        AxisLookAt(transform, wallLeft.transform.position, Vector3.up);
        while (transform.localPosition != wallLeft.transform.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, wallLeft.transform.position, 0.1f * Time.deltaTime);
            yield return 0;
        }
    }
    IEnumerator MoveToWallRight()
    {
        AxisLookAt(transform, wallRight.transform.position, Vector3.up);
        while (transform.localPosition != wallRight.transform.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, wallRight.transform.position, 0.1f * Time.deltaTime);
            yield return 0;
        }        
    }
}
