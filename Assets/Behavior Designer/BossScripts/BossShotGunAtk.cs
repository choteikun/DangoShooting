using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotGunAtk : Action
{
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] GameObject Muzzle;
    [SerializeField] GameObject bulletClone;
    
    public override void OnStart()
    {

    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
    
}
