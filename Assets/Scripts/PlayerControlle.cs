using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidBody;
    Collider2D playerCollider;
    float minX;
    float maxX;
    float minY;
    float maxY;

    public string playerName;
    public Collider2D anotherPlayerCollider;
    public float pushVelocity;
    public float movementSpeed = 10f;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;

        // Ignore collision between players
        Physics2D.IgnoreCollision(playerCollider, anotherPlayerCollider);
    }

    void Update()
    {
        // Move player into the pressed direction
        transform.position += new Vector3(Input.GetAxis(playerName + "Horizontal"), 0.0f, 0.0f) * movementSpeed * Time.deltaTime;

        // Move player inside the borders (screen borders), if they leaved them
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finger"))
        {
            // Get direction of hit
            float hitDirection = collision.transform.position.x - transform.position.x;

            // If hit from the left - push to the right
            if (hitDirection > 0)
            {
                playerRigidBody.AddForce(Vector2.left * pushVelocity, ForceMode2D.Impulse);
            }
            else // If hit from the right - push to the left
            {
                playerRigidBody.AddForce(Vector2.right * pushVelocity, ForceMode2D.Impulse);
            }

            // !!! Here is the place where you can add damage and score logic !!!
        }
    }
}
