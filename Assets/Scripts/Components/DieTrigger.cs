using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieTrigger : MonoBehaviour
{
    private bool isTriggered = false;
    public Text text;
    public float rainbowSpeed = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered)
            return;

        Stats hitStats = collision.gameObject.GetComponent<Stats>();

        if (hitStats && !hitStats.isDead && hitStats.isPlayer)
        {
            PlayerMovement move = collision.gameObject.GetComponent<PlayerMovement>();
            move.acceleration = 456;
            move.maxSpeed = 187999999995;
            move.jumpForce = 1344;
            move.defaultGravity = 420;
            move.slamForce = 1119;

            Time.timeScale = 100;
            GlobalGame.Instance.defaultTimeScale = 100;
            isTriggered = true;
        }
    }

    void Update()
    {
        if (isTriggered)
            text.color = Color.HSVToRGB(Mathf.PingPong((Time.time + 2) * rainbowSpeed, 1), 1, 1);
    }
}
