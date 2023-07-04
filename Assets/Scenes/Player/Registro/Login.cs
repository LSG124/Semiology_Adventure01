using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Login : MonoBehaviour
{
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
    public string loginURL = "http://127.0.0.1:8000/login";
    string csrfToken = "";
    public Text email_input;
    public Text password_input;

    public GameObject panel_logeado;

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
                Debug.Log("CSRF token obtained: " + csrfToken);
                StartCoroutine(LoginPost());
            }
        }
    }
    IEnumerator LoginPost()
    {
        Dictionary<string, string> wwwForm = new Dictionary<string, string>();
        wwwForm.Add("_token", csrfToken);
        wwwForm.Add("email", email_input.text);
        wwwForm.Add("password", password_input.text);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, wwwForm))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("No ha funcionado" + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("ha funcionado" + www.downloadHandler.text);
                //panel_registro.SetActive(true);
                string id = www.downloadHandler.text;
                int parsedInt = 0;
                int.TryParse(id, out parsedInt);
                PlayerPrefs.SetInt("id", parsedInt); //se guarda el id del nuevo usuario registrado.
                Debug.Log("El id es   " + id);
                panel_logeado.SetActive(true);
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
