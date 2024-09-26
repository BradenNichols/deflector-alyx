using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public CountdownTimer CountdownTimer;
    public Melee melee;
    public float timer;

    Transform scale;
    void Start()
    {
        scale = GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            CountdownTimer.initialTimer = timer;
            CountdownTimer.AddTime(CountdownTimer.initialTimer - CountdownTimer.timer);
            
            melee.canAttack = false;

            Destroy(gameObject);
        }
    }
    void Update()
    {
        scale.localScale = Vector3.one * Mathf.Pow(Mathf.Sin(Time.time) + 5, 1.3f);
    }
}
