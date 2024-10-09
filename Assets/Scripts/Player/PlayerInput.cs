using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    private Stats myStats;
    private PlayerMovement myMovement;
    private Melee myWeapon;
    public GameObject pauseMenu;

    public bool canPause;
    public bool paused;

    void Start()
    {
        myStats = GetComponent<Stats>();
        myMovement = GetComponent<PlayerMovement>();
        myWeapon = myStats.meleeWeapon;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale != 0)
            myWeapon.Attack();
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale != 0)
            myStats.Kill();

        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if(!paused)
            {
                Time.timeScale = 0;

                pauseMenu.GetComponentInParent<AudioSource>().Pause();
                pauseMenu.SetActive(true);
            } else
            {
                Time.timeScale = 1;

                pauseMenu.GetComponentInParent<AudioSource>().Play();
                pauseMenu.SetActive(false);
            }

            paused = !paused;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
                SceneManager.LoadScene("Tutorial");
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SceneManager.LoadScene("Level1");
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SceneManager.LoadScene("Level2");
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SceneManager.LoadScene("Level3");
            if (Input.GetKeyDown(KeyCode.Alpha4))
                SceneManager.LoadScene("Level4");
            if (Input.GetKeyDown(KeyCode.Alpha6))
                SceneManager.LoadScene("Level5");
            if (Input.GetKeyDown(KeyCode.Alpha7))
                SceneManager.LoadScene("LevelWin");

            if (Input.GetKeyDown(KeyCode.P))
            {
                myMovement.acceleration = 5;
                myMovement.maxSpeed = 7;
                myMovement.jumpForce = 20;
                myMovement.slamForce = 250;
            } else if (Input.GetKeyDown(KeyCode.L))
            {
                myMovement.acceleration = 2;
                myMovement.maxSpeed = 2.5f;
                myMovement.jumpForce = 9;
                myMovement.slamForce = 20;
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
                SetTime(1);
            if (Input.GetKeyDown(KeyCode.Keypad2))
                SetTime(2);
            if (Input.GetKeyDown(KeyCode.Keypad3))
                SetTime(3);
            if (Input.GetKeyDown(KeyCode.Keypad4))
                SetTime(4);
            if (Input.GetKeyDown(KeyCode.Keypad5))
                SetTime(5);
            if (Input.GetKeyDown(KeyCode.Keypad6))
                SetTime(6);
            if (Input.GetKeyDown(KeyCode.Keypad7))
                SetTime(7);
            if (Input.GetKeyDown(KeyCode.Keypad8))
                SetTime(8);
            if (Input.GetKeyDown(KeyCode.Keypad9))
                SetTime(9);
            if (Input.GetKeyDown(KeyCode.Keypad0))
                SetTime(100);
        }
    }

    void SetTime(float timeScale)
    {
        GlobalGame.Instance.defaultTimeScale = timeScale;
        Time.timeScale = timeScale;     
    }
}
