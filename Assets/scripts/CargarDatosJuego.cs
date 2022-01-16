using Assets.scripts.Constantes;
using Assets.scripts.Controllers;
using Assets.scripts.Entities;
using Assets.scripts.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CargarDatosJuego : MonoBehaviour
{
    public GameObject juego;
    public ControlPlayer player;
    public GameObject zonasEnemigas;
    public Canvas canvasCatga;
    public Main main;
    private ControlZonaEnemiga[] zonas;

    private bool[] cargados = { false,false, false, false, false };
    private Usuario usuario;
    private ZonaEnemiga[] zonasEneClass;
    void Start()
    {
        usuario = JsonConvert.DeserializeObject<Usuario>(File.ReadAllText(RutasArchivos.LoginUsuario));
        zonas = zonasEnemigas.GetComponentsInChildren<ControlZonaEnemiga>(true);
        JugadorController jugadorController = new JugadorController();
        StartCoroutine(jugadorController.GetJugadorUsuario(usuario, AccionGetJugador));
        CheckpointController checkpointController = new CheckpointController();
        StartCoroutine(checkpointController.GetCheckpointUsuario(usuario, AccionGetCheckpoint));
        ZonaEnemigaController zonaEnemigaController = new ZonaEnemigaController();
        StartCoroutine(zonaEnemigaController.GetAllZonas(AccionGetZonasEnemigas));
        PartidaController partidaController = new PartidaController();
        StartCoroutine(partidaController.GetPartidaUsuario(usuario, AccionGetPartida));
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCargado()) {
            juego.gameObject.SetActive(true);
            Destroy(canvasCatga.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void AccionGetJugador(string result) {
        if (result != JugadorController.ERROR)
        {
            Jugador jugador = JsonConvert.DeserializeObject<Jugador>(result);
            player.nivel = jugador.nivel;
            player.SetExpActual(jugador.exp);
            player.SetVidaActual(jugador.vida);
            cargados[0] = true;
        }
        else
        {
            print("ERROR");
        }
    }

    private void AccionGetCheckpoint(string result)
    {
        if (result != JugadorController.ERROR)
        {
            print(result);
            Checkpoint checkpoint = JsonConvert.DeserializeObject<Checkpoint>(result);
            string[] posicion = checkpoint.posicion.Split(' ');
            player.GetComponent<Transform>().localPosition = new Vector3(float.Parse(posicion[0]), float.Parse(posicion[1]), float.Parse(posicion[2]));
            cargados[1] = true;
        }
        else
        {
            print("ERROR");
        }
    }


    private void AccionGetZonasEnemigas(string result)
    {
        if (result != ZonaEnemigaController.ERROR)
        {
            zonasEneClass = JsonConvert.DeserializeObject<ZonaEnemiga[]>(result);
            ZonaEnemigaPartidaController zonaEnemigaPartidaController = new ZonaEnemigaPartidaController();
            StartCoroutine(zonaEnemigaPartidaController.GetZonasEnemigasPartidUsuario(usuario, AccionGetZonasEnemigasPartida));
            cargados[2] = true;
        }
        else
        {
            print("ERROR");
        }
    }

    private void AccionGetZonasEnemigasPartida(string result)
    {
        if (result != ZonaEnemigaPartidaController.ERROR)
        {
            ZonaEnemigaPartida[] zonasEnePar = JsonConvert.DeserializeObject<ZonaEnemigaPartida[]>(result);
            foreach (ZonaEnemiga z in zonasEneClass)
            {
                ControlZonaEnemiga controlZonaEnemiga = Utilities.GetGameObjectByNameInArray(zonasEnemigas.GetComponentsInChildren<ControlZonaEnemiga>(true), z.nombre);
                ZonaEnemigaPartida zonaEnePar = GetZonaEnemigaPartidaById(zonasEnePar, z.idZona);
                controlZonaEnemiga.nivel = zonaEnePar.nivel;
                controlZonaEnemiga.SetUltDerrota(DateTime.Parse(zonaEnePar.fechaDerrota));
                controlZonaEnemiga.derrotada = zonaEnePar.completada == 1 ? true : false;

            }
            cargados[3] = true;
        }
        else
        {
            print("ERROR");
        }
    }

    private void AccionGetPartida(string result) {
        if (result != PartidaController.ERROR)
        {
            print(result);
            main.partida = JsonConvert.DeserializeObject<Partida>(result);
            cargados[4] = true;
        }
    }

    private ZonaEnemigaPartida GetZonaEnemigaPartidaById(ZonaEnemigaPartida[] zonasEne, int id) {
        ZonaEnemigaPartida zona = null;
        foreach (ZonaEnemigaPartida z in zonasEne) {
            if (z.fkZona == id) { zona = z; }
        }
        return zona;
    }

    private bool IsCargado()
    {
        bool cargado = true;
        foreach (bool c in cargados)
        {
            if (!c) { cargado = c; }
        }
        return cargado;
    }
}
