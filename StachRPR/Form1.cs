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
        int velikost = 20;
        List<Tuple<int, int, int>> poziceRobotu = new List<Tuple<int, int, int>>();
        List<Tuple<int, int>> poziceKoncu = new List<Tuple<int, int>>();
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Location = new Point(0, 50);
            
            sloupec = (int)numericUpDown1.Value;
            radek = (int)numericUpDown2.Value;

            pocetRobotu = (int)numericUpDown3.Value;
            pocetPrijmu = (int)numericUpDown4.Value;
            pocetKoncu= (int)numericUpDown5.Value;
            

            pole = new int[sloupec,radek];
            VyplnNulou(pole, sloupec, radek);



            GenerovaniMrizky(sloupec, radek, velikost, pictureBox1, pole);



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
                        // proces kdy robot najede na místo příjmu
                        int squareX = x * velikost;
                        int squareY = y * velikost;
                        int squareSize = velikost;

                        int circleX = x * velikost;
                        int circleY = y * velikost;
                        int circleSize = velikost;

                        g.FillRectangle(Brushes.Yellow, squareX, squareY, squareSize, squareSize);
                        g.FillEllipse(Brushes.Red, circleX, circleY, circleSize, circleSize);


                    }

                    if (pole[x, y] == 4)
                    {
                        // Robot s balickem                    

                        int circleX = x * velikost;
                        int circleY = y * velikost;
                        int circleSize = velikost;
                        
                        g.FillEllipse(Brushes.Red, circleX, circleY, circleSize, circleSize);

                        string text = String.Empty;

                        foreach (Tuple<int, int, int> tuple in poziceRobotu)
                        {
                            int pozicex = tuple.Item1;
                            int pozicey = tuple.Item2;
                            if (pozicex == x && pozicey == y)
                            {
                                int posledniInt = tuple.Item3; // Získání posledního integeru z Tuple
                                text += (posledniInt + 1).ToString(); // Připojení posledního integeru k výslednému řetězci
                            }
                        }
                        Font font = new Font("Arial", 12);
                        Brush brush = Brushes.Black;

                        // Výpočet souřadnic pro umístění textu doprostřed kruhu
                        float textX = circleX + (circleSize - g.MeasureString(text, font).Width) / 2;
                        float textY = circleY + (circleSize - g.MeasureString(text, font).Height) / 2;

                        // Vykreslení textu
                        g.DrawString(text, font, brush, textX, textY);


                    }

                    if (pole[x, y] > 4)
                    {
                        //Místa konců
                        int squareX = x * velikost;
                        int squareY = y * velikost;
                        int squareSize = velikost;


                        g.FillRectangle(Brushes.Green, squareX, squareY, squareSize, squareSize);

                        string text = (pole[x,y] - 4).ToString();
                        Font font = new Font("Arial", 10, FontStyle.Regular);
                        Brush textBrush = Brushes.Black; // You can change the color of the text

                        // Draw the text in the center of the square
                        float textX = squareX + (squareSize - g.MeasureString(text, font).Width) / 2;
                        float textY = squareY + (squareSize - g.MeasureString(text, font).Height) / 2;

                        g.DrawString(text, font, textBrush, textX, textY);
                    }

                    if (pole[x, y] == -1)
                    {
                        //protnutí robota s balickem a misto konce
                        int squareX = x * velikost;
                        int squareY = y * velikost;
                        int squareSize = velikost;


                        g.FillRectangle(Brushes.Green, squareX, squareY, squareSize, squareSize);

                        int circleX = x * velikost;
                        int circleY = y * velikost;
                        int circleSize = velikost;


                        g.FillEllipse(Brushes.Red, circleX, circleY, circleSize, circleSize);


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
            poziceKoncu.Clear();
            Random rnd = new Random();

            for (int i = 1; i < pocetkoncu + 1;)
            {
                int xrnd = rnd.Next(sloupec);
                int yrnd = rnd.Next(radek);
                if (poles[xrnd, yrnd] == 0)
                {
                    poles[xrnd, yrnd] = 4 + i;
                    Console.WriteLine("Konec: " + i + " Pozice: " + xrnd + " " + yrnd);
                    i++;
                    poziceKoncu.Add(new Tuple<int, int>(xrnd, yrnd));
                }
            }
        }


        public int NejblizsiPrijemX(int[,]poles, int sloupec, int radek,int robotX, int robotY )
        {

            int nejblizsiX = -1;
            
            int nejblizsiVzdalenost = int.MaxValue;

            for (int x = 0; x < sloupec; x++)
            {                
                for (int y = 0; y < radek; y++)
                {
                  
                    if (pole[x, y] == 2 || pole[x, y] == 3)
                    {
                        
                        int vzdalenost = Math.Abs(robotX - x) + Math.Abs(robotY - y);

                        if (vzdalenost < nejblizsiVzdalenost)
                        {
                            nejblizsiX = x;
                            
                            nejblizsiVzdalenost = vzdalenost;
                        }


                    }

                }                
            }

            return nejblizsiX;

        }

        public int NejblizsiPrijemY(int[,] poles, int sloupec, int radek, int robotX, int robotY)
        {

            
            int nejblizsiY = -1;
            int nejblizsiVzdalenost = int.MaxValue;

            for (int x = 0; x < sloupec; x++)
            {
                for (int y = 0; y < radek; y++)
                {

                    if (pole[x, y] == 2 || pole[x,y] == 3)
                    {
                        
                        int vzdalenost = Math.Abs(robotX - x) + Math.Abs(robotY - y);

                        if (vzdalenost < nejblizsiVzdalenost)
                        {
                            
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
            GenerovaniMrizky(sloupec, radek, velikost, pictureBox1, pole);

            poziceRobotu.Clear();
            for (int x = 0; x < sloupec; x++)
            {
                for (int y = 0; y < radek; y++)
                {
                    if (pole[x, y] == 1)
                    {
                        poziceRobotu.Add(new Tuple<int, int, int>(x, y, -1));
                    }
                }
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            bool bylProvedenPohyb = false;

            if (!bylProvedenPohyb)
            {
                // Vytvoření kopie seznamu poziceRobotu, aby nedošlo k chybě "Kolekce byla upravena"
                List<Tuple<int, int, int>> kopiePoziceRobotu = new List<Tuple<int, int, int>>(poziceRobotu);

                foreach (var pozice in kopiePoziceRobotu)
                {
                    int x = pozice.Item1;
                    int y = pozice.Item2;
                    int cislo = pozice.Item3;

                    int prijemx = NejblizsiPrijemX(pole, sloupec, radek, x, y);
                    int prijemy = NejblizsiPrijemY(pole, sloupec, radek, x, y);

                    if (pole[x, y] == 1)
                    {
                        if (x != prijemx)
                        {
                            if (x > prijemx)
                            {
                                if (pole[x - 1, y] == 2 || pole[x - 1, y] == 0)
                                {
                                    if (pole[x - 1, y] == 2)
                                    {
                                        pole[x - 1, y] = 3;
                                    }
                                    else
                                    {
                                        pole[x - 1, y] = 1;

                                    }
                                    poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, cislo);
                                }
                                else
                                {
                                    if(pole[x, y - 1] == 0)
                                    {
                                        pole[x, y - 1] = 1;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, cislo);
                                    }
                                    else if (pole[x,y + 1] == 0)
                                    {
                                        pole[x, y - 1] = 1;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, cislo);
                                    }

                                }
                                
                            }
                            else if (x < prijemx)
                            {
                                if (pole[x + 1, y] == 2 || pole[x + 1, y] == 0)
                                {
                                    if (pole[x + 1, y] == 2)
                                    {
                                        pole[x + 1, y] = 3;
                                    }
                                    else
                                    {
                                        pole[x + 1, y] = 1;

                                    }
                                    poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1, y, cislo);
                                }
                                else
                                {
                                    if (pole[x, y - 1] == 0)
                                    {
                                        pole[x, y - 1] = 1;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, cislo);
                                    }
                                    else if (pole[x, y + 1] == 0)
                                    {
                                        pole[x, y + 1] = 1;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, cislo);
                                    }
                                }
                            }
                            pole[x, y] = 0;
                        }
                        else
                        {
                            if (y != prijemy)
                            {

                                if (y > prijemy)
                                {
                                    if (pole[x, y - 1] == 2 || pole[x, y - 1] == 0)
                                    {
                                        if (pole[x, y - 1] == 2)
                                        {
                                            pole[x, y - 1] = 3;
                                        }
                                        else
                                        {
                                            pole[x, y - 1] = 1;

                                        }
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, cislo);
                                    }
                                    else
                                    {
                                        if (pole[x - 1,y] == 0)
                                        {
                                            pole[x - 1,y] = 1;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, cislo);
                                        }
                                        else if (pole[x + 1,y] == 0)
                                        {
                                            pole[x + 1,y] = 1;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1,y, cislo);
                                        }
                                    }
                                }
                                else if (y < prijemy)
                                {
                                    if (pole[x, y + 1] == 2 || pole[x, y + 1] == 0)
                                    {
                                        if (pole[x, y + 1] == 2)
                                        {
                                            pole[x, y + 1] = 3;
                                        }
                                        else
                                        {
                                            pole[x, y + 1] = 1;

                                        }
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, cislo);
                                    }
                                    else
                                    {
                                        if (pole[x - 1, y] == 0)
                                        {
                                            pole[x - 1, y] = 1;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, cislo);
                                        }
                                        else if (pole[x + 1, y] == 0)
                                        {
                                            pole[x + 1, y] = 1;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1, y, cislo);
                                        }
                                    }
                                }
                            }
                            pole[x, y] = 0;
                        }
                    }
                    
                    if (pole[x, y] == 3 && cislo == -1)
                    {
                       
                        Random rnds = new Random();
                        int rng = rnds.Next(0, pocetKoncu);
                                               
                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y, rng);
                        
                        


                    }
                    
                    if ((pole[x, y] == 3 && cislo != -1) || pole[x,y] == 4)
                    {
                        int konecx = poziceKoncu[cislo].Item1;
                        int konecy = poziceKoncu[cislo].Item2;                        
                        
                        
                        if (x != konecx)
                        {
                            if (x > konecx)
                            {
                                if (pole[x - 1, y] == pole[konecx, konecy] || pole[x - 1, y] == 0)
                                {
                                    if (pole[x - 1, y] == pole[konecx, konecy])
                                    {
                                        pole[x - 1, y] = -1;
                                    }
                                    else
                                    {
                                        pole[x - 1, y] = 4;
                                    }
                                    poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, cislo);
                                }
                                else
                                {
                                    if (pole[x, y - 1] == 0)
                                    {
                                        
                                        pole[x, y - 1] = 4;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, cislo);
                                    }
                                    else if (pole[x, y + 1] == 0)
                                    {
                                        pole[x, y + 1] = 4;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, cislo);
                                    }

                                }
                            }
                            else if (x < konecx)
                            {
                                if (pole[x + 1, y] == pole[konecx, konecy] || pole[x + 1, y] == 0)
                                {
                                    if (pole[x + 1, y] == pole[konecx, konecy])
                                    {
                                        pole[x + 1, y] = -1;
                                    }
                                    else
                                    {
                                        pole[x + 1, y] = 4;
                                    }
                                    poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1, y, cislo);
                                }
                                else
                                {
                                    if (pole[x, y - 1] == 0)
                                    {
                                        pole[x, y - 1] = 4;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, cislo);
                                    }
                                    else if (pole[x, y + 1] == 0)
                                    {
                                        pole[x, y + 1] = 4;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, cislo);
                                    }
                                }
                            }
                            if (pole[x,y] == 3) { pole[x, y] = 2; } else { pole[x, y] = 0; }
                            
                        }
                        else
                        {
                            if (y != konecy)
                            {
                                if (y > konecy)
                                {
                                    if (pole[x, y - 1] == pole[konecx, konecy] || pole[x, y - 1] == 0)
                                    {
                                        if (pole[x, y - 1] == pole[konecx, konecy])
                                        {
                                            pole[x, y - 1] = -1;
                                        }
                                        else
                                        {
                                            pole[x, y - 1] = 4;
                                        }
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, cislo);
                                    }
                                    else
                                    {
                                        if (pole[x - 1, y] == 0)
                                        {
                                            pole[x - 1, y] = 4;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, cislo);

                                        }
                                        else if (pole[x + 1, y] == 0)
                                        {
                                            pole[x + 1, y] = 4;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1, y, cislo);
                                        }
                                    }
                                }
                                else if (y < konecy)
                                {
                                    if (pole[x, y + 1] == pole[konecx, konecy] || pole[x, y + 1] == 0)
                                    {
                                        if (pole[x, y + 1] == pole[konecx, konecy])
                                        {
                                            pole[x, y + 1] = -1;
                                        }
                                        else
                                        {
                                            pole[x, y + 1] = 4;
                                        }
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, cislo);
                                    }
                                    else
                                    {
                                        if (pole[x - 1, y] == 0)
                                        {
                                            pole[x - 1, y] = 4;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, cislo);
                                        }
                                        else if (pole[x + 1, y] == 0)
                                        {
                                            pole[x + 1, y] = 4;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1, y, cislo);
                                        }
                                    }
                                }
                            }
                            if (pole[x, y] == 3) { pole[x, y] = 2; } else { pole[x, y] = 0; }
                        }
                    }

                    if (pole[x, y] == -1) 
                    {

                        if (x != prijemx)
                        {
                            if (x > prijemx)
                            {
                                if (pole[x - 1, y] == 0)
                                {

                                    pole[x - 1, y] = 1;


                                    poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, -1);
                                }
                                else
                                {
                                    if (pole[x, y - 1] == 0)
                                    {
                                        pole[x, y - 1] = 1;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, -1);
                                    }
                                    else if (pole[x, y + 1] == 0)
                                    {
                                        pole[x, y - 1] = 1;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, -1);
                                    }
                                }
                            }
                            else if (x < prijemx)
                            {
                                if (pole[x + 1, y] == 0)
                                {
                                    pole[x + 1, y] = 1;


                                    poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1, y, -1);
                                }
                                else
                                {
                                    if (pole[x, y - 1] == 0)
                                    {
                                        pole[x, y - 1] = 1;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, -1);
                                    }
                                    else if (pole[x, y + 1] == 0)
                                    {
                                        pole[x, y - 1] = 1;
                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, -1);
                                    }
                                }
                            }
                            
                            pole[x, y] = 5 + cislo;
                        }
                        else
                        {
                            if (y != prijemy)
                            {
                                if (y > prijemy)
                                {
                                    if (pole[x, y - 1] == 0)
                                    {
                                        pole[x, y - 1] = 1;


                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y - 1, -1);
                                    }
                                    else
                                    {
                                        if (pole[x - 1, y] == 0)
                                        {
                                            pole[x - 1, y] = 1;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, -1);
                                        }
                                        else if (pole[x + 1, y] == 0)
                                        {
                                            pole[x + 1, y] = 1;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1, y, -1);
                                        }
                                    }
                                }
                                else if (y < prijemy)
                                {
                                    if (pole[x, y + 1] == 0)
                                    {
                                        pole[x, y + 1] = 1;


                                        poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x, y + 1, -1);
                                    }
                                    else
                                    {
                                        if (pole[x - 1, y] == 0)
                                        {
                                            pole[x - 1, y] = 1;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x - 1, y, -1);
                                        }
                                        else if (pole[x + 1, y] == 0)
                                        {
                                            pole[x + 1, y] = 1;
                                            poziceRobotu[poziceRobotu.IndexOf(pozice)] = new Tuple<int, int, int>(x + 1, y, -1);
                                        }
                                    }
                                }
                            }
                            
                            pole[x, y] = 5 + cislo;
                        }
                        
                    }
                }
                bylProvedenPohyb = true;
                GenerovaniMrizky(sloupec, radek, velikost, pictureBox1, pole);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled=false;
            }
        }
    }
}
