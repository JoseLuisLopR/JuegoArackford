using Assets.scripts.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controla los hechhizos y el boton de ataque

public class Hechizos : MonoBehaviour
{
    public Button botonataque;
    private Sprite imagenAtaqueBasico;
    // Hechizo sanacion *******
    public Button botonSanacion;
    private int tiempoEsperaSanacion = 20;
    private int duracionSanacion = 10;
    private bool sanacionCargada = true;
    private float tiempoSanacion;
    private Image sanacionImagenEspera;
    private Text sanacionTxtTiempoEspera;
    //*******

    // Condigos de identifacación *****
    private int ataqueBasico = 0;
    private int sanacion = 1;
    //*******

    private Animator anim;
    private int modoActual ;
    private DateTime fecha;
    private ControlPlayer controlPlayer;
    public GameObject efectoSanacion;

    void Start()
    {
        fecha = DateTime.Now;
        imagenAtaqueBasico = botonataque.GetComponent<Image>().sprite;
        sanacionImagenEspera = Utilities.GetGameObjectByNameInArray(botonSanacion.GetComponentsInChildren<Image>(),"porcentTiempo");
        sanacionTxtTiempoEspera = botonSanacion.GetComponentInChildren<Text>();
        controlPlayer = GetComponent<ControlPlayer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        TiempoEsperaSanacion();
        if (tiempoSanacion >= 0) {
            tiempoSanacion -= Time.deltaTime;
        }
        tiempoAtaque();
    }

    //Atacar
    public void ataque()
    {
        print(modoActual);
        if (!anim.GetBool("atacando") && modoActual == ataqueBasico)
        {
            
            anim.SetTrigger("atacar");
        }
        else if (modoActual == sanacion && sanacionCargada) {
            UtilizarHechizoSanacion();
        }
    }

    //Disminuye el tiempo de ataque hasta 0
    private void tiempoAtaque()
    {
        if (anim.GetFloat("tiempoAtaque") > 0)
        {
            anim.SetFloat("tiempoAtaque", anim.GetFloat("tiempoAtaque") - Time.deltaTime);
            if (anim.GetFloat("tiempoAtaque") < 0)
            {
                anim.SetFloat("tiempoAtaque", 0);
                anim.SetInteger("ataque", 1);
            }
        }
    }


    //Cambia la función del botón de ataque a sanacion o viceversa
    public void Sanacion()
    {
        if (sanacionCargada) { 
            if (modoActual != sanacion)
            {
                botonataque.GetComponent<Image>().sprite = botonSanacion.GetComponent<Image>().sprite;
                modoActual = sanacion;
            }
            else
            {
                botonataque.GetComponent<Image>().sprite = imagenAtaqueBasico;
                modoActual = ataqueBasico;
            }
        }
    }

    //Comprueba y controla el tiempo de sanacion para que no se pueda utilizar durante un tiempo
    private void TiempoEsperaSanacion()
    {
        if (tiempoSanacion >= 0)
        {
            //print((DateTime.Now - ultimoUsoSanacion).Milliseconds / 1000);
            sanacionImagenEspera.fillAmount = tiempoSanacion / tiempoEsperaSanacion;
            sanacionTxtTiempoEspera.text = Math.Ceiling(tiempoSanacion).ToString();
        }
        else
        {
            sanacionCargada = true;
            sanacionTxtTiempoEspera.text = "";
            sanacionImagenEspera.fillAmount = 0;
        }
    }

    //utiliza el hechizo de sanacion , pone el boton de a ataque a ataque basico, cura un 45% de la vida , y añade un sistema de particulas
    private void UtilizarHechizoSanacion()
    {
        sanacionCargada = false;
        Instantiate(efectoSanacion, gameObject.GetComponentInParent<Transform>().position, Quaternion.identity);
        controlPlayer.RecibirVida(Convert.ToInt32((controlPlayer.GetLvVida()[controlPlayer.nivel-1])*0.45));
        tiempoSanacion = tiempoEsperaSanacion;
        botonataque.GetComponent<Image>().sprite = imagenAtaqueBasico;
        modoActual = ataqueBasico;
    }
}
