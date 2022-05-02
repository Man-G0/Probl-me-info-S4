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
            
            //image.From_Image_To_File("sortie.bmp");
            //Console.WriteLine("fait");
            //byte[] tableauLargeur = image.Convertir_Int_To_Endian(68,image.LargeurImage);

            //MyImage image2 = image.EffetMiroir();
            //image2.From_Image_To_File("sortieEffetMiroir.bmp");

            //MyImage imageGrey = image.ConvertToGrey();
            //imageGrey.From_Image_To_File("sortieGrey.bmp");

            //MyImage imageAgrandir = image.Agrandir();
            //imageAgrandir.From_Image_To_File("sortieAgrandir.bmp");
            /*MyImage ImageRotation = image.Rotation(90);
             ImageRotation.From_Image_To_File("sortieRotation.bmp");
             MyImage ImageRotation2 = image.Rotation(89);
             ImageRotation2.From_Image_To_File("sortieRotation2.bmp");

             MyImage ImageRotation3 = image.Rotation(180);
             ImageRotation3.From_Image_To_File("sortieRotation3.bmp");
             MyImage ImageRotation4 = image.Rotation(179);
             ImageRotation4.From_Image_To_File("sortieRotation4.bmp");

             MyImage ImageRotation5 = image.Rotation(270);
             ImageRotation5.From_Image_To_File("sortieRotation5.bmp");
             MyImage ImageRotation6 = image.Rotation(269);
             ImageRotation6.From_Image_To_File("sortieRotation6.bmp");

             MyImage ImageRotation7 = image.Rotation(360);
             ImageRotation7.From_Image_To_File("sortieRotation7.bmp");
             MyImage ImageRotation8 = image.Rotation(359);
             ImageRotation8.From_Image_To_File("sortieRotation8.bmp");*/
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

            /* MyImage Histogramme  = image.Histogramme();
             Histogramme.From_Image_To_File("sortieHistogramme1.bmp");*/

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

            string phrase = "HELLO WORLD AND FRIENDS AND CO";
            QRcode a = new QRcode(phrase);
            a.EncodageQRCode();
            MyImage Qr = a.ImageQRcode();
            Qr.From_Image_To_File("Qr.bmp");






            Console.ReadKey();
        }

        public void AffichageConsole()
        {
            
        }
    }

}