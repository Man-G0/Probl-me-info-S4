using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Manon_Aubry_Manon_Goffinet
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding u8 = Encoding.UTF8;
            string a = "HELLO WORD";
            int iBC = u8.GetByteCount(a);
            byte[] bytesa = u8.GetBytes(a);
            string b = "HELLO WORF";
            byte[] bytesb = u8.GetBytes(b);
            //byte[] result = ReedSolomonAlgorithm.Encode(bytesa, 7);
            //Privilégiez l'écriture suivante car par défaut le type choisi est DataMatrix 
            byte[] result = ReedSolomonAlgorithm.Encode(bytesa, 7, ErrorCorrectionCodeType.QRCode);
            byte[] result1 = ReedSolomonAlgorithm.Decode(bytesb, result);
            foreach (byte val in a) Console.Write(val + " ");
            Console.WriteLine();
         
            Console.ReadLine();

        }
    }
}
