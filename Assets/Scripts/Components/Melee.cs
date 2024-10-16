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
    public float cooldown = 0;

    private int baseSwingRotate = -360; // Per second
    private bool isAttacking = false;

    public AudioSource deflectSound;

    private Stats myStats;
    private Transform baseTransform;
    private BoxCollider2D collide;
    private SpriteRenderer render;
    public new Light2D light;

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
            StopAttack();

        if (isAttacking == true)
        {
            if (baseTransform.rotation.eulerAngles.z != 0 && baseTransform.rotation.eulerAngles.z <= (360 - swingAngle))
            {
                StopAttack();
                return;
            }

            baseTransform.Rotate(new Vector3(0, 0, (swingSpeed * (float) baseSwingRotate) * Time.deltaTime));
        }
        cooldown = Mathf.Clamp(cooldown - Time.deltaTime, 0, cooldown);
    }

    // Hit Detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        Stats ourStats = GetComponent<Stats>();
        Stats hitStats = collision.gameObject.GetComponent<Stats>();

        if (isAttacking == true && hitStats != null && hitStats.isPlayer != myStats.isPlayer)
            hitStats.TakeDamage(swingDamage);
    }

    // Public

    public bool Attack()
    {
        if (canAttack == false || isAttacking == true || myStats.isDead == true || myStats.gameObject.GetComponent<PlayerInput>().paused == true || cooldown > 0) 
            return false;

        isAttacking = true;
        render.enabled = true;
        collide.enabled = true;
        light.enabled = true;

        cooldown = .5f;
        myStats.doNotRotate = true;

        if (Input.mousePosition.x < Screen.width / 2)
            transform.parent.parent.transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            transform.parent.parent.transform.rotation = new Quaternion(0, 0, 0, 0);

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
        myStats.doNotRotate = false;

        baseTransform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
