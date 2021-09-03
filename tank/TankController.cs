using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank
{
    class TankController
    {
        private Tank tank;
        private int uron;
        int n = 17;
        int m = 10;
        public TankController(Tank tank)
        {
            this.tank = tank;
        }

        
        public bool Alive(Tank tank)
        {
            if (tank.Kind <= 0)
                return false;
            else
                return true;
        }


        public bool Go(int i, int j)
        {
            if (i >= 0 && i < n && j >= 0 &&  j < m)
            {
                if ((Math.Abs(i-tank.Position[0])+Math.Abs(j-tank.Position[1])) <= tank.Speed)
                {
                    tank.Position[0] = i;
                    tank.Position[1] = j;
                    return true;
                }
                else
                    return false;                
            }
            else
                return false;
        }

        public int Long(int x, int y, int xx, int yy)
        {
            return Math.Abs(xx - x) + Math.Abs(yy - y);
        }

        public void Uron(Ammunition ammunitin)
        {
            Random rnd = new Random();
           int uro= rnd.Next(0, 101);
            if (uro <= ammunitin.P)
            {
                uron = 100;
            }
            else
                uron = 100 - uro;
        }

        public int Ur { get { return uron; } }

        public int Hit(int i, int j,List<Ammunition> amm)
        {
            int longs = Long(i, j, tank.Position[0], tank.Position[1]);
            bool ur = false;
            int k = 11;

                for (int x = 0; x < amm.Count; x++)
                {
                    if (!ur && amm[x].Longs >= longs)
                    {
                        ur = true;
                        Uron(amm[x]);
                        k = x;              
                    }
                }
                return k;
        }
    }
}
