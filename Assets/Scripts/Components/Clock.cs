using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public CountdownTimer CountdownTimer;
    Transform scale;
    void Start()
    {
        scale = GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            CountdownTimer.AddTime(CountdownTimer.initialTimer - CountdownTimer.timer);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        scale.localScale = Vector3.one * Mathf.Pow(Mathf.Sin(Time.time) + 5, 1.3f);
    }
}
