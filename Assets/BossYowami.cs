using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossYowami : MonoBehaviour
{
    Tank boss;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Tank>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && boss.BossShield <= 0)
        {
            boss.BossHp--;
            Debug.Log("BossHp : " + boss.BossHp);
        }
    }
}
