using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class DashForward : Action
{
    public GameObject targetGameObject;
    public float dashSpeed;
    Vector3 playerCurPos;
    Vector3 bossCurPos;
    Vector3 dashCurDir;
    public override void OnStart()
    {
        playerCurPos = targetGameObject.transform.position;
        bossCurPos = transform.position;
        dashCurDir = (playerCurPos - bossCurPos).normalized;
        
    }
    public override TaskStatus OnUpdate()
    {
        
        if (targetGameObject == null)
        {
            return TaskStatus.Failure;
        }
        if (targetGameObject != null)
        {
            transform.position += dashCurDir * dashSpeed * Time.deltaTime;
        }
        if ((transform.position.y >= 40) || (transform.position.y <= -40) || (transform.position.x >= 66) || (transform.position.x <= -66)) 
        {
            GlobalVariables.Instance.SetVariable("BossReady", (SharedBool)false);       
            return TaskStatus.Success;
        }
        else
        {
            GlobalVariables.Instance.SetVariable("LookAtPlayer", (SharedBool)false);
            return TaskStatus.Running;
        }
        
    }

    
}


