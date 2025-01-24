using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    public float moveSpeed = 2f;      // Horizontal movement speed
    public float boostForce = 3f;     // Upward force when pressing W
    public float maxSpeed = 3f;       // Maximum allowed speed

    

    void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandlePlayerInput();
    }

    void HandlePlayerInput()
    {
        // WASD controls
        float horizontalInput = Input.GetAxis("Horizontal"); // A (-1) / D (+1)
        float verticalInput = Input.GetAxis("Vertical");     // W (+1) / S (-1)

        // Move left/right
        rb.AddForce(Vector2.right * horizontalInput * moveSpeed, ForceMode2D.Force);

        // Move up/down
        rb.AddForce(Vector2.up * verticalInput * boostForce, ForceMode2D.Force);

        // Clamp maximum velocity
        rb.linearVelocity = new Vector2(
            Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rb.linearVelocity.y, -maxSpeed, maxSpeed) // Allow downward movement with 'S'
        );
    }
}