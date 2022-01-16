using Assets.scripts.Constantes;
using Assets.scripts.Controllers;
using Assets.scripts.Entities;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.Utilities;

public class fbInicio : MonoBehaviour
{
    //Funciones que serán ejecutadas por los botones
    private Button btAccion;
    private Image info;
    private Usuario usuRegistro = new Usuario();
    public Canvas pantallaCarga;

    public void AbrirVentana(Canvas c) {
        UnityEngine.Object.Instantiate(c.gameObject);
    }

    public void CerrarVentana(Canvas c)
    {
        UnityEngine.Object.Destroy(c.gameObject);
    }


    public void IniciarSesion(Canvas c) {
        btAccion = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<Button>(), "bt_login");
        btAccion.interactable = false;
        info = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<Image>(true), "Info");
        InputField login = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<InputField>(), "login");
        InputField pass = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<InputField>(), "password");
        Usuario usu = new Usuario();
        usu.login = login.text;
        usu.password = pass.text;
        UsuarioController usuControl = new UsuarioController();
        StartCoroutine(usuControl.IniciarSesion(usu, AccionInicioSesion));
    }

    private void AccionInicioSesion(string result) {
        
        if (result == UsuarioController.ERROR)
        {
            ErrorEnAccion("Error de conexión");
        }
        else if (result == "0")
        {
            ErrorEnAccion("Datos incorrectos");
        }
        else
        {
            UnityEngine.Object.Instantiate(pantallaCarga.gameObject);
            File.WriteAllText(RutasArchivos.LoginUsuario, (result));
            StartCoroutine(Utilities.Wait(2,1));
            //SceneManager.LoadScene(1);
        }


    }

    public void resgistro(Canvas c) {
        btAccion = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<Button>(), "bt_registro");
        btAccion.interactable = false;
        info = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<Image>(true), "Info");
        info.gameObject.SetActive(false);
        string nombre = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<InputField>(), "nombre").text;
        string apellidos = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<InputField>(), "apellidos").text;
        string email = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<InputField>(), "email").text;
        string login = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<InputField>(), "usuario").text;
        string pass1 = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<InputField>(), "pass1").text;
        string pass2 = Utilities.GetGameObjectByNameInArray(c.GetComponentsInChildren<InputField>(), "pass2").text;


        if (nombre == "" || apellidos == "" || email == "" || login == "" || pass1 == "" || pass2 == "")
        {
            ErrorEnAccion("Faltan campos por rellenar");
        }
        else if (!Utilities.IsEmailValido(email))
        {
            ErrorEnAccion("Email no válido");
        }
        else if (pass1 != pass2)
        {
            ErrorEnAccion("Las contraseñas no coinciden");
        }
        else
        {
            usuRegistro.nombre = nombre;
            usuRegistro.apellidos = apellidos;
            usuRegistro.email = email;
            usuRegistro.login = login;
            usuRegistro.password = pass1;
            UsuarioController usuControl = new UsuarioController();
            print("LOGIN: " + usuRegistro.login);
            StartCoroutine(usuControl.Registrarse(usuRegistro,AccionRegistro));
        }

    }

    private void AccionRegistro(string result)
    {
        if (result == UsuarioController.ERROR)
        {
            ErrorEnAccion("Error de conexión");
        }
        else if (result == "0")
        {
            ErrorEnAccion("El usuario ya existe");
        }
        else
        {
            UsuarioController usuControl = new UsuarioController();
            StartCoroutine(usuControl.IniciarSesion(usuRegistro, AccionInicioSesion));
        }

    }




    private void ErrorEnAccion(string textError) {
        info.GetComponentInChildren<Text>().text = textError;
        info.gameObject.SetActive(true);
        btAccion.interactable = true;
    }

    
}
