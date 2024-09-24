using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public GameObject weaponGrip;

    public float maxDetectionRange = 20f;
    public bool hasSeenPlayer = false;

    private Stats targetStats;
    private Transform targetTransform;

    private Stats myStats;
    private Gun myGun;

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>();
        myGun = myStats.gunWeapon;

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
            }
        } else
        {
            //if (transform.localScale.x == -1)
            //{
                // Gun facing right (doesnt work)
                //Debug.Log("right");
               // weaponGrip.transform.right = -(weaponGrip.transform.position - targetTransform.position);
            //}
            //else
            //{
                // Gun facing left
                //Debug.Log("left");
                weaponGrip.transform.right = -(targetTransform.position - weaponGrip.transform.position);
            //}

            if (target.transform.position.x > transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = Vector3.one;

            myGun.StartShooting();
        }
    }
}
