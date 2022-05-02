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
            
        
            ConsoleKeyInfo cki;
            Console.WindowHeight = 49;
            Console.WindowWidth = 100;
            do
            {
                Console.Clear();
                Console.WriteLine("Menu photo:\n\n"
                                 + "Photo 1 : Coco\n"
                                 + "Photo 2 : Lena\n"
                                 + "Photo 3 : Test001 noir et blanc\n"
                                 + "Photo 4 : Test002 noir et blanc\n"
                                 + "Photo 5 : Test couleur\n"
                                 + "Photo 6 : Lac et montagne\n"
                                 + "\n"
                                 + "Sélectionnez l'exercice désiré ");
                
                int nbphoto = Convert.ToInt32(Console.ReadLine());
                string fileN = " ";

                switch (nbphoto)
                {
                    case 1:
                        fileN = "coco.bmp";
                        break;
                    case 2:
                        fileN = "lena.bmp";
                        break;
                    case 3:
                        fileN = "Test001.bmp";
                        break;
                    case 4:
                        fileN = "Test002.bmp";
                        break;
                    case 5:
                        fileN = "Test.bmp";
                        break;
                    case 6:
                        fileN = "Test3.bmp";
                        break;
                }
                MyImage image = new MyImage(fileN);


                Console.WriteLine("Menu fonction:\n"
                                 + "Fonction 1  : Fonction Grey\n"
                                 + "Fonction 2  : Effet miroir\n"
                                 + "Fonction 3  : Image en noir et blanc\n"
                                 + "Fonction 4  : Agrandir l'image\n"
                                 + "Fonction 5  : Réduire l'image\n"
                                 + "Fonction 6  : Rotationnez l'image\n"
                                 + "Fonction 7  : Image Flou\n"
                                 + "Fonction 8  : Repoussage des bords\n"
                                 + "Fonction 9  : Détection des bords\n"
                                 + "Fonction 10 : Renforcement des bords\n"
                                 + "Fonction 11 : Fractale\n"
                                 + "Fonction 12 : Histogramme\n"
                                 + "Fonction 13 : Coder une image\n"
                                 + "Fonction 14 : Décoder une image\n"
                                 + "Fonction 15 : Innovation\n"
                                 + "\n"
                                 + "Sélectionnez l'exercice désiré ");

                int exo = Convert.ToInt32(Console.ReadLine());
                switch (exo)
                {
                    case 1:
                        Console.WriteLine("Fonction 1 : Fonction Grey\n");
                        MyImage imageGrey = image.ConvertToGrey();
                        imageGrey.From_Image_To_File("sortieGrey.bmp");
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.WriteLine("Fonction 2 : Effet miroir\n");
                        MyImage image2 = image.EffetMiroir();
                        image2.From_Image_To_File("sortieEffetMiroir.bmp");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("Fonction 3 : Image en noir et blanc\n");
                        MyImage NOIRetBLANC = image.NoirETBlanc();
                        NOIRetBLANC.From_Image_To_File("sortieImageEnNoirEtBlanc.bmp");
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine("Fonction 4 : Agrandir l'image\n");
                        MyImage imageAgrandir = image.Agrandir();
                        imageAgrandir.From_Image_To_File("sortieAgrandir.bmp");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("Fonction 5  : Réduire l'image\n");
                        MyImage imageReduire = image.Réduire();
                        imageReduire.From_Image_To_File("sortieRéduit.bmp");
                        Console.ReadKey();
                        break;

                    case 6:
                        Console.WriteLine("Fonction 6  : Rotationnez l'image\n");
                        Console.WriteLine("Donner un angle en degré : \n");
                        int angle = Convert.ToInt32(Console.ReadLine());
                        MyImage ImageRotation = image.Rotation(angle);
                        ImageRotation.From_Image_To_File("sortieRotation.bmp");
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.WriteLine("Fonction 7  : Image Flou\n");
                        MyImage imageFlou = image.Flou();
                        imageFlou.From_Image_To_File("sortie-Flou.bmp");
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.WriteLine("Fonction 8  : Repoussage des bords\n");
                        MyImage imageRepoussage = image.Repoussage();
                        imageRepoussage.From_Image_To_File("sortie-Repoussage.bmp");
                        Console.ReadKey();
                        break;
                    case 9:
                        Console.WriteLine("Fonction 9  : Détection des bords\n");
                        MyImage imagedétectionDesBords = image.DétectionDesBords();
                        imagedétectionDesBords.From_Image_To_File("sortie-DétectionDesBords.bmp");
                        Console.ReadKey();
                        break;
                    case 10:
                        Console.WriteLine("Fonction 10 : Renforcement des bords\n");
                        MyImage imageRenforcementDesBords = image.RenforcementDesBords();
                        imageRenforcementDesBords.From_Image_To_File("sortie-RenforcementDesBords.bmp");
                        Console.ReadKey();
                        break;
                    case 11:
                        Console.WriteLine("Fonction 11 : Fractale\n");
                        MyImage imageFractaleNOIR = image.FractaleNOIR();
                        imageFractaleNOIR.From_Image_To_File("sortie-FractaleNOIR.bmp");
                        Console.ReadKey();
                        break;
                    case 12:
                        Console.WriteLine("Fonction 12 : Histogramme\n");
                        MyImage Histogramme = image.Histogramme();
                        Histogramme.From_Image_To_File("sortieHistogramme1.bmp");
                        Console.ReadKey();
                        break;
                    case 13:
                        Console.WriteLine("Fonction 13 : Coder une image\n");
                        MyImage ImageCoder = image.CoderImage();
                        ImageCoder.From_Image_To_File("sortieImageCoder.bmp");
                        Console.ReadKey();
                        break;
                    case 14:
                        Console.WriteLine("Fonction 14 : Décoder une image\n");
                        MyImage ImageCoderR = image.CoderImage();
                        MyImage ImageDECoder = image.DECoderImage(ImageCoderR);
                        ImageDECoder.From_Image_To_File("sortieImageDEcoder.bmp");
                        Console.ReadKey();
                        break;
                    case 15:
                        Console.WriteLine("Fonction 15 : Innovation\n");
                        MyImage ImageIn = image.Innovation();
                        ImageIn.From_Image_To_File("sortieInnovation.bmp");
                        Console.ReadKey();
                        break;
                        /*case 15:
                            string phrase = new string"HELLO WORLD".
                            Console.WriteLine("Fonction 15 : QR Code\n"+"Phrase : "+phrase);
                            QRcode QR_Code = new QRcode(phrase);
                            QR_Code.From_Image_To_File("sortieQRcode.bmp");
                            Console.ReadKey();
                            break;*/
                }
                Console.WriteLine("Tapez Escape pour refaire une fonction");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
            Console.Read();
        }

        /*static void Main(string[] args)
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

           /* MyImage imageFlou = image.Flou();
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
            Console.WriteLine("fin");

            MyImage imageFractaleCOULEUR = image.FractaleCOULEURS();
            Console.WriteLine("fin");
            imageFractaleCOULEUR.From_Image_To_File("sortie-FractaleCOULEURS.bmp");
            Console.WriteLine("fin");

            MyImage NOIRetBLANC = image.NoirETBlanc();
            Console.WriteLine("fin");
            NOIRetBLANC.From_Image_To_File("sortieImageEnNoirEtBlanc.bmp");
            Console.WriteLine("fin");

            MyImage ImageCoder  = image.CoderImage();
            Console.WriteLine("fin");
            ImageCoder.From_Image_To_File("sortieImageCoder.bmp");
            Console.WriteLine("fin");

            MyImage ImageDECoder = image.DECoderImage(ImageCoder);
            Console.WriteLine("fin");
            ImageDECoder.From_Image_To_File("sortieImageDEcoder.bmp");
            Console.WriteLine("fin");

            string phrase = "HELLO WORLD AND FRIENDS AND CO";
            QRcode a = new QRcode(phrase);
            a.EncodageQRCode();
            MyImage Qr = a.ImageQRcode();
            Qr.From_Image_To_File("Qr.bmp");




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


            MyImage ImageRotation = image.Rotation(270);
            ImageRotation.From_Image_To_File("sortieRotation.bmp");

            MyImage ImageRotation2 = image.Rotation(-90);
            ImageRotation2.From_Image_To_File("sortieRotation2.bmp");
            Console.WriteLine("fin");

            Console.ReadKey();
        }*/


    }

}