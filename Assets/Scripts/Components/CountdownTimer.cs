using System;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    // References
    private Image image;
    private RectTransform rectTransform;

    [SerializeField]
    private Image background;

    [SerializeField]
    private Stats playerStats;

    // Public Variables

    public float initialTimer = 5;
    public float timeAddPerDeflect = 0.5f;
    public Color addColor;

    // Private Variables

    [HideInInspector]
    public float timer = 0;

    private Color baseColor;
    private float colorTime = 0;
    private bool isActive = false;
    private Vector3 baseScale;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        baseScale = rectTransform.localScale;
        baseColor = image.color;
        timer = initialTimer;
    }

    void UpdateBar()
    {
        background.enabled = true;
        image.enabled = true;

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
        colorTime += 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.isDead || !isActive) return;

        timer -= Time.deltaTime;

        if (colorTime > 0)
            colorTime = Mathf.Clamp(colorTime - Time.deltaTime, 0, colorTime);

        if (colorTime > 0 && timer > 0)
            image.color = addColor;
        else
            image.color = baseColor;

        if (timer <= 0)
        {
            timer = 0;
            playerStats.Kill();
        }

        UpdateBar();
    }
}
