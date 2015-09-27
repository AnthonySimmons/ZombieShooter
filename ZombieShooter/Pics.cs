using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ZombieShooter
{
    class Pics
    {
        public Pics()
        {
            explosion = new Bitmap("explosion.png");
            build = new Bitmap("building.gif");
            ally = new Bitmap("Troop.png");
            shooting = new Bitmap("Firing.png");
            enemy = new Bitmap("EnemyTroop.png");
            MG = new Bitmap("MG.jpg");
            MG.MakeTransparent();
            enemy.MakeTransparent();
            shooting.MakeTransparent();
            ally.MakeTransparent();
            explosion.MakeTransparent();
            build.MakeTransparent();
        }


        public Bitmap MG;
        public Bitmap enemy;
        public Bitmap shooting;
        public Bitmap ally;
        public Bitmap build;
        public Bitmap explosion;

        public int eX;
        public int sX;
        public int cY;
        public int bY;
        public int mY;
    }
}
