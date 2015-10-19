using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace GoodDrawer
{
    [Serializable()]
    public class DrawingCanvas:Canvas, ISerializable 
    {
        private List<Visual> visuals = new List<Visual>();
        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);
            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
            
        }
        public void Clear()
        {
            foreach(Visual v in visuals)
            {
                base.RemoveVisualChild(v);
                base.RemoveLogicalChild(v);
            }
            visuals.Clear();
        }
        public void AddRange(Visual[] vs)
        {
            foreach(Visual v in vs)
            {
                visuals.Add(v);
                base.AddVisualChild(v);
                base.AddLogicalChild(v);
            }
        }
        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);
            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }
        public RenderTargetBitmap SaveToImage()
        {
            DrawingGroup dg = new DrawingGroup();
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96, 96, PixelFormats.Default);
            foreach(Visual v in visuals)
            {
                rtb.Render(v);
            }
            BitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream file = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Save(file);
            FileStream fstream = File.OpenWrite("Hello.jpg");
            file.WriteTo(fstream);
            fstream.Flush();
            fstream.Close();
            return rtb;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ShapeCount",visuals.Count);
            
        }
        public DrawingCanvas() { }
        public DrawingCanvas(SerializationInfo info, StreamingContext context) //конструктор десериализации
        {
        // Reset the property value using the GetValue method.
            int count = (int)info.GetValue("ShapeCount", typeof(int));
            for(int i = 0; i < count; i++)
            {
                string xaml = (string)info.GetValue("Shape"+i,typeof(string));
                
            }
        }
        public DrawingVisual HitTest(System.Windows.Point p)
        {
           var result = VisualTreeHelper.HitTest(this, p);
           return result.VisualHit as DrawingVisual; 
        }

        #region множественный выбор
        List<DrawingVisual> hits = new List<DrawingVisual>();
        public List<DrawingVisual> GetVisuals(Geometry region)
        {
            hits.Clear();
            GeometryHitTestParameters param = new GeometryHitTestParameters(region);
            HitTestResultCallback callback = new HitTestResultCallback(this.HitTestCallBack);
            VisualTreeHelper.HitTest(this, null, callback, param);
            return hits;
        }
        private HitTestResultBehavior HitTestCallBack(HitTestResult result)
        {
            GeometryHitTestResult geometryResult = (GeometryHitTestResult)result;
            DrawingVisual visual = result.VisualHit as DrawingVisual;
            if (visual != null && geometryResult.IntersectionDetail == IntersectionDetail.FullyInside)
                hits.Add(visual);
            return HitTestResultBehavior.Continue;
        }
        #endregion
    }
}
