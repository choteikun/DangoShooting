using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverCanvasPrefab;
    public GameObject gameClearCanvasPrefab;
    public Player player;


    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }


    void Update()
    {
        if (player.playerDead)
        {
            GameOverCanvas();

            Invoke("BackToMenu", 2.5f);
        }
        if (player.playerWin)
        {
            Invoke("GameClearCanvas", 2f);

            Invoke("BackToMenu", 4.5f);
        }
    }
    public void GameClearCanvas()
    {
        Instantiate(gameClearCanvasPrefab, Vector2.zero, Quaternion.identity);
    }
    public void GameOverCanvas()
    {
        Instantiate(gameOverCanvasPrefab, Vector2.zero, Quaternion.identity);
    }
    void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
