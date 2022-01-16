using Assets.scripts.Constantes;
using Assets.scripts.Controllers;
using Assets.scripts.Entities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GuardarPartida : MonoBehaviour
{

    public GameObject zonasEnemigas;
    public ControlPlayer ControlPlayer;
    public Main main;
    public GameObject particulas;


    private bool partidaGuardada = false;
    private Usuario usuario;
    void Start()
    {
        usuario = JsonConvert.DeserializeObject<Usuario>(File.ReadAllText(RutasArchivos.LoginUsuario));

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (!partidaGuardada && col.tag=="Player") {
            Instantiate(particulas,gameObject.GetComponentInParent<Transform>().position, Quaternion.identity);
            partidaGuardada = true;
            Guardarjugador();
            GuardarZonasEnemigas();
            GuardarPartidaUsu();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            partidaGuardada = false;
        }
    }

    private void Guardarjugador()
    {
        Jugador j = new Jugador();
        j.vida = ControlPlayer.GetVidaActual();
        j.exp = ControlPlayer.GetExpActual();
        j.nivel = ControlPlayer.nivel;
        JugadorController jugadorController = new JugadorController();
        StartCoroutine(jugadorController.Update(usuario, j, gameObject.GetComponentInParent<Transform>().gameObject.name));
    }

    private void GuardarZonasEnemigas()
    {
        ZonaEnemigaPartidaController zonaEnemigaPartidaController = new ZonaEnemigaPartidaController();
        foreach (ControlZonaEnemiga z in zonasEnemigas.GetComponentsInChildren<ControlZonaEnemiga>(true))
        {
            ZonaEnemigaPartida ze = new ZonaEnemigaPartida();
            ze.fechaDerrota = z.GetUltDerrota().ToString("yyyy-MM-dd HH:mm:ss");
            ze.nivel = z.nivel;
            ze.completada = z.derrotada ? 1 : 0;
            StartCoroutine(zonaEnemigaPartidaController.Update(usuario, ze, z.gameObject.name));
        }
    }

    private void GuardarPartidaUsu()
    {
        PartidaController partidaController = new PartidaController();
        Partida p = new Partida();
        p.tiempoJugado = SumaTiempoJugado(main.tiempoPartida, main.partida.tiempoJugado);
        p.finalizada = main.isZonasCompletadas()&&p.finalizada==""?p.tiempoJugado:"";
        
        print("Tiempo jugado "+p.tiempoJugado);
        StartCoroutine(partidaController.Update(usuario, p));
    }

    private string SumaTiempoJugado(float tiempoActual, string tiempoGuardado) {
        string[] tSeparado = tiempoGuardado.Split(':');
        int segundosTGuardado = 0;
        segundosTGuardado += Convert.ToInt32(tSeparado[0]) * 3600;
        segundosTGuardado += Convert.ToInt32(tSeparado[1]) * 60;
        segundosTGuardado += Convert.ToInt32(tSeparado[2]);
        int segundosTotales = Convert.ToInt32(Math.Truncate(tiempoActual).ToString()) + segundosTGuardado;
        int horas = (segundosTotales / 3600);
        int minutos = ((segundosTotales - horas * 3600) / 60);
        int segundos = segundosTotales - (horas * 3600 + minutos * 60);
        return horas.ToString() + ":" + minutos.ToString() + ":" + segundos.ToString();

    }

}
