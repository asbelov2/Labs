using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace lab4
{
    enum Ways
    {
        South,
        West,
        North,
        East
    }
    class Hero
    {
        //private
        Image hero = new Image();
        String[][] heroAfkSprites = new String[4][];
        String[][] heroWalkSprites = new String[4][];
        BitmapSource[][] heroAfk = new BitmapSource[4][];
        BitmapSource[][] heroWalk = new BitmapSource[4][];
        Ways heroDirection = 0;
        bool isHeroAfk = false;
        bool attackTrigger = false;
        bool isAttacking = false;
        bool isAction = false;
        int heroFrame = 0;
        Point heroPosition = new Point(0, 0);
        Gun bullet;

        public Hero()
        {

        }

        public Hero(Point position)
        {
            heroPosition = position;
        }

        public void Initialize(Canvas canvas)
        {
            bullet = new Gun(heroPosition, heroDirection);
            bullet.Initialize(canvas);
            canvas.Children.Add(hero);
            hero.Margin = new Thickness(0, canvas.Height - 70, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                heroAfkSprites[i] = new string[3];
                heroAfk[i] = new BitmapSource[3];
                for (int j = 0; j < 3; j++)
                {
                    switch (i)
                    {
                        case 0:
                            heroAfkSprites[i][j] = "afks" + (j + 1).ToString() + ".png";
                            break;
                        case 1:
                            heroAfkSprites[i][j] = "afkw" + (j + 1).ToString() + ".png";
                            break;
                        case 2:
                            heroAfkSprites[i][j] = "afkn1" + ".png";
                            break;
                        case 3:
                            heroAfkSprites[i][j] = "afke" + (j + 1).ToString() + ".png";
                            break;
                    }
                    heroAfk[i][j] = new BitmapImage(new Uri(heroAfkSprites[i][j], UriKind.Relative));
                }
            }
            for (int i = 0; i < 4; i++)
            {
                heroWalkSprites[i] = new string[10];
                heroWalk[i] = new BitmapSource[10];
                for (int j = 0; j < 10; j++)
                {
                    switch (i)
                    {
                        case 0:
                            heroWalkSprites[i][j] = "walks" + (j + 1).ToString() + ".png";
                            break;
                        case 1:
                            heroWalkSprites[i][j] = "walkw" + (j + 1).ToString() + ".png";
                            break;
                        case 2:
                            heroWalkSprites[i][j] = "walkn" + (j + 1).ToString() + ".png";
                            break;
                        case 3:
                            heroWalkSprites[i][j] = "walke" + (j + 1).ToString() + ".png";
                            break;
                    }
                    heroWalk[i][j] = new BitmapImage(new Uri(heroWalkSprites[i][j], UriKind.Relative));
                }
            }
        }

        public void Update(double canvH, double canvW)
        {
            if (!isHeroAfk)
            {
                switch (heroDirection)
                {
                    case Ways.South:
                        heroPosition.Y -= 5;
                        break;
                    case Ways.West:
                        heroPosition.X -= 5;
                        break;
                    case Ways.North:
                        heroPosition.Y += 5;
                        break;
                    case Ways.East:
                        heroPosition.X += 5;
                        break;
                }
            }
            heroPosition.X = heroPosition.X > 0 ? heroPosition.X : 0;
            heroPosition.X = heroPosition.X < (canvW - 60) ? heroPosition.X : (canvW - 60);
            heroPosition.Y = heroPosition.Y < (canvH - 70) ? heroPosition.Y : (canvH - 70);
            heroPosition.Y = heroPosition.Y > 0 ? heroPosition.Y : 0;
            hero.Margin = new Thickness(heroPosition.X, canvH - 70 - heroPosition.Y, 0, 0);
            isHeroAfk = true;

            if(attackTrigger && !isAttacking)
            {
                bullet.SetDirection(heroDirection);
                bullet.SetPosition(heroPosition);
                isAttacking = true;
                attackTrigger = false;
            }
            if(isAttacking)
            {
                attackTrigger = false;
                if (bullet.Update(canvH,canvW)==-1)
                {
                    isAttacking = false;
                }
            }
        }

        public void Draw()
        {
            if (isHeroAfk)
            {
                hero.Source = heroAfk[(int)heroDirection][(heroFrame % 6) / 2];
                heroFrame++;
            }
            else
            {
                hero.Source = heroWalk[(int)heroDirection][heroFrame % 10];
                heroFrame++;
            }
            if(isAttacking)
            {
                bullet.Draw();
            }
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    heroDirection = Ways.North;
                    isHeroAfk = false;
                    break;
                case Key.A:
                    heroDirection = Ways.West;
                    isHeroAfk = false;
                    break;
                case Key.S:
                    heroDirection = Ways.South;
                    isHeroAfk = false;
                    break;
                case Key.D:
                    heroDirection = Ways.East;
                    isHeroAfk = false;
                    break;
                case Key.LeftCtrl:
                    attackTrigger = true;
                    break;
                case Key.Space:
                    heroPosition.X += 80 * (heroDirection == Ways.East ? 1 : heroDirection == Ways.West ? -1 : 0);
                    heroPosition.Y += 80 * (heroDirection == Ways.North ? 1 : heroDirection == Ways.South ? -1 : 0);
                    break;
                case Key.E:
                    isAction = true;
                    break;
                default:
                    isHeroAfk = true;
                    break;
            }
        }

        public Point GetPosition()
        {
            return heroPosition;
        }

        public Ways GetDirection()
        {
            return heroDirection;
        }

        public Gun GetGun()
        {
            return bullet;
        }

        ~Hero()
        {

        }
    }
}
