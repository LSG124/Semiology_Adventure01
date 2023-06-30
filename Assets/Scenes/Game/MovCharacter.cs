using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCharacter : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody2D rb;
    public GameObject panel;

    public void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed;
    }

}
