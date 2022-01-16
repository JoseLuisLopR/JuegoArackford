using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Entities
{
    [System.Serializable]
    public class Jugador
    {
        public int idJugador { get; set; }
        public int fkPartida { get; set; }
        public int nivel { get; set; }
        public int exp { get; set; }
        public int vida { get; set; }
        public int fkCheckpoint { get; set; }

        public Jugador()
        {
        }

        public Jugador(int idJugador, int fkPartida, int nivel, int exp, int vida, int fkCheckpoint)
        {
            this.idJugador = idJugador;
            this.fkPartida = fkPartida;
            this.nivel = nivel;
            this.exp = exp;
            this.vida = vida;
            this.fkCheckpoint = fkCheckpoint;
        }
    }
}
