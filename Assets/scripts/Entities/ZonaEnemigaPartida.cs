using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Entities
{
    [System.Serializable]
    public class ZonaEnemigaPartida
    {
        public int idZonaPartida { get; set; }
        public int fkZona { get; set; }
        public int fkPartidaa { get; set; }
        public int nivel { get; set; }
        public string fechaDerrota { get; set; }
        public int completada { get; set; }

        public ZonaEnemigaPartida()
        {
        }

        public ZonaEnemigaPartida(int idZonaPartida, int fkZona, int fkPartidaa, int nivel, string fechaDerrota, int completada)
        {
            this.idZonaPartida = idZonaPartida;
            this.fkZona = fkZona;
            this.fkPartidaa = fkPartidaa;
            this.nivel = nivel;
            this.fechaDerrota = fechaDerrota;
            this.completada = completada;
        }
    }
}
