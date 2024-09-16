using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Stats : MonoBehaviour
{
    public int health;
    public int defense;

    public bool isDead = false;
    public bool isPlayer;
    public bool dontSetColor = false;

    public Melee meleeWeapon;
    public Gun gunWeapon;

    private Rigidbody2D myBody;
    private SpriteRenderer myRender;
    private GameObject mainCamera;
    private GameControl gameControl;

    private AudioSource deathSound;
    private Color baseColor;

    void Start()
    {
        mainCamera = GameObject.Find("MainCamera");
        gameControl = mainCamera.GetComponent<GameControl>();

        myRender = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();

        if (isPlayer == true)
        {
            if (GlobalGame.Instance.difficulty == "Normal")
            {
                health = GlobalGame.Instance.normalHealth;
            } else if (GlobalGame.Instance.difficulty == "Insane")
            {
                health = GlobalGame.Instance.insaneHealth;
            }

            deathSound = GetComponent<AudioSource>();
        }

        baseColor = myRender.color;
        SetHPColor();
    }

    public void SetHPColor()
    {
        if (dontSetColor == true)
            return;

        if (health >= 2)
        {
            myRender.color = baseColor;
        } else if (health == 1)
        {
            myRender.color = new Color(1, 0.2f, 0);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isDead == true)
            return;

        health -= Mathf.Clamp(dmg - defense, 0, dmg);
        SetHPColor();

        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (isDead == true)
            return;

        health = 0;
        isDead = true;

        myBody.simulated = false;

        if (isPlayer == true)
        {
            StartCoroutine(DeathAnimation());
        } else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DeathAnimation()
    {
        int max = 25;
        Color myColor = myRender.color;

        deathSound.Play();

        for (int i = 0; i <= max; i++)
        {
            myRender.color = new Color(myColor.r, myColor.g, myColor.b, 1 - ((float)i / max));
            yield return new WaitForSecondsRealtime(0.01f);
        }

        StartCoroutine(gameControl.LoseGame());
    }
}
