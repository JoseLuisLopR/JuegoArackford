using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Entities
{
    [System.Serializable]
    public class ZonaEnemiga
    {
        public int idZona { get; set; }
        public string nombre { get; set; }

        public ZonaEnemiga()
        {
        }

        public ZonaEnemiga(int idZona, string nombre)
        {
            this.idZona = idZona;
            this.nombre = nombre;
        }
    }
}
