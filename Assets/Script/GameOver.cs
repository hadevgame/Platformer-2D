using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;

    public void DisplayGameOver() 
    {
        gameOverUI.SetActive(true);
        Invoke("DelayTimeScale", 1f);
    }

    void DelayTimeScale() 
    {
        Time.timeScale = 0f;
    }

    public void TryAgain() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
