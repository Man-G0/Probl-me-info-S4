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
        public Pixel[,] image;
        public string typeImage;
        public int tailleFichier;
        public int tailleOffset;
        public int largeurImage;
        public int longueurImage;
        public int nombreDeBitsCouleurs;

        public MyImage(string fileName)
        {
            byte[] Im = File.ReadAllBytes(fileName);

            char t = (char)Im[0];
            char f = (char)Im[1];
            typeImage = Convert.ToString(t) + f;
            //Console.WriteLine(typeImage);
        }

        //public byte[] Convertir_Int_To_Endian(int val …) convertit un entier en séquence d’octets au format little endian
        public static byte[] Convertir_Int_To_Endian(int v)
        {
            byte[] tabBytes = BitConverter.GetBytes(v);
            return tabBytes;
        }


        //public int Convertir_Endian_To_Int(byte[] tab …) convertit une séquence d’octets au format little endian en entier 
        /// <summary>
        /// récupère un tableau de byte et le convertit en int
        /// </summary>
        /// <param name="v"> tableau de byte à convertir en int </param>
        /// <returns>nombre entier correspondant au tableau de byte</returns>
        public static int Convert_Endian_To_Int(byte[] v)
        {
            int nbEntier = BitConverter.ToInt32(v, 0);          
            return nbEntier;
        }
        

        //public void From_Image_To_File(string file) prend une instance de MyImage et la transforme en fichier binaire respectant la structure du fichier.bmp
        public void From_Image_To_File(string file)
        {
            byte[] tableauLargeur = Convertir_Int_To_Endian(largeurImage);
            byte[] tableauOffset = Convertir_Int_To_Endian(tailleOffset);
            byte[] tabNombreDeBitsCouleurs = Convertir_Int_To_Endian(nombreDeBitsCouleurs);
            byte[] tableauTailleFichier = Convertir_Int_To_Endian(tailleFichier);
            byte[] tableauHauteur = Convertir_Int_To_Endian(longueurImage);
            byte[] tableauType = new byte[] { Convert.ToByte(typeImage[0]), Convert.ToByte(typeImage[1]) };   //Encoding.ASCII.GetBytes(typeImage);


            try
            {
                if (File.Exists(file))
                {
                File.Delete(file);
                }

                using (FileStream fc = new FileStream(file, FileMode.Create))
                {

                    for (int i = 0; i < tailleOffset; i++)
                    {
                        switch (i)
                        {
                            case int j when j < 2:
                                fc.WriteByte(tableauType[i]);
                                break;

                            case int j when j >= 2 && j < 6:
                                fc.WriteByte(tableauTailleFichier[i - 2]);
                                break;

                            case int j when j >= 10 && j < 14:
                                fc.WriteByte(tableauOffset[i - 10]);
                                break;

                            case int j when j >= 14 && j < 18:
                                fc.WriteByte(Convertir_Int_To_Endian(tailleOffset - 14)[i - 14]);
                                break;

                            case int j when j >= 18 && j < 22:
                                fc.WriteByte(tableauLargeur[i - 18]);
                                break;

                            case int j when j >= 22 && j < 26:
                                fc.WriteByte(tableauHauteur[i - 22]);
                                break;

                            case 26:
                                fc.WriteByte((byte)1);
                                break;

                            case int j when j >= 28 && j < 30:
                                fc.WriteByte(tabNombreDeBitsCouleurs[i - 28]);
                                break;

                            case int j when j >= 34 && j < 38:
                                fc.WriteByte(Convertir_Int_To_Endian(tailleFichier - tailleOffset)[i - 34]);
                                break;

                            case int j when j >= 38 && j < 42:
                                fc.WriteByte(Convertir_Int_To_Endian(2835)[i - 38]);
                                break;
                            case int j when j >= 42 && j < 46:
                                fc.WriteByte(Convertir_Int_To_Endian(2835)[i - 42]);
                                break;
                            default:
                                fc.WriteByte((byte)0);
                                break;
                        }
                    }

                    int p = 0;
                    if (largeurImage % 4 != 0)   //la largeur de l'image doit être un multiple de 4
                    {
                        p = 4 - (largeurImage % 4);
                    }
                    for (int i = 0; i < longueurImage; i++)
                    {
                        for (int j = 0; j < largeurImage + p; j++)
                        {
                            if (j < largeurImage)
                            {
                                for (int k = 0; k < nombreDeBitsCouleurs / 8; k++)
                                {
                                    switch (k)
                                    {
                                        case 0:
                                            fc.WriteByte(image[i, j].Red);
                                            break;
                                        case 1:
                                            fc.WriteByte(image[i, j].Green);
                                            break;
                                        case 2:
                                            fc.WriteByte(image[i, j].Blue);
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                fc.WriteByte(0);
                            }

                        }
                    }
                }

                using (StreamReader lien = File.OpenText(file))
                {
                    string sortie = "";
                    while ((sortie = lien.ReadLine()) != null)
                    {
                    Console.WriteLine(sortie);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}
