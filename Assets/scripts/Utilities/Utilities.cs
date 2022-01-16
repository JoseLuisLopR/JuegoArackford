using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

namespace Assets.scripts.Utilities
{
    public class Utilities
    {

        public static Button GetGameObjectByNameInArray(Button[] objetos, string nombre)
        {
            Button valor = null;
            foreach (Button obj in objetos)
            {
                if (obj.name == nombre) { valor = obj; }
            }

            return valor;
        }

        public static Image GetGameObjectByNameInArray(Image[] objetos, string nombre)
        {
            Image valor = null;
            foreach (Image obj in objetos)
            {
                if (obj.gameObject.name == nombre) { valor = obj; }
            }

            return valor;
        }

        public static InputField GetGameObjectByNameInArray(InputField[] objetos, string nombre)
        {
            InputField valor = null;
            foreach (InputField obj in objetos)
            {
                if (obj.name == nombre) { valor = obj; }
            }

            return valor;
        }

        public static ControlZonaEnemiga GetGameObjectByNameInArray(ControlZonaEnemiga[] objetos, string nombre)
        {
            ControlZonaEnemiga valor = null;
            foreach (ControlZonaEnemiga obj in objetos)
            {
                if (obj.name == nombre) { valor = obj; }
            }

            return valor;
        }

        public static void VaciarInputsFields(InputField[] inputFields)
        {
            foreach (InputField input in inputFields)
            {
                input.text = "";
            }
        }
        public static bool IsEmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static IEnumerator Wait(float segundos,int escena)
        {
            yield return new UnityEngine.WaitForSeconds(segundos);
            UnityEngine.SceneManagement.SceneManager.LoadScene(escena);
        }

        public static IEnumerator Wait(float segundos, Canvas c)
        {
            yield return new UnityEngine.WaitForSeconds(segundos);
            UnityEngine.Object.Instantiate(c.gameObject);
        }
    }
}
