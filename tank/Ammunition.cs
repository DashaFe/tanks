using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank
{
    struct Ammunition
    {
        private int longs;
        private int p;
        private int kind;

        public Ammunition(int k)
        {
            longs = 0;
            p = 0;
            kind = 0;
            switch (k)
            {
                case 1:
                    longs = 3;
                    p = 90;
                    kind = 1;
                    break;
                case 2:
                    longs = 4;
                    p = 70;
                    kind = 2;
                    break;
                case 3:
                    longs = 5;
                    p = 65;
                    kind = 3;
                    break;
                default:
                    break;
            }
        }

        public int Longs { get { return longs; } }
        public int P { get { return p; } }
        public int Kind { get { return kind; } }
    }
}
