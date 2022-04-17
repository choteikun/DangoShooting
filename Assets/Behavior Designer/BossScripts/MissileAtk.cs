using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAtk : Action
{
    public override void OnStart()
    {

    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
