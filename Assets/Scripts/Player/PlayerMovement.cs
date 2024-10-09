using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;

    public KeyCode jump;
    public KeyCode jump2;
    public KeyCode jump3;
    public KeyCode slam;

    public float slamForce;
    public float featherForce;
    public float jumpForce;

    public float defaultGravity = 1;
    public bool canFeather = true;
    public float coyoteTime = 0.1f;

    private Stats myStats;
    private Rigidbody2D body2D;
    private Collider2D collide2D;
    private float timeSinceGrounded = 0;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        collide2D = GetComponent<Collider2D>();
        myStats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myStats.isDead == true)
            return;

        // Left / Right Movement.
        float Horizontal = Input.GetAxis("Horizontal");
        body2D.AddForce(new Vector2(Horizontal * (8000 * acceleration) * Time.deltaTime, 0));

        // Rotation for Weapons

        if (!myStats.doNotRotate)
        {
            if (Horizontal < 0)
                transform.rotation = new Quaternion(0, 180, 0, 0);
            else if (Horizontal > 0)
                transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (collide2D.IsTouchingLayers())
            timeSinceGrounded = 0;
        else 
            timeSinceGrounded += Time.deltaTime;

        // Jump.

        if (timeSinceGrounded <= coyoteTime)
            if ((Input.GetKeyDown(jump) || Input.GetKeyDown(jump2) || Input.GetKeyDown(jump3)))
                body2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Slam
        if (Input.GetKey(slam))
            body2D.gravityScale = slamForce;
        else if ((Input.GetKey(jump) || Input.GetKey(jump2) || Input.GetKey(jump3)) && canFeather == true)
            body2D.gravityScale = featherForce;
        else
            body2D.gravityScale = defaultGravity;

        // Clamp speed.
        body2D.velocity = new Vector2(Mathf.Clamp(body2D.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(body2D.velocity.y, -jumpForce, jumpForce));
    }
}
