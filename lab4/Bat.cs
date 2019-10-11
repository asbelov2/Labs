using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace lab4
{
    class Bat
    {
        Image bat = new Image();
        String[][] batSprites = new String[4][];
        BitmapSource[][] batFly = new BitmapSource[4][];
        Ways direction = 0;
        int batFrame = 0;
        Random rnd = new Random(DateTime.Now.Millisecond);
        Point position = new Point(0, 0);
        Point pathVector;

        public Bat()
        {
        }

        public Bat(Point batPosition, Ways batDirection)
        {
            position = batPosition;
            direction = batDirection;
            pathVector = new Point(batDirection == Ways.West ? -1 : batDirection == Ways.East ? 1 : 0, batDirection == Ways.South ? -1 : batDirection == Ways.North ? 1 : 0);
        }
        public void Initialize(Canvas canvas)
        {
            canvas.Children.Add(bat);
            bat.Margin = new Thickness(position.X, canvas.Height - 80 - position.Y, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                batSprites[i] = new string[4];
                batFly[i] = new BitmapSource[4];
                for (int j = 0; j < 4; j++)
                {
                    switch (i)
                    {
                        case 0:
                            batSprites[i][j] = "flys" + (j + 1).ToString() + ".png";
                            break;
                        case 1:
                            batSprites[i][j] = "flyw" + (j + 1).ToString() + ".png";
                            break;
                        case 2:
                            batSprites[i][j] = "flyn" + (j + 1).ToString() + ".png";
                            break;
                        case 3:
                            batSprites[i][j] = "flye" + (j + 1).ToString() + ".png";
                            break;
                    }
                    batFly[i][j] = new BitmapImage(new Uri(batSprites[i][j], UriKind.Relative));
                }
            }
        }

        public int Update(double canvH, double canvW)
        {
            pathVector.X = (pathVector.X + rnd.NextDouble() / 2 - 0.25) % 1;
            pathVector.Y = (pathVector.X - rnd.NextDouble() / 2 - 0.25) % 1;
            direction = (pathVector.X - pathVector.Y > 0) ? ((pathVector.X > 0) ? Ways.East : Ways.West) : ((pathVector.Y > 0) ? Ways.North : Ways.South);
            position.X += pathVector.X*10;
            position.Y += pathVector.Y*10;
            position.X = position.X > 0 ? position.X : 0;
            position.X = position.X < (canvW - 60) ? position.X : (canvW - 60);
            position.Y = position.Y < (canvH - 70) ? position.Y : (canvH - 70);
            position.Y = position.Y > 0 ? position.Y : 0;
            bat.Margin = new Thickness(position.X, canvH - 70 - position.Y, 0, 0);
            return 0;
        }

        public int Collision(Gun gun, Hero hero)
        {
            Point gunCentre = new Point(gun.GetPosition().X + 20, gun.GetPosition().Y - 20);
            Point heroCentre = new Point(hero.GetPosition().X + 16, hero.GetPosition().Y - 28);
            if (Math.Abs(gunCentre.X-position.X) < 40 && Math.Abs(gunCentre.Y - position.Y) < 28)
            {
                return 1;
            }
            if (Math.Abs(heroCentre.X - position.X) < 32 && Math.Abs(heroCentre.Y - position.Y) < 32)
            {
                return 2;
            }
            return 0;
        }

        public void Draw()
        {
            bat.Source = batFly[(int)direction][(batFrame % 8) / 2];
            batFrame++;
        }
    }
}
