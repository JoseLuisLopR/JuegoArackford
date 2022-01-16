using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBarraVida : MonoBehaviour
{
    private ControlPlayer player;
    public GameObject barraVida;
    private RectTransform bvFondo;
    private RectTransform bvVida;
    private int maxSize = 400;

    void Start()
    {
        player = GetComponent<ControlPlayer>();
        bvFondo = barraVida.GetComponent<RectTransform>();
        bvVida = GetBvVida();
        

    }

    // Update is called once per frame
    void Update()
    {
        calcularSizeFondo();
        //bvVida.sizeDelta = new Vector2(200, bvVida.sizeDelta.y);
    }

    private void calcularSizeFondo() {
        int vidaMaxTotal = player.GetLvVida()[player.GetLvVida().Length - 1];
        int vidaMaxActual = player.GetLvVida()[player.nivel - 1];
        int vidaActual = player.GetVidaActual();
        bvFondo.sizeDelta = new Vector2(maxSize * vidaMaxActual / vidaMaxTotal, bvFondo.sizeDelta.y);
        bvVida.sizeDelta = new Vector2(maxSize * vidaActual / vidaMaxTotal, bvVida.sizeDelta.y);


    }

    private void vidaActual() {


    }

    private RectTransform GetBvVida() {
        foreach (RectTransform rtr in bvFondo.GetComponentsInChildren<RectTransform>()) {
            if (rtr.name == "Vida") {
                return rtr;
            }
        }
        return null;
    }
}
