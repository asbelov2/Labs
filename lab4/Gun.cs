using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace lab4
{
    class Gun
    {
        Image gun = new Image();
        String[][] gunSprites = new String[4][];
        BitmapSource[][] gunShoot = new BitmapSource[4][];
        Ways direction = 0;
        int gunFrame = 0;
        Point position = new Point(0, 0);

        public Gun()
        {

        }

        public Gun(Point bulletPosition, Ways bulletDirection)
        {
            position = bulletPosition;
            direction = bulletDirection;
        }
        public void Initialize(Canvas canvas)
        {
            canvas.Children.Add(gun);
            gun.Margin = new Thickness(0, canvas.Height - 80, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                gunSprites[i] = new string[4];
                gunShoot[i] = new BitmapSource[4];
                for (int j = 0; j < 4; j++)
                {
                    switch (i)
                    {
                        case 0:
                            gunSprites[i][j] = "fires" + (j + 1).ToString() + ".png";
                            break;
                        case 1:
                            gunSprites[i][j] = "firew" + (j + 1).ToString() + ".png";
                            break;
                        case 2:
                            gunSprites[i][j] = "firen" + (j + 1).ToString() + ".png";
                            break;
                        case 3:
                            gunSprites[i][j] = "firee" + (j + 1).ToString() + ".png";
                            break;
                    }
                    gunShoot[i][j] = new BitmapImage(new Uri(gunSprites[i][j], UriKind.Relative));
                }
            }
        }

        public int Update(double canvH, double canvW)
        {
            switch (direction)
            {
                case Ways.South:
                    position.Y -= 20;
                    break;
                case Ways.West:
                    position.X -= 20;
                    break;
                case Ways.North:
                    position.Y += 20;
                    break;
                case Ways.East:
                    position.X += 20;
                    break;
            }
            gun.Margin = new Thickness(position.X, canvH - 80 - position.Y, 0, 0);
            if ((position.X < -75) || (position.X > canvW) ||( position.Y < -65) || (position.Y > canvH))
            {
                return -1;
            }
            return 0;
        }
        public void Draw()
        {
            gun.Source = gunShoot[(int)direction][(gunFrame % 8) / 2];
            gunFrame++;
        }

        public void SetPosition(Point pos)
        {
            position = pos;
        }

        public void SetDirection(Ways way)
        {
            direction = way;
        }

        public Point GetPosition()
        {
            return position;
        }

        public Ways GetDirection()
        {
            return direction;
        }

        ~Gun()
        {

        }
    }
}
