using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Manon_Aubry_Manon_Goffinet
{
    public class Pixel
    {
        byte blue;
        byte red;
        byte green;

        /// <summary>
        /// constructeur de la classe Pixel qui prend en paramètres des valeurs pour le rouge, vert et bleu 
        /// </summary>
        /// <param name="red">valeur du rouge du pixel</param>
        /// <param name="green">valeur du vert du pixel</param>
        /// <param name="blue">valeur du bleu du pixel</param>
        public Pixel (byte blue , byte red, byte green)
        {
            this.blue = blue;
            this.red = red;
            this.green = green;
        }
        #region GetSet

        /// <summary>
        /// Méthode get et set de la valeur blue du pixel
        /// </summary>
        public byte Blue
        {
            get { return blue; }
            set { blue = value; }
        }

        /// <summary>
        /// Méthode get et set de la valeur red du pixel
        /// </summary>
        public byte Red
        {
            get { return red; }
            set { red = value; }
        }

        /// <summary>
        /// Méthode get et set de la valeur green du pixel
        /// </summary>
        public byte Green
        {
            get { return green; }
            set { green = value; }
        }
        #endregion

        /// <summary>
        /// Méthode de description du pixel donnant la valeur rgb de ce dernier
        /// </summary>
        /// <returns>un string de la forme "valuered valuegreen valueblue"</returns>
        public string toString()
        {
            string a = red + " " + green + " " + blue;
            return a;
        }
    }
}
