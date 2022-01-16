using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UsuarioController2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public string getText()
    {

        string a = "No ha funcionado";
        Action<string> accion = (html) => {
            a = html;
        };
        StartCoroutine(actualizarTexto(accion)); ;

        return a;
    }

    public IEnumerator actualizarTexto(Action<string> accion)
    {
        WWWForm form = new WWWForm();
        form.AddField("prueba", "");

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.39/Proyecto/prueba.php", form);


        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {

        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            accion(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;

        }
    }
}
