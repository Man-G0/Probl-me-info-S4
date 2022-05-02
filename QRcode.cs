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
        List<byte> listeBytes;

        /// <summary>
        /// Constructeur créant une instance de QRcode à partir d'une phrase
        /// </summary>
        /// <param name="phrase">phrase à encoder dans le QRcode (doit être inférieure ou égale à 47caractères)</param>

        public QRcode(string phrase)
        {
            this.phrase = phrase;
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
                //AffichageListeBytes(b);
                return b;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }


        #endregion

       
        #region AffichageListeBytes(List <byte>listeBytes)
        /// <summary>
        /// Affiche une liste de bytes
        /// </summary>
        /// <param name="listeBytes">liste de bytes à afficher</param>
        public void AffichageListeBytes(List<byte> listeBytes)
        {
            for(int i = 0;i<listeBytes.Count; i++)
            {
                Console.Write(listeBytes[i] + "");
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
        #region QR Code
        public void EncodageQRCode()
        {
            int nbCaractères = NbCaractères(phrase);
            //Console.WriteLine(nbCaractères);
            listeBytes = new List<byte>();
            List<byte> liste = new List<byte>();
            liste.Add(0);
            liste.Add(0);
            liste.Add(1);
            liste.Add(0);
            // le type d'information est alphanumérique et doit débuter la liste (avec 0010)
            if (nbCaractères <= 25) // dans ce cas on utilise la première version de QRcode
            {
                List<byte> nbCaractèresEnBytes = Convertir_Int_To_Binaire(nbCaractères, 9);
                
                for (int i = 0; i<nbCaractèresEnBytes.Count; i++)
                {
                    liste.Add(nbCaractèresEnBytes[nbCaractèresEnBytes.Count - 1 - i]);
                }
                int a = 0;
                //AffichageListeBytes(liste);
                while (a < phrase.Length)
                {
                    if (a + 1 < phrase.Length-1)
                    {
                        
                        char char1 = phrase[a];
                        char char2 = phrase[a + 1];
                        
                        //Console.WriteLine(char1 + " " + char2);
                        int codeASCIILettres = 45 * CodeAsciiCaractères(char1) + CodeAsciiCaractères(char2);
                        //Console.WriteLine(codeASCIILettres);
                        List<byte> LettresBinaires = Convertir_Int_To_Binaire(codeASCIILettres, 11);
                        for (int i = 0; i < LettresBinaires.Count; i++)
                        {
                            liste.Add(LettresBinaires[LettresBinaires.Count - 1 - i]);
                           //Console.WriteLine(LettresBinaires[i]);
                            
                        }
                        //Console.WriteLine();
                        a += 2;
                    }
                    else
                    {
                        char char1 = phrase[a];
                        //Console.WriteLine(char1);
                        int codeASCIILettres = CodeAsciiCaractères(char1);
                        //Console.WriteLine(codeASCIILettres);
                        List<byte> LettresBinaires = Convertir_Int_To_Binaire(codeASCIILettres, 6);
                        
                        for (int i = 0; i < LettresBinaires.Count; i++)
                        {
                            liste.Add(LettresBinaires[LettresBinaires.Count-1-i]);

                        }
                        Console.WriteLine();
                        a++;
                    }
                    
                }
                AffichageListeBytes(liste);
                int b = 0;
                while(b<liste.Count)
                {
                    if (b + 7 < liste.Count)
                    {
                        string t = Convert.ToString(liste[b]) + Convert.ToString(liste[b + 1]) + Convert.ToString(liste[b + 2]) + Convert.ToString(liste[b + 3]) + Convert.ToString(liste[b + 4]) + Convert.ToString(liste[b + 5]) + Convert.ToString(liste[b + 6]) + Convert.ToString(liste[b + 7]);
                        
                        Console.WriteLine("t : " + t);
                        int h = Convert.ToInt32(t, 2);// permet de récupérer la valeur de chaque byte en int 
                        Console.WriteLine("h : "+ h);
                        byte e = (byte) h;
                        Console.WriteLine("e : " + e);
                        b += 8;
                    }
                    else
                    {
                        string t = "";
                        while (b < liste.Count)
                        {
                            t += Convert.ToString(liste[b]);
                            b++;
                        }
                        for(int i = t.Length; i<8; i++)
                        {
                            t += "0";
                        }
                        Console.WriteLine("t : " + t);
                    }
                }
            }
            else if (nbCaractères <= 47) // deuxième version
            {
                Convertir_Int_To_Binaire(nbCaractères, 9);
            }
            else // cas pas traité 
            {
                Console.WriteLine("le message est trop long");
            }
            // 00100000 01011011 00001011 01111000 11010001 01110010 11011100 01001101 01000011 01000000 11101100 00010001 11101100 00010001 11101100 00010001 11101100 00010001 11101100
            // 00100000 01011011 00001011 01111000 11010001 01110010 11011100 01001101 01000011 01000000 11101100 00010001 11101100
        }  //  00100000 01011011 00001011 01111000 11010001 01110010 11011100 01001101 01000011 01

        #endregion
    }
}
