using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GoodDrawer.GoodShapes
{
    class GoodShapesCollection:GoodShape
    {
        List<GoodShape> shapes = new List<GoodShape>();

        public GoodShapesCollection(Bound b, GoodShape[] shapes ):base(b)
        {
            this.shapes.AddRange(shapes);
            geometry = new GeometryGroup();
            foreach (GoodShape s in shapes)
                ((GeometryGroup)geometry).Children.Add(s.geometry);
            
        }
        public override void calculateValues()
        {
            foreach (GoodShape s in shapes)
                s.calculateValues();
            
        }
        public override void confirmGeometry()
        {
            foreach (GoodShape s in shapes)
                s.confirmGeometry();
        }
    }
}
