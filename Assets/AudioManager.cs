using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Player player;
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerDead)
        {
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Gameover"), 0.5f);
        }
    }
}
