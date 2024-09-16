using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGame : MonoBehaviour
{
    public static GlobalGame Instance;
    public string difficulty = "Normal";

    public AudioSource portalSound;

    public int normalHealth = 2;
    public int insaneHealth = 1;
    public float timer = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
