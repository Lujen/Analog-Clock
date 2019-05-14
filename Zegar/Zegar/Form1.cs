using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zegar
{   struct vector2
    {
        public int x;
        public int y;
        public vector2(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public partial class Form1 : Form
    {
        
        vector2 LeftUpperClock;
        vector2 CenterOfTheClock;

        vector2 SecondPointer; // wektor sekundowy
        vector2 HourPointer; // wektor godziny
        vector2 MinutePointer; // wektor minuty

        Graphics g; // używanie grafiki
        public Form1()
        {
            InitializeComponent();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            g = this.CreateGraphics();

            g.DrawRectangle(new Pen(Color.Silver, 6), LeftUpperClock.x-6, LeftUpperClock.y-6, 312, 312);
            g.DrawEllipse(new Pen(Color.DarkRed, 6), LeftUpperClock.x, LeftUpperClock.y, 300, 300);
            g.DrawString("12", new Font("Arial", 26, FontStyle.Bold), Brushes.Black, new Point(ActiveForm.Width / 2 - 25, ActiveForm.Height / 2 - 140));
            g.DrawString("6", new Font("Arial", 26, FontStyle.Bold), Brushes.Black, new Point(ActiveForm.Width / 2 - 16, ActiveForm.Height / 2 + 110));
            g.DrawString("3", new Font("Arial", 26, FontStyle.Bold), Brushes.Black, new Point(ActiveForm.Width / 2 + 110, ActiveForm.Height / 2 - 19));
            g.DrawString("9", new Font("Arial", 26, FontStyle.Bold), Brushes.Black, new Point(ActiveForm.Width / 2 - 140, ActiveForm.Height / 2 - 19));


            DateTime teraz = DateTime.Now;

            g.DrawLine(new Pen(Color.SlateGray, 3), CenterOfTheClock.x, CenterOfTheClock.y, SecondPointer.x, SecondPointer.y);// usuwanie poprzedniej wskazówki sekund

            if (teraz.Second == 0.000f)
            {
                g.DrawLine(new Pen(Color.SlateGray, 4), CenterOfTheClock.x, CenterOfTheClock.y, MinutePointer.x, MinutePointer.y);
                // usuwanie poprzedniej wskazówki minut co 60 sekund(sekundy wchodzą w cykl nowej minuty)
            }
            if (teraz.Minute == 0.0f)
            {
                g.DrawLine(new Pen(Color.SlateGray, 5), CenterOfTheClock.x, CenterOfTheClock.y, HourPointer.x, HourPointer.y);
                // usuwanie poprzedniej wskazówki godziny co 60 minut(kiedy minuty sie wyzerują i wejda w cykl nowej godziny)
            }
            //sekundy

            SecondPointer.x = CenterOfTheClock.x + (int)(140 * Math.Sin(Math.PI * teraz.Second*6 / 180));
            SecondPointer.y = CenterOfTheClock.y - (int)(140 * Math.Cos(Math.PI * teraz.Second*6/ 180));

            g.DrawLine(new Pen(Color.Black, 3), CenterOfTheClock.x, CenterOfTheClock.y, SecondPointer.x, SecondPointer.y);//rysowanie wskazówki

            //minuty
            
            MinutePointer.x = CenterOfTheClock.x + (int)(120 * Math.Sin(Math.PI * teraz.Minute * 6 / 180));
            MinutePointer.y = CenterOfTheClock.y - (int)(120 * Math.Cos(Math.PI * teraz.Minute * 6 / 180));

            g.DrawLine(new Pen(Color.Black, 4), CenterOfTheClock.x, CenterOfTheClock.y, MinutePointer.x, MinutePointer.y);//rysowanie wskazówki
            // godziny
            
            HourPointer.x = CenterOfTheClock.x + (int)(100 * Math.Sin(Math.PI * teraz.Hour * 30 / 180));
            HourPointer.y = CenterOfTheClock.y - (int)(100 * Math.Cos(Math.PI * teraz.Hour * 30 / 180));

            g.DrawLine(new Pen(Color.Black, 5), CenterOfTheClock.x, CenterOfTheClock.y, HourPointer.x, HourPointer.y);//rysowanie wskazówki

            Time.Text = teraz.Hour + ":" + teraz.Minute + ":" + teraz.Second;
            g.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            CenterOfTheClock = new vector2(ActiveForm.Width/2, ActiveForm.Height/2);           
            LeftUpperClock = new vector2((ActiveForm.Width / 2)-150, (ActiveForm.Height / 2)-150);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
        }
    }
}
