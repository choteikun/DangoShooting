using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float bossBulletSpeed;
    [SerializeField] Player player;
    [SerializeField] Boss boss;

    void Start()
    {
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 boss2d = new Vector3(boss.transform.position.x, boss.transform.position.y, 0);
        Vector3 bullet2d = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 dir = (boss2d - bullet2d).normalized;
        transform.position -= dir * bossBulletSpeed * Time.deltaTime;
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
