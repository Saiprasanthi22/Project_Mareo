using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // reference to Rigitbody
    private   Camera m_camera;
    private new Rigidbody2D rigidBody;

    //Movement Variables
    private Vector2 velocity;
    private float inputAxis;
    public float movementSpeed = 4f;
    public float maxJumpHeight = 0.75f;
    public float maxJumptTime = 0.35f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumptTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumptTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.21f || Mathf.Abs(inputAxis) > 0.21f;
    public bool sliding => (inputAxis > 0 && velocity.x < 0f) || (inputAxis < 0 && velocity.x > 0f);
    

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
        float multiplier = falling ? 0.25f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(gravity / 0.45f, velocity.y);
    }

     void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
            AudioManager.Instance.PlaySFX("Jump");
        }
    }

     void HorizontalMovement()
    {
         inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);

        // check if running into a wall
        if (rigidBody.CheckRaycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        // flip sprite to face direction
        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
                
                //Adding switch statement will be better option
                //when adding multiple sounds
                if(collision.gameObject.tag == "Brick")
                {
                    AudioManager.Instance.PlaySFX("BrickBreak");
                }
            }

        }

        
    }

}
