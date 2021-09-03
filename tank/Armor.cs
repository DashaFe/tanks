using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank
{
    struct Armor
    {
        private int armor;
        private int m;
        private int speed;

        public Armor(int k)
        {
            armor = 0;
            m = 0;
            speed = 0;
            switch (k)
            {
                case 1:
                    armor = 50;
                    m = 2;
                    speed = 2;
                    break;
                case 2:
                    armor = 70;
                    m = 3;
                    speed = 3;
                    break;
                case 3:
                    armor = 100;
                    m = 10;
                    speed = 4;
                    break;
                default:
                    break;
            }
        }
        public int Speed { get { return speed; } }
        public int Arm { get { return armor; } }
        public int M { get { return m; } }
    }
}
