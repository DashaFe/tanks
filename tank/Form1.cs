using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*это игра, поле которой состоит из кнопок, вы можете играть за один из трех видов танков (на поле ваш танк подсвечивается желтым цветом); 
 от вида танка зависит его защита, дальность хода и стрельбы; танк в который стреляют подсвечивается красным*/

namespace tank
{
    public partial class Form1 : Form
    {
        Button[,] btn;
        int n = 17;
        int m = 10;
        Game newGame;
        int light = 0;
        int mid = 0;
        int hard = 0;
        bool click = false;
        bool st = true;
        bool tooltip = false;
        Image mg;
        int k, l;

        public Form1()
        {
            InitializeComponent();
            CreateButton();
        }

        public void CreateButton()
        {
            btn = new Button[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].Width = 50;
                    btn[i, j].Height = 50;
                    btn[i, j].Location = new Point(i * 50, j * 50);
                    btn[i, j].FlatStyle = FlatStyle.Flat;
                    btn[i, j].BackColor = Color.Transparent;
                    btn[i, j].FlatAppearance.MouseDownBackColor = Color.Transparent;
                    btn[i, j].FlatAppearance.MouseOverBackColor = Color.Transparent;
                    btn[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    int tmpi = i;
                    int tmpj = j;
                    btn[i, j].MouseHover += new EventHandler((s, args) => { Button_MouseHover(tmpi, tmpj); }); ;
                    btn[i, j].MouseClick += new MouseEventHandler((s, args) => { Button_MouseClick(tmpi, tmpj, args); }); ;
                    this.Controls.Add(btn[i, j]);
                }
            }
        }

        public void Process(int k)
        {
            progressBar1.Value = k;
        }

        public void Amm()
        {
            int k;
            for (int i = 0; i < 10; i++)
            {
                k = newGame.Amm(i);
                switch (k)
                {
                    case 1:
                        light++;
                        break;
                    case 2:
                        mid++;
                        break;
                    case 3:
                        hard++;
                        break;
                    default:
                        break;
                }

            }
            label9.Text = light.ToString();
            label10.Text = mid.ToString();
            label11.Text = hard.ToString();
        }

        public void AmmunitionN(int h)
        {
            switch (h)
            {
                case 1:
                    light--;
                    break;
                case 2:
                    mid--;
                    break;
                case 3:
                    hard--;
                    break;
                default:
                    break;

            }
            label9.Text = light.ToString();
            label10.Text = mid.ToString();
            label11.Text = hard.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tankkind = 0;
            if (radioButton1.Checked)
                tankkind = 1;
            else if (radioButton2.Checked)
                tankkind = 2;
            else if (radioButton3.Checked)
                tankkind = 3;
            int foetank;
            if (textBox1.Text.Length == 0)
                foetank = 0;
            else
                foetank = int.Parse(textBox1.Text);
            newGame = new Game(tankkind, foetank, this);
            Amm();
            progressBar1.Value = 100;
            tooltip = true;

        }

        public void Button_MouseHover(int x, int y)
        {
            if (tooltip)
            {
                ToolTip tp = new ToolTip();
                tp.Show(newGame.Text(x, y), btn[x, y]);
            }

        }

        public void Win(bool win)
        {
            if (win)
                MessageBox.Show("Победа!!!");
            else
                MessageBox.Show("Вы приграли :(");
        }

        public void Button_MouseClick(int x, int y, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (st && btn[x, y].BackColor == Color.Transparent && btn[x, y].BackgroundImage != null)
                {
                    newGame.Shoot(x, y);
                    st = false;
                }
                else if ((btn[x, y].BackColor == Color.Yellow) && !click)
                {
                    btn[x, y].ForeColor = Color.Black;
                    btn[x, y].Focus();
                    k = x;
                    l = y;
                    click = true;
                }
                else if (click)
                {
                    click = false;
                    newGame.Move(x, y);
                    k = x;
                    l = y;
                    st = true;
                }
            }
        }

        public void Er()
        {
            MessageBox.Show("Танк не может пройти такое большое расстояние! Нажмите на другую кнопку");
            click = true;
        }

        public void Cl(int x, int y)
        {
            btn[x, y].ForeColor = Color.Black;
            btn[x, y].BackColor = Color.Transparent;
            btn[x, y].BackgroundImage = null;
        }

        public void StartPaint(int k, int i, int j)
        {
            switch (Math.Abs(k))
            {
                case 1:
                    btn[i, j].BackgroundImage = Image.FromFile("light.png");
                    break;
                case 2:
                    btn[i, j].BackgroundImage = Image.FromFile("middle.png");
                    break;
                case 3:
                    btn[i, j].BackgroundImage = Image.FromFile("hard.png");
                    break;
                default:
                    break;
            }
            if (k > 0)
            {
                btn[i, j].BackColor = Color.Yellow;
                mg = btn[i, j].BackgroundImage;
            }
        }

        public void Paint(int k, int x, int y, int xx, int yy)
        {

            btn[x, y].BackColor = Color.Transparent;
            btn[x, y].BackgroundImage = null;
            if (k > 0)
                btn[xx, yy].BackColor = Color.Yellow;
            switch (Math.Abs(k))
            {
                case 1:
                    btn[xx, yy].BackgroundImage = Image.FromFile("light.png");
                    break;
                case 2:
                    btn[xx, yy].BackgroundImage = Image.FromFile("middle.png");
                    break;
                case 3:
                    btn[xx, yy].BackgroundImage = Image.FromFile("hard.png");
                    break;
                default:
                    break;

            }
        }

        public void Cr(int x, int y)
        {
            btn[x, y].ForeColor = Color.Red;
        }

        public void Cr(int x, int y, int k)
        {
            btn[x, y].ForeColor = Color.Black;
        }

    }
}
