using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : Action
{
    [SerializeField] GameObject bulletClone;
    public override void OnStart()
    {

    }
    public override TaskStatus OnUpdate()
    {
        for (int i = 0; i <= 3; i++)
        {
            float splitAngle = 360 / 2;
            PoolManager.Release(bulletClone, transform.position, Quaternion.Euler(0, 0, splitAngle * i));
            //Instantiate(bulletClone, transform.position, Quaternion.Euler(0, 0, splitAngle * i));          
        }
        return TaskStatus.Success;
    }
}
