using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Boss : MonoBehaviour
{
    private int bossShield;
    private bool noDamage;
    private float noDamageTime;
    private bool takeDamage;
    public BehaviorTree behaviorTree;

    void Start()
    {
        noDamageTime = 1200;
        noDamage = false;
        takeDamage = false;
        bossShield = 0;
        //GlobalVariables.Instance.SetVariable("BossShield", (SharedInt)bossShield);
        GlobalVariables.Instance.SetVariable("TakeDamage", (SharedBool)takeDamage);
        GlobalVariables.Instance.SetVariable("TakeDamage", (SharedBool)true);

    }


    void Update()
    {
        if (noDamage)
        {
            noDamageTime--;
            if (noDamageTime <= 0)
            {
                noDamage = false;
                noDamageTime = 1200;    
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && !noDamage) 
        {
            bossShield--;
            if (bossShield < 0)
            {
                GlobalVariables.Instance.SetVariable("BossActive", (SharedBool)true);
                Invoke("DelayShieldCharge", 3f);
                noDamage = true;
            }
            
            GlobalVariables.Instance.SetVariable("BossShield", (SharedInt)bossShield);
            Debug.Log(GlobalVariables.Instance.GetVariable("BossShield"));
            GlobalVariables.Instance.SetVariable("TakeDamage", (SharedBool)true);

            //GlobalVariables.Instance.SetVariable("BossHp", (SharedInt)hp);
        }
        
    }
    void DelayShieldCharge()
    {
        bossShield = 10;
        GlobalVariables.Instance.SetVariable("BossShield", (SharedInt)bossShield);
    }
    //public float GetHP()
    //{
    //    return hp;
    //}

}
