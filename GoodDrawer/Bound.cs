using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.Serialization;


namespace GoodDrawer
{
    [Serializable]
    public class Bound : Shape, ISerializable
    {
       // Rect r = new Rect(50, 50, 200, 100);
        //public static readonly DependencyProperty RectangleProperty =
        //  DependencyProperty.Register("Rect", typeof(Rect), typeof(Bound),
        //  new FrameworkPropertyMetadata(new Rect(), FrameworkPropertyMetadataOptions.AffectsRender));
     
        public Rect Rect
        {
            get { return rg.Rect; }
            set 
            { 
                rg.Rect = value; 
              
            }
        }
        RectangleGeometry rg;
        Line temp;

        Point startActionPoint;
        private Point AnglePointPlace
        {
            get { return new Point(Rect.X + Rect.Width / 2, Rect.Y - 10); }
        }
        public Point RotatePoint
        {
            get;
            set;
        }
        public Point Center
        {
            get { return new Point(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2); }
        }
        private Point correctMousePosition(Point a)
        {
            return new Point();
        }
        #region control lines
        Line[] lines = new Line[4];
        public Line topLine
        {
            get {return lines[0];}
            protected set {lines[0] = value;}
        }
        public Line rightLine
        {
             get {return lines[1];}
            protected set {lines[1] = value;}
        }
        public Line bottomLine
        {
            get {return lines[2];}
            protected set {lines[2] = value;}
        }
        public Line leftLine
        {
            get {return lines[3];}
            protected set {lines[3] = value;}
        }
        
        void initLines()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new Line();
                lines[i].StrokeThickness = 4;                
                lines[i].Stroke = Brushes.Transparent;
                lines[i].RenderTransform = transform;
            }
            lines[0].X1 = Rect.X;
            lines[0].X2 = Rect.X + Rect.Width;
            lines[0].Y1 = lines[0].Y2 = Rect.Y;

            lines[1].X2 = lines[1].X1 = Rect.X + Rect.Width;
            lines[1].Y1 = Rect.Y;
            lines[1].Y2 = Rect.Y + Rect.Height;

            lines[2].X1 = Rect.X;
            lines[2].X2 = Rect.X + Rect.Width;
            lines[2].Y1 = lines[2].Y2 = Rect.Y + Rect.Height;

