using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject fingerPrefab;
    public static bool isFingerDestroyed = true; // Static - means can be accessed without object instance

    float minX;
    float maxX;

    void Start()
    {
        // Get the max left and the max right positions
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        minX = bottomLeft.x;
        maxX = topRight.x;
    }

    void Update()
    {
        if (isFingerDestroyed)
        {
            GameObject finger = Instantiate(fingerPrefab);
            float randomHorizontalPosition = Random.Range(minX, maxX);
            finger.transform.position = new Vector3(randomHorizontalPosition, 14.1f, 0);
            isFingerDestroyed = false;
        }
    }
}
