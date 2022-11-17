using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolB
{
    class Nodo
    {
        public int[] clave;
        public Nodo[] hijo;
        public int numclaves;

        public Nodo(int tama)
        {
            numclaves = 0;
            clave = new int[tama];
            hijo = new Nodo[tama];
        }
    }
}
