using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.Constantes
{
    public class RutasArchivos
    {
        private static string rutaSistema = Application.persistentDataPath+"/";

        public static string LoginUsuario = rutaSistema + "usuario.txt";
    }
}
