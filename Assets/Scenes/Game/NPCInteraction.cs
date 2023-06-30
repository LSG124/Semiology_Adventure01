using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public Text interactionText;
    public GameObject panel;

    private bool playerInRange = false;

    void Start()
    {
        // Aseg�rate de que el texto y el panel est�n ocultos al inicio
        interactionText.gameObject.SetActive(false);
        panel.SetActive(false);
    }

    void Update()
    {
        // Si el jugador est� en rango y presiona E, muestra el panel
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            panel.SetActive(true);
        }
    }

    // Esto se activa cuando otro collider entra en el collider del NPC

    public void OnCollisionEnter2D(Collision2D other)
    {
        // Comprueba si el objeto que entr� es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            interactionText.gameObject.SetActive(true);
            playerInRange = true;
        }
    }

    // Esto se activa cuando otro collider sale del collider del NPC
    void OnCollisionExit2D(Collision2D other)
    {
        // Comprueba si el objeto que sali� es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            interactionText.gameObject.SetActive(false);
            panel.SetActive(false);
            playerInRange = false;
        }
    }
}
