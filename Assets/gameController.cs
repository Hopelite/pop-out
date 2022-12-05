using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    float coinTimer = 0f;
    public GameObject coinPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinTimer += 1f * Time.deltaTime;
        if (coinTimer >= 0.3f)
        {
            GameObject newCoin = Instantiate(coinPrefab);
            float randomX = Random.Range(-5f, 5f);
            float randomY = Random.Range(-5f, 5f);
            newCoin.transform.position = new Vector3(randomX, randomY, 0f);
            coinTimer = 0f;
        }
    }
}
