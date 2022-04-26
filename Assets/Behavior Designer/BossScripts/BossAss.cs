using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class BossAss : MonoBehaviour
{
    private int hp = 100;
    private bool takeDamage;
    public bool firstDamage;
    public bool invincibly;
    public BehaviorTree behaviorTree;

    void Start()
    {
        //var behaviorTree = GetComponent<BehaviorTree>();
        //var hp = (SharedInt)behaviorTree.GetVariable("BossHp");
        //Debug.Log(hp.Value);
        GlobalVariables.Instance.SetVariable("BossHp", (SharedInt)hp);
        GlobalVariables.Instance.SetVariable("TakeDamage", (SharedBool)takeDamage);

        firstDamage = false;
        takeDamage = false;
        invincibly = true;
        Debug.Log(GlobalVariables.Instance.GetVariable("BossHp"));
        GlobalVariables.Instance.SetVariable("TakeDamage", (SharedBool)true);
        Debug.Log(GlobalVariables.Instance.GetVariable("TakeDamage"));

    }


    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && !invincibly)
        {
            hp--;
            GlobalVariables.Instance.SetVariable("BossHp", (SharedInt)hp);
            GlobalVariables.Instance.SetVariable("TakeDamage", (SharedBool)true);
            //Debug.Log("BossHp: " + GlobalVariables.Instance.GetVariable("BossHp"));
            //Debug.Log(GlobalVariables.Instance.GetVariable("takeDamage"));
            //GlobalVariables.Instance.SetVariable("BossHp", (SharedInt)hp);
            //Debug.Log("000");
        }
        
    }
    public void InvinciblyOff()
    {
        invincibly = false;
    }

    public float GetHP()
    {
        return hp;
    }
}
