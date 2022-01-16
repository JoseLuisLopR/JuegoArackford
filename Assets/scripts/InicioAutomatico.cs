using Assets;
using Assets.scripts.Constantes;
using Assets.scripts.Controllers;
using Assets.scripts.Entities;
using Assets.scripts.Utilities;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioAutomatico : MonoBehaviour
{
    public GameObject menu;
    public Canvas cvCarga;

    void Start()
    {
        Autoinicio();
    }


    private void Autoinicio() {
        //Si existe el archivo donde se guarda el login intenta iniciar sesión
        if (File.Exists(RutasArchivos.LoginUsuario))
        {
            string json =File.ReadAllText(RutasArchivos.LoginUsuario);
            Usuario usu = JsonConvert.DeserializeObject<Usuario>(json);
            UsuarioController usuControl = new UsuarioController();
            StartCoroutine(usuControl.IniciarSesion(usu, AccionInicioSesion,true));
        }
        //Pone visible la ventana de registro e inicio de sesion manual
        else
        {
            menu.SetActive(true);
            Object.Destroy(cvCarga.gameObject);
        }
    }

    private void AccionInicioSesion(string a)
    {
        //Si ocurre algún fallo o es incorrecto el inicio de sesión 
        //pone visible la ventana de registro e inicio de sesion manual
        if (a == UsuarioController.ERROR || a == "0")
        {
            menu.SetActive(true);
            Object.Destroy(cvCarga.gameObject);
        }
        //Si los datos son correctos carga la escena de menu principal
        else
        {
            StartCoroutine(Utilities.Wait(1, 1));
        }


    }
}
