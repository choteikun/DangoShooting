using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public bool setButtonFirst;
    public GameObject[] button;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    Player player;
    EnemyGenerator enemyGenerator;
    TMP_Text missionInfo;


    private void Start()
    {
        setButtonFirst = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!enemyGenerator)
        {
            enemyGenerator = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
            missionInfo = GameObject.Find("MissionInfo").GetComponent<TMP_Text>();
        }
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Player.GameIsPaused && setButtonFirst) //呼叫出菜單時
        {
            button[0].GetComponent<Button>().animator.SetTrigger("Selected");

            player.playerInput.currentActionMap = player.playerInput.actions.FindActionMap("Pause Menu");//玩家的輸入系統 切換成 暫停選單的輸入系統

            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(GameObject.Find("Button"), new BaseEventData(eventSystem));//選取第一個按鈕

            missionInfo.text = "STAGE" + enemyGenerator.currentStage + "\n" + "TargetEnemy" + "[" + enemyGenerator.targetEnemy + "]" + "\n" + "EnemyCount" + "[" + enemyGenerator.enemyDeathCount + "]";//任務資訊

            setButtonFirst = false;
        }
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
        }
    }
    public void Unpause(InputAction.CallbackContext callback)
    {
        if (callback.phase == InputActionPhase.Started && Player.GameIsPaused) 
        {
            Resume();
        }
    }
    public void Resume()
    {
        audioSource.PlayOneShot(audioClip[1], 0.5f);
        for (int i = 0; i < 3; i++)
        {
            button[i].GetComponent<Button>().animator.Rebind();//由重綁動畫來解決Animator SetActive(false)會出現的動畫問題
        }
        player.playerInput.actions.Enable();
        Time.timeScale = 1f;
        Player.GameIsPaused = false;
        setButtonFirst = true;
        gameObject.SetActive(false);
    }
    public void Options()
    {
        
    }
    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
    
}
