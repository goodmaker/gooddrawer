using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GoodDrawer
{
    public class GoodPolygon:GoodShape
    {
        private int topsCount;
        public int TopsCount
        {
            get { return topsCount; }
            set
            {
                if (value > 0)
                {
                    topsCount = value;
                    tops = new Point[topsCount];
                }
                else throw new ArgumentOutOfRangeException();
            }
        }
        public GoodPolygon(int topsCount, Bound b):base(b)
        {
            Name = "Polygon";
            TopsCount = topsCount;
            tops = new Point[TopsCount];
        }
        public override void calculateValues()
        {
            double radius = Math.Min(Bound.Rect.Width, Bound.Rect.Height) / 2;
            double angle = 2 * Math.PI / TopsCount;
            Point center = Bound.Center;
            for (int i = 0; i < tops.Length; i++)
            {
                tops[i].X = center.X + radius * Math.Sin(i * angle);
                tops[i].Y = center.Y + radius * Math.Cos(i * angle);               
            }
        }

        public override void confirmGeometry()
        {
            PathSegmentCollection psc = new PathSegmentCollection();
            for (int i = 1; i < tops.Length; i++)
            {
                psc.Add(new LineSegment(tops[i], true));
            }
            PathFigure pf = new PathFigure(tops[0], psc, true);
            PathFigureCollection pfc = new PathFigureCollection { pf };
            geometry = new PathGeometry(pfc, FillRule.EvenOdd, Bound.Transform);
        }
        public override string Information()
        {
            StringBuilder sb = new StringBuilder(base.Information());
            sb.Append(String.Format("N:{0}]", TopsCount));
            return sb.ToString();
        }
        static GoodPolygon()
        {
            
        }
    }
}
