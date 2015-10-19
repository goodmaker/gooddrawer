using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
namespace GoodDrawer
{
    [Serializable()]
    public class GoodStar:GoodShape
    {
       private int _numberOfBeams;
       private double _lengthCoef;
       public int BeamCount
       {
           get { return _numberOfBeams; }
           set
           {
               if (value > 1)
               {
                   _numberOfBeams = value;
                   tops = new Point[_numberOfBeams*2];
               }
               else throw new ArgumentOutOfRangeException();
           }
       }
       public double LenghtCoefficient
       {
           get { return _lengthCoef; }
           set
           {
               if (value > 0 && value <= 1)
               {
                   _lengthCoef = value;
                   
               }
               else throw new ArgumentOutOfRangeException();
           }
       }
       
      public override void confirmGeometry()
       {
          PathSegmentCollection psc = new PathSegmentCollection();
          for(int i = 1 ; i < tops.Length; i ++)
          {
              psc.Add(new LineSegment(tops[i], true));
          }
          PathFigure pf = new PathFigure(tops[0],psc,true);         
          PathFigureCollection pfc = new PathFigureCollection { pf };
          geometry = new PathGeometry(pfc, FillRule.EvenOdd, Bound.Transform);
       }
       static GoodStar()
      {
         
      }
       public GoodStar(int NumberOfTops, double lenghtCoef, Bound b):base(b)
       {
           Name = "Star";
           _numberOfBeams = NumberOfTops;
           this._lengthCoef = lenghtCoef;
           tops = new System.Windows.Point[NumberOfTops * 2];
           
       }   
        public GoodStar():base(null)
       {
           BeamCount = 4;
           LenghtCoefficient = 0.5;
          
       }
       public override void calculateValues()
       {
           double radius = Math.Min(Bound.Rect.Width, Bound.Rect.Height)/2;
           double angle = Math.PI / BeamCount;
           Point center = Bound.Center;
           for (int i = 0; i < tops.Length; i += 2)
           {
               tops[i].X = center.X + radius*Math.Sin(i * angle);
               tops[i].Y = center.Y + radius*Math.Cos(i * angle);

               tops[i + 1].X = center.X + LenghtCoefficient * radius * Math.Sin(angle * i + angle);
               tops[i + 1].Y = center.Y + LenghtCoefficient * radius * Math.Cos(angle * i + angle);
           }
       }
       public override string Information()
       {
           
           StringBuilder sb = new StringBuilder( base.Information());
           sb.Append(String.Format("N:{0};C:{1}]", BeamCount, LenghtCoefficient));
           return sb.ToString();
       }
        
    }
}
