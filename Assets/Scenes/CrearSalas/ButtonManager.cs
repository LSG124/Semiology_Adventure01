using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button[] buttons;

    private void Start()
    {
        // Asignar el método OnButtonClick a cada botón
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i; // Capturar el índice en una variable local para evitar errores de cierre
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        // Cambiar el color de los botones
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == buttonIndex)
            {
                buttons[i].image.color = Color.green;
            }
            else
            {
                buttons[i].image.color = Color.yellow;
            }
        }

        // Guardar el valor en PlayerPrefs
        PlayerPrefs.SetString("espacio", (buttonIndex + 1).ToString());
        Debug.Log(PlayerPrefs.GetString("espacio"));
    }
}
