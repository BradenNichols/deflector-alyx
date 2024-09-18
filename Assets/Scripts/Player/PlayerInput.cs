using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    private Stats myStats;
    private Melee myWeapon;
    public GameObject pauseMenu;
    public bool paused;

    void Start()
    {
        myStats = GetComponent<Stats>();
        myWeapon = myStats.meleeWeapon;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale != 0)
            myWeapon.Attack();
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale != 0)
            myStats.Kill();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!paused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            } else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }

            paused = !paused;
        }
    }
}
