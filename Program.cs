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
            MyImage image = new MyImage(fileN);
            //image1.From_Image_To_File("sortie.bmp");

            //byte[] tableauLargeur = image.Convertir_Int_To_Endian(68,image.LargeurImage);

            //MyImage image2 = image.EffetMiroir();
            //image2.From_Image_To_File("sortieEffetMiroir.bmp");

            //MyImage imageGrey = image.ConvertToGrey();
            //imageGrey.From_Image_To_File("sortieGrey.bmp");

            //MyImage imageAgrandir = image.Agrandir();
            //imageAgrandir.From_Image_To_File("sortieAgrandir.bmp");

            //MyImage ImageRotation = image.Rotation(100);
            //ImageRotation.From_Image_To_File("sortieRotation.bmp");

            //MyImage imageReduire = image.Réduire();
            //imageReduire.From_Image_To_File("sortieRéduit.bmp");

            /*MyImage imageFlou = image.Flou();
            Console.WriteLine("fin");
            imageFlou.From_Image_To_File("sortie-Flou.bmp");
            Console.WriteLine("fin");*/

            ///////////////////////
            //MyImage imageRepoussage = image.Repoussage();
            //imageRepoussage.From_Image_To_File("sortie-Repoussage.bmp");
            /////////////////////////

            //MyImage imagedétectionDesBords = image.DétectionDesBords();
            //imagedétectionDesBords.From_Image_To_File("sortie-DétectionDesBords.bmp");

            //MyImage imageRenforcementDesBords = image.RenforcementDesBords();
            //imageRenforcementDesBords.From_Image_To_File("sortie-RenforcementDesBords.bmp");

            //MyImage Histogramme  = image.Histogramme();
            //Histogramme.From_Image_To_File("sortieHistogramme1.bmp");

            /*MyImage imageFractaleNOIR = image.FractaleNOIR();
            Console.WriteLine("fin");
            imageFractaleNOIR.From_Image_To_File("sortie-FractaleNOIR.bmp");
            Console.WriteLine("fin");*/

            /*MyImage imageFractaleCOULEUR = image.FractaleCOULEURS();
            Console.WriteLine("fin");
            imageFractaleCOULEUR.From_Image_To_File("sortie-FractaleCOULEURS.bmp");
            Console.WriteLine("fin");*/

            /*MyImage NOIRetBLANC = image.NoirETBlanc();
            Console.WriteLine("fin");
            NOIRetBLANC.From_Image_To_File("sortieImageEnNoirEtBlanc.bmp");
            Console.WriteLine("fin");*/

            /*MyImage ImageCoder  = image.CoderImage();
            Console.WriteLine("fin");
            ImageCoder.From_Image_To_File("sortieImageCoder.bmp");
            Console.WriteLine("fin");

            MyImage ImageDECoder = image.DECoderImage(ImageCoder);
            Console.WriteLine("fin");
            ImageDECoder.From_Image_To_File("sortieImageDEcoder.bmp");
            Console.WriteLine("fin");*/

            //int QRcode = QRCodeV1("HELLO WORLD");
            //Console.WriteLine(QRcode);

            string phrase = "HELLO WORLD";
            List<byte> chaineASCII = new List<byte>();

            chaineASCII.Add(0010);  // le type d'information est alphanumérique

            int nbDeCaractère = 0;
            int reste = 0;
            int nbr = 0;
            for (int i = 0; i < phrase.Length; i++)
            {
                ASCII valeurLettre = new ASCII(phrase[i]);
                nbr = valeurLettre.Valeur;

                while (nbr >= 0)
                {
                    reste = valeurLettre.Valeur % 2;
                    nbr = nbr / 2;
                    if (reste == 0) chaineASCII.Add(0);
                    else chaineASCII.Add(1);
                }

                nbDeCaractère++;
            }
            byte nbDeCaractèreEnByte = (byte)nbDeCaractère;
            Console.WriteLine(nbDeCaractèreEnByte);
            Console.WriteLine(chaineASCII);


            //QRcode.From_Image_To_File("sortieQRCodeV1.bmp");


            //QRcode.From_Image_To_File("sortieQRCodeV1.bmp");


            /*MyImage ImageRotation = image.Rotation(270);
            ImageRotation.From_Image_To_File("sortieRotation.bmp");

            MyImage ImageRotation2 = image.Rotation(-90);
            ImageRotation2.From_Image_To_File("sortieRotation2.bmp");
            Console.WriteLine("fin");*/

            Console.ReadKey();
        }
    }
}