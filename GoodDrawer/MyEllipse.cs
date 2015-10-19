using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
namespace GoodDrawer
{
    class MyEllipse:Shape
    {
        EllipseGeometry ellipse = new EllipseGeometry();
        Rect r = new Rect();

        public MyEllipse()
        {

        }
        protected override System.Windows.Media.Geometry DefiningGeometry
        {
            get { throw new NotImplementedException(); }
        }
    }

    class CopyOfMyEllipse : Shape
    {
        EllipseGeometry ellipse = new EllipseGeometry();
        Rect r = new Rect();

        public CopyOfMyEllipse()
        {

        }
        protected override System.Windows.Media.Geometry DefiningGeometry
        {
            get { throw new NotImplementedException(); }
        }
    }
}
