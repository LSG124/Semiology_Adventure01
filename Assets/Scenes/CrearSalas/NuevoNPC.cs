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

    public Text nombre;
    public Text diagnostico;
    public Text descripcion;
    public Text p1;
    public Text r1;
    public Text p2;
    public Text r2;
    public Text p3;
    public Text r3;


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
        int body_1 = PlayerPrefs.GetInt("body_1");
        int body_2 = PlayerPrefs.GetInt("body_2");
        int body_3 = PlayerPrefs.GetInt("body_3");
        int id_admin = PlayerPrefs.GetInt("id_admin");
        Dictionary<string, string> wwwForm = new Dictionary<string, string>();
        wwwForm.Add("_token", csrfToken);
        wwwForm.Add("sala_id", "4");
        wwwForm.Add("diagnostico", diagnostico.text);
        wwwForm.Add("nombre", nombre.text);
        wwwForm.Add("body1", body_1.ToString());
        wwwForm.Add("body2", body_2.ToString());
        wwwForm.Add("body3", body_3.ToString());
        wwwForm.Add("ubicacion", PlayerPrefs.GetString("espacio"));
        wwwForm.Add("descripcion", descripcion.text);
        wwwForm.Add("p1", p1.text);
        wwwForm.Add("p2", p2.text);
        wwwForm.Add("p3", p3.text);
        wwwForm.Add("r1", r1.text);
        wwwForm.Add("r2", r2.text);
        wwwForm.Add("r3", r3.text);
        wwwForm.Add("p4", p1.text);
        wwwForm.Add("p5", p2.text);
        wwwForm.Add("p6", p3.text);
        wwwForm.Add("r4", r1.text);
        wwwForm.Add("r5", r2.text);
        wwwForm.Add("r6", r3.text);

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
