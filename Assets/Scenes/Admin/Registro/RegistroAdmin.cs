using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;



public class RegistroAdmin : MonoBehaviour
{
    public string registerURL = "http://127.0.0.1:8000/adminregister";
    public new string name = "asdasdasd";
    public string email = "asdasd@adasdas.com";
    public string password = "qwertyuioasdp";
    public string passwordConfirmation = "qwertyuiop";
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
    string csrfToken = "";

    public Text name_input;
    public Text email_input;
    public Text password_input;

    public GameObject panel_registro;

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
        Dictionary<string, string> wwwForm = new Dictionary<string, string>();
        wwwForm.Add("_token", csrfToken);
        wwwForm.Add("name", name_input.text);
        wwwForm.Add("email", email_input.text);
        wwwForm.Add("password", password_input.text);

        using (UnityWebRequest www = UnityWebRequest.Post(registerURL, wwwForm))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("No ha funcionado" + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("ha funcionado" + www.downloadHandler.text);
                panel_registro.SetActive(true);
                string id = www.downloadHandler.text;
                int parsedInt = 0;
                int.TryParse(id, out parsedInt);
                PlayerPrefs.SetInt("id", parsedInt); //se guarda el id del nuevo usuario registrado.
                Debug.Log("El nuevo id ingresado es   " + id);
            }
        }

    }
}