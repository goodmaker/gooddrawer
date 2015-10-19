using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GoodDrawer
{
    [Serializable]
    public abstract class GoodShape:DrawingVisual,INotifyPropertyChanged, ISerializable
    {
        #region fields
        Bound bound;
        Pen borderPen;
        protected Point[] tops;
        public string Name { get; protected set; }
        public Bound Bound
         {
             get { return bound; }
         }
        public Pen BorderPen
        { get { return borderPen; } set { borderPen = value; } }
        public Brush Color
        { get; set; }
        public Geometry geometry { get; protected set;}
        #endregion

        public virtual void Draw()
        {
            calculateValues();
            confirmGeometry();
            using (DrawingContext dc = RenderOpen())
            {
                dc.DrawGeometry(Color, BorderPen, geometry);
            }
        }
        public abstract void calculateValues();
        public abstract void confirmGeometry();
        public GoodShape(Bound b)
        {    
            bound = b;
            if (b != null)
            {
                bound.ChildShape = this;
                //  VisualTransform = Bounds.Transform;
                b.BoundChanged += b_BoundChanged;
            }

            BorderPen = new Pen(Brushes.Black, 1);
        }
        public void removeBound()
        {
            bound = null;
        }
        void b_BoundChanged(object sender, EventArgs e)
        {   
            Draw();                       
        }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) //сериализация
        {
            info.AddValue("Bound", Bound);
            info.AddValue("BorderPen", BorderPen);
            info.AddValue("Color", Color);           
        }
        public GoodShape(SerializationInfo info, StreamingContext context)
        {
            bound = (Bound)info.GetValue("Bound", typeof(Bound));
            if (bound != null)
            {
                bound.ChildShape = this;
                //  VisualTransform = Bounds.Transform;
                bound.BoundChanged += b_BoundChanged;
            }
            BorderPen = (Pen)info.GetValue("BorderPen", typeof(Pen));
            Color = (Brush)info.GetValue("Color", typeof(Brush));
        }
        public  virtual string Information()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + Name + ':');
            sb.Append(String.Format("SColor:{0};BColor:{1};BThick:{2};"
                , (Color as SolidColorBrush).Color.ToString(), (BorderPen.Brush as SolidColorBrush).Color.ToString(),
                BorderPen.Thickness));
            sb.Append(bound.getData());
            return sb.ToString();
        }
        public static GoodShape GetShape(string  metadata, Canvas parent)
        {
            string[] firstData = metadata.Split(new char[]{'[','(',']',')'}, StringSplitOptions.RemoveEmptyEntries);
            if (firstData.Length != 3)
                throw new ArgumentException();
            Bound b = new Bound('('+firstData[1]+')', parent);
            string[] data =(firstData[0]+firstData[2]).Split(new char[] { ';', ':', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            GoodShape gs = null;
            
            if(data[0] ==  "Star")
            {
                gs = new GoodStar(Convert.ToInt32(data[8]), Convert.ToDouble(data[10]), b);
               
               // ColorConverter cc = new ColorConverter();

               
            }
            else if(data[0] == "Polygon")
            {
                gs = new GoodPolygon(Convert.ToInt32(data[8]), b);
            }
            else if (data[0] == "Rectangle")
            {
                gs = new GoodRectangle(b, Convert.ToDouble(data[8]), Convert.ToDouble(data[10]));
            }
            else if( data[0] == "Ellipse")
            {
                gs = new GoodEllipse(b);
            }
            if(gs != null)
            {
                gs.Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data[2]));
                gs.BorderPen.Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data[4]));
                gs.BorderPen.Thickness = Convert.ToDouble(data[6]);
            }
            return gs;
        }
    }
}
