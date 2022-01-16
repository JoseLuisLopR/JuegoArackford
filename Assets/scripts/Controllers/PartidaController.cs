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
    class PartidaController
    {
        //Código que devuelve en caso de error
        public static string ERROR = "ERR";

        public IEnumerator GetPartidaUsuario(Usuario usu, Action<string> accion)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            form.AddField("password", usu.password);


            UnityWebRequest www = UnityWebRequest.Post(Conexiones.PartidaUsuario, form);
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

        public IEnumerator Update(Usuario usu, Partida partida)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            form.AddField("password", usu.password);
            form.AddField("tiempoJugado", partida.tiempoJugado);
            form.AddField("finalizada", partida.finalizada);

            UnityWebRequest www = UnityWebRequest.Post(Conexiones.PartidaUpdate, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //accion(ERROR);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Devuelve el resultado a la función
                //accion(www.downloadHandler.text);
            }
        }
    }
}
