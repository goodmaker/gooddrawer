using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;


namespace GoodDrawer
{
    static class Saver
    {
        public static void SaveVisual(string path, DrawingCanvas dc)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create,FileAccess.ReadWrite))
            {
                //XamlWriter.Save(dc, fs);
                BinaryFormatter f = new BinaryFormatter();
                

                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dc); i++)
                {
                    XamlWriter.Save(VisualTreeHelper.GetChild(dc, i), fs);
                    //f.Serialize(fs, VisualTreeHelper.GetChild(dc,i));
                }
            }
            
        }
        public static DrawingCanvas LoadDC(string path)
        {
            DrawingCanvas dc = new DrawingCanvas();
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
               //dc = (DrawingCanvas)XamlReader.Load(fs);
                BinaryFormatter f = new BinaryFormatter();
                dc = (DrawingCanvas)f.Deserialize(fs);
                
            }
            return dc;
        }
        public static void AddVisual(string path,DrawingCanvas dc)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
               dc.AddVisual((Visual) XamlReader.Load(fs));
            }
        }
        public static void WriteShape(string path, GoodShape s)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                XamlWriter.Save(s, fs);
              

                
            }
        }
    }
}
