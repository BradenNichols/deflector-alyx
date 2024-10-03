using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(SpriteRenderer))]
public class Stats : MonoBehaviour
{
    public int health;
    public int defense;

    public bool isDead = false;
    public bool isPlayer;
    public bool dontSetColor = false;
    public bool oneHP = false;

    [SerializeField]
    private RectTransform bossBar;
    private float bossBarSize;
    private int baseHealth;

    [HideInInspector]
    public bool doNotRotate = false;

    public Melee meleeWeapon;
    public Gun gunWeapon;

    [SerializeField]
    private CountdownTimer countdownTimer;

    private Rigidbody2D myBody;
    private SpriteRenderer myRender;
    private GameObject mainCamera;
    private GameControl gameControl;

    private AudioSource deathSound;
    private Color baseColor;
    public AudioSource yeowch;

    void Start()
    {
        mainCamera = GameObject.Find("MainCamera");
        gameControl = mainCamera.GetComponent<GameControl>();

        baseHealth = health;

        if (bossBar)
            bossBarSize = bossBar.localScale.x;

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
            myRender.color = baseColor;
        else if (health == 1)
        {
            myRender.color = new Color(1, 0.2f, 0);
            if(isPlayer) oneHP = true;
        }

        if (bossBar)
            bossBar.localScale = new Vector3(Mathf.Clamp(bossBarSize * (float)health / baseHealth, 0, bossBarSize), bossBar.localScale.y, bossBar.localScale.z);
    }

    public void TakeDamage(int dmg)
    {
        if (isDead == true)
            return;

        health -= Mathf.Clamp(dmg - defense, 0, dmg);

        if(isPlayer && countdownTimer)
            countdownTimer.OnDamage();

        if (yeowch)
            yeowch.Play();

        SetHPColor();

        if (health <= 0)
            Kill();
    }

    public void Kill()
    {
        if (isDead == true)
            return;

        health = 0;
        isDead = true;

        myBody.simulated = false;

        if (isPlayer == true)
            StartCoroutine(DeathAnimation());
        else
            Destroy(gameObject);
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
    void Update()
    {
        if (oneHP) StartCoroutine(FlashingLights());
    }
    IEnumerator FlashingLights()
    {
        oneHP = false;
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            yield return null;
            GetComponent<Light2D>().intensity = Mathf.Pow(Mathf.Sin(i * Mathf.PI * 3), 2) * 0.25f;
        }

        //Console.ReadLine("ewa");
        //GetComponent<Light2D>().intensity = Mathf.Lerp(0, 0.5f, .5f);
        //Console.ReadLine("ewaa");
        //yield return new WaitForSecondsRealtime(.6f);
        //Console.ReadLine("ah");
        //GetComponent<Light2D>().intensity = Mathf.Lerp(.5f, 0, .5f);
        //yield return new WaitForSecondsRealtime(.5f);
        //oneHP = true;  
    }
}
