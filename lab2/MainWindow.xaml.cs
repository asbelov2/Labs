using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    /* 
     * Цель работы: освоить основные приемы создания приложений с
    использованием графических примитивов.

     * Задание: разработать интерфейс приложения с циклическим процессом
    движения/видоизменения множества графических примитивов (например,
    объектов Shape).

     * Примерные варианты заданий:
    1. Фейерверк.
    2. Гирлянда.
    3. Мыльные пузыри. */
    public partial class MainWindow : Window
    {
        const int NDROPS = 130;
        double speed = 4;
        double angle = 30;
        double level = 4;
        Random rnd = new Random(42);
        int drops = 0;
        int idrop = 0;
        double[][] rain = new double[NDROPS][];
        Line[] tmpLine = new Line[NDROPS];
        BlurEffect[] blurEffect = new BlurEffect[NDROPS];

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < NDROPS; i++)
            {
                rain[i] = new double[3];
                rain[i][0] = -1;
                rain[i][1] = -1;
                rain[i][2] = -1;
                blurEffect[i] = new BlurEffect();
                tmpLine[i] = new Line();
                tmpLine[i].Stroke = Brushes.Blue;
                tmpLine[i].StrokeThickness = 1;
            }

            DispatcherTimer timer = new DispatcherTimer();

            timer.Tick += new EventHandler(Update);
            timer.Tick += new EventHandler(Draw);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            timer.Start();
        }
        private void Update(object sender, EventArgs e)
        {
            _ = double.TryParse(Angle.Text, out angle);
            _ = double.TryParse(Speed.Text, out speed);
            _ = double.TryParse(Level.Text, out level);
            angle = 2.0 * Math.PI * (angle / 360.0);
            if (drops < NDROPS)
            {
                for (int i = 30; i < canvas.Width + 80; i++)
                {
                    if (drops >= NDROPS)
                        break;
                    if (rnd.NextDouble() > (1 - level/4000)) 
                    {
                        drops++;
                        idrop = (idrop + 1) % NDROPS;
                        rain[idrop][0] = i;
                        rain[idrop][1] = 0;
                        rain[idrop][2] = rnd.NextDouble()*4 + 2;
                    }
                }
            }
            for (int i = 0; i < NDROPS; i++)
            {
                if (rain[i][0] == -1)
                    continue;
                rain[i][0] -= Math.Sin(angle)*speed;
                rain[i][1] += Math.Cos(angle)*speed;
                if(rain[i][1] >= canvas.Height)
                {
                    rain[i][0] = -1;
                    rain[i][1] = 0;
                    drops--;
                    canvas.Children.Remove(tmpLine[i]);

                }
            }
        }

        private void Draw(object sender, EventArgs e)
        {
            canvas.Children.Clear();
            for (int i = 0; i < NDROPS; i++)
            {
                if (rain[i][0] == -1)
                    continue;
                blurEffect[i].Radius = rain[i][2];
                tmpLine[i].Effect = blurEffect[i];

                tmpLine[i].X1 = rain[i][0];
                tmpLine[i].Y1 = rain[i][1];
                tmpLine[i].X2 = rain[i][0] + Math.Sin(angle)*10;
                tmpLine[i].Y2 = rain[i][1] - Math.Cos(angle)*10;
                canvas.Children.Add(tmpLine[i]);
            }
        }

        private void Angle_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 44))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Speed_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 44))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Level_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33) && ((int)e.Key < 44))
            {

            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
