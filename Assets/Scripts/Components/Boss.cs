using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject floor;
    public new GameObject camera;

    private void OnDestroy()
    {
        camera.GetComponent<CameraFollow>().offset = new Vector3(0, -0.1f, -2);
        camera.GetComponent<Camera>().orthographicSize = 9;
        Destroy(floor, 1.5f);
    }
}
