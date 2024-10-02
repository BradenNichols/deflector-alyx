using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public GameObject weaponGrip;

    public float maxDetectionRange = 20f;
    public bool hasSeenPlayer = false;
    public float bulletCooldownOnSeen = 0.6f;
    public bool dontFlip = false;

    private Stats targetStats;
    private Transform targetTransform;
    private Vector3 size;

    private Stats myStats;
    private Gun myGun;

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>();
        myGun = myStats.gunWeapon;
        size = transform.localScale;

        targetTransform = target.transform;
        targetStats = target.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!target || targetStats.isDead == true)
        {
            return;
        }

        if (hasSeenPlayer == false)
        {
            myGun.StopShooting();

            int layerMask = LayerMask.GetMask("Player", "Default");
            RaycastHit2D cast = Physics2D.Raycast(transform.position, targetTransform.position - transform.position, maxDetectionRange, layerMask);

            if (cast.transform == targetTransform)
            {
                hasSeenPlayer = true;
                myGun.bulletCooldown = bulletCooldownOnSeen;
            }
        } else
        {
            if (!dontFlip)
            {
                if (target.transform.position.x > transform.position.x) // right
                {
                    transform.localScale = size;
                    weaponGrip.transform.right = -(weaponGrip.transform.position - targetTransform.position);
                }
                else // left
                {
                    transform.localScale = new Vector3(size.x * -1, size.y, size.z);
                    weaponGrip.transform.right = -(targetTransform.position - weaponGrip.transform.position);
                }
            } else
            {
                weaponGrip.transform.right = -(targetTransform.position - weaponGrip.transform.position);
            }

            myGun.StartShooting();
        }
    }
}
