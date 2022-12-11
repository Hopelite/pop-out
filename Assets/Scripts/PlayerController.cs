using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidBody;
    public string playerName;
    public float rotationSpeed = 50f;




    float secondsInInvicibility = 0.5f;
    float currentInvisibilitySeconds;
    bool isTakingDamage;
    bool isOnFloor;

    float minX;
    float maxX;
    float minY;
    float maxY;

    private void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;

        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get the horizontal player's input. Value is between -1.0 to 1.0
        // Where -1.0 stands for the max RIGHT input, and 1.0 for the LEFT one
        // 0.0 means player stands in place
        float horizontalInput = Input.GetAxis(playerName + "Horizontal");
        playerRigidBody.AddTorque(-1 * rotationSpeed * horizontalInput);

        // Move player inside the borders (screen borders), if they leaved them
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0f);

        //if (isTakingDamage)
        //{
        //    currentInvisibilitySeconds += Time.deltaTime;
        //    if (currentInvisibilitySeconds >= secondsInInvicibility)
        //    {
        //        currentInvisibilitySeconds = 0.0f;
        //        isTakingDamage = false;
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTakingDamage && collision.gameObject.CompareTag("Finger"))
        {
            //isTakingDamage = true;
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
