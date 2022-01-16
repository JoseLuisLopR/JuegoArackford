using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    public Animator animPlayer;
    public int ataque;
    private AudioSource sonido;

    private int ataqueAnterior=-1;
    // Start is called before the first frame update
    void Start()
    {
        sonido = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        print(col.tag);
        if (animPlayer.GetBool("atacando") && col.tag == "Enemigo" && animPlayer.GetInteger("ataque")!=ataqueAnterior)
        {
            sonido.Play();
            ataqueAnterior = animPlayer.GetInteger("ataque");
            col.SendMessage("atacado", ataqueAnterior == 5?ataque+(ataque*0.20):ataque);
            
        }

    }
}

