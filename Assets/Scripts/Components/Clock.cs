using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public CountdownTimer CountdownTimer;
    public Melee melee;
    public float timer;

    [SerializeField]
    private MovingPlatform platform;

    private bool used = false;

    Transform scale;
    void Start()
    {
        scale = GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;

        if(collision.name == "Player")
        {
            used = true;

            CountdownTimer.initialTimer = timer;
            CountdownTimer.baseColor = new Color(0.5f, 0, 0.5f);
            CountdownTimer.AddTime(CountdownTimer.initialTimer - CountdownTimer.timer);
            
            melee.canAttack = false;
            platform.triggered = false;

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);

            //Destroy(gameObject);
        }
    }
    void Update()
    {
        //scale.localScale = Vector3.one * Mathf.Pow(Mathf.Sin(Time.time) + 5, 1.3f);
    }
}
