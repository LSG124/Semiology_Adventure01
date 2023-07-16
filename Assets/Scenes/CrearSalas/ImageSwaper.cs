using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwaper : MonoBehaviour
{
    public string nameVarPreferences;
    // Una lista de sprites para almacenar tus im�genes PNG
    public List<Sprite> Images;
    private int currentImageIndex = 0;

    // El componente Image donde se mostrar�n las im�genes
    public Image DisplayImage;

    // M�todo para cambiar la imagen hacia la derecha
    public void ChangeImageRight()
    {
        if (Images.Count == 0) return; // En caso de que la lista est� vac�a

        // Incrementamos el �ndice y si superamos el final de la lista, volvemos a 0
        currentImageIndex = (currentImageIndex + 1) % Images.Count;
        
        // Actualizamos la imagen
        DisplayImage.sprite = Images[currentImageIndex];
        PlayerPrefs.SetInt(nameVarPreferences, currentImageIndex);
    }

    // M�todo para cambiar la imagen hacia la izquierda
    public void ChangeImageLeft()
    {
        if (Images.Count == 0) return; // En caso de que la lista est� vac�a

        // Decrementamos el �ndice y si estamos en el inicio de la lista, vamos al final
        currentImageIndex--;
        if (currentImageIndex < 0) currentImageIndex = Images.Count - 1;

        // Actualizamos la imagen
        DisplayImage.sprite = Images[currentImageIndex];
        PlayerPrefs.SetInt(nameVarPreferences, currentImageIndex);
    }
}


