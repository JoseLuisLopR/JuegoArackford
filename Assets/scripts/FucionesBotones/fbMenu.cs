using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fbMenu : MonoBehaviour
{
    public void Jugar(Canvas canvasCarga) {
        UnityEngine.Object.Instantiate(canvasCarga.gameObject);
        SceneManager.LoadScene(2);
    }

    public void AbrirVentana(Canvas c) {
        Object.Instantiate(c.gameObject);
    }

    public void CerrarVentana(Canvas c)
    {
        Object.Destroy(c.gameObject);
    }

    public void CerrarSesion() {
        if (File.Exists(Application.persistentDataPath + "/usuario.txt"))
        {
            File.Delete(Application.persistentDataPath + "/usuario.txt");
        }
        SceneManager.LoadScene(0);
    }
}
