using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour
{
    private float spawnPositionY;
    private bool isMovingDown = true;
    public float speed;
    public float destinationY;

    void Start()
    {
        spawnPositionY = transform.position.y;
    }

    void Update()
    {
        if (isMovingDown && transform.position.y > destinationY)
        {
            transform.position -= Vector3.up * speed;
        }

        if (isMovingDown && transform.position.y <= destinationY)
        {
            isMovingDown = false;
        }

        if (!isMovingDown)
        {
            // If finger is on the start position or higher
            if (transform.position.y >= spawnPositionY)
            {
                GameController.isFingerDestroyed = true;
                //isMovingDown = true;
                Destroy(gameObject);
            }

            transform.position += Vector3.up * speed;
        }
    }
}
