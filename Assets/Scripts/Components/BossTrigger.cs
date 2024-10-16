using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public new GameObject camera;
    public Melee sword;
    public GameObject boss, bossCanvas;
    public AudioClip fightMusic;

    private bool active;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active && collision.gameObject.CompareTag("Player"))
        {
            active = true;
            sword.canAttack = true;
            bossCanvas.SetActive(true);

            camera.GetComponent<CameraFollow>().offset = new Vector3(0, 0.7f, -2);
            camera.GetComponent<Camera>().orthographicSize = 13;
            camera.GetComponent<AudioSource>().clip = fightMusic;
            camera.GetComponent<AudioSource>().Play();

            boss.SetActive(true);

            if (GlobalGame.Instance.difficulty == "Insane")
                boss.GetComponent<Stats>().health = 15;
        }
    }
}
