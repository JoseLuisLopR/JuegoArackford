using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPie : MonoBehaviour
{
    private AudioSource sonido;
    void Start()
    {
        sonido = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col) {
        if (col.name == "Terrain") {
            sonido.Play();
        }
    }
}
