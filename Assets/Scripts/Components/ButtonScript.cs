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
        GetComponentInParent<AudioSource>().Play();
        gameObject.SetActive(false);
        input.paused = false;
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScreen");
    }
}
