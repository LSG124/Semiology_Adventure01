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
    private void Start()
    {
        // Establecer el primer texto de la lista al inicio del juego
        indiceActual = 0;
        textoElemento.text = listaTextos[indiceActual];
    }

    public void CambiarTextoElemento()
    {
        // Incrementar el �ndice actual y asegurarse de que no se exceda el tama�o de la lista
        indiceActual = (indiceActual + 1) % listaTextos.Count;

        if (indiceActual == 0)
        {
            // Si se alcanza el �ltimo elemento de la lista, cerrar el panel
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
        // Incrementar el �ndice actual y asegurarse de que no se exceda el tama�o de la lista
        indiceActual = (indiceActual + 1) % listaTextos.Count;

        if (indiceActual == 0)
        {
            // Si se alcanza el �ltimo elemento de la lista, cerrar el panel
            panelFinal.SetActive(false);
        }

        // Actualizar el texto del elemento con el texto correspondiente en la lista
        textoElemento.text = listaTextos[indiceActual];
    }
}