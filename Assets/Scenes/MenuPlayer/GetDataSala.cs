using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class GetDataSala : MonoBehaviour
{
    public string getsalaURL = "http://127.0.0.1:8000/getsala";
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
    public Text name_input;
    string csrfToken = "";

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
                StartCoroutine(GetNPCsRoom());
            }
        }
    }

    IEnumerator GetNPCsRoom()
    {
        Dictionary<string, string> wwwForm = new Dictionary<string, string>();
        wwwForm.Add("_token", csrfToken);
        wwwForm.Add("nombre", name_input.text);

        using (UnityWebRequest www = UnityWebRequest.Post(getsalaURL, wwwForm))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("No ha funcionado" + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("ha funcionado" + www.downloadHandler.text);
                string id = www.downloadHandler.text;
                int parsedInt = 0;
                int.TryParse(id, out parsedInt);
                PlayerPrefs.SetInt("id_sala", parsedInt); //se guarda el id del nuevo usuario registrado.
                Debug.Log("El id de la sala es ingresado es   " + id);
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
