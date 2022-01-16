using Assets.scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public Partida partida;
    public float tiempoPartida = 0;
    public GameObject zonasEnemigas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempoPartida += Time.deltaTime;
    }

    public bool isZonasCompletadas() {
        bool completadas = true;
        foreach (ControlZonaEnemiga cz in zonasEnemigas.GetComponentsInChildren<ControlZonaEnemiga>(true)) {
            completadas = cz.derrotada ? completadas : false;
        }
        return completadas;
    }

    public void SetPartida(Partida p) {
        partida = p;
    }
}
