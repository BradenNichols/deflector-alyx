using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector4 speed;
    Rigidbody2D rb;
    public bool triggered;
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered)
        {
            rb.velocity = speed;
        } else
        {
            rb.velocity = Vector3.zero;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Trigger")
        {
            triggered = false;
            triggered = true;
        }
    }
}
