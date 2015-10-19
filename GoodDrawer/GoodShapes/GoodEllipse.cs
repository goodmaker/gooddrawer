using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GoodDrawer
{
    public class GoodEllipse:GoodShape
    {
        public GoodEllipse(Bound b):base(b)
        {
            Name = "Ellipse";
        }



        public override void calculateValues()
        {
        }
        static GoodEllipse()
        {
          
        }
        public override void confirmGeometry()
        {

            geometry = new EllipseGeometry(Bound.Center, Bound.Rect.Width / 2, Bound.Rect.Height/2, Bound.Transform);
           
        }
    }
}
