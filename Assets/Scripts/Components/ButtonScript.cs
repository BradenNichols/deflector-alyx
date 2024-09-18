using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public PlayerInput input;
    public void Quit()
    {
        Application.Quit();
    }
    public void Continue()
    {
        gameObject.SetActive(false);
        input.paused = false;
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
