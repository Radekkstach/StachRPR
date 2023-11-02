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
            GenerovaniMrizky();
        }

        Panel mrizka;
        
        /// ////////////////////////////////////////////////
        
        public void GenerovaniMrizky()
        {

            /// 0 = prazdne misto
            /// 1 = robot
            /// 2 = misto prijmu
            /// 3 = misto dodani
            
            // Mrizka

            int radek = 5;
            int sloupec = 5;
            int velikost = 50;
            int robotx;

            mrizka = new Panel();
            mrizka.Size = new Size(radek * velikost, sloupec * velikost);
            mrizka.Location = new Point(0, 0);

            int[,] plocha = new int[radek, sloupec];

            // Robot 

            Random rng = new Random();
            robotx = rng.Next(radek);
            Console.WriteLine("X robota: " + (robotx+1));
            int roboty = rng.Next(sloupec);
            Console.WriteLine("Y robota: " + (roboty+1));
            
            for (int x = 0; x < radek; x++)
            {
                for(int y = 0; y < sloupec; y++)
                {
                    plocha[x, y] = 0;
                    Panel mriz = new Panel();
                    mriz.Location = new Point((x * velikost), (y * velikost));
                    mriz.Size = new Size(velikost, velikost);
                    mriz.BorderStyle = BorderStyle.FixedSingle;
                    mriz.BackColor = SystemColors.Control;

                    if(robotx == x&& roboty == y)
                    { 
                        plocha[x, y] = 1;
                        mriz.BackColor = Color.Red;
                    }

                    mrizka.Controls.Add(mriz);

                    
                }
                
            }

            Controls.Add(mrizka);

            // Kontrola konzole
            Console.WriteLine("\n\nMrizka:");
            for (int y = 0; y < sloupec; y++)
            {
                for (int x = 0; x < radek; x++)
                {
                    Console.Write(plocha[x, y] + " ");
                }
                Console.WriteLine();
            }


        }

        
    }
}
