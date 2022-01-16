using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public int vidaMax;
    public int ataque;
    public float radioVision;
    public float radioAtaque;
    public float speed;
    public float speedAtack;


    private int vidaActual;
    private GameObject player;
    private Vector3 posicionInicial;
    private Animator anim;
    private Rigidbody rg;
    private NavMeshAgent naveg;
    private float tiempo;
    public GameObject personaje;
    private bool golpeado = false;
    private bool enCombate = false;
    private bool muerto = false;

    private float tiempoDeMuerte;
    
    // Start is called before the first frame update
    void Start()
    {
        naveg = GetComponent<NavMeshAgent>();
        naveg.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
        
        anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        vidaActual = vidaMax;
        
    }

    // Update is called once per frame
    void Update()
    {
        print("hola");
        if (!muerto && !anim.GetBool("atacando"))
        {
            //if (!player.GetComponent<Animator>().GetBool("atacando") && golpeado) { golpeado = false; }
            tiempo = tiempo + Time.deltaTime;
            Vector3 posPerseg = posicionInicial;
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < radioAtaque)
            {
                enCombate = true;
                mirarAlJugador();
                naveg.destination = transform.position;
                anim.SetBool("corriendo", false);
                if (tiempo  > speedAtack)
                {
                    print("enemigo atacando");
                    tiempo = 0;
                    anim.SetTrigger("atacar");
                }
            }
            else if (dist < radioVision)
            {
                if (!anim.GetBool("daniandose"))
                {
                    enCombate = true;
                    mirarAlJugador();
                    posPerseg = player.transform.position;
                    anim.SetBool("corriendo", true);
                    naveg.destination = posPerseg;
                }
            }
            else
            {
                enCombate = false;                
                naveg.destination = posicionInicial;
                if (Vector3.Distance(transform.position, posicionInicial) < 0.5)
                {
                    transform.position = posicionInicial;
                    anim.SetBool("corriendo", false);
                }
                else {
                    anim.SetBool("corriendo", true);
                    print("debería estar corriendo");
                }
            }
        }

        }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.8f, 0), radioVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.8f, 0), radioAtaque);
    }
    void atacado(int danio) {
        if (vidaActual > 0)
        {
            vidaActual -= danio;
            print(vidaActual);
            //golpeado = true;
            if (!anim.GetBool("atacando") && !anim.GetBool("daniandose") && !(tiempo > speedAtack)) { anim.SetTrigger("daniar"); }
        }
        else { morir(); }

        } 
    void morir()
    {
       if (vidaActual<=0 && !muerto)
        {
            muerto = true;
            enCombate = false;
            anim.SetTrigger("morir");
            //int experienciaMin = 
            player.GetComponent<ControlPlayer>().RecibirExp(System.Convert.ToInt32((ataque+vidaMax+speedAtack)*20/143.1));
        }
    }

    public void reiniciarEnemigo() {
        vidaActual = vidaMax;
        if (!this.gameObject.activeInHierarchy)
        {
            transform.position = posicionInicial;
            muerto = false;
            this.gameObject.SetActive(true);
        }

    }

    public bool isEnCombate() { return enCombate; }
    public bool isHerido() { return vidaActual < vidaMax ? true : false; }
    public void CalcularTiempoMuerte() {
        posicionInicial = transform.position;
        tiempoDeMuerte = (float)((vidaMax + ataque + speedAtack) * 5 / 143.1);
    }

    private void mirarAlJugador() {
        Vector3 direcion =  (player.GetComponent<Transform>().position - transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(direcion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 2f * Time.deltaTime);
    }

    public float GetTiempoDeMuerte() { return tiempoDeMuerte; }

}
