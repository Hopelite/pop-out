using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float movementSpeed = 10f;
    void Update()
       
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f)
            * movementSpeed
            * Time.deltaTime;
        }
    
}
