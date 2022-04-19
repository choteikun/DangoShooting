using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class LookAtPlayer : Action
{
    [SerializeField]private GameObject targetGameObject;
    private int bossShield;
    

    public override void OnStart()
    {
        
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
            GlobalVariables.Instance.SetVariable("LookAtPlayer", (SharedBool)true);
        }
        if (GlobalVariables.Instance.GetVariable("LookAtPlayer") == (SharedBool)true)
        {
            return TaskStatus.Running;
        }
        if (Vector3.Angle(transform.rotation * Vector3.up, targetGameObject.transform.position - transform.position) == 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
        //return TaskStatus.Running;
        //return TaskStatus.Success;
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
        tr_self.rotation = Quaternion.AngleAxis(angle, axis) * rotation;
        //緩慢看向玩家
        //tr_self.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.AngleAxis(angle, axis) * rotation, Time.deltaTime * 3);

        //tr_self.localEulerAngles = new Vector3(0, tr_self.localEulerAngles.y, 90);//後來調試增加的，因為我想讓x，z軸向不會有任何變化
    }
}
