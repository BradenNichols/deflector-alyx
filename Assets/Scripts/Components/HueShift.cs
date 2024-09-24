using UnityEngine;

public class HueShift : MonoBehaviour
{
    public float Speed = 0.1f;
    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        rend.color = Color.HSVToRGB(Mathf.PingPong((Time.time + 2) * Speed, 1), 1, 1);
    }
}
