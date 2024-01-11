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
        int[,] pole;
        int sloupec;
        int radek;
        int pocetRobotu;
        int pocetPrijmu;
        int pocetKoncu;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Location = new Point(0, 40);
            //pictureBox1.Dock = DockStyle.Fill;
            sloupec = 70;
            radek = 40;

            pocetRobotu = 2;
            pocetPrijmu = 5;
            pocetKoncu= 10;
            

            pole = new int[sloupec,radek];
            VyplnNulou(pole, sloupec, radek);

            

            GenerovaniMrizky(sloupec, radek, 20, pictureBox1, pole);



        }

        Panel okno = new Panel();

        /// ////////////////////////////////////////////////

        public void GenerovaniMrizky(int sloupec, int radek, int velikost, PictureBox picturebox, int[,] pole)
        {
            Bitmap bitmap = new Bitmap(sloupec * velikost, radek * velikost);

            for (int x = 0; x < sloupec; x++)
            {
                Graphics g = Graphics.FromImage(bitmap);
                Pen pen = new Pen(Color.Black);
                for (int y = 0; y < radek; y++)
                {
                    if (pole[x, y] == 1)
                    {
                        // ROBOT
                        int circleX = x * velikost;
                        int circleY = y * velikost;
                        int circleSize = velikost;

                        
                        g.FillEllipse(Brushes.Red, circleX, circleY, circleSize, circleSize);
                    }

                    if (pole[x, y] == 2)
                    {
                        // PRIJEM
                        int squareX = x * velikost;
                        int squareY = y * velikost;
                        int squareSize = velikost;


                        g.FillRectangle(Brushes.Yellow, squareX, squareY, squareSize, squareSize);

                        
                    }

                    if (pole[x, y] == 3)
                    {
                        //MISTO ODEVZDANI
                        int squareX = x * velikost;
                        int squareY = y * velikost;
                        int squareSize = velikost;


                        g.FillRectangle(Brushes.Green, squareX, squareY, squareSize, squareSize);

                        string text = "1";
                        Font font = new Font("Arial", 10, FontStyle.Regular);
                        Brush textBrush = Brushes.Black; // You can change the color of the text

                        // Draw the text in the center of the square
                        float textX = squareX + (squareSize - g.MeasureString(text, font).Width) / 2;
                        float textY = squareY + (squareSize - g.MeasureString(text, font).Height) / 2;

                        g.DrawString(text, font, textBrush, textX, textY);
                    }

                    g.DrawLine(pen, 0 , y * velikost, sloupec * velikost, y * velikost);
                }

                g.DrawLine(pen, x * velikost , 0, x * velikost, radek * velikost);
            }

            picturebox.Image = bitmap;
            picturebox.Size = new Size(sloupec * velikost, radek * velikost);
            

        }

        public void VyplnNulou(int[,] pole, int sloupec,int radek) 
        {
            for (int x = 0; x < sloupec; x++)
            {
                
                for (int y = 0; y < radek; y++)
                {


                    pole[x, y] = 0;
                }

                
            }
        }

        public void GenerovaniRobotu(int[,] poles, int sloupec, int radek, int pocetrobotu) 
        {
            
            Random x = new Random();
            Random y = new Random();
            int xrnd = x.Next(sloupec);
            int yrnd = y.Next(radek);
            for (int i = 0;i < pocetrobotu;)
            {
                xrnd = x.Next(sloupec);
                yrnd = y.Next(radek);
                if (poles[xrnd,yrnd] == 0) 
                {
                    poles[xrnd, yrnd] = 1;
                    Console.WriteLine("Robot: " + i + " Pozice: " + xrnd + " " + yrnd);
                    i++;
                }
                
            }
        }

        public void GenerovaniPrijmu(int[,] poles, int sloupec, int radek, int pocetprijmu)
        {

            Random x = new Random();
            Random y = new Random();
            int xrnd = x.Next(sloupec);
            int yrnd = y.Next(radek);
            for (int i = 0; i < pocetprijmu;)
            {
                xrnd = x.Next(sloupec);
                yrnd = y.Next(radek);
                if (poles[xrnd, yrnd] == 0)
                {
                    poles[xrnd, yrnd] = 2;
                    Console.WriteLine("Prijem: " + i + " Pozice: " + xrnd + " " + yrnd);
                    i++;
                }

            }
        }

        public void GenerovaniKoncu(int[,] poles, int sloupec, int radek, int pocetkoncu)
        {

            Random x = new Random();
            Random y = new Random();
            int xrnd = x.Next(sloupec);
            int yrnd = y.Next(radek);
            for (int i = 0; i < pocetkoncu;)
            {
                xrnd = x.Next(sloupec);
                yrnd = y.Next(radek);
                if (poles[xrnd, yrnd] == 0)
                {
                    poles[xrnd, yrnd] = 3;
                    Console.WriteLine("Konec: " + i + " Pozice: " + xrnd + " " + yrnd);
                    i++;
                }

            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
            GenerovaniRobotu(pole, sloupec, radek, pocetRobotu);
            GenerovaniPrijmu(pole, sloupec, radek, pocetPrijmu);
            GenerovaniKoncu(pole, sloupec, radek, pocetKoncu);
            for (int y = 0; y< radek; y++)
            {

                for (int x = 0; x < sloupec; x++)
                {


                    Console.Write(pole[x, y]);
                }
                Console.WriteLine();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerovaniMrizky(sloupec, radek, 20, pictureBox1, pole);
        }
    }
}
