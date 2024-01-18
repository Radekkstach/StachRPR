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
            pictureBox1.Location = new Point(0, 50);
            //pictureBox1.Dock = DockStyle.Fill;
            sloupec = (int)numericUpDown1.Value;
            radek = (int)numericUpDown2.Value;

            pocetRobotu = (int)numericUpDown3.Value;
            pocetPrijmu = (int)numericUpDown4.Value;
            pocetKoncu= (int)numericUpDown5.Value;
            

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
            
            Random rnd = new Random();
            
            
            for (int i = 0;i < pocetrobotu;)
            {
                int xrnd = rnd.Next(sloupec);
                int yrnd = rnd.Next(radek);
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

            Random rnd = new Random();

            for (int i = 0; i < pocetprijmu;)
            {
                int xrnd = rnd.Next(sloupec);
                int yrnd = rnd.Next(radek);
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

            Random rnd = new Random();

            for (int i = 0; i < pocetkoncu;)
            {
                int xrnd = rnd.Next(sloupec);
                int yrnd = rnd.Next(radek);
                if (poles[xrnd, yrnd] == 0)
                {
                    poles[xrnd, yrnd] = 3;
                    Console.WriteLine("Konec: " + i + " Pozice: " + xrnd + " " + yrnd);
                    i++;
                }

            }
        }

        public int NejblizsiPrijemX(int[,]poles, int sloupec, int radek,int robotX, int robotY )
        {

            int nejblizsiX = -1;
            int nejblizsiY = -1;
            int nejblizsiVzdalenost = int.MaxValue;

            for (int x = 0; x < sloupec; x++)
            {                
                for (int y = 0; y < radek; y++)
                {
                  
                    if (pole[x, y] == 2)
                    {
                        // PRIJEM
                        int vzdalenost = Math.Abs(robotX - x) + Math.Abs(robotY - y);

                        if (vzdalenost < nejblizsiVzdalenost)
                        {
                            nejblizsiX = x;
                            nejblizsiY = y;
                            nejblizsiVzdalenost = vzdalenost;
                        }


                    }

                }                
            }

            return nejblizsiX;

        }

        public int NejblizsiPrijemY(int[,] poles, int sloupec, int radek, int robotX, int robotY)
        {

            int nejblizsiX = -1;
            int nejblizsiY = -1;
            int nejblizsiVzdalenost = int.MaxValue;

            for (int x = 0; x < sloupec; x++)
            {
                for (int y = 0; y < radek; y++)
                {

                    if (pole[x, y] == 2)
                    {
                        // PRIJEM
                        int vzdalenost = Math.Abs(robotX - x) + Math.Abs(robotY - y);

                        if (vzdalenost < nejblizsiVzdalenost)
                        {
                            nejblizsiX = x;
                            nejblizsiY = y;
                            nejblizsiVzdalenost = vzdalenost;
                        }


                    }

                }
            }

            return nejblizsiY;

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            sloupec = (int)numericUpDown1.Value;
            radek = (int)numericUpDown2.Value;
            pole = new int[sloupec, radek];
            VyplnNulou(pole, sloupec, radek);
            pocetRobotu = (int)numericUpDown3.Value;
            pocetPrijmu = (int)numericUpDown4.Value;
            pocetKoncu = (int)numericUpDown5.Value;
            GenerovaniRobotu(pole, sloupec, radek, pocetRobotu);
            GenerovaniPrijmu(pole, sloupec, radek, pocetPrijmu);
            GenerovaniKoncu(pole, sloupec, radek, pocetKoncu);
            GenerovaniMrizky(sloupec, radek, 20, pictureBox1, pole);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool bylProvedenPohyb = false;
            
            
            if (!bylProvedenPohyb)
            {
                
                List<(int, int)> poziceObjektu = new List<(int, int)>();

                
                for (int x = 0; x < sloupec; x++)
                {
                    for (int y = 0; y < radek; y++)
                    {
                        if (pole[x, y] == 1)
                        {
                            poziceObjektu.Add((x, y));
                        }
                    }
                }

                
                foreach (var (x, y) in poziceObjektu)
                {


                    if (x != NejblizsiPrijemX(pole, sloupec, radek, x, y))
                    {
                        if (x > NejblizsiPrijemX(pole, sloupec, radek, x, y))
                        {
                            
                            pole[x - 1, y] = 1;
                            pole[x, y] = 0;
                        }
                        else if (x < NejblizsiPrijemX(pole, sloupec, radek, x, y))
                        {
                            pole[x + 1, y] = 1;
                            pole[x, y] = 0;
                        }
                    }
                    else
                    {
                        if (y != NejblizsiPrijemY(pole, sloupec, radek, x, y))
                        {
                            if (y > NejblizsiPrijemY(pole, sloupec, radek, x, y))
                            {
                                pole[x, y - 1] = 1;
                                pole[x, y] = 0;
                            }
                            else if (y < NejblizsiPrijemY(pole, sloupec, radek, x, y))
                            {
                                pole[x, y + 1] = 1;
                                pole[x, y] = 0;
                            }
                        }
                    }
                }

                
                bylProvedenPohyb = true;

                
                GenerovaniMrizky(sloupec, radek, 20, pictureBox1, pole);
            }
        }
    }
}
