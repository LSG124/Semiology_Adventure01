using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using SimpleJSON;

public class Diagnostico : MonoBehaviour
{

    public string getsalaURL = "http://127.0.0.1:8000/diagnostico";
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
    string csrfToken = "";
    public Text diagnostico;

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
                StartCoroutine(PostDiagnostico());
            }
        }
    }
    IEnumerator PostDiagnostico()
    {
        Dictionary<string, string> wwwForm = new Dictionary<string, string>();
        wwwForm.Add("_token", csrfToken);
        wwwForm.Add("diagnostico", diagnostico.text);
        wwwForm.Add("npc_id", "3");
        wwwForm.Add("user_id", "1");
        wwwForm.Add("sala_id", "4");
       

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
