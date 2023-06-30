using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pausa : MonoBehaviour
{
    public GameObject panelPausa;
    // Start is called before the first frame update
    void Start()
    {
        panelPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivarPanelPausa(){
        panelPausa.SetActive(true);
        Pause();
    }

    public void DesactivarPanelPausa()
    {
        panelPausa.SetActive(false);
        NoPause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void NoPause()
    {
        Time.timeScale = 1f;
    }

}
