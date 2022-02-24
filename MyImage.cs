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
        Pixel[,] image;
        string typeImage;
        int tailleFichier;
        int tailleOffset;
        int largeurImage;
        int hauteurImage;
        int nombreDeBitsCouleurs;


        /// <summary>
        /// Constructeur créant une instance de MyImage a partir de tous les attributs de la classe.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="typeImage"></param>
        /// <param name="tailleFichier"></param>
        /// <param name="tailleOffset"></param>
        /// <param name="largeurImage"></param>
        /// <param name="hauteurImage"></param>
        /// <param name="nombreDeBitsCouleurs"></param>
        public MyImage(Pixel[,] image, string typeImage, int tailleFichier, int tailleOffset, int largeurImage, int hauteurImage, int nombreDeBitsCouleurs)
        {
            this.image = image;
            this.typeImage = typeImage;
            this.tailleFichier = tailleFichier;
            this.tailleOffset = tailleOffset;
            this.largeurImage = largeurImage;
            this.hauteurImage = hauteurImage;
            this.nombreDeBitsCouleurs = nombreDeBitsCouleurs;

            /*Console.WriteLine(this.typeImage);
            Console.WriteLine(this.tailleFichier);
            Console.WriteLine(this.largeurImage);
            Console.WriteLine(this.image.GetLength(1));
            Console.WriteLine(this.hauteurImage);
            Console.WriteLine(this.image.GetLength(0));
            Console.WriteLine(this.nombreDeBitsCouleurs);
            Console.WriteLine("\n\nAFFICHER IMAGE\n");*/
        }

        /// <summary>
        /// Constructeur créant une instance de MyImage a partir d'une image à l'emplacement donné : convertie une image en instance MyImage
        /// </summary>
        /// <param name="fileName">Emplacement de l'image à convertir en instance MyImage</param>
        public MyImage(string fileName)
        {
            byte[] Im = File.ReadAllBytes(fileName);

            #region TypeImage
            //Console.WriteLine(Im[0] + "et" + Im[1]);
            char t = (char)Im[0];
            char f = (char)Im[1];
            typeImage = Convert.ToString(t) + f;
            #endregion

            #region tailleFichier
            byte[] tailleEnBytes = new byte[] { Im[2], Im[3], Im[4], Im[5] }; // récupère les bytes 2,3,4,5 correspondant a la taille de l'image
            //Console.WriteLine(Im[2] + " " + Im[3] + " " + Im[4]+" "+Im[5]);
            tailleFichier = Convert_Endian_To_Int(tailleEnBytes);
            //Console.WriteLine(tailleFichier);
            #endregion

            #region tailleOffset
            byte[] tailleHeaderEnBytes = new byte[] { Im[14], Im[15], Im[16], Im[17] }; // récupère les bytes 14,15,16,17 correspondant a la taille du header
            //Console.WriteLine(Im[14] + " " + Im[15] + " " + Im[16] + " " + Im[17]);
            int tailleHeader = Convert_Endian_To_Int(tailleHeaderEnBytes);
            tailleOffset = tailleHeader + 14; //additionne la taille header plus la taille du header info qui est de 14
            Console.WriteLine(tailleOffset);
            //Console.WriteLine(tailleOffset);
            #endregion

            #region HauteurImage
            byte[] hauteurEnBytes = new byte[] { Im[22], Im[23], Im[24], Im[25] }; //récupère les bytes 23,24,25,26
            //Console.WriteLine(Im[22] + " " + Im[23] + " " + Im[24] + " " + Im[25]);
            hauteurImage = Convert_Endian_To_Int(hauteurEnBytes);
            //Console.WriteLine(hauteurImage);
            #endregion

            #region LargeurImage
            byte[] largeurEnBytes = new byte[] { Im[18], Im[19], Im[20], Im[21] }; //récupère les bytes 19,20,21,22
            //Console.WriteLine(Im[18] + " " + Im[19] + " " + Im[20] + " " + Im[21]);
            largeurImage = Convert_Endian_To_Int(largeurEnBytes);
            //Console.WriteLine(largeurImage);
            #endregion

            #region NombreDeBitsCouleurs
            byte[] nombreDeBitsCouleursEnBytes = new byte[] {Im[28], Im[29]}; //récupère les bytes 29,30
            //Console.WriteLine(Im[28] + " " + Im[29]);
            nombreDeBitsCouleurs = Convert_Endian_To_Int(nombreDeBitsCouleursEnBytes);
            //Console.WriteLine(nombreDeBitsCouleurs);
            #endregion

            #region AffichageHeader

            Console.WriteLine("Header\n");
            for (int i = 0; i < 14; i++)
            {
                Console.Write(Im[i] + "   ");
            }
            Console.WriteLine("\n\nHeader Infos\n");
            for (int i = 14; i < tailleOffset; i++)
            {
                Console.Write(Im[i] + "   ");
            }
            #endregion

            #region AffichageImageBytes
            Console.WriteLine("\n\nAFFICHER IMAGE\n");

            image = new Pixel[hauteurImage, largeurImage];
            int k = 0;
            int j = 0;
            try
            {
                for (int i = tailleOffset; i < tailleFichier; i += 3) // taille offset = 54
                {
                    byte blue = Im[i];
                    byte red = Im[i + 1];
                    byte green = Im[i + 2];
                    Pixel a = new Pixel(blue, red, green);

                    if (j >= largeurImage)
                    {
                        j = 0;
                        k++;
                        image[k, j] = a;

                        j++;
                    }

                    else
                    {
                        image[k, j] = a;
                        j++;
                    }
                }
                //AfficherMatricePixel(image);
                //Console.WriteLine("\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            #endregion
        }

        #region Get&Set

        public Pixel[,] Image
        {
            get { return image; }
        }

        public string TypeImage
        {
            get { return typeImage; }
        }

        public int TailleFichier
        {
            get { return tailleFichier; }
        }
        public int TailleOffset
        {
            get { return tailleOffset; }
        }
        public int LargeurImage
        {
            get { return largeurImage; }
        }

        public int HauteurImage
        {
            get { return hauteurImage; }
        }
        public int NombreDeBitsCouleurs
        {
            get { return nombreDeBitsCouleurs; }
        }


        #endregion
        public void AfficherMatricePixel(Pixel[,] tabPix)
        {
            try
            {
                for (int i = 0; i < tabPix.GetLength(0); i++)
                {
                    for (int j = 0; j < tabPix.GetLength(1); j++)
                    {
                        Console.Write(tabPix[i, j].toString() + "   ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        #region public byte[] Convertir_Int_To_Endian(int val …)
        //public byte[] Convertir_Int_To_Endian(int val …) convertit un entier en séquence d’octets au format little endian 
        /// <summary>
        /// Récupère la valeur binaire d'un int, la converti en bytes (groupe de 8 chiffre binaires), et la met dans un tableau de bytes
        /// </summary>
        /// <param name="v">chiffre a convertir en bytes</param>
        /// <returns></returns>
        public byte[] Convertir_Int_To_Endian(int v, int taille)
        {

            //byte[] tabBytes = BitConverter.GetBytes(v);
            byte[] tabBytes = new byte[taille];
            for(int i =0; i < taille; i++)
            {
                tabBytes[i] = (byte)(v % 256);
                v /= 256;
            }

            return tabBytes;
        }

       
        #endregion

        #region public int Convertir_Endian_To_Int(byte[] tab …)
        //public int Convertir_Endian_To_Int(byte[] tab …) convertit une séquence d’octets au format little endian en entier 
        /// <summary>
        /// récupère un tableau de byte et le convertit en int
        /// </summary>
        /// <param name="v"> tableau de byte à convertir en int </param>
        /// <returns>nombre entier correspondant au tableau de byte</returns>
        public static int Convert_Endian_To_Int(byte[] v)
        {
            int res = 0;
            for(int i=0; i < v.Length; i++)
            {
                res += (int) Math.Pow(256, i) * v[i];
            }
            return res;
        }
        #endregion

        #region From_Image_To_File(string file)

        //public void From_Image_To_File(string file) prend une instance de MyImage et la transforme en fichier binaire respectant la structure du fichier.bmp
        /// <summary>
        /// Prend une instance de MyImage et la transforme en fichier binaire respectant la structure du fichier.bmp permettant sa lecture (son affichage)
        /// </summary>
        /// <param name="file">emplacement et nom du document.bmp à créer</param>
    public void From_Image_To_File(string myfile)
        {
            byte[] tableauLargeur = Convertir_Int_To_Endian(largeurImage, 4);
            byte[] tableauOffset = Convertir_Int_To_Endian(tailleOffset, 4);
            byte[] tabNombreDeBitsCouleurs = Convertir_Int_To_Endian(nombreDeBitsCouleurs, 2);
            byte[] tableauTailleFichier = Convertir_Int_To_Endian(tailleFichier, 4);
            byte[] tableauHauteur = Convertir_Int_To_Endian(hauteurImage, 4);
            byte[] tableauType = new byte[] { (byte)typeImage[0], (byte)typeImage[1] };  //transforme le type de fichier (string) en un tableau de bytes
            

            byte[] tab = new byte[tailleFichier];

            for (int i = 0; i < 54; i++)
            {
                switch (i)
                {
                    case int j when j < 2:
                        tab[i] = tableauType[i];
                        break;

                    case int j when j >= 2 && j < 6:
                        tab[i] = tableauTailleFichier[i - 2];
                        break;

                    case int j when j >= 10 && j < 14:
                        tab[i] = tableauOffset[i - 10];
                        break;

                    case int j when j >= 14 && j < 18:
                        tab[i] = Convertir_Int_To_Endian(tailleOffset - 14,4)[i - 14];
                        break;

                    case int j when j >= 18 && j < 22:
                        tab[i] = tableauLargeur[i - 18];
                        break;

                    case int j when j >= 22 && j < 26:
                        tab[i] = tableauHauteur[i - 22];
                        break;

                    case 26:
                        tab[i] = (byte)1;
                        break;

                    case int j when j >= 28 && j < 30:
                        tab[i] = tabNombreDeBitsCouleurs[i - 28];
                        break;

                    case int j when j >= 34 && j < 38:
                        tab[i] = Convertir_Int_To_Endian(tailleFichier - tailleOffset,4)[i - 34]; // Taille image codée entre les bytes 34 et 38
                        break;

                    case int j when j >= 38 && j < 42:
                        tab[i] = Convertir_Int_To_Endian(2835,4)[i - 38];
                        break;
                    case int j when j >= 42 && j < 46:
                        tab[i] = Convertir_Int_To_Endian(2835,4)[i - 42];
                        break;
                    default:
                        tab[i] = (byte)0;
                        break;
                }
            }

            int k = tailleOffset; // 54 normalement
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int u = 0; u < image.GetLength(1); u++)
                {
                    tab[k] = image[i, u].Blue;
                    tab[k + 1] = image[i, u].Red;
                    tab[k + 2] = image[i, u].Green;
                    k += 3;
                }
            }
            File.WriteAllBytes(myfile, tab);

        }
        #endregion

        #region Effet miroir
        /// <summary>
        /// recevoir une image et la transformée pour qu'elle devienne sont reflet dans un miroir
        /// </summary>
        public MyImage EffetMiroir()
        {
            try
            {
                Pixel[,] a = new Pixel[hauteurImage, largeurImage];
                int j = 0;
                for (int i = 0; i < hauteurImage; i++)
                {
                    j = largeurImage - 1;
                    for (int u = 0; u < largeurImage; u++)
                    {
                        a[i, u] = image[i, j];
                        j--;
                        Console.Write(a[i, u].toString() + "   ");
                    }
                    Console.WriteLine();
                }
                MyImage image2 = new MyImage(a, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);

                return image2;
            }
            catch (Exception e)
            {
                MyImage image3 = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                Console.WriteLine(e.Message);
                return image3;
            }

        }
        #endregion

        #region Grey
        public MyImage ConvertToGrey()
        {
            try
            {
                Pixel[,] mat = new Pixel[hauteurImage, largeurImage];
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int u = 0; u < image.GetLength(1); u++)
                    {
                        byte m = Convert.ToByte((image[i, u].Blue + image[i, u].Red + image[i, u].Green) / 3);
                        mat[i, u] = new Pixel(m, m, m);
                    }
                }
                
                MyImage resul = new MyImage(mat, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return resul;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }
        #endregion

        #region Agrandir et Réduire
        public MyImage Agrandir()
        {
            try
            {
                Console.WriteLine("De combien de pixels voulez-vous agrandir l'image ? ");
                int nbPixel = Convert.ToInt32(Console.ReadLine());

                Pixel[,] mat = new Pixel[image.GetLength(0) * (nbPixel+1), image.GetLength(1) * (nbPixel + 1)];
                MyImage resul = new MyImage(mat, typeImage, image.GetLength(1) * (nbPixel + 1)* image.GetLength(0) * (nbPixel + 1)*9 + tailleOffset, tailleOffset, image.GetLength(1) * (nbPixel + 1), image.GetLength(0) * (nbPixel + 1), nombreDeBitsCouleurs);

                int k = 0;
                int j = 0;
                for (int i = 0; i < image.GetLength(0); i ++)
                {
                    for (int u = 0; u < image.GetLength(1); u ++)
                    {
                        byte m1 = image[i, u].Blue;
                        byte m2 = image[i, u].Red;
                        byte m3 = image[i, u].Green;

                        for (int a = 0; a <= nbPixel; a++)
                        {
                            for (int b = 0; b <= nbPixel; b++)
                            {
                                resul.image[k + a, j + b] = new Pixel(m1,m2,m3);
                            }
                        }
                        j += nbPixel + 1;
                    }
                    j = 0;
                    k += nbPixel + 1;
                }
                return resul;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }

        public MyImage Réduire()
        {
            try
            {
                Console.WriteLine("De combien de pourcent voulez-vous réduire l'image ? ");
                int nbPourcent0 = Convert.ToInt32(Console.ReadLine());
                int nbPourcent = 1 - nbPourcent0 / 100;

                Pixel[,] mat = new Pixel[image.GetLength(0) * nbPourcent, image.GetLength(1) * nbPourcent];
                MyImage resul = new MyImage(mat, typeImage, image.GetLength(1) * nbPourcent * (image.GetLength(0) * nbPourcent) * 9 + tailleOffset, tailleOffset, image.GetLength(1) * nbPourcent, image.GetLength(0) * nbPourcent, nombreDeBitsCouleurs);
                
                int k = 0;
                int j = 0;
                for (int i = 0; i < resul.image.GetLength(0); i++)
                {
                    for (int u = 0; u < resul.image.GetLength(1); u++)
                    {
                        byte m1 = 0;
                        byte m2 = 0;
                        byte m3 = 0;
                        for (int a = 0; a <= nbPourcent0; a++)
                        { 
                            for (int b = 0; b <= nbPourcent0; b++)
                            {
                                m1 += image[k + a, j + b].Blue;
                                m2 += image[k + a, j + b].Red;
                                m3 += image[k + a, j + b].Green;
                            }
                        }
                        m1 = Convert.ToByte(m1 / nbPourcent0);
                        m2 = Convert.ToByte(m2 / nbPourcent0);
                        m3 = Convert.ToByte(m3 / nbPourcent0);

                        resul.image[i, u] = new Pixel(m1, m2, m3);

                        j += nbPourcent0 + 1;
                    }
                    j = 0;
                    k += nbPourcent0 + 1;
                }
                return resul;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }
        public MyImage Réduire2()
        {
            try
            {
                Console.WriteLine("De combien de pixels voulez-vous réduire l'image ? ");
                int nbPixel = Convert.ToInt32(Console.ReadLine());
                if (image.GetLength(0) % nbPixel == 0 && image.GetLength(1) % nbPixel == 0)
                {
                    Pixel[,] mat = new Pixel[image.GetLength(0) / nbPixel, image.GetLength(1) / nbPixel];
                    MyImage resul = new MyImage(mat, typeImage, image.GetLength(1) / nbPixel * (image.GetLength(0) / nbPixel) * 9 + tailleOffset, tailleOffset, image.GetLength(1) / nbPixel, image.GetLength(0) / nbPixel, nombreDeBitsCouleurs);

                    int k = 0;
                    int j = 0;
                    for (int i = 0; i < image.GetLength(0); i++)
                    {
                        for (int u = 0; u < image.GetLength(1); u++)
                        {
                            byte m1 = 0;
                            byte m2 = 0;
                            byte m3 = 0;

                            for (int a = 0; a <= nbPixel; a++)
                            {
                                for (int b = 0; b <= nbPixel; b++)
                                {
                                    m1 += image[k + a, j + b].Blue;
                                    m2 += image[k + a, j + b].Red;
                                    m3 += image[k + a, j + b].Green;
                                }
                            }
                            m1 = Convert.ToByte(m1 / nbPixel + 1);
                            m2 = Convert.ToByte(m2 / nbPixel + 1);
                            m3 = Convert.ToByte(m3 / nbPixel + 1);

                            resul.image[i,u] = new Pixel(m1, m2, m3);

                            j += nbPixel + 1;
                        }
                        j = 0;
                        k += nbPixel + 1;
                    }
                    return resul;
                }
                else
                {
                    Console.WriteLine("\nchoisir un autre nb de réduc");
                    MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                    return Im;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }
        #endregion


        #region Rotation
        public MyImage Rotation(int angle)
        {
            Pixel[,] im=image;
            int tailleFichierRes=tailleFichier;
            int largeurImageRes=largeurImage;
            int hauteurImageres=largeurImage;
            while (angle > 360) angle = angle - 360;
            if (angle == 90)
            {
                largeurImageRes = hauteurImage;
                hauteurImageres = largeurImage;
                tailleFichierRes = tailleFichier;
                im = new Pixel[hauteurImageres, largeurImageRes];
                for(int i = 0; i < hauteurImageres; i++)
                {
                    for(int j = 0; j < largeurImageRes; j++)
                    {
                        im[i, j] = image[j, i];
                    }
                }
            }else if (angle == 180)
            {

            }else if (angle == 270)
            {

            }else if (angle == 360)
            {

            }
            MyImage resul = new MyImage(im,typeImage,tailleFichierRes,tailleOffset,largeurImageRes,hauteurImageres,nombreDeBitsCouleurs);
            return resul;
        }
        #endregion

    }
}