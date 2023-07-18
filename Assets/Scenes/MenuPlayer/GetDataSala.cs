using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using SimpleJSON;

public class GetDataSala : MonoBehaviour
{
    public string getsalaURL = "http://127.0.0.1:8000/getsala";
    public string csrfTokenURL = "http://127.0.0.1:8000/returncsrf";
    public Text name_input;
    public Text nombre;
    public Text descripcion;
    public Text respuesta;
    public GameObject panel;
    public GameObject thisPanel;
    public Text p1;
    public Text p2;
    string csrfToken = "";

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
                Debug.Log(www.downloadHandler.text);
                string response = www.downloadHandler.text;
                json = JSON.Parse(response);

                ProcessTopScores(json);

                //int parsedInt = 0;
                //int.TryParse(id, out parsedInt);
                //PlayerPrefs.SetInt("id_sala", parsedInt); //se guarda el id del nuevo usuario registrado.
                //Debug.Log("El id de la sala es ingresado es   " + id);
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
        nombre.text = json[0]["nombre"];
        descripcion.text = json[0]["descripcion"];
        p1.text = json[0]["p1"];
        p2.text = json[0]["p2"];
        descripcion.text = json[0]["diagnostico"];
        panel.SetActive(true);
        thisPanel.SetActive(false);
        //nameText2.text = json[1]["name"];
        //nameText3.text = json[2]["name"];
        //scoreText1.text = json[0]["puntaje"].AsInt.ToString();
        //scoreText2.text = json[1]["puntaje"].AsInt.ToString();
        //scoreText3.text = json[2]["puntaje"].AsInt.ToString();
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
