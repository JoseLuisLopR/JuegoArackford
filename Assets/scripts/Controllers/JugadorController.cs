using Assets.scripts.Constantes;
using Assets.scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.scripts.Controllers
{
    class JugadorController
    {
        //Código que devuelve en caso de error
        public static string ERROR = "ERR";

        public IEnumerator GetJugadorUsuario(Usuario usu, Action<string> accion)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            form.AddField("password", usu.password);


            UnityWebRequest www = UnityWebRequest.Post(Conexiones.JugadorUsuario, form);
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

        public IEnumerator Update(Usuario usu, Entities.Jugador jugador, string nombreCheckpoint )
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            form.AddField("password", usu.password);
            form.AddField("nombreZona", nombreCheckpoint);
            form.AddField("nivel", jugador.nivel);
            form.AddField("vida", jugador.vida);
            form.AddField("exp", jugador.exp);



            UnityWebRequest www = UnityWebRequest.Post(Conexiones.JugadorUpdate, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Çódigo de error de conexión
                //accion(ERROR);
                Debug.Log(www.error);
            }
            else
            {
                //Devuelve el resultado a la función
                //accion(www.downloadHandler.text);
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
