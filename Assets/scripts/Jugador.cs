using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    public class Jugador : MonoBehaviour
    {
        public int idJudador { get; private set; }
        public int nivel { get; private set; }
        public int exp { get; private set; }
        public int vida { get; private set; }
        public string posicion { get; private set; }
        public string rotacion { get; private set; }

        public Jugador(int idJudador, int nivel, int exp, int vida, string posicion, string rotacion)
        {
            this.idJudador = idJudador;
            this.nivel = nivel;
            this.exp = exp;
            this.vida = vida;
            this.posicion = posicion;
            this.rotacion = rotacion;
        }
    }
}
