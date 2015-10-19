using GoodDrawer.GoodShapes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
namespace GoodDrawer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle bound = new Rectangle();
        DrawingVisual dv = new DrawingVisual();
        ShapeInfoManager sim = new ShapeInfoManager();
        List<GoodShape> gShapes = new List<GoodShape>();
        public MainWindow()
        {
            InitializeComponent();
            sim.setObjects(typeof(GoodStar), ShapeInfo);
        }
        List<UIElement> Controls
        { get { return sim.Elements; } }
        void bo_LostFocus(object sender, RoutedEventArgs e)
        {
           // bo = null;
        }
        Bound bo;
        static GoodShape currentShape;
        public static GoodShape CurrentShape
        {
            get { return currentShape; }
        }
        void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(drawingSurface);
            if (bo != null)
                if (!bo.contains(pos))
                {
                    bo.hide();
                    bo = null;
                    return;
                }
            if (bo == null)
            {
                bo = new Bound(canvas, pos);
                bo.LostFocus += bo_LostFocus;
                bo.Focus();
                if (Star.IsChecked == true)
                {
                   
                    currentShape = new GoodStar(5, 0.4, bo);
                    currentShape.Color = new SolidColorBrush(ShapeColor.SelectedColor);
                    currentShape.BorderPen = new Pen(new SolidColorBrush(StrokeColor.SelectedColor), BorderThickness.Value);
                    drawingSurface.AddVisual(currentShape);
                    gShapes.Add(currentShape);
                }
                if (Polygon.IsChecked == true)
                {

                    currentShape = new GoodPolygon(7, bo);
                    currentShape.Color = new SolidColorBrush(ShapeColor.SelectedColor);
                    currentShape.BorderPen = new Pen(new SolidColorBrush(StrokeColor.SelectedColor), BorderThickness.Value);
                    drawingSurface.AddVisual(currentShape);
                    gShapes.Add(currentShape);
                }
                if(Ellipse.IsChecked == true)
                {
                    currentShape = new GoodEllipse(bo);
                    currentShape.Color = new SolidColorBrush(ShapeColor.SelectedColor);
                    currentShape.BorderPen = new Pen(new SolidColorBrush(StrokeColor.SelectedColor), BorderThickness.Value);
                    drawingSurface.AddVisual(currentShape);
                    gShapes.Add(currentShape);
                }
                if (Rectangle.IsChecked == true)
                {
                    currentShape = new GoodRectangle(bo, 0, 0);
                    currentShape.Color = new SolidColorBrush(ShapeColor.SelectedColor);
                    currentShape.BorderPen = new Pen(new SolidColorBrush(StrokeColor.SelectedColor), 1);
                    drawingSurface.AddVisual(currentShape);
                    gShapes.Add(currentShape);
                }
                if (Picker.IsChecked == true)
                {
                    
                }
            }
        }

        bool isIntersects(Rect r, Point p)
        {
            if (r.X <= p.X && r.X + r.Width >= p.X &&
                r.Y <= p.Y && r.Y + r.Height >= p.Y)
                return true;
            else return false;
        }

        private void ShapeColorChanged(object sender,  RoutedEventArgs e)
        {
            if (currentShape != null)
            {
                currentShape.Color = new SolidColorBrush(ShapeColor.SelectedColor);
                currentShape.Draw();
            }
        }
        
        private void BorderColorChanged(object sender,  RoutedEventArgs e)
        {
            if (currentShape != null)
            {
                currentShape.BorderPen.Brush = new SolidColorBrush(StrokeColor.SelectedColor);
                currentShape.Draw();
            }
        }
        private void Window_MouseMove_1(object sender, MouseEventArgs e)
        {

            
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
           // bo = null;
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
           // bo = null;
        }

        private void drawingSurface_mouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            //Point pos = e.GetPosition(drawingSurface);
            //if (bo == null)
            //{
            //    Rect r = new Rect(100, 100, 100, 100);
            //    bo = new Bound(canvas, r);
            //    gs = new GoodStar(5, 0.5, bo);
            //    gs.Draw();
            //    drawingSurface.AddVisual(gs); 
            //    bo.Focus();
            //}
            
        }
        private void drawingSurface_mouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(drawingSurface);
            if (Picker.IsChecked == true )
            {
                //GoodShape s = (GoodShape)drawingSurface.HitTest(p);
                
                if (bo != null)
                {
                //    List<DrawingVisual> s = drawingSurface.GetVisuals(bo.RenderedGeometry);
                //    List<GoodShape> shapes = new List<GoodShape>();
                //    foreach(DrawingVisual dv in s)
                //    {
                //        shapes.Add(dv as GoodShape);
                //    }
                //    currentShape = new GoodShapesCollection(bo, shapes.ToArray());
                }

                else
                {
                    GoodShape g = (GoodShape)drawingSurface.HitTest(p);
                    
                    if (g != null)
                    {
                        setActive(g);
                        ShapeColor.SelectedColor = (g.Color as SolidColorBrush).Color;
                        StrokeColor.SelectedColor = (g.BorderPen.Brush as SolidColorBrush).Color;
                        BorderThickness.Value = g.BorderPen.Thickness;
                    }
                }
            }
        }
        private void setActive(GoodShape shape)
        {
            if(shape != null)
            {
                if (bo != null) bo.hide();
                bo = shape.Bound;
                currentShape = shape;
                sim.setObjects(shape.GetType(), ShapeInfo);
                bo.show();
                bo.Focus();
            }
        }
        private void File_Click(object sender, RoutedEventArgs e)
        {
            //image.Source = drawingSurface.SaveToImage();

           // Saver.SaveVisual("hello.txt", drawingSurface);
           // Saver.WriteShape("hello.txt", gShapes[0]);
             //metadata = gShapes[0].Information();
          //  createFile("Shapes.txt");
        }
        string metadata = "";
        private void Load_Click(object sender, RoutedEventArgs e)
        {
           // drawingSurface = Saver.LoadDC("hello.txt");
           // Saver.AddVisual("hello.txt", drawingSurface);

          //  openFile("Shapes.txt");
        }

        private void BorederThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (currentShape != null && currentShape.BorderPen.Thickness != e.NewValue)
                currentShape.BorderPen.Thickness = e.NewValue;
        }

        private void ShapePanel_MouseDown(object sender, MouseButtonEventArgs e)
        {    sim.setObjects(typeof(GoodPolygon), ShapeInfo);
          
        }
        void createFile(string path)
        {
            using(StreamWriter sw = new StreamWriter(path))
            {
                foreach(GoodShape s in gShapes)
                {
                    sw.Write(s.Information());
                }
            }
        }
        void removeFocus(GoodShape g)
        {
            g.Bound.hide();
        }
        void openFile(string path)
        {
            try
            {
                using (StreamReader sw = new StreamReader(path))
                {
                    string s = sw.ReadToEnd();
                    string[] shapes = s.Split("[]".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    gShapes.Clear();
                    drawingSurface.Clear();
                    foreach (string shape in shapes)
                    {
                        GoodShape gs = GoodShape.GetShape("["+shape+"]", canvas);
                        drawingSurface.AddVisual(gs);
                        gs.Draw();
                        gs.Bound.hide();
                        gShapes.Add(gs);
                    }
                }
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
        private void Shape_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == Rectangle)
                sim.setObjects(typeof(GoodRectangle), ShapeInfo);
            if (sender == Star)
                sim.setObjects(typeof(GoodStar), ShapeInfo);
            if (sender == Ellipse)
                sim.setObjects(typeof(GoodEllipse), ShapeInfo);
            if (sender == Polygon)
                sim.setObjects(typeof(GoodPolygon), ShapeInfo);
            if(currentShape != null)
            currentShape = null;
            if (bo != null)
            {
                bo.hide();
                bo = null;
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = ".egd";

            openDialog.Filter = "EgoodDrawer documents (.egd)|*.egd"; 
           // openDialog.
            if(openDialog.ShowDialog().Value)
            {
                //FileOpen(openDialog.SafeFileName
                openFile(openDialog.FileName);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ".egd";
            saveDialog.Filter = "EgoodDrawer documents (.egd)|*.egd";
            if(saveDialog.ShowDialog().Value)
            {
                //if(saveDialog.)
                createFile(saveDialog.FileName);
            }
        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (CurrentShape != null)
                {
                    CurrentShape.Bound.hide();
                    CurrentShape.removeBound();
                    drawingSurface.DeleteVisual(CurrentShape);
                    currentShape = null;
                    bo = null;
                    gShapes.Remove(CurrentShape);
                }
            }
        }

        private void window_KeyDown(object sender, KeyEventArgs e)
        {

        }
       
     
    }
}
