using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Manon_Aubry_Manon_Goffinet
{
    class MyImage
    {
        Pixel [,] image;
        string typeImage;
        int tailleFichier;
        int tailleOffset;
        int largeurImage;
        int longueurImage;
        int nombreDeBitsCouleurs;

        public MyImage(string fileName)
        {

            byte[] Im = File.ReadAllBytes(fileName);

            char t = (char)Im[0];
            char f = (char)Im[1];
            typeImage = Convert.ToString(t) + f; // récupère le type de fichier en convertissant le byte 0 et 1 en string
            //Console.WriteLine(typeImage);
            



        }

    }
}
