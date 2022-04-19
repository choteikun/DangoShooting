using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : Action
{
    [SerializeField]private GameObject targetGameObject;
    [SerializeField] GameObject Muzzle;
    [SerializeField] GameObject TankBullet;
    public override void OnStart()
    {
        NormalAtk(transform, 5);
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;

    }
    public void NormalAtk(Transform boss, int NwayCount)
    {
        float r = Random.Range(1, NwayCount + 1);
        float angle = -(NwayCount + 1) * 5 / 2 + r * 5;
        PoolManager.Release(TankBullet, Muzzle.transform.position, Quaternion.Euler(new Vector3(90, 0, angle)));
    }
}
