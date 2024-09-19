using System;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    // References
    [SerializeField]
    private GameObject countdownBar;
    [SerializeField]
    private Image countdownIcon;

    private Image image;
    private RectTransform rectTransform;
    private Canvas canvas;

    [SerializeField]
    private Stats playerStats;

    // Public Variables

    public float initialTimer = 5;
    public float timeAddPerDeflect = 0.5f;
    public Color barAddColor;
    public Color iconAddColor;

    // Private Variables

    [HideInInspector]
    public float timer = 0;
    public bool isActive = false;

    private Color baseColor;
    private Color baseIconColor;

    private float colorTime = 0;
    private Vector3 baseScale;

    // Start is called before the first frame update
    void Start()
    {
        image = countdownBar.GetComponent<Image>();
        rectTransform = countdownBar.GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();

        baseScale = rectTransform.localScale;
        baseColor = image.color;
        baseIconColor = countdownIcon.color; 
        timer = initialTimer;
    }

    void UpdateBar()
    {
        canvas.enabled = isActive;
        rectTransform.localScale = new Vector3(baseScale.x * (timer / initialTimer), baseScale.y, baseScale.z);
    }

    // Public

    public void OnDeflect()
    {
        if (!isActive)
            isActive = true;

        AddTime(timeAddPerDeflect);
    }

    public void AddTime(float time)
    {
        timer = Mathf.Clamp(timer + time, timer, initialTimer);
        colorTime += 0.14f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.isDead || !isActive) return;

        timer -= Time.deltaTime;

        if (colorTime > 0)
            colorTime = Mathf.Clamp(colorTime - Time.deltaTime, 0, colorTime);

        if (colorTime > 0 && timer > 0)
        {
            image.color = barAddColor;
            countdownIcon.color = iconAddColor;
            countdownIcon.transform.localScale = new Vector3(1.17f, 1.17f, 1.17f);
        }
        else
        {
            float redRatio = Mathf.Clamp((timer + 1) / (initialTimer / 1.8f), 0.3f, 1);
            image.color = new Color(baseColor.r * redRatio, baseColor.g, baseColor.b, baseColor.a);
            countdownIcon.color = baseIconColor;
            countdownIcon.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (timer <= 0)
        {
            timer = 0;
            playerStats.Kill();
        }

        UpdateBar();
    }
}
