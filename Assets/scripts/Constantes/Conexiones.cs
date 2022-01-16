using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Constantes
{
    class Conexiones
    {
        private static string servidor = "192.168.43.46";
        private static string proyecto = "Proyecto";

        public static string UsuarioLogin = GetUrlConexion() + "Usuario/IniciarSesion.php";
        public static string UsuarioRegistro = GetUrlConexion() + "Usuario/Registrarse.php";

        public static string PartidaUsuario = GetUrlConexion() + "Partida/GetPartida.php";
        public static string PartidaUpdate = GetUrlConexion() + "Partida/UpdatePartida.php";


        public static string CheckpointUsuario = GetUrlConexion() + "Checkpoint/GetCheckpoint.php";

        public static string JugadorUsuario = GetUrlConexion() + "Jugador/GetJugador.php";
        public static string JugadorUpdate = GetUrlConexion() + "Jugador/UpdateJugador.php";


        public static string ZonaEnemigaTodas = GetUrlConexion() + "ZonaEnemiga/GetAll.php";

        public static string ZonasEnemigasPartidaUsuario = GetUrlConexion() + "ZonaEnemigaPartida/GetZonasEnemigasPartida.php";
        public static string ZonasEnemigasPartidaUdate = GetUrlConexion() + "ZonaEnemigaPartida/UpdateZona.php";


        private static string GetUrlConexion() {
            return "http://" + servidor + "/" + proyecto + "/";
        }
    }
}
