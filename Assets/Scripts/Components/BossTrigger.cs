using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public new GameObject camera;
    public GameObject boss;

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active && collision.gameObject.CompareTag("Player"))
        {
            active = true;

            camera.GetComponent<CameraFollow>().offset = new Vector3(0, 0.8f, -2);
            camera.GetComponent<Camera>().orthographicSize = 12;
            boss.SetActive(true);
        }
    }
}
