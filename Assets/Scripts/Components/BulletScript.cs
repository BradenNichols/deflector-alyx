using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public int bulletSpeed = 1;
    public int bulletDamage = 5;
    public bool shouldHitPlayers = true, deflecting = false, deflected = false;

    private float lifetime = 0;

    public TimeSlow timeSlow;

    private CircleCollider2D myCollider;
    private SpriteRenderer mySprite;
    private Rigidbody2D myBody;

    [SerializeField]
    private CountdownTimer countdownTimer;

    public Vector2 bulletDirection;
    public Vector3 bulletOffset;
    public ParticleSystem sparks;
    public AudioSource sching;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<CircleCollider2D>();
        myBody = GetComponent<Rigidbody2D>();

        mySprite.enabled = true;
        myCollider.enabled = true;

        transform.Translate(bulletOffset, Space.Self);
    }

    // Hit Detect
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 2) // Ignore Raycast
            return;

        Stats hitStats = collision.gameObject.GetComponent<Stats>();

        if (hitStats)
        {
            if (hitStats.isPlayer != shouldHitPlayers || hitStats.isDead == true)
                return;

            hitStats.TakeDamage(bulletDamage);
        }
        if(!deflecting) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "moving") Destroy(gameObject);
        if (deflected) return;

        Melee hitMeleeWeapon = collision.gameObject.GetComponent<Melee>();

        if (hitMeleeWeapon)
        {
            bulletDamage = hitMeleeWeapon.deflectDamage;

            hitMeleeWeapon.cooldown = 0;
            Deflect();
            return;
        }
    }

    // Deflect
    void Deflect()
    {
        lifetime = 0;
        animator.SetBool("Deflect", true);

        deflected = true;
        deflecting = true;
        sparks.Play();
        sching.Play();
        shouldHitPlayers = false;
        bulletDirection *= -1;
        if (countdownTimer)
            countdownTimer.OnDeflect();

        timeSlow.AddTime(0.35f);
        StartCoroutine(WaitUntilTimeScale());
    }

    Vector2 GetMouseDirection()
    {
        Vector3 dir = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        dir = Camera.main.ScreenToWorldPoint(dir);
        dir = dir - transform.position;

        return new Vector2(dir.x, dir.y);
    }

    IEnumerator WaitUntilTimeScale()
    {
        yield return new WaitUntil(() => Time.timeScale == GlobalGame.Instance.defaultTimeScale);
        bulletDirection = GetMouseDirection();
        deflecting = false;
    }

    // Update is called once per frame
    void Update()
    {
        myBody.velocity = bulletDirection * bulletSpeed;
        lifetime += Time.deltaTime;

        if (lifetime >= 6)
            Destroy(gameObject);
    }
}
