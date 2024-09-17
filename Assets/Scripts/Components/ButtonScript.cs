using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public void Quit()
    {
        Application.Quit();
    }
    public void Continue()
    {
        //input.paused = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
