using UnityEngine;
using UnityEngine.UI;

public class PreguntasRespuestas : MonoBehaviour
{
    public Text preguntaTexto;
    public Text preguntaTexto2;
    public Text respuestaTexto;
    public Button boton1;
    public Button boton2;

    private int indicePreguntaActual;
    private int indicePreguntaActual2;
    public string[] preguntas;
    public string[] respuestas;

    public string[] preguntas2;
    public string[] respuestas2;

    private void Start()
    {
        boton1.onClick.AddListener(ClickBoton1);
        boton2.onClick.AddListener(ClickBoton2);

        // Inicializar las preguntas y respuestas
        /*preguntas = new string[]
        {
            "Pregunta 1",
            "Pregunta 2",
            "Pregunta 3"
        };

        respuestas = new string[]
        {
            "Respuesta 1",
            "Respuesta 2",
            "Respuesta 3"
        };*/

        indicePreguntaActual = 0;
        indicePreguntaActual2 = 0;
        MostrarPreguntaActual();
        MostrarPreguntaActual2();
    }

    private void ClickBoton1()
    {
        CambiarPregunta();
        MostrarRespuestaActual();
    }

    private void ClickBoton2()
    {
        CambiarPregunta2();
        MostrarRespuestaActual2();
    }

    private void MostrarPreguntaActual()
    {
        preguntaTexto.text = preguntas[indicePreguntaActual];
    }
    private void MostrarPreguntaActual2()
    {
        preguntaTexto2.text = preguntas2[indicePreguntaActual2];
    }

    private void MostrarRespuestaActual()
    {
        if (indicePreguntaActual == 0)
        {
            respuestaTexto.text = respuestas[preguntas.Length - 1];
        }
        else
        {
            respuestaTexto.text = respuestas[indicePreguntaActual - 1];
        }
    }
    private void MostrarRespuestaActual2()
    {
        if (indicePreguntaActual2 == 0)
        {
            respuestaTexto.text = respuestas2[preguntas2.Length - 1];
        }
        else
        {
            respuestaTexto.text = respuestas2[indicePreguntaActual2 - 1];
        }
    }

    private void CambiarPregunta()
    {
        indicePreguntaActual = (indicePreguntaActual + 1) % preguntas.Length;
        MostrarPreguntaActual();
        respuestaTexto.text = string.Empty; // Limpiar el texto de la respuesta al cambiar de pregunta
    }
    private void CambiarPregunta2()
    {
        indicePreguntaActual2 = (indicePreguntaActual2 + 1) % preguntas2.Length;
        MostrarPreguntaActual2();
        respuestaTexto.text = string.Empty; // Limpiar el texto de la respuesta al cambiar de pregunta
    }
}