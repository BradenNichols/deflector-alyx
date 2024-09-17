using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public SpriteRenderer youDied;
    public SpriteRenderer diedBG;

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

        for (int i = 0; i <= max; i++)
        {
            float ratio = (float)i / max;
            youDied.color = new Color(1, 1, 1, ratio);

            if (ratio <= 0.42f)
                diedBG.color = new Color(0, 0, 0, ratio);

            yield return new WaitForSecondsRealtime(0.01f);
        }

        max = 150;

        Vector3 baseSize = youDied.transform.localScale;

        for (int i = 0; i <= max; i++)
        {
            float ratio = 1 - ((float)i / max);

            youDied.color = new Color(ratio, ratio, ratio, 1);
            youDied.transform.localScale = baseSize * (1 + ((float)i / max) / 8);

            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return new WaitForSecondsRealtime(1.5f);
        deathSound.Stop();

        if (GlobalGame.Instance.difficulty == "Normal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        } else if (GlobalGame.Instance.difficulty == "Insane")
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        }
    }
}
