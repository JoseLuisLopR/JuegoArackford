using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlPlayer : MonoBehaviour
{
    //Valores de vida y experiencia por nivel
    private int[] lvVida = { 500, 750, 1125 ,1687};
    private int[] lvExp = { 2000, 5000, 8000,0 };

    //Avates por nivel para que cambien
    public Avatar[] avatares;

    public int nivel;
    private int nivelAnt;
    private int nivelMax = 4;

    private int vidaActual;
    private int expActual;

    public GameObject joystick;
    public float velAndar;
    public float velCorrer;
    public float velSprintar;
    private FloatingJoystick fj;
    private Transform tr;
    private Rigidbody rb;
    private Animator anim;
    private float gradAnt = 0;
    
    public FixedTouchField touchField;
    public FixedButton botonAtaque;
    protected float CameraAngle;
    protected float CameraAngleSpeed = 0.3f;


    void Start()
    {
        nivelAnt = nivel;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        fj = joystick.GetComponent<FloatingJoystick>();
        aplicarNivel();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Controla que la vida no sea mayor
        ControlVida();

        //Mientras esta atacando o siendo golpeado no puede moverse ni rotar
        if (!anim.GetBool("atacando") && !anim.GetBool("daniandose"))
        {
            rotar();
            movimiento();
        }
        rotacionCamara();
        //ataque();
        //tiempoAtaque();
        cambioDeNivel();
        rb.AddForce(new Vector3(0,-10,0));
    }
    //Rota en el eje y al jugador
    private void rotar() {       
        tr.eulerAngles = new Vector3(0, calcularRotacion(), 0);
    }


    //calcula los grados del joystick
    private float calcularRotacion() {
        float grados = 0;
        float x = fj.Horizontal;
        float y = fj.Vertical;
        if (x != 0 && y != 0)
        {
            double cos = (1 * (y)) / (Math.Sqrt(Math.Pow(1, 2)) * Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
            float grad = (float)(Math.Acos(cos) * (180 / Math.PI));
            if (x < 0)
            {
                grad = 180 + (180 - grad);
            }
            grad += Camera.main.transform.eulerAngles.y;
            grados = tr.eulerAngles.y + (grad - gradAnt) ;
            gradAnt = grad ;
        }
        else {
            grados = gradAnt; }

        return grados;
    }

    //Calculad la distacia de separacion al mover el joystick
    private float desplazamientoJoystick() {
        float x = fj.Horizontal;
        float y = fj.Vertical;
        return (float)Math.Sqrt(Math.Pow((x), 2) + Math.Pow((y), 2));
    }

    //Mueve al personaje a distintas velocidades
    private void movimiento() {
        //if (!anim.GetBool("atacando"))
        {
            float movim = desplazamientoJoystick();
            float velocidad = 0;
            anim.SetFloat("movimiento", movim);
            if (movim > 0 && movim <= (0.33))
            {
                velocidad = velAndar;
                anim.SetFloat("movimiento", 0.1f);
            }
            else if (movim > 0 && movim <= (0.66))
            {
                velocidad = velCorrer;
            }
            else if (movim > 0 && movim <= 1)
            {
                velocidad = velSprintar;
            }
            rb.velocity = tr.forward * velocidad;
        }
    }

    //La camara persigue al jugador 
    private void rotacionCamara() {

        CameraAngle += touchField.TouchDist.x * CameraAngleSpeed;

        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * new Vector3(0, 3, 4);
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
    }

    //Recibe un ataque
    public void atacado(int ataque)
    {
        vidaActual -= ataque;
        Handheld.Vibrate();
        if (vidaActual <= 0)
        {
            anim.SetTrigger("morir");
        }
        else
        {

            if (anim.GetInteger("ataque") != 5 && anim.GetInteger("ataque") != 4)
            {
                anim.SetTrigger("daniar");
                anim.SetInteger("ataque", 1);
            }
        }
    }

    //Ajusta los valores del player al nivel correspondiente
    public void aplicarNivel() {
        
        anim.avatar = avatares[nivel - 1];
        Transform[] niveles = this.gameObject.GetComponentsInChildren<Transform>(true);
        print(niveles.Length);
        foreach (Transform lv in niveles) {
            if (lv.gameObject.name != nivel.ToString() && lv.gameObject.tag == "lv_player")
            {
                lv.gameObject.SetActive(false);
            }
            else if(lv.tag=="lv_player") {
                lv.gameObject.SetActive(true);
            }
        }
    }

    //Cambia de nivel
    private void cambioDeNivel() {
       if(nivelAnt != nivel)
        {
            nivelAnt = nivel;
            aplicarNivel();
        }
    }
    //Recibe experiencia y sube de nivel si es necesario
    public void RecibirExp(int exp) {
        
        if (nivel != nivelMax) {
            expActual += exp;
            if (expActual >= lvExp[nivel - 1])
            {
                nivel++;
                if (nivel != nivelMax)
                {
                    expActual = expActual - lvExp[nivel - 2];
                }
            }
        }
    }

    //recibe vida y se aseguara que no puede ser mayor a la capacidad maxima
    public void RecibirVida(int vida)
    {
        vidaActual += vida;
        if (vidaActual > lvVida[nivel-1])
        {
            vidaActual = lvVida[nivel - 1];
        }
    }


    private void ControlVida() {
        if (vidaActual == lvVida[nivel - 1]) {
            vidaActual = lvVida[nivel - 1];
        }
    }

    //Funciones Getters y Setters

    public int[] GetLvVida() {
        return lvVida;
    }

    public int[] GetLvExp()
    {
        return lvExp;
    }

    public int GetVidaActual() {
        return vidaActual;
    }
    public void SetVidaActual(int vida)
    {
        vidaActual=vida;
    }

    public int GetExpActual() {
        return expActual;
    }
    public void SetExpActual(int exp)
    {
        expActual=exp;
    }
    public int GetNivelMaxl()
    {
        return nivelMax;
    }
}

