using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidBody;
    float maxSecondsInInvicibility = 0.5f;
    float currentInvisibilitySeconds;
    public int currentHp;
    bool isTakingDamage;
    bool isOnFloor;

    public string playerName;
    public int maxHp = 3;
    public float horizontalSpeed = 50f;
    public float jumpSpeed = 10f;
    public List<GameObject> hitPoints;

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

        currentHp = maxHp;
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get the horizontal player's input. Value is between -1.0 to 1.0
        // Where -1.0 stands for the max RIGHT input, and 1.0 for the LEFT one
        // 0.0 means player stands in place
        float horizontalInput = Input.GetAxis(playerName + "Horizontal");
        playerRigidBody.AddTorque(-1 * horizontalSpeed * horizontalInput);
        playerRigidBody.AddForce(Vector2.right * horizontalSpeed * horizontalInput);

        if (isOnFloor)
        {
            float verticalInput = Input.GetAxis(playerName + "Vertical");
            if (verticalInput > 0f)
            {
                playerRigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }

        // Move player inside the borders (screen borders), if they leaved them
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0f);

        if (isTakingDamage)
        {
            currentInvisibilitySeconds += Time.deltaTime;
            if (currentInvisibilitySeconds >= maxSecondsInInvicibility)
            {
                currentInvisibilitySeconds = 0.0f;
                isTakingDamage = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }

        if (!isTakingDamage && collision.gameObject.CompareTag("Finger"))
        {
            isTakingDamage = true;
            currentHp--;
            if (currentHp >= 0)
            {
                if (currentHp != hitPoints.Count - 1)
                {
                    Debug.LogError("You forgot to add hit point object to " + playerName + "'s hit points list.");
                    return;
                }

                GameObject hitPoint = hitPoints[currentHp];
                hitPoints.Remove(hitPoint); // Remove life
                Destroy(hitPoint);
            }

            if (currentHp <= 0)
            {
                // Game over
                return;
            }
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
