using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StachRPR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Dock = DockStyle.Fill;
            GenerovaniMrizky(70, 40, 20, pictureBox1);
            Panel bomba = new Panel();
            bomba.Size = new Size();
            Controls.Add(bomba);
            Controls.Add(pictureBox1);
        }

        Panel okno = new Panel();

        /// ////////////////////////////////////////////////

        public void GenerovaniMrizky(int sloupec, int radek, int velikost, PictureBox picturebox)
        {
            Bitmap bitmap = new Bitmap(sloupec * velikost, radek * velikost);

            for (int x = 0; x <= sloupec; x++)
            {
                Graphics g = Graphics.FromImage(bitmap);
                Pen pen = new Pen(Color.Black);
                for (int y = 0; y <= radek; y++)
                {


                    g.DrawLine(pen, 0, y * velikost, sloupec * velikost, y * velikost);
                }

                g.DrawLine(pen, x * velikost, 0, x * velikost, radek * velikost);
            }

            picturebox.Image = bitmap;
            picturebox.Size = new Size(sloupec * velikost, radek * velikost);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //okno.Location = new Point(0, 30);
            //okno.Visible = true;
            //okno.Visible = false;
            
            

        }
    }
}
