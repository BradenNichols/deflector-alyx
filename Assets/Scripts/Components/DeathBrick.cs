using UnityEngine;

public class DeathBrick : MonoBehaviour
{
    public bool isActive = true;
    public bool ignoreEnemies = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive == false)
            return;

        Stats hitStats = collision.gameObject.GetComponent<Stats>();

        if (hitStats && !hitStats.isDead)
        {
            if (ignoreEnemies && !hitStats.isPlayer)
                return;

            hitStats.Kill();
        } 
    }
}
