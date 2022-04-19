using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSkill : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject tankBullet;

    private void Start()
    {
        muzzle = GameObject.Find("TankFriend_Canon1_end");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void NormalShooting()
    {
        Invoke("SpawnTankBullet", 0f);
    }
    public void ShotGunShooting()
    {
        PoolManager.Release(tankBullet, muzzle.transform.position, muzzle.transform.rotation);
        PoolManager.Release(tankBullet, muzzle.transform.position + new Vector3(5, 0, 0), muzzle.transform.rotation);
        PoolManager.Release(tankBullet, muzzle.transform.position + new Vector3(0, 5, 0), muzzle.transform.rotation);
        PoolManager.Release(tankBullet, muzzle.transform.position + new Vector3(-5, 0, 0), muzzle.transform.rotation);
        PoolManager.Release(tankBullet, muzzle.transform.position + new Vector3(5, 5, 0), muzzle.transform.rotation);
    }
    public void MissileAtk()
    {

    }
    void SpawnTankBullet()
    {
        PoolManager.Release(tankBullet, muzzle.transform.position, muzzle.transform.rotation);
    }
}
