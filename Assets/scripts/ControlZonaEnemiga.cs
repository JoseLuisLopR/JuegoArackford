using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlZonaEnemiga : MonoBehaviour
{
    private float[] ataqueNivel = { 0, 0.5f, 0.5f, 0.5f};
    private float[] vidaNivel = { 0, 0.5f, 0.5f, 0.5f};
    public int nivel = 1;
    public bool derrotada = false;
    private int maxNivel = 4;
    private Enemigo[] enemigos;

    private DateTime fUltDerrota ;
    private DateTime fUltEnAtaque;
    private DateTime fInicioAtaque;
    private bool enAtaque = false;
    private bool muertos = true;

    private float tiempoReaparicion = 5f;
    private float tiempoReavivar = 0.5f;

    private Text txtInfoLv;

    void Start()
    {

        enemigos = GetComponentsInChildren<Enemigo>(true);
        txtInfoLv = GetComponentInChildren<Text>();
        cargarNivelInicial();
        activacionInicial();
        CargarTextoInfo();
        print("Cantidad Enemigos " + enemigos.Length);

    }

    // Update is called once per frame
    void Update()
    {
        print(this.gameObject.name +fUltDerrota);
        controlZona();
        print(fInicioAtaque);

    }

    private void controlZona() {
        if (muertos && DateTime.Now.Subtract(fUltDerrota).TotalMinutes > tiempoReaparicion) {
            cargarNivel();
            reiniciarEnemigos();
            muertos = false;
        }

        if (isEnemigosMuertos() && !muertos)
        {
            fUltDerrota = DateTime.Now;
            //Actualizar fecha en la base de datos

            print("Tu tiempo: " + (DateTime.Now.Subtract(fInicioAtaque).TotalMilliseconds / 1000) + " Tiempo Max: " + GetTiempoMaxDerrota());
            if ((DateTime.Now.Subtract(fInicioAtaque).TotalMilliseconds / 1000) <= GetTiempoMaxDerrota() && nivel!=maxNivel) {
                nivel++;
            }
            CargarTextoInfo();
            muertos = true;
        }
        else if (!muertos) {
            if (isEnemigosEnCombate()) {
                if(fInicioAtaque == new DateTime())
                {
                    fInicioAtaque = DateTime.Now;
                }
                fUltEnAtaque = DateTime.Now;
                print("Enemigos en combate");
            }else if(isEnemigosHeridos() && DateTime.Now.Subtract(fUltEnAtaque).TotalMinutes > tiempoReavivar)
            {
                reiniciarEnemigos();
            }
        }
    }

    private void reiniciarEnemigos() {
        print("reiniciandoEnemigos");
        fInicioAtaque = new DateTime();
        foreach (Enemigo ene in enemigos) {           
            ene.reiniciarEnemigo();
        }
    }

    private bool isEnemigosMuertos() {
        bool muertos = true;
        foreach (Enemigo ene in enemigos) {
            if (ene.gameObject.activeInHierarchy) { muertos = false; }
        }
        return muertos;
    }

    private bool isEnemigosEnCombate() {
        bool enCombate = false;
        foreach (Enemigo ene in enemigos)
        {
            if (ene.isEnCombate()) { enCombate = true; }
        }
        return enCombate;
    }

    private bool isEnemigosHeridos() {
        bool heridos = false;
        foreach (Enemigo ene in enemigos)
        {
            if (ene.isHerido()) { heridos = true; }
        }
        return heridos;
    }

    private void cargarNivel()
    {
        print("cargando nivel");
        foreach (Enemigo ene in enemigos)
        {
            ene.vidaMax += (int)(ene.vidaMax * vidaNivel[nivel-1]);
            ene.ataque += (int)(ene.ataque * ataqueNivel[nivel-1]);
        }
        
    }

    private void cargarNivelInicial()
    {
        print("Cargando nivel inicial: " + nivel);
        for (int i = 0; i < nivel; i++) {
            foreach (Enemigo ene in enemigos)
            {
                ene.CalcularTiempoMuerte();
                ene.vidaMax += (int)(ene.vidaMax * vidaNivel[i]);
                ene.ataque += (int)(ene.ataque * ataqueNivel[i]);
            }

        }

    }
    private void CargarTextoInfo() {
        txtInfoLv.text = "Zona Nivel" + System.Environment.NewLine + (nivel == maxNivel ? "MAX" : nivel.ToString()) + (derrotada && nivel == maxNivel ? System.Environment.NewLine + "Derrotada" : "");
    }

    private void activacionInicial() {
        if (DateTime.Now.Subtract(fUltDerrota).TotalMinutes > tiempoReaparicion)
        {
            muertos = false;
            foreach (Enemigo ene in enemigos)
            {
                ene.gameObject.SetActive(true);
            }
        }
    }

    private float GetTiempoMaxDerrota() {
        float tiempo = 0;
        float segundosExtraPorEnemigo = 5;
        foreach (Enemigo ene in enemigos)
        {
            tiempo += ene.GetTiempoDeMuerte();
        }
        tiempo += enemigos.Length * segundosExtraPorEnemigo;
        return tiempo;
    }

    public void SetUltDerrota(DateTime fecha) {
        fUltDerrota = fecha;
    }

    public DateTime GetUltDerrota()
    {
        return fUltDerrota ;
    }

}
