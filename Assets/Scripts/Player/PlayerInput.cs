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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !paused)
            myWeapon.Attack();
        if (Input.GetKeyDown(KeyCode.R) && !paused)
            myStats.Kill();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if(!paused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            } else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }

        }
    }
}
