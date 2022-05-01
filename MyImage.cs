using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Manon_Aubry_Manon_Goffinet
{
    public class MyImage
    {
        Pixel[,] image;
        string typeImage;
        int tailleFichier;
        int tailleOffset;
        int largeurImage;
        int hauteurImage;
        int nombreDeBitsCouleurs;
        int[] tabAléatoire;


        /// <summary>
        /// Constructeur créant une instance de MyImage a partir de tous les attributs de la classe.
        /// </summary>
        /// <param name="image">matrice de pixels correspondant à l'image</param>
        /// <param name="typeImage">format de l'image : BM pour bitmap par exemple</param>
        /// <param name="tailleFichier">taille en nombre d'octets du fichier global de l'image</param>
        /// <param name="tailleOffset">taille de l'offset en nombre d'octets</param>
        /// <param name="largeurImage">largeur de l'image en pixels</param>
        /// <param name="hauteurImage">hauteur de l'image en pixels</param>
        /// <param name="nombreDeBitsCouleurs">nombre de bits par couleur</param>
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
            byte[] nombreDeBitsCouleursEnBytes = new byte[] { Im[28], Im[29] }; //récupère les bytes 29,30
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
                //AfficherimPixel(image);
                //Console.WriteLine("\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            #endregion
        }

        #region Get&Set
        /// <summary>
        /// Méthode get de la matrice de pixel correspondant a l'image
        /// </summary>
        public Pixel[,] Image
        {
            get { return image; }
        }

        /// <summary>
        /// Méthode get du type Image = BM par exemple
        /// </summary>
        public string TypeImage
        {
            get { return typeImage; }
        }
        /// <summary>
        /// Méthode get de la taille fichier en octets
        /// </summary>
        public int TailleFichier
        {
            get { return tailleFichier; }
        }

        /// <summary>
        /// Méthode get de la taille offset en octets
        /// </summary>
        public int TailleOffset
        {
            get { return tailleOffset; }
        }

        /// <summary>
        /// Méthode get de la largeur image en pixel
        /// </summary>
        public int LargeurImage
        {
            get { return largeurImage; }
        }

        /// <summary>
        /// Méthode get de la hauteur image en pixel
        /// </summary>
        public int HauteurImage
        {
            get { return hauteurImage; }
        }

        /// <summary>
        /// Méthode get du nombre de bits couleurs
        /// </summary>
        public int NombreDeBitsCouleurs
        {
            get { return nombreDeBitsCouleurs; }
        }

        /// <summary>
        /// Get et set de tab Aléatoire
        /// </summary>
        public int[] TabAléatoire
        {
            get { return tabAléatoire; }
            set { tabAléatoire = value; }
        }


        #endregion

        /// <summary>
        /// Matrice permettant l'affichage d'une matrice de pixel
        /// </summary>
        /// <param name="tabPix">matrice de pixels a afficher</param>
        public void AfficherimPixel(Pixel[,] tabPix)
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
        /// Récupère la valeur d'un int, la converti en bytes (groupe de 8 chiffres binaires), et la met dans un tableau de bytes
        /// </summary>
        /// <param name="v">chiffre a convertir en bytes</param>
        /// <param name="taille">nombre de bytes sur lequel va être codé la valeur</param>
        /// <returns></returns>
        public byte[] Convertir_Int_To_Endian(int v, int taille)
        {

            //byte[] tabBytes = BitConverter.GetBytes(v);
            byte[] tabBytes = new byte[taille];
            for (int i = 0; i < taille; i++)
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
        /// récupère un tableau de bytes et le convertit en int
        /// </summary>
        /// <param name="v"> tableau de byte à convertir en int </param>
        /// <returns>nombre entier correspondant au tableau de byte</returns>
        public static int Convert_Endian_To_Int(byte[] v)
        {
            int res = 0;
            for (int i = 0; i < v.Length; i++)
            {
                res += (int)Math.Pow(256, i) * v[i];
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
            int ajout = 0;
            if ((largeurImage * (nombreDeBitsCouleurs / 8)) % 4 != 0)
            {
                ajout = 4 - (largeurImage * (nombreDeBitsCouleurs / 8) % 4);
            }
            int complementTailleIm = hauteurImage * ((nombreDeBitsCouleurs / 8 * largeurImage) + ajout);
            if (complementTailleIm != 0)
            {
                tailleFichier = tailleOffset + complementTailleIm;
            }
            

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
                        tab[i] = Convertir_Int_To_Endian(tailleOffset - 14, 4)[i - 14];
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
                        tab[i] = Convertir_Int_To_Endian(tailleFichier - tailleOffset, 4)[i - 34]; // Taille image codée entre les bytes 34 et 38
                        break;

                    case int j when j >= 38 && j < 42:
                        tab[i] = Convertir_Int_To_Endian(2835, 4)[i - 38];
                        break;
                    case int j when j >= 42 && j < 46:
                        tab[i] = Convertir_Int_To_Endian(2835, 4)[i - 42];
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

        #region matrice noir ou blanche

        /// <summary>
        /// Méthode renvoyant une matrice de pixels remplie de noir ou de blanc en fonction de C
        /// </summary>
        /// <param name="mat">matrice de pixels a mettre en noir ou blanc</param>
        /// <param name="C">C = N ou B</param>
        /// <returns></returns>
        public Pixel[,] MatriceNOIRouBLANCHE(Pixel[,]mat, char C)
        {

            for (int i = 0; i < mat.GetLength(0); i++) //met l'image en noir et blanc (pas de gris)
            {
                for (int u = 0; u < mat.GetLength(1); u++)
                {
                    if (C == 'N') mat[i, u] = new Pixel(0, 0, 0);
                    if (C == 'B') mat[i, u] = new Pixel(255, 255, 255);
                }
            }
            return mat;
        }
        #endregion

        #region Effet miroir
        /// <summary>
        /// recevoir une image et la transformer pour qu'elle devienne son reflet dans un miroir
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
                        //Console.Write(a[i, u].toString() + "   ");
                    }
                    //Console.WriteLine();
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
        /// <summary>
        /// Transforme une image couleur en image en nuances de gris.
        /// </summary>
        /// <returns>image en nuances de nuit</returns>
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

        /// <summary>
        /// Crée une nouvelle instance de MyImage qui sera agrandi de n pixels
        /// </summary>
        /// <returns>une instance d'image agrandie</returns>
        public MyImage Agrandir()
        {
            try
            {
                Console.WriteLine("De combien de pixels voulez-vous agrandir l'image ? ");
                int nbPixel = Convert.ToInt32(Console.ReadLine());

                Pixel[,] mat = new Pixel[image.GetLength(0) * (nbPixel + 1), image.GetLength(1) * (nbPixel + 1)];
                MyImage resul = new MyImage(mat, typeImage, image.GetLength(1) * (nbPixel + 1) * image.GetLength(0) * (nbPixel + 1) * 9 + tailleOffset, tailleOffset, image.GetLength(1) * (nbPixel + 1), image.GetLength(0) * (nbPixel + 1), nombreDeBitsCouleurs);

                int k = 0;
                int j = 0;
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int u = 0; u < image.GetLength(1); u++)
                    {
                        byte m1 = image[i, u].Blue;
                        byte m2 = image[i, u].Red;
                        byte m3 = image[i, u].Green;

                        for (int a = 0; a <= nbPixel; a++)
                        {
                            for (int b = 0; b <= nbPixel; b++)
                            {
                                resul.image[k + a, j + b] = new Pixel(m1, m2, m3);
                            }
                        }
                        j += nbPixel + 1;
                    }
                    j = 0;
                    k += nbPixel + 1;
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

        public MyImage Réduire()
        {
            try
            {
                Console.WriteLine("De combien de pourcent voulez-vous réduire l'image ? ");
                int multiple = Convert.ToInt32(Console.ReadLine());

                Pixel[,] mat = new Pixel[image.GetLength(0) / multiple, image.GetLength(1) / multiple];
                MyImage resul = new MyImage(mat, typeImage, image.GetLength(1) / multiple * (image.GetLength(0) / multiple) * 9 + tailleOffset, tailleOffset, image.GetLength(1) / multiple, image.GetLength(0) / multiple, nombreDeBitsCouleurs);

                for (int i = 0; i < resul.image.GetLength(0); i++) //remplir la im de pixel noir
                {
                    for (int u = 0; u < resul.image.GetLength(1); u++)
                    {
                        resul.image[i, u] = new Pixel(0, 0, 0);
                    }
                }

                int k = 0;
                int j = 0;
                for (int i = 0; i < resul.image.GetLength(0); i++)    //réduction
                {
                    for (int u = 0; u < resul.image.GetLength(1); u++)
                    {
                        byte m1 = Convert.ToByte(image[k, j].Blue);
                        byte m2 = Convert.ToByte(image[k, j].Red);
                        byte m3 = Convert.ToByte(image[k, j].Green);

                        resul.image[i, u] = new Pixel(m1, m2, m3);

                        j += multiple;
                    }
                    j = 0;
                    k += multiple;
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
        #endregion

        #region Rotation
        
        /// <summary>
        /// calcule la hauteur et la largeur après des rotations successives de 90°
        /// </summary>
        /// <param name="hauteurImageRes">hauteur image</param>
        /// <param name="largeurImageRes">largeur image</param>
        /// <param name="angleDegré">angle multiple de 90 dont /90 donne le nombre rotations par 90 à réaliser</param>
        /// <returns>un tableau de dim 2 de la forme {newHauteur,newLargeur}</returns>
        public int [] LongueuretHauteur90(int hauteurImageRes, int largeurImageRes, int angleDegré)
        {
            for (int i = 0; i < (angleDegré / 90); i++)
            {

                int a = largeurImageRes;
                largeurImageRes = hauteurImageRes;
                hauteurImageRes = a;

            }
            return new int[] { hauteurImageRes, largeurImageRes };
        }
        /// <summary>
        /// applique a la matrice de pixel les rotations multiples de 90 correspondant à angleDegré
        /// </summary>
        /// <param name="imag">matrice de pixel à modifier</param>
        /// <param name="hauteurImageRes">hauteur de l'image finale après rotation</param>
        /// <param name="largeurImageRes">largeur de l'image finale après rotation</param>
        /// <param name="angleDegré">angle en degré multiple de 90 dont il faut tourner l'image</param>
        /// <returns>matrice de pixels avec la rotation demandée appliquée</returns>
        public Pixel[,] Rotation90(Pixel[,] imag, int hauteurImage, int largeurImage, int angle)
        {

            Pixel[,] im = new Pixel[hauteurImage, largeurImage];

            for (int i = 0; i < hauteurImage; i++)
            {
                for (int j = 0; j < largeurImage; j++)
                {
                    im[i, j] = new Pixel(0, 0, 0);
                }
            }
            for (int i = 0; i < hauteurImage; i++)
            {
                for (int j = 0; j < largeurImage; j++)
                {
                    if (angle == 0)
                    {
                        im[i, j] = imag[i, j];
                    }
                    else if (angle == 90)
                    {
                        im[i, j] = imag[largeurImage - 1 - j, hauteurImage - 1 - i];

                    }
                    else if(angle == 180)
                    {
                        im[i, j] = imag[hauteurImage - 1 - i, largeurImage - 1 - j];
                    }
                    else if (angle == 270)
                    {
                        im[i, j] = imag[j, i];
                        
                    }
                    else if (angle== 360)
                    {
                        im[i, j] = imag[i, j];
                    }

                    
                }
            }
            return im;
        }

        /// <summary>
        /// Prend l'instance de MyImage et lui fait exécuter une rotation d'un angle quelconque en degré 
        /// </summary>
        /// <param name="angleDegré">angle en degré duquel l'image doit tourner</param>
        /// <returns>une nouvelle instance d'image tournée de l'angle en entrée</returns>
        public MyImage Rotation(int angleDegré)

        {
            //try
            {
                while (angleDegré > 360) angleDegré = angleDegré - 360; // permet d'avoir l'angle correspondant en degré inférieur à 360
                if (angleDegré < 0) angleDegré = 360 + angleDegré;
                Console.WriteLine(angleDegré);
                Pixel[,] im = new Pixel [hauteurImage,largeurImage];
                int tailleFichierRes = tailleFichier;
                int largeurImageRes = largeurImage;
                int hauteurImageRes = hauteurImage;
                
                for (int i = 0; i < hauteurImageRes; i++)
                {
                    for (int j = 0; j < largeurImageRes; j++)
                    {
                        im[i, j] = image[i, j];
                    }
                }
                
                
                if (angleDegré % 90 == 0)
                {
                    //Console.WriteLine("a");
                    Pixel[,] imag=new Pixel [im.GetLength(0),im.GetLength(1)];
                    for(int i = 0; i<hauteurImageRes; i++)
                    {
                        for(int j =0; j<largeurImageRes; j++)
                        {
                            imag[i, j] = im[i, j];
                        }
                    }

                    int[] hauteurLargeur = LongueuretHauteur90(hauteurImageRes, largeurImageRes, angleDegré);
                    hauteurImageRes = hauteurLargeur[0];
                    largeurImageRes = hauteurLargeur[1];
                    im = Rotation90(imag, hauteurLargeur[0], hauteurLargeur[1], angleDegré);
                    
                }
                else 
                {
                    
                    int angleRestant = angleDegré % 90; // si on a 184 récupère 4 
                    
                    int angle90 = angleDegré-angleRestant;// avec l'ex d'au dessus récupère 180
                    
                    Pixel[,] Rot90;// Image tournée de 90 degré le nombre de fois nécessaire pour ne plus qu'avoir un angle < 90° a tourner

                    if (angle90 !=0)
                    {
                        int[] hauteurLargeur = LongueuretHauteur90(hauteurImage, largeurImage, angle90);
                        hauteurImageRes = hauteurLargeur[0];
                        largeurImageRes = hauteurLargeur[1];
                    }
                    
                    Rot90 = Rotation90(im, hauteurImageRes, largeurImageRes, angle90);

                    im = new Pixel[Rot90.GetLength(0), Rot90.GetLength(1)];
                    for (int i = 0; i < im.GetLength(0); i++)
                    {
                        for (int j = 0; j < im.GetLength(1); j++)
                        {
                            im[i, j] = Rot90[i, j];
                        }
                    }
                    

                    double angle = Math.PI * angleRestant / 180; //passage en radiant de l'angle restant
                    double sin = Math.Sin(angle);
                    double cos = Math.Cos(angle);


                    largeurImageRes = Convert.ToInt32(Math.Abs(cos * Rot90.GetLength(1) + sin * Rot90.GetLength(0))); // a partir de la hauteur et largeur de l'image résultat des rotations de 90 on recalcule la hauteur et la largeu rde l'image
                    hauteurImageRes = Convert.ToInt32(Math.Abs(cos * Rot90.GetLength(0) + sin * Rot90.GetLength(1)));

                    int centreImageL = Rot90.GetLength(1) / 2;
                    int centreImageH = Rot90.GetLength(0) / 2;

                    int centreImageResL = largeurImageRes / 2;
                    int centreImageResH = hauteurImageRes / 2;


                    int centreImageIntermédiaireL = Convert.ToInt32(centreImageL * cos - centreImageH * sin);
                    int centreImageIntermédiaireH = Convert.ToInt32(centreImageL * sin + centreImageH * cos);


                   


                    im = new Pixel[hauteurImageRes, largeurImageRes];

                    

                    for (double i = 0; i < Rot90.GetLength(0); i+=0.5)
                    {
                        for (double j = 0; j < Rot90.GetLength(1); j+=0.5)
                        {
                            /*int x = (int)(sin * j + cos * i + centreImageResH - centreImageIntermédiaireH);
                            int y = (int)(cos * j - sin * i + centreImageResL - centreImageIntermédiaireL);
                            if (x>=0&&x<hauteurImageRes&& y >= 0 && y < hauteurImageRes)
                            {
                                im[x,y]= Rot90[(int)i, (int)j];
                            }*/
                            
                        }
                    }
                    for (int i = 0; i < hauteurImageRes; i++)
                    {
                        for (int j = 0; j < largeurImageRes; j++)
                        {
                            //if (im[i, j] == null)
                            {
                                im[i, j] = new Pixel(0, 0, 0); //remplissage de la matrice en noir pour que toute les pixels de l'image soient remplies
                                if (j == 0 || i == 0)
                                {
                                    im[i, j] = new Pixel(255, 255, 255);
                                }
                                if (j == 0 && i == 0)
                                {
                                    im[i, j] = new Pixel(255, 0, 0);
                                }
                            }

                        }
                    }

                }
                    
                int ajout = 0;
                int complementTailleIm = 0;
                if ((largeurImageRes * (nombreDeBitsCouleurs / 8)) % 4 != 0)
                {
                    ajout = 4 - (largeurImageRes * (nombreDeBitsCouleurs / 8) % 4);
                }

                complementTailleIm = hauteurImageRes * ((nombreDeBitsCouleurs / 8 * largeurImageRes) + ajout);



                if (complementTailleIm != 0)
                {
                    tailleFichierRes = tailleOffset + complementTailleIm;

                }
                else
                {
                    tailleFichierRes = tailleOffset + 3 * largeurImageRes * hauteurImageRes;
                }
                
                
                MyImage resul = new MyImage(im, typeImage, tailleFichierRes, tailleOffset, largeurImageRes, hauteurImageRes, nombreDeBitsCouleurs);
                
                return resul;
            }
            /*catch (Exception e)
            {
                Console.WriteLine("Problème dans la méthode Rotation : "+e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }*/

        }
        #endregion

        #region  Convolution

        /// <summary>
        /// Pour la convolution, calcule la valeur d'une couleur d'un pixel a placer dans la case de coordonnées (ligne, colonne) du résultat a partir du noyau et de la matrice de départ
        /// </summary>
        /// <param name="noyau">noyau de convolution (matrice 3*3 ou 9*9)</param>
        /// <param name="resul">matrice de pixels dans laquelle on souhaite appliquer la convolution</param>
        /// <param name="ligne">coordonnées de la ligne de la case dont on cherche à calculer la valeur après convolution</param>
        /// <param name="colonne">coordonnées de la colonne de la case dont on cherche à calculer la valeur après convolution</param>
        /// <param name="lettre">R, B ou G : correspond a quel couleur du pixel final on cherche à calculer</param>
        /// <returns></returns>
        public int Somme(int[,] noyau, Pixel[,] resul, int ligne, int colonne,char lettre)
        {
            try
            {
                int somme = 0;
                int ligneNoyau = noyau.GetLength(0);
                int colonneNoyau = noyau.GetLength(1);

                for (int i = 0; i < ligneNoyau; i++)
                {
                    for (int u = 0; u < colonneNoyau; u++)
                    {
                        /*int x = i + (ligne - ligneNoyau / 2);
                        if (x < 0) x = resul.GetLength(0) - 1;
                        if (x >= resul.GetLength(0)) x = 0;
                        int y = u + (colonne - colonneNoyau / 2);
                        if (y < 0) y = resul.GetLength(1) - 1;
                        if (y >= resul.GetLength(1)) y = 0;
                        somme += resul[x, y].Red * noyau[i, u];*/
                        if (lettre == 'R') somme += resul[ligne - 1 + i, colonne - 1 + u].Red * noyau[i, u];
                        if (lettre == 'B') somme += resul[ligne - 1 + i, colonne - 1 + u].Blue * noyau[i, u];
                        if (lettre == 'G') somme += resul[ligne - 1 + i, colonne - 1 + u].Green * noyau[i, u];
                    }
                }
                if (somme < 0) somme = 0;
                if (somme > 255) somme = 255;
                return somme;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            
        }

        /// <summary>
        /// Applique la convolution a partir du noyau souhaité
        /// </summary>
        /// <param name="noyau">noyau de convolution</param>
        /// <returns></returns>
      
        public MyImage Convolution(int[,]noyau)
        {
            try
            {
                //resultat = resultat.ConvertToGrey();


                Pixel[,] mat0 = new Pixel[image.GetLength(0), image.GetLength(1)];
                Pixel[,] imagecalcul = MatriceNOIRouBLANCHE(mat0, 'N');

                /*Pixel[,] imagecalcul = new Pixel[image.GetLength(0), image.GetLength(1)];
               
                for (int i = 0; i < imagecalcul.GetLength(0); i++) //met l'image en noir et blanc (pas de gris)
                {
                    for (int u = 0; u < imagecalcul.GetLength(1); u++)
                    {
                        imagecalcul[i, u] = new Pixel(0, 0, 0);
                    }
                }*/
                    //int[,] mat1 = { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } }; //matrice double
                    //int[,] mat2 = { { -1, -1, -1 }, { 0, 0, 0 }, { 1, 1, 1 } };//matrice double
                    //int[,] noyau = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };  //matrice détection des bords  /docgimps.org
                    //int[,] noyau = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };  //matrice détection des bords  /docgimps.org
                    //int[,] noyau = { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } }; //matrice renforcement des bords /docgimps.org
                    //int[,] noyau = { { 0, 1, 2 }, { -1, 1, 1 }, { -2, -1, 0 } }; //matrice REPOUSSAGE /docgimps.org//
                    //int[,] noyau = { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } }; //matrice détection des bords  /wiki
                    //int[,] noyau = { { -1, 0, 1 }, { 0, 0, 0 }, { 1, 0, -1 } };  //matrice détection des bords  /wiki
                    //int[,] noyau = { { 1, 0, -1 }, { 0, 1, 0 }, { -1, 0, 1 } };  //test
                    //int[,] noyau = { { 1, 0, -1 }, { 1, 0, -1 }, { -1, 0, 1 } };  //test
                    //int[,] noyau = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };  //matrice test  /wiki

                    for (int i = 1; i < image.GetLength(0) - 1; i++)
                {
                    for (int u = 1; u < image.GetLength(1) - 1; u++)
                    {
                        imagecalcul[i, u].Red = Convert.ToByte(Somme(noyau, image, i, u, 'R'));
                        imagecalcul[i, u].Blue = Convert.ToByte(Somme(noyau, image, i, u, 'B'));
                        imagecalcul[i, u].Green = Convert.ToByte(Somme(noyau, image, i, u, 'G'));
                    }
                }
                MyImage resultat = new MyImage(imagecalcul, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return resultat;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }
        #endregion

        #region Flou

        /// <summary>
        /// Prend une instance de MyImage et la floute
        /// </summary>
        /// <returns>une nouvelle instance d'image floutée</returns>
        public MyImage Flou()
        {
            try
            {
                Pixel[,] mat0 = new Pixel[image.GetLength(0), image.GetLength(1)];
                Pixel[,] imagecalcul = MatriceNOIRouBLANCHE(mat0, 'N');
                                
                int[,] mat = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                for (int i = 1; i < image.GetLength(0) - 1; i++)
                {
                    for (int u = 1; u < image.GetLength(1) - 1; u++)
                    {
                        imagecalcul[i, u].Red = Convert.ToByte((mat[0, 0] * image[i - 1, u - 1].Red + mat[0, 1] * image[i - 1, u].Red + mat[0, 2] * image[i - 1, u + 1].Red + mat[1, 0] * image[i, u - 1].Red + mat[1, 1] * image[i, u].Red + mat[1, 2] * image[i, u + 1].Red + mat[2, 0] * image[i + 1, u - 1].Red + mat[2, 1] * image[i + 1, u].Red + mat[2, 2] * image[i + 1, u + 1].Red) / 9);
                        imagecalcul[i, u].Blue = Convert.ToByte((mat[0, 0] * image[i - 1, u - 1].Blue + mat[0, 1] * image[i - 1, u].Blue + mat[0, 2] * image[i - 1, u + 1].Blue + mat[1, 0] * image[i, u - 1].Blue + mat[1, 1] * image[i, u].Blue + mat[1, 2] * image[i, u + 1].Blue + mat[2, 0] * image[i + 1, u - 1].Blue + mat[2, 1] * image[i + 1, u].Blue + mat[2, 2] * image[i + 1, u + 1].Blue) / 9);
                        imagecalcul[i, u].Green = Convert.ToByte((mat[0, 0] * image[i - 1, u - 1].Green + mat[0, 1] * image[i - 1, u].Green + mat[0, 2] * image[i - 1, u + 1].Green + mat[1, 0] * image[i, u - 1].Green + mat[1, 1] * image[i, u].Green + mat[1, 2] * image[i, u + 1].Green + mat[2, 0] * image[i + 1, u - 1].Green + mat[2, 1] * image[i + 1, u].Green + mat[2, 2] * image[i + 1, u + 1].Green) / 9);
                    }
                }
                MyImage resultat = new MyImage(imagecalcul, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);

                return resultat;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }
        #endregion

        #region  Repoussage

        /// <summary>
        /// Prend une instance de MyImage et repousse les bords
        /// </summary>
        /// <returns>une nouvelle instance avec les bords repoussés</returns>
        public MyImage Repoussage()
        {
            try
            {
                int[,] noyau = { { 0, 1, 2 }, { -1, 1, 1 }, { -2, -1, 0 } };
                MyImage res = Convolution(noyau);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }
        #endregion

        #region Détection des bords
        /// <summary>
        /// Prend une instance de MyImage et lui applique un filtre de détection des bords
        /// </summary>
        /// <returns></returns>
        public MyImage DétectionDesBords()
        {
            try
            {   
                int[,] noyau = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                MyImage res = Convolution(noyau);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }

        }
        #endregion

        #region Renforcement des bords

        /// <summary>
        /// Prend une instance de MyImage et lui applique un filtre de renforcement des bords
        /// </summary>
        /// <returns>Une nouvelle instance de MyImage avec le filtre renforcement des bords</returns>
        public MyImage RenforcementDesBords()
        {
            try
            {
                int[,] noyau = { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
                MyImage res = Convolution(noyau);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }
        #endregion

        #region Fractale

        /// <summary>
        /// Prend une instance de MyImage et créé sa fractale associée en couleur
        /// </summary>
        /// <returns>une nouvelle instance de MyImage de fractale en couleur</returns>
        public MyImage FractaleCOULEURS()
        {
            try
            {
                double x1 = -2.1;
                double x2 = 0.6;
                double y1 = -1.2;
                double y2 = 1.2;
                int zoom = 100; //pour une distance de 1 sur le plan, on a 100 pixels sur l'image
                double iteration_Max = 50 * image.GetLength(1) / 240;
                int imageX = (int)((x2 - x1) * zoom + 1);
                int imageY = (int)((y2 - y1) * zoom + 1);

                Pixel[,] mat0 = new Pixel[imageX, imageY];
                Pixel[,] resultat = MatriceNOIRouBLANCHE(mat0, 'B'); //mettre la fonction en noir


                for (int x = 0; x < imageX; x++)
                {
                    for (int y = 0; y < imageY; y++)
                    {
                        double cR = x / zoom + x1;
                        double cI = y / zoom + y1;
                        double zR = 0;
                        double zI = 0;
                        int i = 0;

                        do
                        {
                            double tmp = zR;
                            zR = Math.Pow(zR, 2) - Math.Pow(zI, 2) + cR;
                            zI = 2 * zI * tmp + cI;
                            i++;
                        } while ((zR * zR + zI * zI) < 4 && i < iteration_Max);

                        if (i == iteration_Max)
                        {
                            resultat[x, y] = new Pixel(0, 0, 0);
                            //Console.Write(resultat[x, y] + " ");
                        }
                        else
                        {
                            resultat[x, y] = new Pixel((byte)(i * 255 / iteration_Max), 0, 0 ); //(byte)((10 * i) % 256), (byte)((3 * i) % 256), (byte)((1 * i) % 256));  
                                                                                               //Console.Write(resultat[x, y] + " ");
                        }
                        //Console.Write(zR+" "+zI+"  "+i+" "+iteration_Max);
                        //i++;
                    }
                }
                MyImage res = new MyImage(resultat, typeImage, tailleOffset + (resultat.GetLength(1) * resultat.GetLength(0) * 3), tailleOffset, resultat.GetLength(1), resultat.GetLength(0), nombreDeBitsCouleurs);

                return res;
                //screen.set_at((x, y), ((3 * n) % 256, (1 * n) % 256, (10 * n) % 256))   ---(byte)((3 * i) % 256), (byte)((1 * i) % 256), (byte)((10 * i) % 256)
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }

        }
       
        ///<summary>
        /// Prend une instance de MyImage et créé sa fractale associée en noir et blanc
        /// </summary>
        /// <returns>une nouvelle instance de MyImage de fractale en noir et blanc</returns>
        public MyImage FractaleNOIR()
        {
            try
            {
                double x1 = -2.1;
                double x2 = 0.6;
                double y1 = -1.2;
                double y2 = 1.2;
                double iteration_Max = 50 * image.GetLength(1) / 240;

                int imageX = image.GetLength(0);
                int imageY = image.GetLength(1);

                double zoomX = imageX / (x2 - x1);
                double zoomY = imageY / (y2 - y1);

                Pixel[,] mat0 = new Pixel[imageX, imageY];
                Pixel[,] resultat = MatriceNOIRouBLANCHE(mat0, 'B'); //mettre la fonction en noir

                for (int x = 0; x < imageX; x++)
                {
                    for (int y = 0; y < imageY; y++)
                    {
                        double cR = x / zoomX + x1;
                        double cI = y / zoomY + y1;
                        double zR = 0;
                        double zI = 0;
                        int i = 0;

                        do
                        {
                            double tmp = zR;
                            zR = Math.Pow(zR, 2) - Math.Pow(zI, 2) + cR;
                            zI = 2 * zI * tmp + cI;
                            i++;
                        } while (zR * zR + zI * zI < 4 && i < iteration_Max);

                        if (i == iteration_Max)
                        {
                            resultat[x, y] = new Pixel(0, 0, 0);
                            //Console.Write(resultat[x, y] + " ");
                        }
                    }
                }
                MyImage res = new MyImage(resultat, typeImage, tailleOffset + (resultat.GetLength(1) * resultat.GetLength(0) * 3), tailleOffset, resultat.GetLength(1), resultat.GetLength(0), nombreDeBitsCouleurs);

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MyImage Im = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return Im;
            }
        }
        #endregion

        #region Histogramme

        /// <summary>
        /// Permet d'afficher les valeurs successives de l'histogramme pour chaque pixel
        /// </summary>
        /// <param name="tab">tableau de valeurs de l'histogramme ass</param>
        public void AffichageHistogramme(int[] tab)
        {
            Console.WriteLine("Histogramme de l'image : \n");
            for(int i =0; i < tab.Length; i++)
            {
                if(i < 10)
                {
                    Console.Write(" ." + i + "   ");
                }
                else if (i<100)
                {
                    Console.Write(" ." + i + "  ");
                }
                else
                {
                    Console.Write(" ." + i + " ");
                }

            }
            Console.WriteLine();
            for (int i = 0; i < tab.Length; i++)
            {
            //if (tab[i] < 10)
                {
                Console.Write(" |" + tab[i] + "   ");
                }


            }
    }   
    public MyImage Histogramme()
    {
        int[] Rouge = new int[256];
        int[] Vert = new int[256];
        int[] Bleu = new int[256];
        Pixel[,] His;

        for (int i =0; i < Rouge.Length; i++)
        {
            Rouge[i] = 0;
            Vert[i] = 0;
            Bleu[i] = 0;
        }


        int max = 0;
        for(int i = 0; i < image.GetLength(0); i++)
        {
            for(int j =0; j<image.GetLength(1); j++)
            {
                Rouge[image[i, j].Red]++;
                Vert[image[i, j].Green]++;
                Bleu[image[i, j].Blue]++;
                if(Rouge[image[i, j].Red]>= Vert[image[i, j].Green] && Rouge[image[i, j].Red]>=Bleu[image[i, j].Blue] && Rouge[image[i, j].Red] >=max)
                {
                    max = Rouge[image[i, j].Red];
                }
                else if(Vert[image[i, j].Green] >= Rouge[image[i, j].Red] && Vert[image[i, j].Green] >= Bleu[image[i, j].Blue] && Vert[image[i, j].Green] >= max)
                {
                    max = Vert[image[i, j].Green];
                }
                else if(Bleu[image[i, j].Blue] >= Rouge[image[i, j].Red] && Bleu[image[i, j].Blue]>= Vert[image[i, j].Green] && Bleu[image[i, j].Blue] >= max)
                {
                    max = Bleu[image[i, j].Blue];
                }
            }
        }
        His = new Pixel[max, 768];

         for (int i = 0; i < His.GetLength(0); i++)
         {
              for (int j = 0; j < His.GetLength(1); j++)
              {
                  if (His[i, j] == null)
                  {
                    His[i, j] = new Pixel(0, 0, 0);
                  }
              }
         }

        for (int i = 0; i < His.GetLength(0); i++)
        {
            for (int j = 0; j < His.GetLength(1); j++)
            {
                if (j < 256&&Rouge[j]!=0)
                {
                    His[i, j].Red = 255;
                    Rouge[j]--;
                }
                else if (j < 512 && j>=256&& Vert[j-256] != 0)
                {
                    His[i, j].Green = 255;
                    Vert[j-256]--;
                }
                else if(j>=512&&Bleu[j-512] != 0)
                {
                    His[i, j].Blue = 255;
                    Bleu[j-512]--;
                }
            }
        }

        int tailleFichierRes = tailleOffset + His.GetLength(0) * His.GetLength(1) * 3;
       // AffichageHistogramme(Rouge);
        MyImage resultat = new MyImage(His, typeImage, tailleFichierRes, tailleOffset, His.GetLength(1), His.GetLength(0), nombreDeBitsCouleurs);
        return resultat;
    }
        #endregion

        #region MyImage NOIR et BLANC
        public MyImage NoirETBlanc()
        {
            try
            {
                Pixel[,] resul1 = new Pixel[image.GetLength(0), image.GetLength(1)]; 

                for (int i = 0; i < image.GetLength(0); i++) //met l'image en noir et blanc (pas de gris)
                {
                    for (int u = 0; u < image.GetLength(1); u++)
                    {
                        double valeur = image[i, u].Red + image[i, u].Blue + image[i, u].Green;

                        if ((valeur / 3) <= 127) resul1[i, u] = new Pixel(0, 0, 0);
                        else resul1[i, u] = new Pixel(255, 255, 255);
                    }
                }
                MyImage res = new MyImage(resul1, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                return res;
                }
            catch (Exception e)
            {
                MyImage image2 = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                Console.WriteLine(e.Message);
                return image2;
            }
        }
        #endregion

        #region Coder une image
        public MyImage CoderImage()
        {
            try
            {
                Pixel NOIR = new Pixel(0, 0, 0);        //création d'un pixel noir pour pouvoir comparer facilement dans le for suivant
                Pixel BLANC = new Pixel(255, 255, 255); //création d'un pixel blanc pour pouvoir comparer facilement dans le for suivant

                Pixel[,] resul1 = new Pixel[image.GetLength(0), image.GetLength(1)]; //matrice résultat n°1
                
                for (int i = 0; i < image.GetLength(0); i++) //met l'image en noir et blanc (pas de gris)
                {
                    for (int u = 0; u < image.GetLength(1); u++)
                    {
                        double valeur = image[i, u].Red + image[i, u].Blue + image[i, u].Green;

                        if ((valeur / 3) <= 127) resul1[i, u] = NOIR;
                        else resul1[i, u] = BLANC;
                    }
                }

                //création d'un tableau aléatoire de 0 et 1
                //il a la même taille que l'image à chiffrer : chaque case = un bit (0 ou 1)
                TabAléatoire = new int[image.GetLength(0) * image.GetLength(1)];
                Pixel[,] resul2 = new Pixel[image.GetLength(0), image.GetLength(1)]; // matrice résultat n°2
                Random aleatoire = new Random();
                for (int i = 0; i < TabAléatoire.Length; i++)
                {
                    int entier = aleatoire.Next(2); //Génère un entier aléatoire positif : 0 ou 1
                    TabAléatoire[i] = entier;

                }

                //chiffrage entre le tableau aléatoire et l'image source avec XOR (OU EXCLUSIF) 
                int a = 0;
                for (int i = 0; i < resul1.GetLength(0); i++)
                {
                    for (int u = 0; u < resul1.GetLength(1); u++)
                    {
                        if (TabAléatoire[a] == 0 && resul1[i, u] == NOIR) resul2[i, u] = NOIR;
                        if (TabAléatoire[a] == 1 && resul1[i, u] == NOIR) resul2[i, u] = BLANC;
                        if (TabAléatoire[a] == 0 && resul1[i, u] == BLANC) resul2[i, u] = BLANC;
                        if (TabAléatoire[a] == 1 && resul1[i, u] == BLANC) resul2[i, u] = NOIR;
                        a++;
                    }
                }

                //on applique les biPixel clé :
                //si on a 0 ds la matrice source resul2, on met un pixel blanc/noir dans la matrice résultat
                //si on a 1 ds la matrice source resul2, on met un pixel noir/blanc dans la matrice résultat

                Pixel[,] resul3 = new Pixel[resul1.GetLength(0), resul1.GetLength(1)*2]; //matrice résultat n°3
                int j = 0;
                for (int i = 0; i < resul2.GetLength(0); i++)
                {
                    for (int u = 0; u < resul2.GetLength(1); u++)
                    {
                        if (resul2[i, u] == NOIR)
                        {
                            resul3[i, j] = BLANC;
                            resul3[i, j + 1] = NOIR;
                        }
                        if (resul2[i, u] == BLANC)
                        {
                            resul3[i, j] = NOIR;
                            resul3[i, j + 1] = BLANC;
                        }
                        j+=2;
                    }
                    j = 0;
                }
                
                MyImage res = new MyImage(resul3, typeImage, resul3.GetLength(1) * resul3.GetLength(0)*3+ tailleOffset, tailleOffset, resul3.GetLength(1), resul3.GetLength(0), nombreDeBitsCouleurs);
                return res;
            }
            catch (Exception e)
            {
                MyImage image2 = new MyImage(image, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                Console.WriteLine(e.Message);
                return image2;
            }
        }
        #endregion

        #region Décoder une image
        public MyImage DECoderImage(MyImage Classedeimagecoder)
        {
            try
            {
                Pixel NOIR = new Pixel(0, 0, 0);          //création d'un pixel noir pour pouvoir comparer facilement dans le for suivant
                Pixel BLANC = new Pixel(255, 255, 255);   //création d'un pixel blanc pour pouvoir comparer facilement dans le for suivant

                Pixel[,] imageCODER = Classedeimagecoder.Image; //pour une simplification du code
                
                Pixel[,] mat0 = new Pixel[imageCODER.GetLength(0), imageCODER.GetLength(1)/2];  //matrice résultat n°1
                Pixel[,] resul1 = MatriceNOIRouBLANCHE(mat0, 'N');
                
                for (int i = 0; i < resul1.GetLength(0); i++) //met l'image en noir et blanc (pas de gris)
                { 
                    for (int u = 0; u < resul1.GetLength(1); u++)
                    {
                        resul1[i, u] = NOIR; //new Pixel(0, 0, 0);
                    }
                }

                int j = 0;
                for (int i = 0; i < resul1.GetLength(0); i++)  //transformer l'image codé grace à la clé, en une image qui est le résultat du XOR
                {
                    for (int u = 0; u < resul1.GetLength(1); u++)
                    {
                        if (imageCODER[i, j].Red == 0 && imageCODER[i, j + 1].Red == 255) resul1[i, u] = BLANC; // new Pixel(255, 255, 255);
                        if (imageCODER[i, j].Red == 255 && imageCODER[i, j + 1].Red == 0) resul1[i, u] = NOIR; // new Pixel(0, 0, 0);
                        j += 2;
                        //Console.Write("clé ");
                    }
                    j = 0;
                }
                //Console.WriteLine("\n étape 1 fait\n\n");

                //déchiffrage entre le tableau aléatoire et l'image codé passer à la clé, avec XOR (OU EXCLUSIF) 

                Pixel[,] resul2 = new Pixel[resul1.GetLength(0), resul1.GetLength(1)]; 
                for (int i = 0; i < resul2.GetLength(0); i++) //MET LA MAT 2 EN BLANC POUR TESTER 
                {
                    for (int u = 0; u < resul2.GetLength(1); u++)
                    {
                        resul2[i, u] = BLANC; // new Pixel(255,255,255);
                    }
                }
                int a = 0;

                for (int i = 0; i < resul2.GetLength(0); i++)
                {
                    for (int u = 0; u < resul2.GetLength(1); u++)
                    {
                        if (TabAléatoire[a] == 0 && resul1[i, u].Red == 0) resul2[i, u] = NOIR; // new Pixel(0, 0, 0);
                        if (TabAléatoire[a] == 1 && resul1[i, u].Red == 0) resul2[i, u] = BLANC; // new Pixel(255, 255, 255);
                        if (TabAléatoire[a] == 0 && resul1[i, u].Red == 255) resul2[i, u] = BLANC; // new Pixel(255, 255, 255);
                        if (TabAléatoire[a] == 1 && resul1[i, u].Red == 255) resul2[i, u] = NOIR; // new Pixel(0, 0, 0);
                        a++;
                        //Console.Write("xor ");
                    }
                }
                //Console.WriteLine("\nétape 2 fait ");

                MyImage image2 = new MyImage(resul2, typeImage, resul2.GetLength(1) * resul2.GetLength(0) * 3 + tailleOffset, tailleOffset, resul2.GetLength(1), resul2.GetLength(0), nombreDeBitsCouleurs);
                //Console.WriteLine("\nétape 3 fait ");

                return image2;
            }
            catch (Exception e)
            {
                Pixel[,] resul1 = new Pixel[image.GetLength(0), image.GetLength(1)];

                for (int i = 0; i < resul1.GetLength(0); i++) //met l'image dans une couleurs différente de noir et blanc pour tester si ça crache
                {
                    for (int u = 0; u < resul1.GetLength(1); u++)
                    {
                        resul1[i, u] = new Pixel(0, 225, 255);
                    }
                }
                MyImage image4 = new MyImage(resul1, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                Console.WriteLine(e.Message);
                return image4;
            }
        }
        #endregion

       
    }
}