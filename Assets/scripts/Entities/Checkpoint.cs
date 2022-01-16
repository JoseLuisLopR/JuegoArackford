using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Entities
{
    [System.Serializable]
    public class Checkpoint
    {
        public int idCheckpoint { get; set; }
        public string nombre { get; set; }
        public string posicion { get; set; }
        public string rotacion { get; set; }

        public Checkpoint()
        {
        }

        public Checkpoint(int idCheckpoint, string nombre, string posicion, string rotacion)
        {
            this.idCheckpoint = idCheckpoint;
            this.nombre = nombre;
            this.posicion = posicion;
            this.rotacion = rotacion;
        }
    }
}
