using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    public float slowSpeed = 0.01f;
    private float timeAmount = 0;

    public Color slowColor;
    private Color normalColor;

    private Camera myCamera;

    private void Start()
    {
        myCamera = GetComponent<Camera>();
        normalColor = myCamera.backgroundColor;
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
            Time.timeScale = slowSpeed;
        }
        else if (Time.timeScale == slowSpeed)
        {
            myCamera.backgroundColor = normalColor;
            Time.timeScale = 1f;
        }
    }
}
