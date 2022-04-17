using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotGunAtk : Action
{
    [SerializeField] GameObject bulletClone;
    [SerializeField] GameObject Muzzle;
    public override void OnStart()
    {
        for (int i = 0; i <= 5; i++)
        {
            float splitAngle = 180 / 5;
            PoolManager.Release(bulletClone, Muzzle.transform.position, Quaternion.Euler(0, 0, splitAngle * i));
            //Instantiate(bulletClone, transform.position, Quaternion.Euler(0, 0, splitAngle * i));          
        }
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
