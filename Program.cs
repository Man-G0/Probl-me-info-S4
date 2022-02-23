using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Manon_Aubry_Manon_Goffinet
{
    class Program
    {
        static void Main(string[] args)
        {


            
            string fileN = "coco.bmp";
            MyImage image1 = new MyImage(fileN);
            //image1.From_Image_To_File("sortie.bmp");

            //MyImage image2 = image1.EffetMiroir();
            //image2.From_Image_To_File("sortie3.bmp");
            //image1.From_Image_To_File("sortie4.bmp");

            //byte[] tableauLargeur = image1.Convertir_Int_To_Endian(image1.LargeurImage);
            //byte[] tableauLargeur2 = image1.Convertir_Int_To_Endian2(image1.LargeurImage);

            MyImage image3 = image1.ConvertToGrey();
            image3.From_Image_To_File("sortie5.bmp");
            
            Console.ReadKey();
        }
    }
}
