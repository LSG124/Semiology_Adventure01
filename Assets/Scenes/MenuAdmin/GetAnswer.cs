using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using SimpleJSON;

public class GetAnswer : MonoBehaviour
{
    public GameObject panel;
    public string getsalaURL = "http://127.0.0.1:8000/allanswer";
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
    string csrfToken = "";
    public Text diagnostico_1;
    public Text salaid_1;
    public Text userid_1;
    public Text npcid_1;
    public Text diagnostico_2;
    public Text salaid_2;
    public Text userid_2;
    public Text npcid_2;
    private JSONNode json;
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
                string response = www.downloadHandler.text;
                json = JSON.Parse(response);

                ProcessTopScores(json);
            }
        }
    }
    public void ProcessTopScores(JSONNode json)
    {
        // Verificar si se obtuvo una respuesta válida
        if (json == null || !json.IsArray)
        {
            Debug.Log("Respuesta inválida");
            return;
        }

        // Asignar los valores a los objetos de texto correspondientes
        diagnostico_1.text = json[0]["diagnostico"];
        npcid_1.text = json[0]["npc_id"];
        userid_1.text = json[0]["user_id"];
        salaid_1.text = json[0]["sala_id"];

        //diagnostico_2.text = json[1]["diagnostico"];
        //npcid_2.text = json[1]["npc_id"];
        //userid_2.text = json[1]["user_id"];
        //salaid_2.text = json[1]["sala_id"];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Active(bool foo)
    {
        panel.SetActive(foo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
