using UnityEngine;

public class FingerController : MonoBehaviour
{
    float spawnPositionY;
    float stayedTime;
    bool isMovingDown = true;
    bool isOnFloor = false;

    public float speed;
    public float lowestVerticalPosition;
    public float maxStayTime;

    void Start()
    {
        spawnPositionY = transform.position.y;
    }

    void Update()
    {
        if (isMovingDown && transform.position.y > lowestVerticalPosition)
        {
            transform.position -= Vector3.up * speed;
        }

        if (isMovingDown && transform.position.y <= lowestVerticalPosition)
        {
            isMovingDown = false;
            isOnFloor = true;
        }

        if (!isMovingDown)
        {
            // Check, if finger is pressing the floor
            if (isOnFloor)
            {
                stayedTime += 1.0f * Time.deltaTime;
                if (stayedTime >= maxStayTime) // Check, if press time is out
                {
                    isOnFloor = false;
                    stayedTime = 0.0f;
                }

                return;
            }

            // If finger is on the start position or higher
            if (transform.position.y >= spawnPositionY)
            {
                GameController.isFingerDestroyed = true;
                Destroy(gameObject);
            }

            transform.position += Vector3.up * speed;
        }
    }
}
