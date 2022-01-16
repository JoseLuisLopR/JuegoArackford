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
    class ZonaEnemigaPartidaController
    {
        //Código que devuelve en caso de error
        public static string ERROR = "ERR";

        public IEnumerator GetZonasEnemigasPartidUsuario(Usuario usu, Action<string> accion)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            form.AddField("password", usu.password);


            UnityWebRequest www = UnityWebRequest.Post(Conexiones.ZonasEnemigasPartidaUsuario, form);
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

        public IEnumerator Update(Usuario usu,ZonaEnemigaPartida zona,string nombreZona)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            form.AddField("password", usu.password);
            form.AddField("nombreZona", nombreZona);
            form.AddField("nivel", zona.nivel);
            form.AddField("fechaDerrota", zona.fechaDerrota);
            form.AddField("completada", zona.completada);



            UnityWebRequest www = UnityWebRequest.Post(Conexiones.ZonasEnemigasPartidaUdate, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Çódigo de error de conexión
                //accion(ERROR);
            }
            else
            {
                //Devuelve el resultado a la función
               // accion(www.downloadHandler.text);
            }
        }
    }
}
