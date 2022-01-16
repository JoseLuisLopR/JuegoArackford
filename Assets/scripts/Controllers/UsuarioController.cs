using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using Assets.scripts.Constantes;
using Assets.scripts.Entities;

namespace Assets.scripts.Controllers
{
    class UsuarioController
    {
        //Código que devuelve en caso de error
        public static string ERROR = "ERR";

        public IEnumerator IniciarSesion(Usuario usu,Action<string> accion,bool autoinicio=false)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            if (autoinicio)
            {
                form.AddField("password", usu.password);
            }
            else
            {
                form.AddField("password", MD5(usu.password));
            }
            
            
            UnityWebRequest www = UnityWebRequest.Post(Conexiones.UsuarioLogin, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Çódigo de error de conexión
                accion(ERROR);
            }
            else
            {
                //Devuelve el resultado a la función
                accion(www.downloadHandler.text);
            }
        }

        public IEnumerator Registrarse(Usuario usu, Action<string> accion)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            form.AddField("password", MD5(usu.password));
            form.AddField("nombre", usu.nombre);
            form.AddField("apellidos", usu.apellidos);
            form.AddField("email", usu.email);


            UnityWebRequest www = UnityWebRequest.Post(Conexiones.UsuarioRegistro, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Çódigo de error de conexión
                accion(ERROR);
            }
            else
            {
                //Devuelve el resultado a la función
                accion(www.downloadHandler.text);
            }
        }



        public string MD5(string str) {
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(str);
            byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
