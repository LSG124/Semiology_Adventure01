using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;

public class NuevoNPC : MonoBehaviour
{
    public string salaURL = "http://127.0.0.1:8000/guardarnpc";
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
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
                StartCoroutine(RegisterPost());
            }
        }
    }

    IEnumerator RegisterPost()
    {
        int id_admin = PlayerPrefs.GetInt("id_admin");
        Dictionary<string, string> wwwForm = new Dictionary<string, string>();
        wwwForm.Add("_token", csrfToken);
        wwwForm.Add("sala_id", "4");
        wwwForm.Add("diagnostico", "asdasdadasd");
        wwwForm.Add("nombre", "asdasdadasd");
        wwwForm.Add("body1", "asdasdadasd");
        wwwForm.Add("body2", "asdasdadasd");
        wwwForm.Add("body3", "asdasdadasd");
        wwwForm.Add("ubicacion", "asdasdadasd");
        wwwForm.Add("descripcion", "asdasdadasd");
        wwwForm.Add("p1", "asdasdadasd");
        wwwForm.Add("p2", "asdasdadasd");
        wwwForm.Add("p3", "asdasdadasd");
        wwwForm.Add("p4", "asdasdadasd");
        wwwForm.Add("p5", "asdasdadasd");
        wwwForm.Add("p6", "asdasdadasd");
        wwwForm.Add("r1", "asdasdadasd");
        wwwForm.Add("r2", "asdasdadasd");
        wwwForm.Add("r3", "asdasdadasd");
        wwwForm.Add("r4", "asdasdadasd");
        wwwForm.Add("r5", "asdasdadasd");
        wwwForm.Add("r6", "asdasdadasd");

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
                string id = www.downloadHandler.text;
                int parsedInt = 0;
                int.TryParse(id, out parsedInt);
                PlayerPrefs.SetInt("id_npc", parsedInt); //se guarda el id del nuevo usuario registrado.
                Debug.Log("El nuevo id NPC ingresado es   " + id);
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
