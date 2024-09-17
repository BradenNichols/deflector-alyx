using System;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private Text myText;

    [SerializeField]
    private Stats playerStats;

    public float initialTimer = 5;
    public float timeAddPerDeflect = 0.5f;

    [HideInInspector]
    public float timer = 0;

    public Color addColor;

    private Color baseColor;
    private float colorTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();

        baseColor = myText.color;
        timer = initialTimer;

        UpdateText();
    }

    void UpdateText()
    {
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("F2");

        if (seconds.Length == 4)
            seconds = "0" + seconds;

        myText.text = minutes + ":" + seconds;
    }


    // Public

    public void AddTime(float time)
    {
        timer += time;
        colorTime += 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.isDead) return;

        timer -= Time.deltaTime;

        if (colorTime > 0)
            colorTime = Mathf.Clamp(colorTime - Time.deltaTime, 0, colorTime);

        if (colorTime > 0 && timer > 0)
            myText.color = addColor;
        else
            myText.color = baseColor;

        if (timer <= 0)
        {
            timer = 0;
            playerStats.Kill();
        }    

        UpdateText();
    }
}
