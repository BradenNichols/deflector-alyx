using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject floor, healthBar;
    public new GameObject camera;

    private void OnDestroy()
    {
        camera.GetComponent<CameraFollow>().offset = new Vector3(0, -0.1f, -2);
        camera.GetComponent<Camera>().orthographicSize = 9;
        camera.GetComponent<AudioSource>().mute = true;

        Destroy(floor, 2f);
    }
}
