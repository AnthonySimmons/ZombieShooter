using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ZombieShooter
{
    public partial class Form1 : Form
    {
        int barracks;
        Weapons []mortars;
        Pen bluePen;
        Pen redPen;
        Pics pics;
        Zombies[] bunkers;
        int stagger;
        bool rally;
        //Zombies[] tanks;
        Zombies []buildings;
        Point visual;
        Pen grPen;
        Pen blPen;
        Font TNR;
        Graphics g;
        Graphics text;
        Weapons bomb;
        Weapons []guns;
        Weapons cannons;
        SolidBrush blue;
        SolidBrush white;
        SolidBrush black;
        SolidBrush red;
        SolidBrush green;
        Random rand;
        int survivors;
        int level;
        int chX;
        int chY;
        int screenX;
        int screenY;
        int numBuildings;
        int numZombies;
        double zoom;
        bool machineGuns;
        int timeCount;
        int numBunks;
        int borderX;
        int numBunkers;
        int borderY;
        int numTargets;
        int numTanks;
        int numFriendlies;
        float angle;
        Point rallyPoint;
        int respawn;
        Zombies[] Zeds;
        Zombies[] allies;
        int numAllies;
         
        public Form1()
        {
            bluePen = new Pen(Color.Blue);
            redPen = new Pen(Color.Red);
            pics = new Pics();
            numBunkers = 3;
            numBunks = numBunkers;
            grPen = new Pen(Color.Green);
            rallyPoint = new Point();
            TNR = new Font("Times New Roman", 12);
            InitializeComponent();
            numBuildings = 30;
            buildings = new Zombies[numBuildings];
            level = 1;
            blPen = new Pen(Color.Black, 2);
            borderX = (int)(pictureBox1.Width * 1.7);
            borderY = (int)(pictureBox1.Width * 1.7);
            numTanks = 3;
            //tanks = new Zombies[numTanks];
            
            text = this.pictureBox1.CreateGraphics();
            g = this.pictureBox1.CreateGraphics();
            blue = new SolidBrush(Color.Blue);
            white = new SolidBrush(Color.White);
            black = new SolidBrush(Color.Black);
            red = new SolidBrush(Color.Red);
            green = new SolidBrush(Color.Green);
            //timer1.Start();
            chX = pictureBox1.Width / 2;
            chY = pictureBox1.Height / 2;
            screenX = 0;
            screenY = 0;
            zoom = 1.0;
            numZombies = 10;
            numAllies = 10;
            bunkers = new Zombies[numBunkers];

            Zeds = new Zombies[numZombies];
            allies = new Zombies[numAllies];
            guns = new Weapons[80];
            cannons = new Weapons();
            bomb = new Weapons();

            for (int i = 0; i < guns.Length; i++)
            {
                guns[i] = new Weapons();
            }
            rand = new Random();
            createBuildings();
            createZombies(numZombies);
            //createTanks();
            createBunkers();
            numTargets = numZombies;
            numFriendlies = numAllies;
            //g.TranslateTransform(Width / 2, Height / 2);
            bomb.timeFired = 0;
            cannons.timeFired = 0;
            visual.X = pictureBox1.Width;
            visual.Y = pictureBox1.Height;

            mortars = new Weapons[numBunkers];
            createMortars();
            g.TranslateTransform((float)-0.3*borderX, (float)-0.3*borderY);


            zoom *= 0.7;
            g.TranslateTransform(400, 200);
            g.ScaleTransform((float)0.7, (float)0.7);
        }

        private void createMortars()
        {
            for (int i = 0; i < numBunkers; i++)
            {
                mortars[i] = new Weapons();
                mortars[i].vX = 0;
                mortars[i].vY = 0;
                mortars[i].x = (int)bunkers[i].x;
                mortars[i].y = (int)bunkers[i].y;
                mortars[i].fired = false;
            }
        }
        private void respawnEnemies(Zombies[] tgt, int rS)
        {
            for (int i = 0; i < tgt.Length; i++)
            {
                if (!tgt[i].alive)// && rand.Next(0, rS) == 0)
                {
                    
                    //int j = -1;
                    //do{
                        //barracks++;
                        //j = barracks;
                    //}while(!buildings[j].alive && (j < buildings.Length - 1));// && !(buildings[j].x < borderX/2 && buildings[j].y < borderY/2)));
                    if (buildings[barracks].alive)// && buildings[j].x < borderX*0.7 && buildings[j].y < borderY*0.7)
                    {
                        //j = barracks;
                        tgt[i].alive = true;
                        tgt[i].x = buildings[barracks].x;
                        tgt[i].y = buildings[barracks].y;
                        //buildings[j].barracks = true;
                    }
                }
            }
        }
        private void fireMortars()
        {
            for (int i = 0; i < numBunkers; i++)
            {
                if (bunkers[i].alive && !mortars[i].fired && rand.Next(0, 200) == 0)
                {
                    mortars[i].timeFired = timeCount;
                    mortars[i].fired = true;
                    int j = -1;
                    if (numFriendlies > 0)
                    {
                        do
                        {
                            j++; //= rand.Next(0, numAllies);
                        } while (!allies[j].alive && j < allies.Length-1);
                    }
                    mortars[i].x = (int)allies[j].x + rand.Next(-75, 75);
                    mortars[i].y = (int)allies[j].y + rand.Next(-75, 75);
                }
                else
                {
                    
                }
            }
        }

        private void createBunkers()
        {
            for (int i = 0; i < numBunkers; i++)
            {
                bunkers[i] = new Zombies();
            }
            for (int i = 0; i < numBunkers; i++)
            {
                bunkers[i].vX = 0;
                bunkers[i].vY = 0;
                bunkers[i].width = 20;
                bunkers[i].height = 20;
                bunkers[i].alive = true;

                
                int j = -1;
                do
                {
                    j++; //= rand.Next() % numBuildings;
                } while ((buildings[j].x > (borderX / 2) || buildings[j].y > (borderY / 2)) || (buildings[j].garrison)&&(j < numBuildings-1));// && buildings[j].bunkerNum != i/2 && i != 0));
                buildings[j].garrison = true;
                bunkers[i].x = buildings[j].x + buildings[j].width - 20;
                bunkers[i].y = buildings[j].y + buildings[j].height - 20;

                
                buildings[j].bunkerNum = i;
                bunkers[i].visible = false;
            }
        }
        private void drawCrossHairs(int x, int y)
        {
            x -= 20;
            y -= 20;
            text.FillRectangle(black, (float)(((x+25))), (float)((y)), (float)(1), (float)(50.0));
            text.FillRectangle(black, (float)((x)), (float)(((y+25))), (float)(50), (float)(1));
            text.DrawEllipse(blPen, x+5, y+5, 40, 40);
            
            Weapons w = new Weapons();
            w.x = x;
            w.y = y;

            transformToPlane(w, 0, 0);

            //w.x = (int)(w.x / zoom);
            //w.y = (int)(w.y / zoom);


            
            //g.FillRectangle(red, (float)(((w.x + 9))), (float)((w.y)), (float)(2.0), (float)(20.0));
            //g.FillRectangle(red, (float)((w.x)), (float)(((w.y + 9))), (float)(20), (float)(2));
            g.FillEllipse(red, (float)w.x, (float)w.y, 20, 20);
            //g.FillRectangle(black, (float)(((x + 9) - screenX)), (float)((y - screenY)), (float)(2.0), (float)(20.0));
            //g.FillRectangle(black, (float)((x - screenX)), (float)(((y + 9) - screenY)), 20, 2);
        }
        /*private void createTanks()
        {
            for (int i = 0; i < numTanks; i++)
            {
                tanks[i] = new Zombies();
            }
            for (int i = 0; i < numTanks; i++)
            {
                tanks[i].width = 30;
                tanks[i].height = 30;
                tanks[i].alive = true;
                do{
                tanks[i].x = rand.Next(20, borderX/3);
                tanks[i].y = rand.Next(20, borderY/3);
                }while(garrison(i, tanks[i]));
            }
        }*/
        private void createBuildings()
        {
            for (int i = 0; i < numBuildings; i++)
            {
                buildings[i] = new Zombies();
            }
                for (int i = 0; i < numBuildings; i++)
                {
                    do
                    {
                        buildings[i].x = rand.Next(50, borderX - 100) - 30;
                        buildings[i].y = rand.Next(50, borderY - 100) - 30;
                        buildings[i].width = rand.Next(50, 140);
                        buildings[i].height = rand.Next(50, 140);
                        buildings[i].alive = true;
                    } while (buildingIntersect(i) || checkBorder(buildings[i]));

                    buildings[i].x += 20;
                    buildings[i].y += 20;

                    buildings[i].width -= 30;
                    buildings[i].height -= 30;
                }
                barracks = rand.Next(0, numBuildings);
                buildings[barracks].barracks = true;
        }
        private bool strayRounds(Weapons sh)
        {
            Rectangle shooter = new Rectangle((int)sh.x, (int)sh.y, (int)3, (int)3);
            for (int i = 0; i < numBuildings; i++)
            {
                if (buildings[i].alive)
                {
                    Rectangle target = new Rectangle((int)buildings[i].x, (int)buildings[i].y, (int)buildings[i].width, (int)buildings[i].height);
                    if (shooter.IntersectsWith(target))
                    {
                        sh.fired = false;
                        return true;
                    }
                }
            }
            return false;
        }
        private bool garrison(int i, Zombies sh)
        {
            Rectangle shooter = new Rectangle((int)sh.x, (int)sh.y, (int)sh.width, (int)sh.height);
            for (int j = 0; j < numBuildings; j++)
            {
                if (buildings[j].alive)
                {
                    Rectangle target = new Rectangle((int)buildings[j].x, (int)buildings[j].y, (int)buildings[j].width, (int)buildings[j].height);
                    if(shooter.IntersectsWith(target))
                    {   
                        return true;
                    }
                }
            }
                return false;
        }
        private bool buildingIntersect(int i)
        {
            Rectangle shooter = new Rectangle((int)buildings[i].x, (int)buildings[i].y, (int)buildings[i].width, (int)buildings[i].height);
            for (int j = 0; j < numBuildings; j++)
            {
                if (i != j)
                {
                    Rectangle target = new Rectangle((int)buildings[j].x, (int)buildings[j].y, (int)buildings[j].width, (int)buildings[j].height);
                    if (shooter.IntersectsWith(target))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void drawBuildings()
        {
            for (int i = 0; i < numBuildings; i++)
            {
                if (buildings[i].alive)
                {
                    Rectangle size = new Rectangle((int)buildings[i].x, (int)buildings[i].y, (int)buildings[i].width, (int)buildings[i].height);
                    Rectangle src = new Rectangle(0, 0, 116, 111);
                    //g.FillRectangle(black, buildings[i].x, buildings[i].y, buildings[i].width, buildings[i].height);
                    g.DrawImage(pics.build, size, src, GraphicsUnit.Pixel);
                    if (buildings[i].barracks)
                    {
                        g.DrawRectangle(redPen, buildings[i].x - 5, buildings[i].y - 5, buildings[i].width + 10, buildings[i].height + 10); 
                    }
                }
            }
        }
        private void drawWeapons()
        {
            for (int i = 0; i < numBunkers; i++)
            {
                if (mortars[i].fired && timeCount % 2 == 0)
                {
                    Zombies z = new Zombies();
                    z.x = mortars[i].x;
                    z.y = mortars[i].y;
                    z.width = 100;
                    z.height = 100;
                    z.alive = true;
                    collision(i, z, buildings, true, true);

                    pics.mY += 100;
                    mortars[i].vY += 100;
                    Rectangle size = new Rectangle(mortars[i].x, mortars[i].y, 60, 60);
                    Image m = pics.explosion;

                    g.DrawImage(m, size, 0, mortars[i].vY, 100, 100, GraphicsUnit.Pixel);
                }
            }
            if (bomb.fired && timeCount % 2 == 0)
            {
                pics.bY += 100;
                //g.FillEllipse(red, (bomb.x), (bomb.y), (150), (150));

                Rectangle size = new Rectangle(bomb.x, bomb.y, 200, 200);
                Image m = pics.explosion;

                g.DrawImage(m, size, 0, pics.bY, 100, 100, GraphicsUnit.Pixel);
            }
            for (int i = 0; i < guns.Length; i++)
            {
                if (guns[i].fired)
                {
                    if ((timeCount - guns[i].timeFired) > 50)
                    {
                        guns[i].fired = false;
                    }
                    g.FillEllipse(black, ((guns[i].x)), ((guns[i].y)), (3), (3));
                }
            }

            if (cannons.fired && timeCount % 2 == 0)
            {
                //g.FillEllipse(red, (cannons.x), (cannons.y), (75), (75));
                pics.cY += 100;
                Rectangle size = new Rectangle(cannons.x, cannons.y, 100, 100);
                Image m = pics.explosion;

                g.DrawImage(m, size, 0, pics.cY, 100, 100, GraphicsUnit.Pixel);
            }

        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            respawnEnemies(Zeds, 50);
            timeCount++;            

            if (timeCount % 20 == 0)
            {
                
                angle++;
                
                if (angle == 360)
                {
                    angle = 0;
                }
                //System.Drawing.Drawing2D.Matrix tran = g.Transform;
                //tran.RotateAt(1, new Point(borderX / 2, borderY / 2));
                //g.Transform = tran;
                g.TranslateTransform((float)(borderX / 2), (float)(borderY / 2));
                g.RotateTransform(1);
                g.TranslateTransform((float)(-borderX /2), (float)(-borderY / 2));
                
                
            }
            
            if (machineGuns)
            {
                fireGuns();
                //drawWeapons();
            }
            if (timeCount % 5 == 0)
            {
                g.Clear(Color.BurlyWood);

                drawBorder();

                drawBuildings();
                
                drawWeapons();
                drawCrossHairs(pictureBox1.Width/2, pictureBox1.Height/2);
                
                drawStrings();
               
                if (rally)
                {
                    drawRallyPoint();
                    moveToRallyPoint(allies, rallyPoint);
                }
                //if (numFriendlies < numAllies)
                {
                    if (numFriendlies > 0)
                    {
                        int j = -1;
                        do
                        {
                            j++;// = rand.Next(0, numAllies);
                        } while (!allies[j].alive && j < allies.Length-1);
                        moveToRallyPoint(Zeds, new Point((int)allies[j].x, (int)allies[j].y));
                    }
                }
                //else
                {
                   // moveZombies();
                }

                checkBomb(buildings);
                checkBomb(Zeds);
                checkBomb(allies);
                
                checkCannon(Zeds);
                checkCannon(allies);
                checkCannon(bunkers);
                smallArms(Zeds, allies, 50, numFriendlies);
                smallArms(allies, Zeds, 10, numTargets);
                smallArms(bunkers, allies, 1, numFriendlies);

                fireMortars();
                //smallArms(allies, bunkers, 10, numBunks);
                drawSmallArms(Zeds);
                drawSmallArms(allies);
                drawSmallArms(bunkers);
                checkSmallArms(Zeds, allies, false, 10);
                checkSmallArms(allies, Zeds, true, 1);
                checkSmallArms(bunkers, allies, false, 1);
                checkSmallArms(allies, bunkers, true, 5);
                //checkSmallArms(allies, bunkers);
                checkMortars(mortars, allies, true);
                checkMortars(mortars, Zeds, true);
                drawZombies();
                
                if (numTargets <= 0 && numBunks <= 0)
                {
                   // nextLevel(false);
                }
                if (numFriendlies <= 0)
                {
                    timer1.Stop();
                    MessageBox.Show("Mission Failed!");
                    nextLevel(true);
                }
            }
            if (timeCount % 5 == 0)
            {
                deployArtillery();
            }
        }
        private void deployArtillery()
        {
            for (int i = 0; i < numBunkers; i++)
            {
                if (Math.Abs(timeCount - mortars[i].timeFired) > 40)
                {
                    mortars[i].fired = false;
                    mortars[i].timeFired = 1;
                    pics.mY = 0;
                    mortars[i].vY = 0;
                }

                else if (timeCount - mortars[i].timeFired > 10 && mortars[i].timeFired != 0)
                {
                    mortars[i].fired = true;
                    //checkCannon();
                }
            }
                if (Math.Abs(timeCount - bomb.timeFired) > 100)
                {
                    text.DrawString("Bomb Ready", TNR, red, 450, 10);
                    bomb.fired = false;
                    pics.bY = 0;
                    bomb.timeFired = 1;
                }
                else if (timeCount - bomb.timeFired > 30 && bomb.timeFired != 0)
                {
                    bomb.fired = true;
                    checkBomb(buildings);
                    checkBomb(Zeds);
                    checkBomb(allies);

                }
            if (Math.Abs(timeCount - cannons.timeFired) > 40)
            {
                cannons.fired = false;
                cannons.timeFired = 1;
                pics.cY = 0;
            }
           
            else if (timeCount - cannons.timeFired > 10 && cannons.timeFired != 0)
            {
                cannons.fired = true;
                //checkCannon();
            }

        }
        private void nextLevel(bool reload)
        {
            rally = false;
            visual.X = pictureBox1.Width;
            visual.Y = pictureBox1.Height;
            if (!reload)
            {
                borderX += 100;
                borderY += 100;
                level++;
                numAllies += 5;
                numZombies += 10;
                numBunkers++;
                numBuildings += 2;
                
            }
            timer1.Stop();
            bunkers = new Zombies[numBunkers];
            mortars = new Weapons[numBunkers];
            buildings = new Zombies[numBuildings];
            allies = new Zombies[numAllies];
            Zeds = new Zombies[numZombies];
            
            btnStart.Visible = true;
            //MessageBox.Show("Level: "+level.ToString());
            
            numTargets = numZombies;
            numFriendlies = numAllies;
            numBunks = numBunkers;
            survivors = 0;
            createBuildings();
            createZombies(numZombies);
            createBunkers();
            createMortars();
            bomb.fired = false;
            bomb.timeFired = 0;
            cannons.fired = false;
            cannons.timeFired = 0;
            chX = pictureBox1.Width / 2;
            chY = pictureBox1.Height / 2;
            zoom = 1.0;
            g.ResetTransform();
            g.TranslateTransform((float)-0.3 * borderX, (float)-0.3 * borderY);
            angle = 0;
            for (int i = 0; i < guns.Length; i++)
            {
                guns[i].fired = false;
            }
            /*g.Clear(Color.Black);
            drawBorder();
            drawBuildings();
            drawZombies();

            drawCrossHairs(chX, chY);
            drawStrings();*/
            timeCount = 0;

            zoom *= 0.7;
            g.TranslateTransform(400, 200);
            g.ScaleTransform((float)0.7, (float)0.7);
        }
        
        private void drawStrings()
        {
            text.DrawString("Targets: "+numTargets.ToString(), TNR, green, 100, 10);
            text.DrawString("Bunkers: " + numBunks.ToString(), TNR, green, 300, 10);
            text.DrawString("Allies: "+numFriendlies.ToString(), TNR, green, 200, 10);
            //text.DrawString("Angle: " + angle.ToString(), TNR, red, pictureBox1.Width - 100, 20);
        }
        private bool checkBorder(Zombies sh)
        {
            return (sh.x + sh.width > borderX || sh.y + sh.height > borderY || sh.x < 5 || sh.y < 5);
        }
        private void drawBorder()
        {
            //g.DrawRectangle(blPen, 0, 0, borderX, borderY);
            //g.FillEllipse(green, 0, 0, 50, 50);
            g.DrawEllipse(grPen, 0, 0, 100, 100);
            g.DrawEllipse(grPen, 20, 20, 60, 60);
            g.DrawEllipse(grPen, 40, 40, 20, 20);
        }
        /*private void drawTanks()
        {
            for (int i = 0; i < numTanks; i++)
            {
                if (tanks[i].alive)
                {
                    g.FillRectangle(red, tanks[i].x, tanks[i].y, tanks[i].width, tanks[i].height);
                }
            }
        }*/
        private bool getVisual(Zombies sh, Zombies []tgt)
        {
            for (int i = 0; i < tgt.Length; i++)
            {
                if (tgt[i].alive && !tgt[i].visible)
                {
                    if (getDistance(sh, tgt[i]) < 300)
                    {
                        tgt[i].visible = true;
                        //return true;
                    }
                }
            }
            return false;
        }
        private int getDistance(Zombies sh, Zombies tgt)
        {
            float diffX = sh.x - tgt.x;
            float diffY = sh.y - tgt.y;
            return (int)Math.Sqrt((diffX * diffX)+(diffY * diffY));
        }
        private void drawZombies()
        {
            numTargets = 0;
            for (int i = 0; i < numZombies; i++)
            {
                if (Zeds[i].alive)
                {
                    numTargets++;
                    if (Zeds[i].visible)
                    {
                       // g.FillRectangle(red, (float)((Zeds[i].x)), (float)((Zeds[i].y)), (float)(Zeds[i].width), (float)(Zeds[i].height));

                        if (pics.eX > 0)
                        {
                            pics.eX = 0;
                        }
                        else
                        {
                            pics.eX = 110;
                        }

                        Image m = pics.enemy;

                        Rectangle dest = new Rectangle((int)Zeds[i].x, (int)Zeds[i].y, (int)Zeds[i].width, (int)Zeds[i].height * 2);
                        Rectangle size = new Rectangle(pics.eX, 0, 110, pics.ally.Height);
                        g.DrawImage(m, dest, size, GraphicsUnit.Pixel);
                        //g.DrawEllipse(bluePen, allies[i].x-5, allies[i].y-5, allies[i].width*2, allies[i].height*2);
                        g.FillRectangle(red, Zeds[i].x - 1, Zeds[i].y - 5, Zeds[i].width+2, 3);
                    }
                }
            }
            numBunks = 0;
            for (int i = 0; i < numBunkers; i++)
            {
                if (bunkers[i].alive)
                {
                    numBunks++;
                    if (bunkers[i].visible)
                    {
                        //g.DrawEllipse(redPen, bunkers[i].x, bunkers[i].y, bunkers[i].width, bunkers[i].height);
                        //g.FillEllipse(red, bunkers[i].x, bunkers[i].y, bunkers[i].width, bunkers[i].height);

                        Image m = pics.MG;

                        Rectangle dest = new Rectangle((int)bunkers[i].x, (int)bunkers[i].y, (int)bunkers[i].width, (int)bunkers[i].height);
                        Rectangle size = new Rectangle(0, 0, pics.MG.Width, pics.MG.Height);
                        g.DrawImage(m, dest, size, GraphicsUnit.Pixel);
                        g.DrawEllipse(redPen, bunkers[i].x, bunkers[i].y, bunkers[i].width, bunkers[i].height);
                        g.DrawEllipse(redPen, bunkers[i].x-10, bunkers[i].y-10, bunkers[i].width+20, bunkers[i].height+20);
                        //g.FillRectangle(red, bunkers[i].x - 1, [i].y - 5, allies[i].width + 2, 3);
                    }
                }
            }
            numFriendlies = 0;
            for (int i = 0; i < numAllies; i++)
            {
                if (allies[i].alive)
                {

                    numFriendlies++;
                    //g.FillRectangle(blue, (float)((allies[i].x)), (float)((allies[i].y)), (float)(allies[i].width), (float)(allies[i].height));

                    if (pics.sX > 0)
                    {
                        pics.sX = 0;
                    }
                    else
                    {
                        pics.sX = 100;
                    }

                    Image m = pics.ally;
                    
                    Rectangle dest = new Rectangle((int)allies[i].x, (int)allies[i].y, (int)allies[i].width, (int)allies[i].height * 2);
                    Rectangle size = new Rectangle(pics.sX, 0, 100, pics.ally.Height);
                    g.DrawImage(m, dest, size, GraphicsUnit.Pixel);
                    //g.DrawEllipse(bluePen, allies[i].x-5, allies[i].y-5, allies[i].width*2, allies[i].height*2);
                    g.FillRectangle(blue, allies[i].x-1, allies[i].y - 5, allies[i].width+2, 3);
                }
            }
        }

        private void moveZombies()
        {
                for (int i = 0; i < numZombies; i++)
                {
                    if (Zeds[i].alive)
                    {
                        //if (rand.Next() % 5 == 0)
                        {
                            //if (Zeds[i].x < borderX / 2 - 20 && Zeds[i].y < borderY / 2 - 20)
                            float diffX = Zeds[i].x - Zeds[i].startX;
                            float diffY = Zeds[i].y - Zeds[i].startY;
                            if (Math.Sqrt((diffX * diffX) + (diffY * diffY)) < Math.Sqrt((borderX * borderX) + (borderY * borderY)) / 4)
                            {
                                Zeds[i].vX = rand.Next(1, 2);//(Zeds[i].vX);
                                Zeds[i].vY = rand.Next(1, 2);//(Zeds[i].vY);

                                Zeds[i].x += Zeds[i].vX;
                                if (garrison(i, Zeds[i]))
                                {
                                    Zeds[i].x -= 2 * Zeds[i].vX;
                                }
                                Zeds[i].y += Zeds[i].vY;
                                if (garrison(i, Zeds[i]))
                                {
                                    Zeds[i].y -= 2 * Zeds[i].vY;
                                }
                                stopAtBorder(Zeds[i]);
                            }
                            
                        }
                    }
                }

            if (!rally)
            {
                for (int i = 0; i < numAllies; i++)
                {
                    if (allies[i].alive)
                    {
                        //if (rand.Next() % 10 == 0)
                        {
                            //if (allies[i].x > borderX / 2 + 20 && allies[i].y > borderY / 2 + 20)
                            float diffX = allies[i].startX - allies[i].x;
                            float diffY = allies[i].startY - allies[i].y;
                            if (Math.Sqrt((diffX * diffX) + (diffY * diffY)) < Math.Sqrt((pictureBox1.Width * pictureBox1.Width) + (pictureBox1.Height * pictureBox1.Height)) / 8)
                            {
                                allies[i].vX = rand.Next(-2, -1);//(allies[i].vX);
                                allies[i].vY = rand.Next(-2, -1);//(allies[i].vY);

                                allies[i].x += allies[i].vX;
                                if (garrison(i, allies[i]))
                                {
                                    allies[i].x -= 2 * allies[i].vX;
                                }
                                allies[i].y += allies[i].vY;
                                if (garrison(i, allies[i]))
                                {
                                    allies[i].y -= 2 * allies[i].vY;
                                }
                                stopAtBorder(allies[i]);
                            }
                        }
                        collision(i, allies[i], Zeds, false, true);
                    }
                }
            }
        }

        private void stopAtBorder(Zombies sh)
        {
            if (sh.x < 10)
            {
                sh.x += 5;
            }
            if (sh.y < 10)
            {
                sh.y += 5;
            }
            if (sh.x > borderX - 10)
            {
                sh.x -= 5;
            }
            if (sh.y > borderY - 10)
            {
                sh.y -= 5;
            }
        }
        private void createZombies(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Zeds[i] = new Zombies();
                do{
                do{
                    Zeds[i].x = rand.Next() % borderX / 2 - 100;
                    Zeds[i].y = rand.Next() % borderY / 2 - 100;
                } while (Zeds[i].x <= 0 || Zeds[i].y <= 0);

                Zeds[i].width = 10;
                Zeds[i].height = 10;
                }while(garrison(i, Zeds[i]));
                Zeds[i].alive = true;
                Zeds[i].startX = Zeds[i].x;
                Zeds[i].startY = Zeds[i].y;
                Zeds[i].visible = false;
            }

            for (int i = 0; i < numAllies; i++)
            {
                allies[i] = new Zombies();
                do{
                do
                {
                    allies[i].x = rand.Next(borderX/2+100, borderX-50);// % 400 + borderX - 250;
                    allies[i].y = rand.Next(borderY/2+100, borderY-50);// % 400 + borderY - 250;
                } while (allies[i].x > borderX || allies[i].y > borderY);

                allies[i].width = 10;
                allies[i].height = 10;
                }while(garrison(i, allies[i]));
                allies[i].alive = true;
                allies[i].startX = allies[i].x;
                allies[i].startY = allies[i].y;
                allies[i].visible = true;
            }

        }

        private void smallArms(Zombies[] sh, Zombies[] tgt, int ran, int numAlive)
        {
            for (int i = 0; i < sh.Length; i++)
            {
                if (sh[i].alive && !sh[i].gun.fired && sh[i].visible)
                {
                    if (rand.Next() % ran == 0)
                    {
                        int j = -1;
                        int c = 0;
                        //if (numAlive > 0)
                        {
                            do
                            {
                                c++;
                                j++; //= rand.Next() % tgt.Length;
                                if (c > 1000)
                                {
                                    MessageBox.Show(numAlive.ToString());
                                }
                            } while (!tgt[j].alive && j < tgt.Length-1);
                        }
                        if(getDistance(sh[i], tgt[j]) < 250)
                        {
                            tgt[j].visible = true;
                        }
                        if (tgt[j].visible && tgt[j].alive)
                        {
                            sh[i].gun.x = (int)sh[i].x;
                            sh[i].gun.y = (int)sh[i].y;
                            sh[i].gun.fired = true;


                            int d = getDistance(sh[i], tgt[j]);
                            int diffX = (int)(tgt[j].x - sh[i].x);
                            int diffY = (int)(tgt[j].y - sh[i].y);
                            int comp = 1;
                        
                            sh[i].gun.vX = ((diffX * 25) / d) * comp;
                            sh[i].gun.vY = ((diffY * 25) / d) * comp;
                        }
                    }
                }

            }
        }

        private void drawSmallArms(Zombies[] sh)
        {
            for (int i = 0; i < sh.Length; i++)
            {
                if (sh[i].gun.fired && sh[i].visible)
                {
                    sh[i].gun.x += sh[i].gun.vX;
                    sh[i].gun.y += sh[i].gun.vY;
                    g.FillEllipse(black, sh[i].gun.x, sh[i].gun.y, 3, 3);
                    if(sh[i].gun.x < 0 || sh[i].gun.y < 0 || sh[i].gun.x > borderX || sh[i].gun.y > borderY)
                    {
                        sh[i].gun.fired = false;
                    }
                }
            }
            
        }
        private void checkMortars(Weapons[] sh, Zombies[] tgt, bool enemy)
        {
            for (int i = 0; i < sh.Length; i++)
            {
                Rectangle shooter = new Rectangle(sh[i].x, sh[i].y, 60, 60);
                for (int j = 0; j < tgt.Length; j++)
                {
                    if (tgt[j].alive)
                    {
                        Rectangle target = new Rectangle((int)tgt[j].x, (int)tgt[j].y, (int)tgt[j].width, (int)tgt[j].height);
                        if (shooter.IntersectsWith(target))
                        {
                            //sh[i].gun.fired = false;
                            tgt[j].alive = false;
                        }
                    }
                }
            }

        }
        private void checkSmallArms(Zombies []sh, Zombies []tgt, bool enemy, int offSet)
        {
            for (int i = 0; i < sh.Length; i++)
            {
                Rectangle shooter = new Rectangle(sh[i].gun.x, sh[i].gun.y, 5, 5);
                for (int j = 0; j < tgt.Length; j++)
                {
                    if (tgt[j].alive)
                    {
                        Rectangle target = new Rectangle((int)tgt[j].x, (int)tgt[j].y, (int)tgt[j].width, (int)tgt[j].height);
                        if (shooter.IntersectsWith(target) && rand.Next(0, offSet) == 0)
                        {
                            sh[i].gun.fired = false;
                            tgt[j].alive = false;
                        }
                    }
                }
                strayRounds(sh[i].gun);
            }

        }
        private void fireGuns()
        {
            for (int i = 0; i < guns.Length; i++)
            {
                if (!guns[i].fired)
                {
                    guns[i].timeFired = timeCount;
                    
                    
                    transformToPlane(guns[i], 0, 0);
                    guns[i].x += rand.Next(-15, 25);
                    guns[i].y += rand.Next(-15, 25);
                    guns[i].fired = true;
                    checkShot(i, Zeds);
                    checkShot(i, bunkers);
                    break;
                }
            }
        }
        private void checkShot(int j, Zombies[] tgt)
        {                        
            Rectangle shooter = new Rectangle((int)(guns[j].x), (int)(guns[j].y), 3, 3);
            for (int i = 0; i < tgt.Length; i++)
            {
                if (tgt[i].alive)
                {
                    Rectangle target = new Rectangle((int)(tgt[i].x), (int)(tgt[i].y), (int)(tgt[i].width), (int)(tgt[i].height));
                    if(shooter.IntersectsWith(target))
                    {
                        tgt[i].alive = false;
                        //numTargets--;
                    }
                }
            }
        }
        private void checkCannon(Zombies []sh)
        {
            if (cannons.fired)
            {
                Point[] p = new Point[1];
                Rectangle shooter = new Rectangle((int)(cannons.x), (int)(cannons.y), (int)(100), (int)(100));
                for (int i = 0; i < sh.Length; i++)
                {
                    if (sh[i].alive)
                    {
                        Rectangle target = new Rectangle((int)(sh[i].x), (int)(sh[i].y), (int)(sh[i].width), (int)(sh[i].height));
                        if (target.IntersectsWith(shooter))
                        {
                            sh[i].alive = false;
                           // numTargets--;
                        }
                    }
                }
            }
        }
        private void checkBomb(Zombies[] sh)
        {
            Point[] p = new Point[1];
            Rectangle shooter = new Rectangle((int)(bomb.x), (int)(bomb.y), (int)(200), (int)(200));
            if (bomb.fired)
            {
                for (int i = 0; i < sh.Length; i++)
                {
                    if (sh[i].alive)
                    {
                        Rectangle target = new Rectangle((int)sh[i].x, (int)sh[i].y, (int)sh[i].width, (int)sh[i].height);
                        if (target.IntersectsWith(shooter))
                        {
                            sh[i].alive = false;
                            if (sh.Length == numBuildings)
                            {
                                if (sh[i].barracks && i < sh.Length-1)
                                {
                                    sh[i + 1].barracks = true;
                                    barracks++;
                                }
                            }
                            if (sh[i].garrison)
                            {
                                if (sh[i].bunkerNum < bunkers.Length)
                                {
                                    bunkers[sh[i].bunkerNum].alive = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            btnStart.Focus();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                machineGuns = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                machineGuns = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                MessageBox.Show("Level "+level.ToString());
            }
            btnStart.Visible = false;
            timer1.Start();
        }

        private void translate(int x, int y)
        {
            System.Drawing.Drawing2D.Matrix transform = g.Transform;
            
            //g.ResetTransform();

            g.TranslateTransform((float)(pictureBox1.Width / 2), (float)(pictureBox1.Height / 2));
            g.RotateTransform(-angle);
            g.TranslateTransform((float)(-pictureBox1.Width / 2), (float)(-pictureBox1.Height / 2));


            g.TranslateTransform(x, y);

            g.TranslateTransform((float)(pictureBox1.Width / 2), (float)(pictureBox1.Height / 2));
            g.RotateTransform(angle);
            g.TranslateTransform((float)(-pictureBox1.Width / 2), (float)(-pictureBox1.Height / 2));

            //g.ScaleTransform((float)(zoom), (float)(zoom));
            
        }
        private void transformToPlane(Weapons W, int offSetX, int offSetY)
        {
            Point[] p = new Point[1];

            int n = (int)Math.Floor(angle/4) + 1;
            p[0] = new Point((int)((pictureBox1.Width / 2) - (offSetX)), (int)((pictureBox1.Height / 2) - (offSetY)));
           

            g.TransformPoints(System.Drawing.Drawing2D.CoordinateSpace.World, System.Drawing.Drawing2D.CoordinateSpace.Device, p);

            W.x = p[0].X;
            W.y = p[0].Y;
        }
        private void btnStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            int inc = (int)(50);
            if (e.KeyChar == 'f')
            {
                translate(-inc, 0);
            }
            if (e.KeyChar == 's')
            {
                translate(inc, 0);
            }
            if (e.KeyChar == 'd')
            {
                translate(0, -inc);
            }
            if (e.KeyChar == 'e')
            {
                translate(0, inc);
            }
            if (e.KeyChar == 'c' && !cannons.fired)
            {
                cannons.angle = angle;
                cannons.timeFired = timeCount;
                //cannons.fired = true;
               
                transformToPlane(cannons, 1, 1);
               // drawWeapons();
                //checkCannon();
            }
            if (e.KeyChar == 'i')
            {
                zoom *= 1.1;
                
                float diffX = (float)(pictureBox1.Width * 1.1) - (float)(pictureBox1.Width);
                float diffY = (float)(pictureBox1.Height * 1.1) - (float)(pictureBox1.Height);
                translate((int)-diffX, (int)-diffY);
                g.ScaleTransform((float)(1.1), (float)(1.1));
                //g.TranslateTransform((diffX), diffY);
            }
            if (e.KeyChar == 'o')
            {
                zoom *= 0.9;
                
                float diffX = (float)(pictureBox1.Width) - (float)(pictureBox1.Width * 0.9);
                float diffY = (float)(pictureBox1.Height) - (float)(pictureBox1.Height * 0.9);
                translate((int)diffX, (int)diffY);
                g.ScaleTransform((float)0.9, (float)0.9);
                //g.TranslateTransform(-diffX, -diffY);
            }
            if (e.KeyChar == 'b' && !bomb.fired)
            {
                if (bomb.timeFired < timeCount)
                {
                    bomb.timeFired = timeCount;
                }
               // bomb.fired = true;
                bomb.angle = angle;
                transformToPlane(bomb, 5, 5);
                
                //drawWeapons();
                
            }
            if (e.KeyChar == 'p')
            {
                if (timer1.Enabled)
                {
                    timer1.Stop();
                }
                else
                {
                    timer1.Start();
                }
            }
            if (e.KeyChar == 'm')
            {
                Point[] p = new Point[1];
                p[0] = new Point(pictureBox1.Width/2, pictureBox1.Height/2);
                g.TransformPoints(System.Drawing.Drawing2D.CoordinateSpace.World, System.Drawing.Drawing2D.CoordinateSpace.Page, p);

                rallyPoint.X = p[0].X;
                rallyPoint.Y = p[0].Y;
                rally = true;
                
                stagger = 3;
            }
        }
        private void drawRallyPoint()
        {
            g.DrawEllipse(grPen, rallyPoint.X+5, rallyPoint.Y+5, 10, 10);
            g.DrawEllipse(grPen, rallyPoint.X, rallyPoint.Y, 20, 20);
        }
        
        private void moveToRallyPoint(Zombies[] sh, Point rallyP)
        {
            for (int i = 0; i < sh.Length; i++)
            {
                float diffX = rallyP.X - sh[i].x;
                float diffY = rallyP.Y - sh[i].y;
                double distance = Math.Sqrt((double)((diffX * diffX) + (diffY * diffY)));
                
                if (sh[i].alive)// && !collision(i, (int)distance))
                {
                    
                    //if (distance > 10)
                    {
                        if (diffX > 0)
                        {
                            sh[i].vX = 1;
                        }
                        if (diffX < 0)
                        {
                            sh[i].vX = -1;
                        }
                        if (diffY > 0)// && Math.Abs(diffX) > 1)
                        {
                            sh[i].vY = diffY / diffX;
                        }
                        if (diffY < 0 && diffX < 0)// && Math.Abs(diffX) > 1)
                        {
                            sh[i].vY = -(diffY / diffX);
                        }
                        if (diffY < 0 && diffX > 0)// && Math.Abs(diffX) > 1)
                        {
                            sh[i].vY = diffY / diffX;
                        }
                        if (diffX == 0 && diffY > 0)
                        {
                            sh[i].vY = 1;
                        }
                        if (diffX == 0 && diffY < 0)
                        {
                            sh[i].vY = -1;
                        }
                        if (Math.Abs(diffX) < 50)
                        {
                            sh[i].vX = 0;
                            if (diffY > 10)
                            {
                                sh[i].vY = 1;
                            }
                            if (diffY < -10)
                            {
                                sh[i].vY = -1;
                            }
                            
                        }
                        if (Math.Abs(diffY) < 50)
                        {
                            sh[i].vY = 0;
                            if (diffX > 10)
                            {
                                sh[i].vX = 1;
                            }
                            if (diffX < -10)
                            {
                                sh[i].vX = -1;
                            }

                        }

                        sh[i].vX = sh[i].vX * 5;
                        sh[i].vY = sh[i].vY * 5;

                        if (Math.Abs(sh[i].vX) < 8)
                        {
                            sh[i].x += sh[i].vX;
                        }
                        if (collision(i, sh[i], sh, false, false) || garrison(i, sh[i]))
                        {
                            if (Math.Abs(sh[i].vX) < 8)
                            {
                                sh[i].x -= (float)1.3 * sh[i].vX;
                            }
                        }
                        if (Math.Abs(sh[i].vY) < 8)
                        {
                            sh[i].y += sh[i].vY;
                        }

                        if (collision(i, sh[i], sh, false, false) || garrison(i, sh[i]))
                        {
                            if (Math.Abs(sh[i].vY) < 8)
                            {
                                sh[i].y -= (float)1.3 * sh[i].vY;
                            }
                        }
                        getVisual(sh[i], Zeds);
                        getVisual(sh[i], bunkers);
                        collision(i, sh[i], Zeds, false, false);
                        if (sh[i].x < visual.X && sh[i].y < visual.Y)
                        {
                            visual.X = (int)sh[i].x;
                            visual.Y = (int)sh[i].y;
                        }
                        
                        
                    }
                }
                if(sh.Length == numAllies)
                {
                    collision(i, sh[i], Zeds, false, true);
                    if (sh[i].alive && sh[i].x < 100 && sh[i].y < 100)
                    {
                        nextLevel(false);
                    }
                }
                stopAtBorder(sh[i]);
            }
        }
        private bool collision(int i, Zombies sh, Zombies[] tgt, bool friendly, bool kill)
        {
            Rectangle shooter = new Rectangle((int)sh.x - stagger, (int)sh.y - stagger, (int)sh.width + stagger, (int)sh.width + stagger);
                for (int j = 0; j < tgt.Length; j++)
                {
                    if (i != j || friendly)
                    {
                        if (sh.alive && tgt[j].alive)
                        {
                            Rectangle target = new Rectangle((int)tgt[j].x-stagger, (int)tgt[j].y-stagger, (int)tgt[j].width+stagger, (int)tgt[j].width+stagger);
                            if (shooter.IntersectsWith(target))
                            {
                                if (kill)
                                {
                                    tgt[j].alive = false;
                                }
                                return true;
                            }
                        }
                    }
                }
            return false;
        }
        private void btnStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                machineGuns = true;
            }
        }

        private void btnStart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                machineGuns = false;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
           
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            //pictureBox1.Width = this.Width - 5;
            //pictureBox1.Height = this.Height - 5;
            //MessageBox.Show(Width.ToString());
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //pictureBox1.Width = this.Width - 5;
            //pictureBox1.Height = this.Height - 5;
            //MessageBox.Show(this.Width.ToString());
        }

        private void Form1_MaximumSizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Width.ToString());
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Width = this.Width - 5;
            pictureBox1.Height = this.Height - 5;
            //chX = pictureBox1.Width;
            //chY = pictureBox1.Height;
            System.Drawing.Drawing2D.Matrix tran = g.Transform;
            g = pictureBox1.CreateGraphics();
            g.Transform = tran;
        }
    }
}
