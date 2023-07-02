using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCharacter : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody2D rb;

    private Vector2 direction = Vector2.zero;
   
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = 0;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            direction.y = Input.GetAxis("Vertical");
            direction.x = 0;
        }
        else 
        {
            direction.x = 0;
            direction.y = 0;
        };
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }
}
