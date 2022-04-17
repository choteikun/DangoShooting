using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class DashForward : Action
{
    public GameObject targetGameObject;

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
            //Vector3 dir = transform.position - targetGameObject.transform.position;
            //transform.position -= transform.up * Time.deltaTime * 20;
        }
        return TaskStatus.Success;
    }
}


