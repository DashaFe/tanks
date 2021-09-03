using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace tank
{
    class Tank
    {
        private int health = 100;
        private int[] position = new int[2];
        private int speed = 5;
        private int m = 1;
        private int armor = 0;
        private int kind;
        private Armor arr;

        public Tank(int k)
        {
            kind = k;
            arr = new Armor(Math.Abs(k));
            speed = Speeds();
            m = M();
            armor = Armors();
        }

        public void StartPOs(int x, int y)
        {
            position[0] = x;
            position[1] = y;
        }

        public int M()
        {
            m += arr.M;
            return m;
        }

        public int Armors()
        {
            armor += arr.Arm;
            return armor;
        }

        public int Speeds()
        {
            return speed -= arr.Speed;
        }

        public int Armor { get { return armor; } set { armor = value; } }
        public int Kind { get { return kind; } }
        public int Speed { get { return speed; } }
        public int Health { get { return health; } set { health = value; } }
        public int[] Position { get { return position; } set { position = value; } }
    }
}
