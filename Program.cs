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
            Console.WriteLine("hello world");
            string fileN = "Test001.bmp";
            MyImage test = new MyImage(fileN);
            Console.ReadKey();
        }
    }
}
