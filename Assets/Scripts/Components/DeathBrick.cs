using UnityEngine;

public class DeathBrick : MonoBehaviour
{
    public bool isActive = true;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive == false)
            return;

        Stats hitStats = collision.gameObject.GetComponent<Stats>();

        if (hitStats && !hitStats.isDead)
            hitStats.Kill();
    }
}
