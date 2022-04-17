using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBullet : MonoBehaviour
{
    public float cloneBulletSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right.normalized * cloneBulletSpeed * Time.deltaTime;
        //Destroy(gameObject, 3.5f);
        Invoke("SetAct", 2.0f);
    }
    void SetAct()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
