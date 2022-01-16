using Assets.scripts.Constantes;
using Assets.scripts.Controllers;
using Assets.scripts.Entities;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CargaDatosMenu : MonoBehaviour
{
    public Canvas cvCargando;
    public Canvas cvPricipal;
    public Text txUsuario;
    public Text txTiempoJugado;

    private bool[] cargados = {false,false};
    private Usuario usuario;

    void Start()
    {
        usuario = JsonConvert.DeserializeObject<Usuario>(File.ReadAllText(RutasArchivos.LoginUsuario));
        CargarDatos();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCargado())
        {
            cvPricipal.gameObject.SetActive(true);
            Destroy(cvCargando.gameObject);
            Destroy(this.gameObject);
        }
        
    }

    private void CargarDatos() {
        CargarNombreUsu();
        CargarTiempoJugado();
    }

    private void CargarNombreUsu() {
        txUsuario.text = usuario.login;
        cargados[0] = true;
    }

    private void CargarTiempoJugado() {
        PartidaController partidaControl = new PartidaController();
        StartCoroutine(partidaControl.GetPartidaUsuario(usuario, AccionCargarTiempoJugado));
    }

    private void AccionCargarTiempoJugado(string result) {
        if (result != PartidaController.ERROR)
        {
            Partida partida = JsonConvert.DeserializeObject<Partida>(result);
            txTiempoJugado.text = "Tiempo jugado: " + partida.tiempoJugado + " h";
            cargados[1] = true;
        }
        else {
            print("ERROR");
        }
    }

    private bool IsCargado()
    {
        bool cargado = true;
        foreach (bool c in cargados) {
            if (!c) { cargado = c; }
        }
        return cargado;
    }

}
