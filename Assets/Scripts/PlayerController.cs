using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float secondsInInvicibility = 0.5f;
    public string playerName;
    public float movementSpeed = 10f;

    Rigidbody2D playerRigidBody;
    float currentInvisibilitySeconds;
    bool isTakingDamage;
    bool isOnFloor;

    float rotationAngle = 5.0f;
    float minX;
    float maxX;
    float minY;
    float maxY;



    public float pushVelocity;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;
    }

    void Update()
    {
        // Move player into the pressed direction
        float horizontalInput = Input.GetAxis(playerName + "Horizontal");
        transform.position += new Vector3(horizontalInput, 0.0f, 0.0f) * movementSpeed * Time.deltaTime;

        // Move player inside the borders (screen borders), if they leaved them
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0f);

        // Rotate player towards movement
        transform.Rotate(Vector3.back, rotationAngle * horizontalInput);

        if (isTakingDamage)
        {
            currentInvisibilitySeconds += Time.deltaTime;
            if (currentInvisibilitySeconds >= secondsInInvicibility)
            {
                currentInvisibilitySeconds = 0.0f;
                isTakingDamage = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTakingDamage && collision.gameObject.CompareTag("Finger"))
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
            isTakingDamage = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
        }
    }
}
