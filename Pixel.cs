using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Manon_Aubry_Manon_Goffinet
{
    class Pixel
    {
        byte blue;
        byte red;
        byte green;

        public Pixel (byte blue, byte red, byte green)
        {
            this.blue = blue;
            this.red = red;
            this.green = green;
        }
        #region GetSet
        public byte Blue
        {
            get { return blue; }
            set { blue = value; }
        }
        public byte Red
        {
            get { return red; }
            set { red = value; }
        }

        public byte Green
        {
            get { return green; }
            set { green = value; }
        }
        #endregion

        #region Gris
        /// <summary>
        /// Passage d’une photo couleur à une photo en nuances de gris et en noir et blanc
        /// </summary>
        /// chaque pixel a 3 couleurs/chaque couleurs a un nombre de bit/on fait la moyenne du nb de bit des 3 couleurs/et on remplace les bitCouleurs par la moyenne => gris
        /// <returns></returns>
        
        public MyImage ConvertToGrey()
        {
            try
            {
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for(int u = 0; u < image.GetLength(1); u++)
                    {
                        image[i, u].Blue = Convert.ToByte((image[i, u].Blue + image[i, u].Red + image[i, u].Green) / 3);
                        image[i, u].Red = Convert.ToByte((image[i, u].Blue + image[i, u].Red + image[i, u].Green) / 3);
                        image[i, u].Green = Convert.ToByte((image[i, u].Blue + image[i, u].Red + image[i, u].Green) / 3);
                    }
                }
                MyImage resul = new MyImage(image, image.typeImage, image.tailleFichier, image.tailleOffset, image.largeurImage, image.hauteurImage, image.nombreDeBitsCouleurs);
                return resul;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        public string toString()
        {
            string a = blue + " " + red + " " + green;
            return a;
        }
    }
}
