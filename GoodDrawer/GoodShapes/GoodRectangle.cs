using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GoodDrawer
{
    public class GoodRectangle:GoodShape
    {
        public double xRadius
        { get; set; }
     public double yRadius
        { get; set; }
        public GoodRectangle(Bound b, double xRad, double yRad) : base(b) 
        {
            Name = "Rectangle";
            xRadius = xRad;
            yRadius = yRad;
            tops = new System.Windows.Point[4];
        }

        public override void calculateValues()
        {
            tops[0] = Bound.topLeft.Center;
            tops[1] = Bound.topRight.Center;
            tops[2] = Bound.bottomLeft.Center;
            tops[3] = Bound.bottomRight.Center;
        }

        public override void confirmGeometry()
        {
            Rect r = new Rect(tops[0].X, tops[0].Y, Bound.Rect.Width,Bound.Rect.Height);
            geometry = new RectangleGeometry(r,xRadius,yRadius,Bound.Transform);
           
        }
        public override string Information()
        {
            StringBuilder sb = new StringBuilder(base.Information());
            sb.Append(String.Format("Xr:{0}; Yr:{1}]", xRadius,yRadius));
            return sb.ToString();
        }
        static GoodRectangle()
        {
            
        }
    }
}
