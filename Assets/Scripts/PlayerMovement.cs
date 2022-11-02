using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // reference to Rigitbody
    private  Camera m_camera;
    private Rigidbody2D rigidBody;

    //Movement Variables
    private Vector2 velocity;
    private float inputAxis;
    public float movementSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumptTime = 1f;
    public float jumpForce => (0.2f * maxJumpHeight) / (maxJumptTime / 0.2f);
    public float gravity => (-0.2f * maxJumpHeight) / Mathf.Pow((maxJumptTime / 0.6f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }


     void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        m_camera = Camera.main;
    }

     void Update()
    {
        HorizontalMovement();

        grounded = rigidBody.CheckRaycast(Vector2.down);
       
        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity(); 
    }

     void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 0.02f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(gravity / 0.02f, velocity.y);
    }

     void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

     void HorizontalMovement()
    {
         inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);
    }

     void FixedUpdate()
    {
        Vector2 position = rigidBody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = m_camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = m_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.05f, rightEdge.x - 0.05f);

        rigidBody.MovePosition(position);
    }

   /*  void OnCollisionEnter2d(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }

        }
    }*/

}
