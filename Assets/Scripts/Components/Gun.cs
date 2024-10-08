using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Gun : MonoBehaviour
{
    public bool canShoot = true;
    public float fireTime = 1;
    public GameObject Bullet;
    public Vector3 bulletOffset;
    public int bulletspeed = 10;

    public float minTimeToShoot = 0.1f;
    public float maxTimeToShoot = 0.5f;
    public float bulletLightTime = 0.5f;

    public bool isShooting = false;

    public float bulletCooldown = 0;
    public AudioSource shootSound;

    private new Light2D light;

    void Start()
    {
        shootSound = GetComponent<AudioSource>();
        light = GetComponent<Light2D>();
    }

    void FixedUpdate() // Shoot bullets every frame.
    {
        if (isShooting != true)
            return;

        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.fixedDeltaTime;

            if (bulletCooldown < bulletLightTime)
                light.intensity += (Time.fixedDeltaTime * (12 * (0.5f / bulletLightTime)));

            return;
        }

        light.intensity = 0;

        shootSound.Play();
        bulletCooldown = fireTime + Random.Range(minTimeToShoot, maxTimeToShoot);

        GameObject newBullet = Instantiate(Bullet, transform.position, transform.parent.rotation);
        BulletScript bulletScript = newBullet.GetComponent<BulletScript>();

        bulletScript.bulletSpeed = bulletspeed;

        if (transform.parent.parent.localScale.x < 0)
        {
            bulletScript.bulletOffset = -bulletOffset;
            bulletScript.bulletDirection = transform.right;
        }
        else
        {
            bulletScript.bulletOffset = bulletOffset;
            bulletScript.bulletDirection = transform.right * -1;
        }

        bulletScript.enabled = true;
    }

    // Public
    public bool StartShooting()
    {
        if (canShoot == false || isShooting == true)
            return false;

        isShooting = true;
        return true;
    }

    public void StopShooting()
    {
        if (isShooting == false)
            return;

        isShooting = false;
        bulletCooldown = 0;
    }
}
