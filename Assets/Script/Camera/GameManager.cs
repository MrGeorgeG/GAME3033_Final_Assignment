using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject StartUI;

    public GameObject LevelUI;

    public GameObject PauseUI;

    public GameObject GameOverUI;

    public void StartButton()
    {
        StartUI.SetActive(false);
        LevelUI.SetActive(true);
    }

    public void BackButton()
    {
        StartUI.SetActive(true);
        LevelUI.SetActive(false);
    }

    public void PauseButton()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitPauseButton()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;

    }

    public void CityButton()
    {
        Application.LoadLevel("Map_City");
    }

    public void ForestButton()
    {
        Application.LoadLevel("Map_ForestScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
        Application.LoadLevel(Application.loadedLevel);

    }
    public void MainMeun()
    {
        //SceneManager.LoadScene("MainScene");
        Application.LoadLevel("Map_MainScene");

    }
}
