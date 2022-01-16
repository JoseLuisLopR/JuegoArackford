using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Entities
{
    [System.Serializable]
    class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string activo { get; set; }

        public Usuario()
        {
        }

        public Usuario(int idUsuario, string nombre, string apellidos, string email, string login, string password, string activo)
        {
            this.idUsuario = idUsuario;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.login = login;
            this.password = password;
            this.activo = activo;
        }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}
