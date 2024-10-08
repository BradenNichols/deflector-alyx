using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeSlow : MonoBehaviour
{
    public float slowSpeed = 0.01f;
    public Light2D sun;

    private float sunIntensity;
    private float timeAmount = 0;

    public Color slowColor;
    private Color normalColor;

    private Camera myCamera;

    private void Start()
    {
        myCamera = GetComponent<Camera>();
        normalColor = myCamera.backgroundColor;
        sunIntensity = sun.intensity;

        Time.timeScale = GlobalGame.Instance.defaultTimeScale;
    }

    public void AddTime(float length)
    {
        timeAmount += length;

        if (timeAmount > 0)
            Time.timeScale = slowSpeed;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        timeAmount = Mathf.Clamp(timeAmount - (Time.deltaTime / slowSpeed), 0, timeAmount);

        if (timeAmount > 0)
        {
            myCamera.backgroundColor = slowColor;
            sun.intensity = 0.75f;
            Time.timeScale = slowSpeed;
        }
        else if (Time.timeScale == slowSpeed)
        {
            myCamera.backgroundColor = normalColor;
            sun.intensity = sunIntensity;
            Time.timeScale = GlobalGame.Instance.defaultTimeScale;
        }
    }
}
