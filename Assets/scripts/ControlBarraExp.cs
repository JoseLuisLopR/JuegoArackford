using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.Utilities;

public class ControlBarraExp : MonoBehaviour
{
    private ControlPlayer player;
    public GameObject barraExp;
    private Image porcentExp;
    private Text texto;

    void Start()
    {
        player = GetComponent<ControlPlayer>();
        texto = barraExp.GetComponentInChildren<Text>();
        porcentExp = Utilities.GetGameObjectByNameInArray(barraExp.GetComponentsInChildren<Image>(), "porcentaje");
    }

    // Update is called once per frame
    void Update()
    {
        TextoNivel();
        PocentajeEsxperiencia();
    }

    public void TextoNivel() {
        if (player.nivel == 4)
        {
            texto.text = "Nivel"+ System.Environment.NewLine + "MAX";
        }
        else {
            texto.text = "Nivel" + System.Environment.NewLine + player.nivel;
        }
    }

    public void PocentajeEsxperiencia() {
        porcentExp.fillAmount = (float)player.GetExpActual()  / (float)player.GetLvExp()[player.nivel - 1];
    }
}
