using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5._1.Objects
{
    class GreenCircle : BaseObject
    {
        public float R = 50;
        public Action <GreenCircle> onDeath;
        
        public GreenCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.GreenYellow), -15, -15, R, R);
            this.R -= (float)0.5;
            if (R <= 0)
            {
                R = 50;
                onDeath(this);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, R, R);
            return path;
        }

    }
}
    

