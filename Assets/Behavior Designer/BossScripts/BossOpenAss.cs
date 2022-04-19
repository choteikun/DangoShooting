using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class BossOpenAss : Action
{
    [SerializeField] BossAss bossAss;
    [SerializeField] GameObject wallTOP;
    [SerializeField] GameObject wallBottom;
    [SerializeField] GameObject wallLeft;
    [SerializeField] GameObject wallRight;
    public GameObject targetGameObject;
    
    bool openAss;
    float timeCounter = 5f;

    public override void OnStart()
    {
        openAss = true;
    }

    public override TaskStatus OnUpdate()
    {
        if (transform.position.y == wallTOP.transform.position.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.05f);
        }
        else if (transform.position.y == wallBottom.transform.position.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 180), 0.05f);
        }
        else if (transform.position.x == wallLeft.transform.position.x)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 0.05f);
        }
        else if (transform.position.x == wallRight.transform.position.x)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 270), 0.05f);
        }
        if (openAss)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                timeCounter = 3f;
                bossAss.invincibly = true;
                openAss = false;
            }
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
       
    }

}
