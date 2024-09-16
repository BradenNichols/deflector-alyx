using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Stats hitStats = collision.gameObject.GetComponent<Stats>();

        if (hitStats && hitStats.isPlayer == true)
        {
            Application.Quit();
        }
    }
}
