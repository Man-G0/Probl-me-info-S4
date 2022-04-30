using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manon_Aubry_Manon_Goffinet
{
    class ASCII
    {
        int valeur;

        public ASCII(Char caractère)
        {
            switch (caractère)
            {
                case 'A':
                    valeur = 65;
                    break;
                case 'B':
                    valeur = 66;
                    break;
                case 'C':
                    valeur = 67;
                    break;
                case 'D':
                    valeur = 68;
                    break;
                case 'E':
                    valeur = 69;
                    break;
                case 'F':
                    valeur = 70;
                    break;
                case 'G':
                    valeur = 71;
                    break;
                case 'H':
                    valeur = 72;
                    break;
                case 'I':
                    valeur = 73;
                    break;
                case 'J':
                    valeur = 74;
                    break;
                case 'K':
                    valeur = 75;
                    break;
                case 'L':
                    valeur = 76;
                    break;
                case 'M':
                    valeur = 77;
                    break;
                case 'N':
                    valeur = 78;
                    break;
                case 'O':
                    valeur = 79;
                    break;
                case 'P':
                    valeur = 80;
                    break;
                case 'Q':
                    valeur = 81;
                    break;
                case 'R':
                    valeur = 82;
                    break;
                case 'S':
                    valeur = 83;
                    break;
                case 'T':
                    valeur = 84;
                    break;
                case 'U':
                    valeur = 85;
                    break;
                case 'V':
                    valeur = 86;
                    break;
                case 'W':
                    valeur = 87;
                    break;
                case 'X':
                    valeur = 88;
                    break;
                case 'Y':
                    valeur = 89;
                    break;
                case 'Z':
                    valeur = 90;
                    break;
                case ' ':
                    valeur = 32;
                    break;
            }
        }

        public int Valeur
        {
            get { return valeur; }
        }
    }
}