            lines[3].X2 = lines[3].X1 = Rect.X;
            lines[3].Y1 = Rect.Y;
            lines[3].Y2 = Rect.Y + Rect.Height;
        }
        void refreshLines()
        {
            lines[0].X1 = Rect.X;
            lines[0].X2 = Rect.X + Rect.Width;
            lines[0].Y1 = lines[0].Y2 = Rect.Y;

            lines[1].X2 = lines[1].X1 = Rect.X + Rect.Width;
            lines[1].Y1 = Rect.Y;
            lines[1].Y2 = Rect.Y + Rect.Height;

            lines[2].X1 = Rect.X;
            lines[2].X2 = Rect.X + Rect.Width;
            lines[2].Y1 = lines[2].Y2 = Rect.Y + Rect.Height;

            lines[3].X2 = lines[3].X1 = Rect.X;
            lines[3].Y1 = Rect.Y;
            lines[3].Y2 = Rect.Y + Rect.Height;
        }
        #endregion
        #region controlDots
        ControlCircle[] controlDots = new ControlCircle[6];
        public ControlCircle AngleDot
        {
            get { return controlDots[4]; }
            protected set { controlDots[4] = value; }
        }
        public ControlCircle RotateDot
        {
            get { return controlDots[5]; }
            protected set { controlDots[5] = value; }
        }
        public ControlCircle topLeft
        {
            get { return controlDots[0]; }
            protected set { controlDots[0] = value; }
        }
        public ControlCircle topRight
        {
            get { return controlDots[1]; }
            protected set { controlDots[1] = value; }
        }
        public ControlCircle bottomLeft
        {
            get { return controlDots[2]; }
            protected set { controlDots[2] = value; }
        }
        public ControlCircle bottomRight
        {
            get { return controlDots[3]; }
            protected set { controlDots[3] = value; }
        }

        void initControlDots()
        {
            topLeft = new ControlCircle(5, Rect.TopLeft);
            topRight = new ControlCircle(5, Rect.TopRight);
            bottomLeft = new ControlCircle(5, Rect.BottomLeft);
            bottomRight = new ControlCircle(5, Rect.BottomRight);
            AngleDot = new ControlCircle(3,AnglePointPlace); //10?
            RotateDot = new ControlCircle(3, RotatePoint);

            foreach (ControlCircle c in controlDots)
            {
                c.Fill = Brushes.White;
                c.StrokeThickness = 1;
                c.Stroke = Brushes.Black;
                if(c != RotateDot)
                 c.RenderTransform = transform;
            }
            AngleDot.Fill = Brushes.ForestGreen;
            RotateDot.Fill = Brushes.Blue;
            refreshCircles();
        }
        void refreshCircles()
        {
            topLeft.Center = Rect.TopLeft;
            topRight.Center = Rect.TopRight;
            bottomLeft.Center = Rect.BottomLeft;
            bottomRight.Center = Rect.BottomRight;
            AngleDot.Center = AnglePointPlace;
            RotateDot.Center = RotatePoint;
        }

        #endregion //не факт, что понадобятся

        TransformGroup transform = new TransformGroup();
        RotateTransform rt = new RotateTransform();
        protected override Geometry DefiningGeometry
        {
            get { return rg; }
        }
        public void hide()
        {
            Visibility = System.Windows.Visibility.Hidden;
            IsEnabled = false;
            foreach(ControlCircle c in controlDots)
            {
                c.Visibility = System.Windows.Visibility.Hidden;
                c.IsEnabled = false;
            }
            foreach(Line l in lines )
            {
                l.Visibility = System.Windows.Visibility.Hidden;
                l.IsEnabled = false;
            }
        }
        public void show()
        {
            Visibility = System.Windows.Visibility.Visible;
            IsEnabled = true;
            foreach (ControlCircle c in controlDots)
            {
                c.Visibility = System.Windows.Visibility.Visible;
                c.IsEnabled = true;
            }
            foreach (Line l in lines)
            {
                l.Visibility = System.Windows.Visibility.Visible;
                l.IsEnabled = true;
            }
        }
        void connectLines()
        {
            foreach (Line l in lines)
            {
                l.MouseLeftButtonDown += l_MouseLeftButtonDown;
                l.MouseEnter += l_MouseEnter;
                l.MouseLeave += l_MouseLeave;
            }
        }
        void connectDots()
        {
            foreach(ControlCircle c in controlDots)
            {
                c.MouseLeftButtonDown += controlDot_MouseLeftButtonDown;
            }
        }
        //обеспечивает расширение/сужения прямоугольника
        #region rectIncreases 
        void increaseRectDown(double d)
        {
            bottomLine.Y1 += d;
            bottomLine.Y2 += d;

            leftLine.Y2 = bottomLine.Y2;
            rightLine.Y2 = bottomLine.Y2;

            leftLine.Y1 = topLine.Y2;
            rightLine.Y1 = topLine.Y2;

            Rect r = Rect;
            if (r.Height + d > 0)
                r.Height += d;
            else
            {
                
                Line temp = topLine;
                topLine = bottomLine;
                bottomLine = temp;


                //меняем местами точки
                ControlCircle c1 = topRight;
                topRight = bottomRight;
                bottomRight = c1;

                c1 = topLeft;
                topLeft = bottomLeft;
                bottomLeft = c1;

                r.Height = bottomLine.Y2 - topLine.Y2;
                r.Y = topLine.Y2;
            }
            Rect = r;
            refreshCircles();
        }
        void increaseRectUp(double d)
        {
            topLine.Y1 -= d;
            topLine.Y2 -= d;

            leftLine.Y1 = topLine.Y1;
            rightLine.Y1 = topLine.Y1;
            leftLine.Y2 = bottomLine.Y2;
            rightLine.Y2 = bottomLine.Y2;
            Rect r = Rect;
            r.Y -= d;
            
            if (r.Height + d > 0)
                r.Height += d;
            else
            {
                
                Line temp = topLine;
                topLine = bottomLine;
                bottomLine = temp;

                //меняем местами точки
                ControlCircle c1 = topRight;
                topRight = bottomRight;
                bottomRight = c1;

                c1 = topLeft;
                topLeft = bottomLeft;
                bottomLeft = c1;

                r.Height = Math.Abs(Rect.Height + d);
                r.Y = topLine.Y2;
            }
            Rect = r;
            refreshCircles();          

        }
        void increaseRectLeft(double d)
        {
            leftLine.X1 += d;
            leftLine.X2 += d;

            topLine.X1 = leftLine.X1;
            bottomLine.X1 = leftLine.X2;
            topLine.X2 = rightLine.X1;
            bottomLine.X2 = rightLine.X2;
            Rect r = Rect;
            r.X = leftLine.X2;
            if (r.Width - d < 0)
            {
                Line temp = leftLine;
                leftLine = rightLine;
                rightLine = temp;

                //меняем местами точки
                ControlCircle c1 = topRight;
                topRight = topLeft;
                topLeft = c1;

                c1 = bottomRight;
                bottomRight = bottomLeft;
                bottomLeft = c1;

                r.Width = Math.Abs(Rect.Width - d);
                r.X = leftLine.X2;
            }
            else
            {
                r.Width -= d;
            }
            Rect = r;
            refreshCircles();
           // rg.Rect = r;
            
        }
        void increaseRectRight(double d)
        {
            rightLine.X1 += d;
            rightLine.X2 += d;

            topLine.X1 = leftLine.X1;
            bottomLine.X1 = leftLine.X2;
            topLine.X2 = rightLine.X1;
            bottomLine.X2 = rightLine.X2;
            Rect r = Rect;
            r.X = leftLine.X2;
            if (r.Width + d < 0)
            {
                Line temp = leftLine;
                leftLine = rightLine;
                rightLine = temp;

                //меняем местами точки
                ControlCircle c1 = topRight;
                topRight = topLeft;
                topLeft = c1;

                c1 = bottomRight;
                bottomRight = bottomLeft;
                bottomLeft = c1;

                r.Width = Math.Abs(r.Width + d);
                r.X = leftLine.X2;
            }
            else
            {
                r.Width += d;
            }
            Rect = r;
            refreshCircles();
        }
        #endregion //
        #region lineEvent
        private void l_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            temp.ReleaseMouseCapture();
            temp = null;
            (sender  as FrameworkElement).MouseMove -= l_onDrag;
            (sender as FrameworkElement).
                MouseLeftButtonUp -= l_MouseLeftButtonUp;
        }
        void l_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            temp = sender as Line;
            temp.CaptureMouse();
            temp.MouseMove += l_onDrag;
            (sender as FrameworkElement).
                MouseLeftButtonUp += l_MouseLeftButtonUp;
            startActionPoint = e.GetPosition((IInputElement)(sender as FrameworkElement).Parent);
        }       
        private void l_onDrag(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
            Line l = sender as Line;
            double delta;
            Point p = e.GetPosition(l.Parent as IInputElement);
            p = transform.Inverse.Transform(p);
            //Вектор движения указателя
            Vector movingVector = new Vector(p.X - startActionPoint.X, p.Y - startActionPoint.Y);
            if (l == topLine)
            {
                delta = p.Y - Rect.Y;
                increaseRectUp(-delta);            
            }
            if (l == leftLine)
            {
                delta = Rect.X - p.X;
                increaseRectLeft(-delta) ;
            }
            if (l == bottomLine)
            {
                delta = p.Y - l.Y2;
                increaseRectDown(delta);
            }
            if (l == rightLine)
            {
                delta = p.X - l.X1;
                increaseRectRight(delta);
            }
            if (BoundChanged != null)
                BoundChanged(this, new EventArgs());
        }
        void l_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            e.MouseDevice.OverrideCursor = (System.Windows.Input.Cursors.Arrow);
        }
        void l_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Line line = sender as Line;
            
           if(line == topLine || line == bottomLine)
               e.MouseDevice.OverrideCursor = (System.Windows.Input.Cursors.SizeNS);
           else
               e.MouseDevice.OverrideCursor = (System.Windows.Input.Cursors.SizeWE);
        }
        #endregion
        #region controlDotsEvents
        private void controlDot_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ControlCircle dot = sender as ControlCircle;
            dot.CaptureMouse();
            dot.MouseLeftButtonUp += controlDot_MouseLeftButtonUp;
            dot.MouseMove += controlDot_onDrag;
            if(dot == AngleDot)
            {
                rt = new RotateTransform();
                rt.CenterX = RotatePoint.X;
                rt.CenterY = RotatePoint.Y;
                transform.Children.Add(rt);
            }
        }
        private void controlDot_onDrag(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ControlCircle dot = sender as ControlCircle;
            double delta;
            Point p = e.GetPosition(dot.Parent as IInputElement);
            p = transform.Inverse.Transform(p);
            if(dot == topLeft)
            {
                delta = p.Y - Rect.Y;
                increaseRectUp(-delta);

                delta = Rect.X - p.X;
                increaseRectLeft(-delta);
            }
            else if (dot == topRight)
            {
                delta = p.Y - Rect.Y;
                increaseRectUp(-delta);

                delta = p.X - dot.Center.X;
                increaseRectRight(delta);
            }
            else if (dot == bottomLeft)
            {
                delta = p.Y - dot.Center.Y;
                increaseRectDown(delta);

                delta = Rect.X - p.X;
                increaseRectLeft(-delta);
            }
            else if (dot == bottomRight)
            {
                delta = p.Y - dot.Center.Y;
                increaseRectDown(delta);

                delta = p.X - dot.Center.X;
                increaseRectRight(delta);
            }
            else if(dot == AngleDot)
            {
               //double ang =  Math.Atan(((AnglePointPlace.X - RotatePoint.X)*(p.Y - RotatePoint.Y) 
               //     - (AnglePointPlace.Y - RotatePoint.Y)*(p.X - RotatePoint.X))/
               //     ((AnglePointPlace.X - RotatePoint.X) * (p.X - RotatePoint.X) +
               //     (AnglePointPlace.Y - RotatePoint.Y) * (p.Y - RotatePoint.Y)));
                Vector v1 = new Vector(AnglePointPlace.X - RotateDot.Center.X, AnglePointPlace.Y - RotateDot.Center.Y);
                Vector v2 = new Vector(p.X - RotateDot.Center.X, p.Y - RotateDot.Center.Y);
                double angle = Vector.AngleBetween(v1, v2);
                rt.Angle += angle;
            }
            else if(dot == RotateDot && e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                
                RotateDot.Center = Transform.Transform( p);
                RotatePoint = Transform.Transform(p);
            }
            if(BoundChanged != null)
                BoundChanged(this, new EventArgs());
        }
        private void controlDot_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ControlCircle dot = sender as ControlCircle;
            dot.ReleaseMouseCapture();
             rightLine.MouseMove -= l_onDrag;
             dot.MouseLeftButtonUp -= controlDot_MouseLeftButtonUp;
             dot.MouseMove -= controlDot_onDrag;
        }
        #endregion
        #region BoundEvents
        Point offset = new Point();
        Point RotateOffset = new Point();
        public void bounds_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.MouseDevice.OverrideCursor = (System.Windows.Input.Cursors.Hand);
            Point position = transform.Inverse.Transform(e.GetPosition(sender as IInputElement));
            offset = new Point(-Rect.TopLeft.X +position.X,
                -Rect.TopLeft.Y + position.Y);
            RotateOffset = new Point(position.X- RotatePoint.X, position.Y - RotatePoint.Y);
            MouseMove += bounds_onDrag;
            MouseUp += bounds_MouseLeftButtonUp;
            this.CaptureMouse();
        }
        private void bounds_onDrag(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(this.Parent as IInputElement);
            mousePos = transform.Inverse.Transform(mousePos);
            Rect r = Rect;
            r.X = mousePos.X - offset.X;
            r.Y = mousePos.Y - offset.Y;  
            Rect = r;
            
            RotatePoint = new Point(mousePos.X - RotateOffset.X, mousePos.Y - RotateOffset.Y);
            
            refreshCircles();
            refreshLines();
            if (BoundChanged != null)
            BoundChanged(this, new EventArgs());
        }
        private void bounds_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseUp -= bounds_MouseLeftButtonUp;
            MouseMove -= bounds_onDrag;
            e.MouseDevice.OverrideCursor = (System.Windows.Input.Cursors.Arrow);
           
            this.ReleaseMouseCapture();
        }
        void Bound_LostFocus(object sender, RoutedEventArgs e)
        {
            //hide();
            
        }
        #endregion

        void connectElements()
        {
            connectDots();
            connectLines();
        }
        void init()
        {
            initLines();
            initControlDots();
            
        }
        public Bound(Canvas parent, Point p)
        {
            rg = new RectangleGeometry(new Rect(p.X, p.Y, 1, 1), 0, 0, transform);
            DoubleCollection d = new DoubleCollection() { 3, 1 };
            this.Stroke = Brushes.Black;
            this.StrokeDashArray = d;
            this.StrokeThickness = 3;
            this.StrokeEndLineCap = PenLineCap.Round;
            this.Fill = Brushes.Transparent;
            init();

            parent.Children.Add(this);
            foreach (Line l in lines)
                parent.Children.Add(l);
            foreach (ControlCircle e in controlDots)
                parent.Children.Add(e);

            RotatePoint = Center;
            connectElements();
            
          
            transform.Children.Add(rt);
            rt.CenterX = Rect.Left + Rect.Width / 2;
            rt.CenterY = Rect.Top + Rect.Height / 2;
            Focusable = true;
            Focus();
            //если начинаем с точки то присоединяемся к перетягиванию сразу
            MouseLeftButtonDown += bounds_MouseLeftButtonDown;
            LostFocus += Bound_LostFocus;
            
            controlDot_MouseLeftButtonDown(bottomLeft, 
                new System.Windows.Input.MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice,0,MouseButton.Left));
        }
        public Bound(Canvas parent, Rect rect)
        {
            rg = new RectangleGeometry(rect, 0, 0, transform);
            DoubleCollection d = new DoubleCollection() { 3, 1 };
            Angle += 45;
            this.Stroke = Brushes.Black;
            this.StrokeDashArray = d;
            this.StrokeThickness = 3;
            this.StrokeEndLineCap = PenLineCap.Round;
            this.Fill = Brushes.Transparent;
            init();

            parent.Children.Add(this);
            foreach (Line l in lines)
                parent.Children.Add(l);
            foreach (ControlCircle e in controlDots)
                parent.Children.Add(e);

            RotatePoint = Center;
            connectElements();

            MouseLeftButtonDown += bounds_MouseLeftButtonDown;
            transform.Children.Add(rt);
            rt.CenterX = Rect.Left + Rect.Width / 2;
            rt.CenterY = Rect.Top + Rect.Height / 2;
         
        }
        public double Angle
        {
            get { return rt.Angle; }
            set { rt.Angle = value; }
        }
        public double BorderThickness
        {
            get { return leftLine.StrokeThickness; }
            set
            {
                if(value > 0)
                foreach (Line l in lines)
                    l.StrokeThickness = value;
            }
        }
        protected Rect IntersectionRect
        {
            get
            {
                return new Rect(Rect.X - BorderThickness, Rect.Y - BorderThickness, Rect.Width
                    + 2 * BorderThickness, Rect.Height + 2 * BorderThickness);
            }
        }
        public bool contains(Point p)
        {
            Point n = Transform.Inverse.Transform(p);
            if (IntersectionRect.Contains(n))
                return true;
            foreach(ControlCircle c in controlDots)
            {
                if (c.Contain(n))
                    return true;
            }
            return false;
            
        }
        public GoodShape ChildShape
        {
            get;
            set;
        }


        public Transform Transform { get { return transform; } }
        public delegate void boundChanged(object sender, EventArgs e);
        public event boundChanged BoundChanged;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", Rect.X);
            info.AddValue("Y", Rect.Y);
            info.AddValue("H", Rect.Height);
            info.AddValue("W", Rect.Width);
            info.AddValue("Transform", Transform);

        }
        public string getData()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(Bound:");
            sb.Append(String.Format("X:{0}!Y:{1}!H:{2}!W:{3}!",Rect.X,Rect.Y, Rect.Height, Rect.Width));

            sb.Append("Transform:" +new MatrixConverter().ConvertToString(Transform.Value));
            sb.Append("!);");
            
            return sb.ToString();
        }
        public Bound(string DataString, Canvas parent)
        {
            //создание прямоугольника и трансформации с метафайла
            if (DataString[0] != '(' && DataString[DataString.Length-1] != ')')
                throw new ArgumentException();
            string[] values = DataString.Split(new char[] { ':', '!' });
            if (values.Length != 12)
                throw new ArgumentException();
            Rect r = new Rect(Convert.ToDouble(values[2]), Convert.ToDouble(values[4])
                , Convert.ToDouble(values[8]), Convert.ToDouble(values[6]));
            MatrixConverter mc = new MatrixConverter();
            Matrix m;
            try
            {
                m = (Matrix)mc.ConvertFromString(values[10]);
            }
            catch(Exception e)
            {
            string[] matrix = values[10].Split(';');
             m = new Matrix(Convert.ToDouble(matrix[0]),Convert.ToDouble(matrix[1]),Convert.ToDouble(matrix[2])
                ,Convert.ToDouble(matrix[3]),Convert.ToDouble(matrix[4]),Convert.ToDouble(matrix[5]));
            }
            transform.Children.Add(new MatrixTransform(m));
            MouseLeftButtonDown += bounds_MouseLeftButtonDown;
            rg = new RectangleGeometry(r, 0, 0, transform);
            DoubleCollection d = new DoubleCollection() { 3, 1 };
            this.Stroke = Brushes.Black;
            this.StrokeDashArray = d;
            this.StrokeThickness = 3;
            this.StrokeEndLineCap = PenLineCap.Round;
            this.Fill = Brushes.Transparent;
            init();

            parent.Children.Add(this);
            foreach (Line l in lines)
                parent.Children.Add(l);
            foreach (ControlCircle e in controlDots)
                parent.Children.Add(e);

            RotatePoint = Center;
            connectElements();


            transform.Children.Add(rt);
            rt.CenterX = Rect.Left + Rect.Width / 2;
            rt.CenterY = Rect.Top + Rect.Height / 2;
            Focusable = true;
            Focus();
            //если начинаем с точки то присоединяемся к перетягиванию сразу
          
            LostFocus += Bound_LostFocus;

       
            
        }
    }
}
