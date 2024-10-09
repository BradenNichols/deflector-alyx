using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public Text youDied;
    public Image diedBG;

    private AudioSource deathSound;

    private GameObject timerObject;
    private Timer timer;

    void Start()
    {
        timerObject = GameObject.Find("Timer");
        timer = timerObject.GetComponent<Timer>();

        deathSound = youDied.GetComponent<AudioSource>();
    }

    // Win
    public void WinLevel(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }

    // Lose
    public IEnumerator LoseGame()
    {
        timer.enabled = false;
        deathSound.Play();

        int max = 35;
        Color baseColor = youDied.color;

        for (int i = 0; i <= max; i++)
        {
            float ratio = (float)i / max;
            youDied.color = new Color(baseColor.r, baseColor.g, baseColor.b, ratio);

            if (ratio <= 0.42f)
                diedBG.color = new Color(0, 0, 0, ratio);

            yield return new WaitForSeconds(0.01f);
        }

        max = 175;

        int baseTextSize = youDied.fontSize;
        int textSizeIncrease = 31;

        for (int i = 0; i <= max; i++)
        {
            float ratio = ((float)i / max);

            youDied.color = new Color(baseColor.r - ((baseColor.r / 2) * ratio), baseColor.g, baseColor.b, 1);
            youDied.fontSize = baseTextSize + (int)(textSizeIncrease * ratio);

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.25f);
        deathSound.Stop();

        if (GlobalGame.Instance.difficulty == "Normal")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        else if (GlobalGame.Instance.difficulty == "Insane")
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }
}
