using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataqueEnemigoCuerpoACuerpo : MonoBehaviour
{
    public GameObject personaje;

    private Animator anim;
    private int ataque;

    void Start()
    {
        anim = personaje.GetComponent<Animator>();
        ataque = personaje.GetComponent<Enemigo>().ataque;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        print(col.tag);
        if (anim.GetBool("atacando") && col.tag == "Player")
        {
            print("atacando");
            anim.SetBool("atacando", false);
            col.SendMessage("atacado", ataque);
        }
        
    }
}

