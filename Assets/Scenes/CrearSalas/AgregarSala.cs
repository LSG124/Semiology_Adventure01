using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;

public class AgregarSala : MonoBehaviour
{
    public string salaURL = "http://127.0.0.1:8000/guardarsala";
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
    string csrfToken = "";

    public Text name_input;
    public Text profesor_input;
    public Text tema_input;

    public GameObject panel_nueva_sala;

    public void GetCsrfToken()
    {
        StartCoroutine(FetchCsrfToken());
    }

    IEnumerator FetchCsrfToken()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(csrfTokenURL))
        {
            www.SendWebRequest();

            while (!www.isDone)
            {
                yield return null;
            }

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching CSRF token: " + www.error);
            }
            else
            {
                csrfToken = www.downloadHandler.text;
                Debug.Log(csrfToken);
                StartCoroutine(RegisterPost());
            }
        }
    }

    IEnumerator RegisterPost()
    {
        int id_admin = PlayerPrefs.GetInt("id_admin");
        Dictionary<string, string> wwwForm = new Dictionary<string, string>();
        wwwForm.Add("_token", csrfToken);
        wwwForm.Add("admin_id", "1");
        wwwForm.Add("nombre", name_input.text);
        wwwForm.Add("profesor", profesor_input.text);
        wwwForm.Add("tema", tema_input.text);

        using (UnityWebRequest www = UnityWebRequest.Post(salaURL, wwwForm))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("No ha funcionado" + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("ha funcionado" + www.downloadHandler.text);
                panel_nueva_sala.SetActive(false);
                string id = www.downloadHandler.text;
                int parsedInt = 0;
                int.TryParse(id, out parsedInt);
                PlayerPrefs.SetInt("id_sala", parsedInt); //se guarda el id del nuevo usuario registrado.
                Debug.Log("El nuevo id sala ingresado es   " + id);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
