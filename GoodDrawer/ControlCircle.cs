using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GoodDrawer
{
    public class ControlCircle:Shape,INotifyPropertyChanged
    {
        EllipseGeometry geometry;

        public Point Center
        {
            get { return geometry.Center; }
            set
            {
                geometry.Center = value;
                propertyChanged("DefiningGeometry");
            }
        }
        protected override Geometry DefiningGeometry
        {
            get { return geometry; }
        }
        public ControlCircle(double radius, Point center)
        {
            geometry = new EllipseGeometry(center, radius, radius);
        }
        public bool Contain(Point p)
        {
            if (Point.Subtract(p, Center).Length < geometry.RadiusX)
                return true;
            else return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string PropertyName)
        {
            if(PropertyChanged !=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
