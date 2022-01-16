using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hechizo : MonoBehaviour
{
    public int tiempoEspera = 20;
    public int duracion = 10;
    public int codHechizo;

    private bool cargado = true;
    private DateTime ultimoUso;

    private Image imagenEspera;
    private Text txtTiempoEspera;



    void Start()
    {
        imagenEspera = GetComponentInChildren<Image>();
        txtTiempoEspera = GetComponentInChildren<Text>();
        ultimoUso = DateTime.Now.AddSeconds(-tiempoEspera);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TiempoEspera() {
        if ((DateTime.Now - ultimoUso).Milliseconds / 1000 < tiempoEspera)
        {
            imagenEspera.fillAmount = tiempoEspera - ((DateTime.Now - ultimoUso).Milliseconds / 1000) / tiempoEspera;
            txtTiempoEspera.text = (tiempoEspera - ((DateTime.Now - ultimoUso).Seconds)).ToString();
        }
        else {
            cargado = true;
            txtTiempoEspera.text = "";
            imagenEspera.fillAmount = 0;
        }
    }

    public void UtilizarHechizo() {
        cargado = false;
        ultimoUso = DateTime.Now;
    }

    public bool IsCargado() { return cargado; }
}
