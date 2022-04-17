using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject[] button;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayScene()
    {
        StartCoroutine(DelayedPlayScene());
    }
    public void BossChallengeScene()
    {
        StartCoroutine(DelayedBossChallengeScene());
    }
    public void ExitScene()
    {
        Application.Quit();
    }
    IEnumerator DelayedPlayScene()
    {
        audioSource.PlayOneShot(audioClip[1], 0.5f);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
    }
    IEnumerator DelayedBossChallengeScene()
    {
        audioSource.PlayOneShot(audioClip[1], 0.5f);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(2);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.PlayOneShot(audioClip[0], 0.5f);
        }
        
        for (int i = 0; i < 3; i++)
        {
            if (button[i].GetComponent<Button>().animator.GetCurrentAnimatorStateInfo(0).IsName("Selected"))
            {
                button[i].GetComponentInChildren<MyShaderCustomValue>()._myCustomFloat = 0.3f;
            }
            else
            {
                button[i].GetComponentInChildren<MyShaderCustomValue>()._myCustomFloat = 0;
            }
            //if (button[i].GetComponent<Button>().animator.GetCurrentAnimatorStateInfo(0).IsName("Normal"))
            //{
            //    button[i].GetComponentInChildren<MyShaderCustomValue>()._myCustomFloat = 0;
            //}
        }
    }
}
