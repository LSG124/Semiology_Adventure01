using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Se ha presionado la tecla W");
            anim.SetBool("arriba", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log("Se ha soltado la tecla W");
            anim.SetBool("arriba", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Se ha presionado la tecla W");
            anim.SetBool("abajo", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("Se ha soltado la tecla W");
            anim.SetBool("abajo", false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Se ha presionado la tecla W");
            anim.SetBool("left", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("Se ha soltado la tecla W");
            anim.SetBool("left", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Se ha presionado la tecla W");
            anim.SetBool("right", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("Se ha soltado la tecla W");
            anim.SetBool("right", false);
        }


    }
}