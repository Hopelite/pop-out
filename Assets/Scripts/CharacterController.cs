using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float movementSpeed = 10f;

    float minX;
    float maxX;
    float minY;
    float maxY;

    public int numberOfCoins;

    public string playerName;


    private void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;
    }

    void Update()
    {
        transform.position += new Vector3(Input.GetAxis(playerName + "Horizontal"), Input.GetAxis(playerName + "Vertical"), 0f)
        * movementSpeed
        * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                         Mathf.Clamp(transform.position.y, minY, maxY),
                                         0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            numberOfCoins++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Finger"))
        {
            Debug.Log($"'{playerName}' is hit by finger!");
        }
    }
}
