using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class BossOpenAss : Action
{
    public GameObject targetGameObject;
    private int bossShield;
    public override void OnStart()
    {
        //GlobalVariables.Instance.SetVariable("BossShield", (SharedInt)bossShield);
    }

    public override TaskStatus OnUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.05f);
        return TaskStatus.Success;
    }

}
