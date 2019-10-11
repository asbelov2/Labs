using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        Hero player = new Hero();
        Bat bat;
        Random rnd = new Random(DateTime.Now.Millisecond);
        Result resWindow;
        static public int killedBats = 0;

        public MainWindow()
        {
            InitializeComponent();
            resWindow = new Result();
            player.Initialize(canvas);
            bat = new Bat(new Point((rnd.Next()%(canvas.Width-200)+100),( rnd.Next() % (canvas.Height - 200) + 100)),player.GetDirection());
            bat.Initialize(canvas);
            timer.Tick += new EventHandler(Draw);
            timer.Tick += new EventHandler(Update);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 33);
            timer.Start();
        }


        private void Update(object sender, EventArgs e)
        {
            player.Update(canvas.Height, canvas.Width);
            bat.Update(canvas.Height, canvas.Width);
            int res = bat.Collision(player.GetGun(),player);
            if(res == 1)
            {
                killedBats++;
                BatsKilled.Content = killedBats.ToString();
                bat = new Bat(new Point((rnd.Next() % (canvas.Width - 200) + 100), (rnd.Next() % (canvas.Height - 200) + 100)), player.GetDirection());
                bat.Initialize(canvas);
            }
            else if (res == 2)
            {
                res = 0;
                resWindow.Show();
            }
            if(Result.isDone)
            {
                Close();
            }
        }


        private void Draw(object sender, EventArgs e)
        {
            bat.Draw();
            player.Draw();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            player.KeyDown(sender, e);     
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            player.KeyDown(sender, e);
        }
    }
}
