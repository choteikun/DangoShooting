using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHp;
    public float enemySpeed = 2.0f;
    bool enemyDeath;
    [SerializeField]EnemyGenerator enemyGenerator;
    Player player;
    
    void Start()
    {
        enemyDeath = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (!enemyGenerator)
        {
            enemyGenerator = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        }
    }

    
    void Update()
    {
        if (player != null)
        {
            Vector3 trackDir = (transform.position - player.transform.position).normalized;
            transform.Translate(-trackDir * enemySpeed * Time.deltaTime);
        }
        if(enemyHp <= 0)
        {
            enemyDeath = true;
            enemyHp = 0;
            Destroy(gameObject);
        }
        if (enemyDeath)
        {
            enemyGenerator.enemyDeathCount++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemyHp--;
            enemyDeath = false;
        }    
    }
}
