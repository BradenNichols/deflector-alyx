using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(BoxCollider2D))]
public class Melee : MonoBehaviour
{
    public bool canAttack = true;

    public float swingSpeed = 1;
    public int swingDamage = 1;
    public int deflectDamage = 3;
    public int swingAngle = 120; // At what angle will the swing stop.

    private int baseSwingRotate = -360; // Per second
    private bool isAttacking = false;

    public AudioSource deflectSound;

    private Stats myStats;
    private Transform baseTransform;
    private BoxCollider2D collide;
    private SpriteRenderer render;
    public Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        baseTransform = transform.parent.transform;
        myStats = transform.parent.parent.GetComponent<Stats>();

        collide = GetComponent<BoxCollider2D>();
        render = GetComponent<SpriteRenderer>();
        deflectSound = GetComponent<AudioSource>();

        render.enabled = false;
        collide.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (myStats.isDead == true)
        {
            StopAttack();
        }

        if (isAttacking == true)
        {
            if (baseTransform.rotation.eulerAngles.z != 0 && baseTransform.rotation.eulerAngles.z <= (360 - swingAngle))
            {
                StopAttack();
                return;
            }

            baseTransform.Rotate(new Vector3(0, 0, (swingSpeed * (float) baseSwingRotate) * Time.deltaTime));
        }
    }

    // Hit Detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        Stats ourStats = GetComponent<Stats>();
        Stats hitStats = collision.gameObject.GetComponent<Stats>();

        if (isAttacking == true && hitStats != null && hitStats.isPlayer != myStats.isPlayer)
        {
            hitStats.TakeDamage(swingDamage);
        }
    }

    // Public

    public bool Attack()
    {
        if (canAttack == false || isAttacking == true || myStats.isDead == true || myStats.gameObject.GetComponent<PlayerInput>().paused == true) 
            return false;

        isAttacking = true;
        render.enabled = true;
        collide.enabled = true;
        light.enabled = true;


        return true;
    }

    public void StopAttack()
    {
        if (isAttacking == false)
            return;

        isAttacking = false;
        render.enabled = false;
        collide.enabled = false;
        light.enabled = false;

        baseTransform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
