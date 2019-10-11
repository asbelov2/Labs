using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random(4242);
        uint level = 1;
        uint loseTime = 30;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Storyboard storyBoard = new Storyboard();
        ThicknessAnimationUsingKeyFrames stopButtonAnimation = new ThicknessAnimationUsingKeyFrames();
        DoubleAnimationUsingKeyFrames stopButtonWidthAnimation = new DoubleAnimationUsingKeyFrames();
        string conamiCode = "uuddlrlrba";

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Start();
            LoseTime.Content = loseTime.ToString();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (loseTime>0)
            {
                loseTime -= 1;
                LoseTime.Content = loseTime.ToString();
                if (loseTime <= 5)
                {
                    LoseTime.Foreground = System.Windows.Media.Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show("You lose\nYou've reached level " + level.ToString());
                StopButton.Visibility = Visibility.Hidden;
                timer.Stop();
            }
        }

        private void PrepareWidthAnimation()
        {
            int i;
            for (i = 52; i > 20; i -= 1)
                stopButtonWidthAnimation.KeyFrames.Add(new DiscreteDoubleKeyFrame(i));
        }

        private void PrepareAnimation()
        {
            stopButtonAnimation = new ThicknessAnimationUsingKeyFrames();
            stopButtonWidthAnimation = new DoubleAnimationUsingKeyFrames();
            double i;
            double j;
            double pi = 0;
            double pj = 0;
            Point vec = new Point(0, 0);
            if (level <= 4)
            {
                for (i = StopButton.Margin.Top; i < Height - 100; i += 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(StopButton.Margin.Left, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (j = StopButton.Margin.Left; j < Width - 273; j += 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (; i > 40; i -= 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (; j > 63; j -= 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
            }
            else if (level <= 9)
            {
                for (i = 180; i < 200 + rnd.Next(40, 350); i += 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(350, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (j = 350; j < 400 + rnd.Next(60, 600); j += 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                pi = i; pj = j;
                for (; i > pi - rnd.Next(50, 350); i -= 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (; j > pj - rnd.Next(70, 600); j -= 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                pi = i; pj = j;
                for (; i < pi + rnd.Next(40, 350); i += 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (; j < pj + rnd.Next(60, 600); j += 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                pi = i; pj = j;
                for (; i > pi - rnd.Next(50, 350); i -= 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (; j > pj - rnd.Next(70, 600); j -= 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                pi = i; pj = j;
                for (; i < pi + rnd.Next(40, 350); i += 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (; j < pj + rnd.Next(60, 600); j += 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                pi = i; pj = j;
                for (; i > pi - rnd.Next(50, 350); i -= 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                for (; j > pj - rnd.Next(70, 600); j -= 1)
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
            }
            else
            {
                i = 180;j = 350;
                for(int n=0; n<10000 ; n++)
                {
                    vec.X += (j > 500) ? (-Math.Abs((rnd.NextDouble() - 0.5) / 10)) : ((j < 200) ? Math.Abs((rnd.NextDouble() - 0.5) / 10) : ((rnd.NextDouble() - 0.5) / 10));
                    vec.Y += (i > 280) ? (-Math.Abs((rnd.NextDouble() - 0.5) / 10)) : ((i < 80) ? Math.Abs((rnd.NextDouble() - 0.5) / 10) : ((rnd.NextDouble() - 0.5) / 10));
                    i += vec.Y;
                    j += vec.X;
                    stopButtonAnimation.KeyFrames.Add(new DiscreteThicknessKeyFrame(new Thickness(j, i, StopButton.Margin.Right, StopButton.Margin.Bottom)));
                }
            }
        }
        private void DoAnimation()
        {
            stopButtonAnimation.Duration = new TimeSpan(0, 0, 0, 0, 30000);
            Storyboard.SetTargetProperty(stopButtonAnimation, new PropertyPath("(Button.Margin)"));
            Storyboard.SetTarget(stopButtonAnimation, StopButton);
            stopButtonAnimation.RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(loseTime*storyBoard.SpeedRatio));
            if(level>2)
            {
                Storyboard.SetTargetProperty(stopButtonWidthAnimation, new PropertyPath("(Button.Width)"));
                Storyboard.SetTarget(stopButtonWidthAnimation, StopButton);
                stopButtonWidthAnimation.RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(loseTime * storyBoard.SpeedRatio));
                stopButtonWidthAnimation.AutoReverse = true;
                storyBoard.Children.Add(stopButtonWidthAnimation);
            }
            storyBoard.Children.Add(stopButtonAnimation);
            if (level < 5)
            {
                storyBoard.SpeedRatio = Math.Pow(1.9, level);
            }
            else if (level < 10)
            {
                storyBoard.SpeedRatio = Math.Pow(1.8, level-4);
            }
            else
            {
                storyBoard.SpeedRatio = Math.Pow(Math.Sqrt(1.3), level - 9);
            }
            storyBoard.Begin();
        }

        private void ButtonAnimation_Completed(object sender, EventArgs e)
        {
            DoAnimation();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            storyBoard.Stop();
            MessageBoxResult result = MessageBox.Show("You win level "+level.ToString()+"! Congratulations!\nYour time is "+(30-loseTime)+ " seconds\n Do you want to go to the next level?", "Winner",MessageBoxButton.YesNo);
            level++;
            PrepareAnimation();
            if (result == MessageBoxResult.Yes)
            {
                lvl.Text = level.ToString();
                loseTime = 30;
                LoseTime.Foreground = System.Windows.Media.Brushes.Black;
                LoseTime.Content = loseTime.ToString();
                DoAnimation();
                timer.Start();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PrepareWidthAnimation();
            PrepareAnimation();
            DoAnimation();
            lvl.Text = level.ToString();
        }

        private void Again_Click(object sender, RoutedEventArgs e)
        {
            StopButton.Visibility = Visibility.Visible;
            level = 1;
            PrepareAnimation();
            lvl.Text = level.ToString();
            loseTime = 30;
            LoseTime.Foreground = System.Windows.Media.Brushes.Black;
            LoseTime.Content = loseTime.ToString();
            DoAnimation();
            timer.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (conamiCode[0] == 'u')
                    conamiCode = conamiCode.Substring(1);
                else
                    conamiCode = "uuddlrlrba";
            }
            if (e.Key == Key.Down)
            {
                if (conamiCode[0] == 'd')
                    conamiCode = conamiCode.Substring(1);
                else
                    conamiCode = "uuddlrlrba";
            }
            if (e.Key == Key.Left)
            {
                if (conamiCode[0] == 'l')
                    conamiCode = conamiCode.Substring(1);
                else
                    conamiCode = "uuddlrlrba";
            }
            if (e.Key == Key.Right)
            {
                if (conamiCode[0] == 'r')
                    conamiCode = conamiCode.Substring(1);
                else
                    conamiCode = "uuddlrlrba";
            }
            if (e.Key == Key.B)
            {
                if (conamiCode[0] == 'b')
                    conamiCode = conamiCode.Substring(1);
                else
                    conamiCode = "uuddlrlrba";
            }
            if (e.Key == Key.A)
            {
                if (conamiCode[0] == 'a')
                    conamiCode = conamiCode.Substring(1);
                else
                    conamiCode = "uuddlrlrba";
            }
            if (conamiCode.Length == 0)
            {
                MessageBox.Show("You entered conami code ;)");
                conamiCode = "uuddlrlrba";
                lvlLabel.Visibility = Visibility.Visible;
                lvl.Visibility = Visibility.Visible;
                cheater.Visibility = Visibility.Visible;
                skipTo.Visibility = Visibility.Visible;
            }
        }

        private void Lvl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            level = (uint)e.NewValue;
        }

        private void SkipTo_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            storyBoard.Stop();
            level = uint.Parse(lvl.Text);
            MessageBoxResult result = MessageBox.Show("Level " + level.ToString(), "Cheater", MessageBoxButton.OK);
            PrepareAnimation();
            lvl.Text = level.ToString();
            loseTime = 30;
            LoseTime.Foreground = System.Windows.Media.Brushes.Black;
            LoseTime.Content = loseTime.ToString();
            DoAnimation();
            timer.Start();
        }

        private void Lvl_KeyDown(object sender, KeyEventArgs e)
        {
            if ((((int)e.Key > 33) && ((int)e.Key < 44)) || (((int)e.Key > 73) && ((int)e.Key < 84)))
            {

            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
