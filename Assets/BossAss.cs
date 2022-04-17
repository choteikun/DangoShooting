using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class BossAss : MonoBehaviour
{
    private int hp = 100;
    private bool takeDamage;
    public BehaviorTree behaviorTree;

    void Start()
    {
        //var behaviorTree = GetComponent<BehaviorTree>();
        //var hp = (SharedInt)behaviorTree.GetVariable("BossHp");
        //Debug.Log(hp.Value);
        GlobalVariables.Instance.SetVariable("BossHp", (SharedInt)hp);
        GlobalVariables.Instance.SetVariable("takeDamage", (SharedBool)takeDamage);

        takeDamage = false;

        Debug.Log(GlobalVariables.Instance.GetVariable("BossHp"));
        GlobalVariables.Instance.SetVariable("takeDamage", (SharedBool)true);
        Debug.Log(GlobalVariables.Instance.GetVariable("takeDamage"));
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hp--;
            GlobalVariables.Instance.SetVariable("BossHp", (SharedInt)hp);
            GlobalVariables.Instance.SetVariable("takeDamage", (SharedBool)true);
            //Debug.Log(GlobalVariables.Instance.GetVariable("BossHp"));
            //Debug.Log(GlobalVariables.Instance.GetVariable("takeDamage"));
            //GlobalVariables.Instance.SetVariable("BossHp", (SharedInt)hp);
            //Debug.Log("000");
        }

    }
    //public float GetHP()
    //{
    //    return hp;
    //}
}
