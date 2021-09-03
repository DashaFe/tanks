using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank
{
    class Game
    {
        private Tank tank;
        private int process,p;
        private List<Tank> foetank = new List<Tank>();
        private List<Ammunition> amm = new List<Ammunition>();
        private List<Ammunition> foeamm = new List<Ammunition>();
        Random rnd = new Random();
        private TankController tankconrl;
        private Form1 form1;
        private List<TankController> foetankcontrl = new List<TankController>();


        public Game(int kindtank, int foekol, Form1 form1)
        {
            this.form1 = form1;
            tank = new Tank(kindtank);
            for (int i = 0; i < foekol; i++)
            {
                foetank.Add(new Tank(-rnd.Next(1, 4)));
            }
            CreateAmmunition();
            CreateFoeAmmunition();
            tankconrl = new TankController(tank);
            for (int i = 0; i < foetank.Count; i++)
            {
                foetankcontrl.Add(new TankController(foetank[i]));
            }
            tank.Health += (foetank.Count -1)*100;
            StartPos();
            process=tank.Health + tank.Armor;
            form1.Process((tank.Health + tank.Armor)*100/process);
        }

        public bool Prov(int k)
        {
            bool prov = false;
            if (tank.Position == foetank[k].Position)
                prov = true;
            for (int i = 0; i < k; i++)
                if (i != k && foetank[i].Position == foetank[k].Position)
                    prov = true;
            return prov;
        }

        public void StartPos()
        {
            tank.StartPOs(rnd.Next(0, 17), rnd.Next(0, 10));
            form1.StartPaint(tank.Kind, tank.Position[0], tank.Position[1]);
            for (int i = 0; i < foetank.Count; i++)
            {
                foetank[i].StartPOs(rnd.Next(0, 17), rnd.Next(0, 10));
                form1.StartPaint(foetank[i].Kind, foetank[i].Position[0], foetank[i].Position[1]);
                while (Prov(i))
                    foetank[i].StartPOs(rnd.Next(0, 17), rnd.Next(0, 10));
            }

        }

        public void CreateAmmunition()
        {
            for (int i = 0; i < 10; i++)
            {
                amm.Add(new Ammunition(rnd.Next(1, 4)));               
            }
        }

        public void CreateFoeAmmunition()
        {
            for (int i = 0; i < 10; i++)
            {
                foeamm.Add(new Ammunition(rnd.Next(1, 4)));
            }
        }


        public int Amm(int h) { return amm[h].Kind; }

        public string Text(int x, int y)
        {
            String text = "";
            if(tank.Position[0]==x && tank.Position[1]==y)
            {
                text = "Здоровье: "+(tank.Health).ToString()+ Environment.NewLine +"Броня: " + tank.Armor+ Environment.NewLine;
            }
            else
            {
                for (int i=0;i<foetank.Count;i++)
                {
                    if (foetank[i].Position[0]==x && foetank[i].Position[1]==y)
                    {
                        text = "Здоровье: " + (foetank[i].Health).ToString() + Environment.NewLine + "Броня: " + foetank[i].Armor + Environment.NewLine;
                    }
                }
            }
            return text;
        }

        public int[] Ran(int rast, int k)
        {
            int[] ran = new int[2];
            bool h = false;
            while (!h)
            {
                ran[0] = rnd.Next(-rast, rast + 1);
                ran[1] = rnd.Next(-rast, rast + 1);
                if (Math.Abs(ran[0]) + Math.Abs(ran[1]) <= rast && Math.Abs(ran[0]) + Math.Abs(ran[1]) != 0)
                {
                    bool t = false;
                    if (tank.Position == ran)
                        t = true;
                    for (int i = 0; i < foetank.Count; i++)
                    {
                        if (k != i && foetank[i].Position == ran)
                            t = true;
                    }
                    if (!t)
                        h = true;
                }
            }
            return ran;
        }


        public void Move(int x, int y)
        {
            int xx, yy;
            xx = tank.Position[0];
            yy = tank.Position[1];
            bool t = false;
            for (int i = 0; i < foetank.Count; i++)
            {
                if (x == foetank[i].Position[0] && y == foetank[i].Position[1])
                    t = true;
            }
            if (tankconrl.Go(x, y) && !t)
            {
                form1.Paint(tank.Kind, xx, yy, tank.Position[0], tank.Position[1]);
                for (int i = 0; i < foetank.Count; i++)
                {
                    ProvSh(i);
                    xx = foetank[i].Position[0];
                    yy = foetank[i].Position[1];
                    int[] h = new int[2];
                    h = Ran(foetank[i].Speed, i);
                    h[0] += foetank[i].Position[0];
                    h[1] += foetank[i].Position[1]; 
                    if(p<100)
                        form1.Cr(foetank[p].Position[0], foetank[p].Position[1], foetank[p].Kind);
                    foetankcontrl[i].Go(h[0], h[1]);
                    form1.Paint(foetank[i].Kind, xx, yy, foetank[i].Position[0], foetank[i].Position[1]);
                   
                }
              //  form1.Cr(tank.Position[0], tank.Position[1], tank.Kind);
            }
            else
                form1.Er();

        }

       public void Shoot(int x, int y)
        {
            int i;
            i = tankconrl.Hit(x,y,amm);
            int uron = tankconrl.Ur;
            if(i<11)
            {
                form1.AmmunitionN(amm[i].Kind);
                amm.Remove(amm[i]);
                for (int j=0;j<foetank.Count; j++)
                {
                    if (foetank[j].Position[0]==x && foetank[j].Position[1]==y)
                    {
                        p = j;
                        form1.Cr(foetank[j].Position[0], foetank[j].Position[1]);
                        if (uron<foetank[j].Armor)
                        {
                            foetank[j].Armor -= uron;
                        }
                        else
                        {
                            int raz = uron - foetank[j].Armor;
                            foetank[j].Armor = 0;
                            if (foetank[j].Health>raz)
                            {
                                foetank[j].Health -= raz;
                            }
                            else
                            {
                                foetank[j].Health = 0;
                                form1.Cl(foetank[j].Position[0], foetank[j].Position[1]); 
                                form1.Cr(foetank[p].Position[0], foetank[p].Position[1], foetank[p].Kind);
                                p = 100;
                                foetank.Remove(foetank[j]);
                            }
                        }
                        //form1.Cr(foetank[j].Position[0], foetank[j].Position[1], foetank[j].Kind);
                       // form1.Cr(x, y);
                       // form1.Gif(tank.Position[0],tank.Position[1],x,y);
                    }
                }
            }
            
            if (foetank.Count==0)
            {
                form1.Win(true);
            }
            if (amm.Count == 0)
                CreateAmmunition();
        }

        public void Cl()
        {
            form1.Cr(tank.Position[0], tank.Position[1], tank.Kind);
        }

        public void ProvSh(int k)
        { 
            int i=foetankcontrl[k].Hit(tank.Position[0],tank.Position[1],foeamm);
            int uron = tankconrl.Ur;
            if (i < 11)
            {
                form1.Cr(tank.Position[0], tank.Position[1]);
                foeamm.Remove(amm[i]);
                if (uron < tank.Armor)
                {
                    tank.Armor -= uron;
                }
                else
                {
                    int raz = uron - tank.Armor;
                    tank.Armor = 0;
                    if (tank.Health > raz)
                    {
                        tank.Health -= raz;
                        form1.Process((tank.Health + tank.Armor) * 100 / process);
                    }
                    else
                    {
                        tank.Health = 0;
                        form1.Process((tank.Health + tank.Armor) * 100 / process);
                        form1.Win(false);
                    }
                }
            }
            if (foeamm.Count == 0)
                CreateFoeAmmunition();

        }
    }
}
