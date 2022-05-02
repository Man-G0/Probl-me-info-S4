using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manon_Aubry_Manon_Goffinet
{
    class QRcode
    {
        string phrase;
        string bytes;
        char typeCorrection;
        string correction;
        int version;
        string masque;
        /// <summary>
        /// Constructeur créant une instance de QRcode à partir d'une phrase
        /// </summary>
        /// <param name="phrase">phrase à encoder dans le QRcode (doit être inférieure ou égale à 47caractères)</param>

        public QRcode(string phrase)
        {
            this.phrase = phrase;
            typeCorrection = 'L';
            masque = "111011111000100";
        }

        #region get et set
        /// <summary>
        /// Get et set de la phrase du QrCode
        /// </summary>
        public string Phrase
        {
            get { return phrase; }
            set { phrase = value; }
        }

        /// <summary>
        /// Get et set de la phrase du QrCode
        /// </summary>
        public string Bytes
        {
            get { return bytes; }
            set { bytes = value; }
        }

        #endregion

        #region public byte[] Convertir_Int_To_Binaire(int val …)
        //public byte[] Convertir_Int_To_Endian(int val …) convertit un entier en séquence d’octets au format little endian 


        /// <summary>
        /// Récupère la valeur d'un int, la converti en binaire, et la met dans un tableau de bytes en complétant à droite par le nombre de 0 manquant 
        /// </summary>
        /// <param name="v">chiffre a convertir en bytes</param>
        /// <param name="taille">nombre de bits sur lequel va être codé la valeur</param>
        /// <returns></returns>
        public List<byte> Convertir_Int_To_Binaire(int v, int taille)
        {
            try
            {
                
                int reste;
                List<byte> b = new List<byte> { };
                List<byte> binaire = new List<byte> { };
                //Console.WriteLine();
                while (v >=1)
                {
                    
                    reste = v % 2;
                    v /= 2;
                    if (reste >= 1)
                    {
                        b.Add(1);
                    }
                    else
                    {
                        b.Add(0);
                    }
                    
                    
                }
                if (b.Count < taille)
                {
                    int a = taille - b.Count;
                    for(int i = 0; i<a; i++)
                    {
                        b.Add(0);
                    }                                          
                }
                //AffichageListebytes(b);
                return b;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }


        #endregion

        #region Convertion binaireToInt

        /// <summary>
        /// prends un chiffre binaire et donne sa valeur en int
        /// </summary>
        /// <param name="a">le binaire à convertir</param>
        /// <returns>le int trouvé</returns>
        public int ConvertionBinaireToInt(string a)
        {
            int b = Convert.ToInt32(a,2);
            return b;
        }
        #endregion

        #region From_Binary_To_Byte
        public static byte[] From_Binary_To_Byte(string chaine)
        {
            byte[] result = new byte[chaine.Length / 8];

            int val = 0;
            int compt = 0;
            for (int i = chaine.Length - 1; i > 0; i = i - 8)
            {
                val = 0;
                for (int j = 0; j < 8; j++)
                {
                    val += Convert.ToInt32((Convert.ToInt32(chaine[i - j]) - 48) * Math.Pow(2, j));

                }
                result[result.Length - 1 - compt] = Convert.ToByte(val);
                compt++;
            }

            return result;
        }
        #endregion

        #region Affichage Listes
        /// <summary>
        /// Affiche une liste de bytes
        /// </summary>
        /// <param name="listebytes">liste de bytes à afficher</param>
        public void AffichageListebytes(List<byte> listebytes)
        {
            for(int i = 0;i<listebytes.Count; i++)
            {
                Console.Write(listebytes[i] + " ");            
            }
            Console.WriteLine("\n");
        }
        public void AffichageStrings(string bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                
                Console.Write(bytes[i] + "");
                if ((i+1) % 8 == 0)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine("\n");
        }
        #endregion


        #region NbCaractères
        /// <summary>
        /// Calcule le nombre de caractères d'une chaine de caractères (string)
        /// </summary>
        /// <param name="phrase">chaine de caractères dont on doit calculer le nombre de caractères</param>
        /// <returns></returns>
        public int NbCaractères(string phrase)
        {
            try
            {
                /*Pixel[,] QRc = new Pixel[21, 21];
                QRc[0, 0] = new Pixel(0, 0, 0); ;

                for (int i = 0; i < QRc.GetLength(0); i++) //mettre en bleu
                {
                    for (int u = 0; u < QRc.GetLength(1); u++)
                    {
                        //if(i<=7 && u<=7) QRc[i, u] = NOIR;
                        QRc[i, u] = new Pixel(200, 200, 200); // new Pixel(120, 120, 120);
                    }
                }
                QRc[0, 0] = NOIR;
                QRc[0, 1] = NOIR;
                QRc[0, 6] = NOIR;
                QRc[6, 6] = NOIR;
                QRc[6, 1] = NOIR;
                QRc[6, 8] = NOIR;

                int a = 0;
                for(int i = 0; i < 7; i++)
                {
                    if (i == 2 || i==4) a++;
                    for(int u = 0; u < 7; u++)
                    {
                        QRc[i, u + a] = new Pixel(0, 0, 0); ;   //en bas à gauche
                        QRc[QRc.GetLength(0) - 1 - i, QRc.GetLength(1)-1 - u] = new Pixel(0, 0, 0); ;  //en haut à gauche
                        //QRc[i, QRc.GetLength(1) - 1 - u] = NOIR;  //en haut à droite
                    }
                }

                MyImage resultat = new MyImage(QRc, typeImage, tailleOffset + QRc.GetLength(0) * QRc.GetLength(1) * 3, tailleOffset, QRc.GetLength(1), QRc.GetLength(0), nombreDeBitsCouleurs);
                return resultat;*/

                List<byte> chaineASCII = new List<byte>();
                chaineASCII.Add(0010);  // le type d'information est alphanumérique

                int nbDeCaractère = 0;
                for (int i = 0; i < phrase.Length; i++)
                {
                    nbDeCaractère++;
                }
                byte nbDeCaractèreEnByte = (byte)nbDeCaractère;
                //chaineASCII.Add(000001011); //le nombre de caractère

                return nbDeCaractère;
            }
            catch (Exception e)
            {
                /*Pixel[,] resul1 = new Pixel[image.GetLength(0), image.GetLength(1)];

                for (int i = 0; i < resul1.GetLength(0); i++) //met l'image dans une couleurs différente de noir et blanc pour tester si ça crache
                {
                    for (int u = 0; u < resul1.GetLength(1); u++)
                    {
                        resul1[i, u] = new Pixel(0, 225, 255); //met en jaune
                    }
                }
                MyImage image4 = new MyImage(resul1, typeImage, tailleFichier, tailleOffset, largeurImage, hauteurImage, nombreDeBitsCouleurs);
                Console.WriteLine(e.Message);*/

                Console.WriteLine(e.Message);
                return 65;
            }
        }
        #endregion

        #region CodeAsciiCaractères
        /// <summary>
        /// récupère un caractère et calcule sa valeur en code ASCII
        /// </summary>
        /// <param name="a">caractère à traduire en code ascii</param>
        /// <returns></returns>
        public int CodeAsciiCaractères(char a)
        {
            try
            {
                int codeAscii = 0;
                if ((int)a > 47 && (int)a < 58)
                {
                    codeAscii = (int)a - 47;
                }
                else if ((int)a > 10 + 55 && (int)a < 36 + 55) ///lettres 
                {
                    codeAscii = (int)a - 55;
                }
                else if (a == ' ')
                {
                    codeAscii = 36;
                }
                return codeAscii;
            }
            catch(Exception e)
            {
                Console.WriteLine("CodeAsciiCaractères : " + e.Message);
                return 0;
            }
           
        }
        #endregion

        #region Chaine de données

        /// <summary>
        /// Calcule la chaine de données a partir de la phrase
        /// </summary>
        /// <param name="nbCaractères">nombre de caractères de la phrase</param>
        /// <param name="liste">début de la chaine de données</param>
        /// <param name="nbDeBitsTotalChaine">taille maximale de la chaine de données</param>
        /// <returns></returns>
        public void ChaineDeDonnées(int nbCaractères, List<byte> liste, int nbDeBitsTotalChaine,int nbOctetsCorrection)
        {
            List<byte> nbCaractèresEnbytes = Convertir_Int_To_Binaire(nbCaractères, 9);
            List<byte> listebytes = new List<byte>();


            for (int i = 0; i < nbCaractèresEnbytes.Count; i++)
            {
                liste.Add(nbCaractèresEnbytes[nbCaractèresEnbytes.Count - 1 - i]);
            }
            int a = 0;
            while (a < phrase.Length)
            {
                if (a + 1 < phrase.Length - 1)
                {

                    char char1 = phrase[a];
                    char char2 = phrase[a + 1];

                    int codeASCIILettres = 45 * CodeAsciiCaractères(char1) + CodeAsciiCaractères(char2);
                    List<byte> LettresBinaires = Convertir_Int_To_Binaire(codeASCIILettres, 11);
                    for (int i = 0; i < LettresBinaires.Count; i++)
                    {
                        liste.Add(LettresBinaires[LettresBinaires.Count - 1 - i]);
                    }
                    a += 2;
                }
                else
                {
                    char char1 = phrase[a];
                    int codeASCIILettres = CodeAsciiCaractères(char1);
                    List<byte> LettresBinaires = Convertir_Int_To_Binaire(codeASCIILettres, 6);

                    for (int i = 0; i < LettresBinaires.Count; i++)
                    {
                        liste.Add(LettresBinaires[LettresBinaires.Count - 1 - i]);

                    }
                    Console.WriteLine();
                    a++;
                }

            }/// jusque la les bytes contenu sont ceux du type d'informations utilisées: 0010 pour alpha num + les infos de la phrase par groupe de deux caractères  
            //AffichageListebytes(liste);
            if (liste.Count % 8 != 0)/// ce if permet de compléter le byte incomplet a la fin de la chaine si besoin
            {
                for (int i = 0; i < liste.Count % 8; i++)
                {
                    liste.Add(0);
                }
            }

            int temp = liste.Count;

            for (int i = 0; i < (nbDeBitsTotalChaine - temp) / 8; i++) /// ici on complète les bytes vides restant pour atteindre le maximum de la chaine en V1 : 152
            {

                if (i % 2 == 0)//11101100
                {
                    liste.Add(1);
                    liste.Add(1);
                    liste.Add(1);
                    liste.Add(0);
                    liste.Add(1);
                    liste.Add(1);
                    liste.Add(0);
                    liste.Add(0);
                }
                else //00010001
                {
                    liste.Add(0);
                    liste.Add(0);
                    liste.Add(0);
                    liste.Add(1);
                    liste.Add(0);
                    liste.Add(0);
                    liste.Add(0);
                    liste.Add(1);
                }

               
                
            }



            int b = 0;
            while (b < liste.Count)
            {
                string t = Convert.ToString(liste[b]) + Convert.ToString(liste[b + 1]) + Convert.ToString(liste[b + 2]) + Convert.ToString(liste[b + 3]) + Convert.ToString(liste[b + 4]) + Convert.ToString(liste[b + 5]) + Convert.ToString(liste[b + 6]) + Convert.ToString(liste[b + 7]);
                bytes += t;
                //Console.WriteLine("t : " + t);
                int h = Convert.ToInt32(t, 2);// permet de récupérer la valeur de chaque byte en int 
                                              //Console.WriteLine(s);
                                              //Console.WriteLine("h : "+ h);
                byte e = Convert.ToByte(h);
                //Console.WriteLine("e : " + e);
                listebytes.Add(e);
                b += 8;
            }


            byte[] chaine_non_corrigee = From_Binary_To_Byte(bytes);
            byte[] chaine_corrigee = ReedSolomonAlgorithm.Encode(chaine_non_corrigee, nbOctetsCorrection, ErrorCorrectionCodeType.QRCode);
            string binaire_corrigé = "";
            // for (int i = 0; i < chaine_corrigee.Length; i++) Console.Write(chaine_corrigee[i] + " ");
            for (int i = 0; i < chaine_corrigee.Length; i++)
            {
                binaire_corrigé = Convert.ToString(chaine_corrigee[i], 2);
                if (binaire_corrigé.Length < 8)
                {
                    int longueur_binaire_corrige = binaire_corrigé.Length;
                    for (int j = 0; j < 8 - longueur_binaire_corrige; j++)
                    {
                        binaire_corrigé = '0' + binaire_corrigé;
                    }
                }
                this.correction += binaire_corrigé;
            }
            //Console.WriteLine(this.correction);
            this.bytes += this.correction + this.masque;
            string st="";
            string an = "";
            for (int i = 0; i < bytes.Length; i+=8)
            {
                st = Convert.ToString(bytes[b]) + Convert.ToString(bytes[b + 1]) + Convert.ToString(bytes[b + 2]) + Convert.ToString(bytes[b + 3]) + Convert.ToString(bytes[b + 4]) + Convert.ToString(bytes[b + 5]) + Convert.ToString(bytes[b + 6]) + Convert.ToString(bytes[b + 7]);
                int h = Convert.ToInt32(st, 2);
                an += st + " ";
            }

            AffichageStrings(an);
            Console.WriteLine("--");
            AffichageStrings(bytes);

        }

        #endregion

        #region Encodage QR Code
        /// <summary>
        /// En fonction de la taille de la chaine, encode les données pour une version 1 ou 2 du QRcode
        /// </summary>
        public void EncodageQRCode()
        {
            int nbCaractères = NbCaractères(phrase);
            //Console.WriteLine(nbCaractères);
           
            List<byte> liste = new List<byte>();
            bytes = "";
            liste.Add(0);
            liste.Add(0);
            liste.Add(1);
            liste.Add(0);
            // le type d'information est alphanumérique et doit débuter la liste (avec 0010)
            if (nbCaractères <= 25) // dans ce cas on utilise la première version de QRcode
            {
                version = 1;
                ChaineDeDonnées(nbCaractères, liste, 152,7);
            }
            else if (nbCaractères <= 47) // deuxième version
            {
                version = 2;
                ChaineDeDonnées(nbCaractères, liste, 256,10);
            }
            else // cas pas traité 
            {
                Console.WriteLine("le message est trop long");
            }

            
        }

        #endregion

        
        
        #region MyImage QRcode

        public Pixel[,] BlocsRecherches(int Version)
        {
            Pixel[,] bloc = new Pixel[7, 7];
            for(int a = 0; a< 7; a++)
            {
               for(int b= 0; b<7; b++)
                {
                    bloc[a, b] = new Pixel(0, 0, 0);
                    if (a == 1 || b == 1 || a == 5 || b == 5)
                    { 
                    
                        bloc[a, b] = new Pixel(255, 255, 255);

                    }
                    if (a == 0 || b == 0 || a == 6 || b == 6)
                    {
                        bloc[a, b] = new Pixel(0, 0, 0);
                    }
                }
            }
            if (version == 2)
            {
                Pixel[,] temp = new Pixel[8, 8];
                for (int a = 0; a < 8; a++)
                {
                    for (int b = 0; b < 8; b++)
                    {
                        temp[a, b] = new Pixel(255, 255, 255);
                    }
                }
                for (int a = 0; a < 7; a++)
                {
                    for (int b = 0; b < 7; b++)
                    {
                         temp[a,b]= bloc[a, b];
                        
                    }
                }
                bloc = new Pixel[8, 8];
                for (int a = 0; a < 8; a++)
                {
                    for (int b = 0; b < 8; b++)
                    {
                        bloc[a, b] = temp[a, b];

                    }
                }
            }
            return bloc;
        }
        public MyImage ImageQRcode()
        {
            int nombreDeBitsCouleurs=24;
            string typeImage = "BM";
            Pixel[,] resul;
            int tailleOffset = 54;
            if (version == 1)
            {
               resul = new Pixel[21, 21];
                
            }
            else
            {
                resul = new Pixel[25, 25];
            }

            for (int a = 0; a < resul.GetLength(0); a++)
            {
                for (int b = 0; b < resul.GetLength(1); b++)
                {
                    resul[a, b] = new Pixel(50, 50, 50);
                }
            }
            if (version == 2)
            {
                for (int a = 0; a < resul.GetLength(0); a++)
                {
                    for (int b = 0; b < resul.GetLength(1); b++)
                    {
                        if (b == 7 )
                        {
                            if (a % 2 == 0)
                            {
                                resul[a, b] = new Pixel(0, 0, 0);
                            }
                            else
                            {
                                resul[a, b] = new Pixel(255, 255, 255);
                                
                            }
                        }
                        if (a == resul.GetLength(0) - 7)
                        {
                            if (b % 2 == 0)
                            {
                                resul[a, b] = new Pixel(0, 0, 0);
                            }
                            else
                            {
                                resul[a, b] = new Pixel(255, 255, 255);
                                
                            }
                        }
                    }
                }
            }

            Pixel[,] bloc = BlocsRecherches(version);
            for (int a = 0; a < bloc.GetLength(0); a++)
            {
                for (int b = 0; b < bloc.GetLength(1); b++)
                {
                    resul[a, b] = bloc[a, b];
                    resul[resul.GetLength(0) - 1 - a, b] = bloc[a, b];
                    resul[resul.GetLength(0) - 1 - a, resul.GetLength(1) - 1- b] = bloc[a, b];
                }
            }

            

            MyImage image = new MyImage(resul, typeImage, resul.GetLength(1) * resul.GetLength(0) * 3 + tailleOffset, tailleOffset, resul.GetLength(1), resul.GetLength(0), nombreDeBitsCouleurs);
            return image;
        }

        #endregion
    }
}
