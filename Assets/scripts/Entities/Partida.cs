using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Entities
{
    [System.Serializable]
    public class Partida
    {
        public int idPartida { get; set; }
        public int fkUsuario { get; set; }
        public string tiempoJugado { get; set; }
        public string finalizada { get; set; }
        public int activo { get; set; }

        public Partida()
        {
        }

        public Partida(int idPartida, int fkUsuario, string tiempoJugado, string finalizada, int activo)
        {
            this.idPartida = idPartida;
            this.fkUsuario = fkUsuario;
            this.tiempoJugado = tiempoJugado;
            this.finalizada = finalizada;
            this.activo = activo;
        }
    }
}
