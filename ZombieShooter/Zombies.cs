using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieShooter
{
    class Zombies
    {
        public Zombies()
        {
            x = 0;
            y = 0;
            height = 10;
            width = 10;
            vX = 0;
            vY = 0;
            alive = false;
            gun = new Weapons();
        }

        public bool barracks;
        public bool visible;
        public int bunkerNum;
        public bool garrison;
        public float startX;
        public float startY;
        public Weapons gun;
        public bool alive;
        public float x;
        public float y;
        public float height;
        public float width;
        public float vX;
        public float vY;


    }
}
