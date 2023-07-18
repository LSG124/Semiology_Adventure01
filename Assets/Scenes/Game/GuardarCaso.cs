using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class GuardarCaso : MonoBehaviour
{
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
    public string loginURL = "http://127.0.0.1:8000/storehistoria";
    string csrfToken = "";
    public Text diagnostico;

    public GameObject dialogo_final;
    public GameObject diagnostico_panel;

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
        wwwForm.Add("diagnostico", diagnostico.text);
        wwwForm.Add("user_id", "1");//PlayerPrefs.GetInt("id_admin"));

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
                diagnostico_panel.SetActive(false);
                dialogo_final.SetActive(true);
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
