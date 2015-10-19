using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GoodDrawer
{
    public class ShapeInfoManager
    {
        List<UIElement> elements = new List<UIElement>();
        public void setObjects(Type ShapeType, StackPanel dp)
        {
            Elements.Clear();
            dp.Children.Clear();
            if(ShapeType == typeof(GoodStar))
            {
                dp.Children.Add(new TextBlock() { Text = "Tops Count: " });
               // dp.Children.Add(new TextBox() { Name= "topsCount",Width = 30 });
                Slider tops = new Slider() { Name = "topsCount", Value =5, Minimum = 1, Maximum = 30, SmallChange = 1, LargeChange = 1, Width = 100 };
                tops.ValueChanged += tops_ValueChanged;
                dp.Children.Add(tops);
                dp.Children.Add(new TextBlock() { Text = "Coeficient: " });
                Slider coef = new Slider() { Name = "Coefficient",Value = 0.5, Minimum = 0.01, Maximum = 1, SmallChange = 0.01, LargeChange = 0.1, Width = 100 };
                coef.ValueChanged += coef_ValueChanged;
                dp.Children.Add(coef);
            }
            if(ShapeType == typeof(GoodPolygon))
            {
                dp.Children.Add(new TextBlock() { Name= "topsCount", Text = "Tops Count: " });
                Slider tops = new Slider() { Name = "topsCount", Value = 5, Minimum = 1, Maximum = 30, SmallChange = 1, LargeChange = 1, Width = 100 };
                tops.ValueChanged += tops_ValueChanged;
                dp.Children.Add(tops);
            }
            if(ShapeType == typeof(GoodRectangle))
            {
                dp.Children.Add(new TextBlock() { Text = "X radius: " });
                Slider xRadius = new Slider() { Name = "xRadius", Value = 0, Minimum = 0, Maximum = 90, SmallChange = 1, LargeChange = 1, Width = 100 };
                xRadius.ValueChanged += Angle_ValueChanged;
                dp.Children.Add(xRadius);
                dp.Children.Add(new TextBlock() { Text = "Y radius: " });
                Slider yRadius = new Slider() { Name = "yRadius", Value = 0, Minimum = 0, Maximum = 90, SmallChange = 1, LargeChange = 1, Width = 100 };
                yRadius.ValueChanged += Angle_ValueChanged;
                dp.Children.Add(yRadius);
            }

        }

        void Angle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(MainWindow.CurrentShape != null)
            {
                if(MainWindow.CurrentShape.GetType() == typeof(GoodRectangle))
                {
                    if ((sender as Slider).Name == "xRadius")
                    {
                        (MainWindow.CurrentShape as GoodRectangle).xRadius = e.NewValue;
                        
                    }
                    else
                        (MainWindow.CurrentShape as GoodRectangle).yRadius = e.NewValue;
                    MainWindow.CurrentShape.Draw();
                }
            }
            
        }

        void coef_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (MainWindow.CurrentShape as GoodStar).LenghtCoefficient = e.NewValue;
            MainWindow.CurrentShape.Draw();
        }

        void tops_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.CurrentShape != null)
            {
                if(MainWindow.CurrentShape.GetType() == typeof(GoodStar))
                    (MainWindow.CurrentShape as GoodStar).BeamCount = (int)e.NewValue + 1;
                if(MainWindow.CurrentShape.GetType() == typeof(GoodPolygon))
                    (MainWindow.CurrentShape as GoodPolygon).TopsCount = (int)e.NewValue+1;
                MainWindow.CurrentShape.Draw();
            }
        }
        public List<UIElement> Elements
        { get { return elements; } }
  
    }
}
