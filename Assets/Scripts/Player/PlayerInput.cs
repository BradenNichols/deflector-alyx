using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Stats myStats;
    private Melee myWeapon;

    void Start()
    {
        myStats = GetComponent<Stats>();
        myWeapon = myStats.meleeWeapon;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            myWeapon.Attack();
        if (Input.GetKeyDown(KeyCode.R))
            myStats.Kill();
    }
}
