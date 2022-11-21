using lab5._1.Objects;
using Microsoft.VisualBasic;
using System;

namespace lab5._1
{
    public partial class Form1 : Form
    {
        
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        GreenCircle green1;
        GreenCircle green2;
        int counter = 0;
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            var rnd = new Random();

            green1 = new GreenCircle(rnd.Next(15, 430), rnd.Next(15, 240), 0);
            green2 = new GreenCircle(rnd.Next(15, 430), rnd.Next(15, 240), 0);
           
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.OnCircleOverlap += (m) =>
            {
                var rnd = new Random();
                objects.Remove(m);

                if (m == green1)
                {
                    green1 = new GreenCircle(rnd.Next(15, 430), rnd.Next(15, 240), 0);
                    objects.Add(green1);
                }
                else
                {
                    green2 = new GreenCircle(rnd.Next(15, 430), rnd.Next(15, 240), 0);
                    objects.Add(green2);
                }
                counter++;
                txtPoints.Text = $"Очки: {counter}";
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
          
            objects.Add(green1);
            objects.Add(green2);
            

            objects.Add(marker);
            objects.Add(player);
            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 45));
        }
        
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            updatePlayer();

           
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overplaps(obj, g))
                {
                    player.Overlap(obj);
                    
                }
            }

            
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
         

            pbMain.Invalidate();
            
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;

           
        }

        private void circleTimer_Tick(object sender, EventArgs e)
        {
            green1.R--;
            green2.R--;

            var rnd = new Random();
            
            if (green1.R <= 0)
            {
                objects.Remove(green1);
                green1 = new GreenCircle(rnd.Next(15, 430), rnd.Next(15, 240), 0);
                objects.Add(green1);
            }
            
            if (green2.R <= 0)
            {
                objects.Remove(green2);
                green2 = new GreenCircle(rnd.Next(15, 430), rnd.Next(15, 240), 0);
                objects.Add(green2);
            }
        }
    }
}