using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameControl gameControl;
    public bool isActive = true;
    public string sceneName;
    public string setDifficulty = "";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive == false)
            return;

        Stats hitStats = collision.gameObject.GetComponent<Stats>();

        if (hitStats && hitStats.isPlayer == true && hitStats.isDead == false)
        {
            if (setDifficulty != "")
            {
                GlobalGame.Instance.difficulty = setDifficulty;
            }

            GlobalGame.Instance.portalSound.Play();
            gameControl.WinLevel(sceneName);
        }
    }
}
