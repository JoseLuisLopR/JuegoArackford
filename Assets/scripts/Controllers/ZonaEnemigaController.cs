using Assets.scripts.Constantes;
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
    class ZonaEnemigaController
    {
        //Código que devuelve en caso de error
        public static string ERROR = "ERR";

        public IEnumerator GetAllZonas(Action<string> accion)
        {
            WWWForm form = new WWWForm();

            UnityWebRequest www = UnityWebRequest.Post(Conexiones.ZonaEnemigaTodas, form);
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
    }
}
