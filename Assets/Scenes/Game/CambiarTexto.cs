using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CambiarTexto : MonoBehaviour
{
    public Text textoElemento;
    public List<string> listaTextos;
    private int indiceActual;
    public GameObject panel;
    public GameObject panelPreguntas;
    public GameObject panelDiagnostico;
    public GameObject panelFinal;
    public GameObject panelfinaljuego;
    private void Start()
    {
        // Establecer el primer texto de la lista al inicio del juego
        indiceActual = 0;
        textoElemento.text = listaTextos[indiceActual];
        PlayerPrefs.SetInt("npcs", 0);
    }

    public void CambiarTextoElemento()
    {
        // Incrementar el índice actual y asegurarse de que no se exceda el tamaño de la lista
        indiceActual = (indiceActual + 1) % listaTextos.Count;

        if (indiceActual == 0)
        {
            // Si se alcanza el último elemento de la lista, cerrar el panel
            panel.SetActive(false);
            panelPreguntas.SetActive(true);
        }

        // Actualizar el texto del elemento con el texto correspondiente en la lista
        textoElemento.text = listaTextos[indiceActual];
    }

    public void CambiarADiagnostico()
    {
        panelPreguntas.SetActive(false);
        panelDiagnostico.SetActive(true);
    }
    public void CambiarADiagnostico2()
    {
        panelPreguntas.SetActive(false);
        panelDiagnostico.SetActive(true);
        
    }
    public void CambiarTextoElemento2()
    {
        // Incrementar el índice actual y asegurarse de que no se exceda el tamaño de la lista
        indiceActual = (indiceActual + 1) % listaTextos.Count;

        if (indiceActual == 0)
        {
            // Si se alcanza el último elemento de la lista, cerrar el panel
            panelFinal.SetActive(false);
            int npcs = PlayerPrefs.GetInt("npcs");
            npcs++;
            PlayerPrefs.SetInt("npcs",npcs);
            if (npcs == 2)
            {
                //panelfinaljuego.SetActive(true);
                //
            }
        }

        // Actualizar el texto del elemento con el texto correspondiente en la lista
        textoElemento.text = listaTextos[indiceActual];
    }
}