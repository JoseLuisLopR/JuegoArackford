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
    class CheckpointController
    {
        //Código que devuelve en caso de error
        public static string ERROR = "ERR";

        public IEnumerator GetCheckpointUsuario(Usuario usu, Action<string> accion)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", usu.login);
            form.AddField("password", usu.password);


            UnityWebRequest www = UnityWebRequest.Post(Conexiones.CheckpointUsuario, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Çódigo de error de conexión
                Debug.Log(www.error+ " "+ Conexiones.CheckpointUsuario);
                accion(ERROR);
            }
            else
            {
                //Devuelve el resultado a la función
                accion(www.downloadHandler.text);
            }
        }
    }
}
