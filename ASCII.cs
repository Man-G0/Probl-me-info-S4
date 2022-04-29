using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manon_Aubry_Manon_Goffinet
{
    class ASCII
    {
        List<byte>chaineASCII;

        public ASCII(String str)
        {
            for(int i=0; i<str.Length;i++)
            {
                this.chaineASCII.Add((byte)str[i]);
            }
        }

        public List<byte> ChaineASCII
        {
            get { return chaineASCII; }
            set { chaineASCII = value; }
        }

        public string toString()
        {
            string a = "";
            for (int i = 0; i < chaineASCII.Count; i++)
            {
                a += chaineASCII[i]+" ";
            }
            return a;
        }
    }
}
