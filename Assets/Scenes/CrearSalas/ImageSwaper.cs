using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwaper : MonoBehaviour
{
    public string nameVarPreferences;
    // Una lista de sprites para almacenar tus imágenes PNG
    public List<Sprite> Images;
    private int currentImageIndex = 0;

    // El componente Image donde se mostrarán las imágenes
    public Image DisplayImage;

    // Método para cambiar la imagen hacia la derecha
    public void ChangeImageRight()
    {
        if (Images.Count == 0) return; // En caso de que la lista esté vacía

        // Incrementamos el índice y si superamos el final de la lista, volvemos a 0
        currentImageIndex = (currentImageIndex + 1) % Images.Count;
        
        // Actualizamos la imagen
        DisplayImage.sprite = Images[currentImageIndex];
        PlayerPrefs.SetInt(nameVarPreferences, currentImageIndex);
    }

    // Método para cambiar la imagen hacia la izquierda
    public void ChangeImageLeft()
    {
        if (Images.Count == 0) return; // En caso de que la lista esté vacía

        // Decrementamos el índice y si estamos en el inicio de la lista, vamos al final
        currentImageIndex--;
        if (currentImageIndex < 0) currentImageIndex = Images.Count - 1;

        // Actualizamos la imagen
        DisplayImage.sprite = Images[currentImageIndex];
        PlayerPrefs.SetInt(nameVarPreferences, currentImageIndex);
    }
}


