﻿using System;
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
            string fileN = "test001.bmp";
            MyImage image = new MyImage(fileN);
            //image1.From_Image_To_File("sortie.bmp");

            //MyImage image2 = image1.EffetMiroir();
            //image2.From_Image_To_File("sortie3.bmp");
            //image1.From_Image_To_File("sortie4.bmp");

            //byte[] tableauLargeur = image1.Convertir_Int_To_Endian(image1.LargeurImage);
            //byte[] tableauLargeur2 = image1.Convertir_Int_To_Endian2(image1.LargeurImage);



            //MyImage imageGrey = image.ConvertToGrey();
            //imageGrey.From_Image_To_File("sortieGrey.bmp");

            //MyImage imageAgrandir = image1.Agrandir();
            //imageAgrandir.From_Image_To_File("sortie6.bmp");
            //MyImage ImageRotation = image.Rotation(200);
            //ImageRotation.From_Image_To_File("sortieRotation.bmp");

            //MyImage imageReduire = image.Réduire();
            //imageReduire.From_Image_To_File("sortie7.bmp");

            /*MyImage imageFlou = image.Flou();
            imageFlou.From_Image_To_File("sortie-Flou.bmp");

            MyImage imageRepoussage = image.Repoussage();
            imageRepoussage.From_Image_To_File("sortie-Repoussage.bmp");

            MyImage imagedétectionDesBords = image.DétectionDesBords();
            imagedétectionDesBords.From_Image_To_File("sortie-DétectionDesBords.bmp");

            MyImage imageRenforcementDesBords = image.RenforcementDesBords();
            imageRenforcementDesBords.From_Image_To_File("sortie-RenforcementDesBords.bmp");*/

            MyImage imageFractale = image.Fractale();
            Console.WriteLine("fin");
            imageFractale.From_Image_To_File("sortie-Fractale.bmp");
            Console.WriteLine("fin");

            Console.ReadKey();
        }
    }
}