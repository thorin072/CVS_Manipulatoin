using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MinipulVS
{
    public interface IGraphServise
    {
        List<ApproxRobot> GetApprox(List<MetricaContour> Trajectory);
    }
    class GraphServise : IGraphServise
    {
        private class Solution
        {
            public bool Flag { get; set; }
            public int Point { get; set; }
        }

        private Tuple<double, double, double> Coefficients(double x1, double y1, double x2, double y2)
        {
            var A = (y1 - y2);
            var B = (x2 - x1);
            var C = (x1 * y2 - x2 * y1);
            return Tuple.Create(A, B, C);
        }
        public bool IsPoint(int x, int y, List<Point> Point,int type)
        {
          
            if (
               Point.Contains(new Point(x, y)) ||
               Point.Contains(new Point(x, y - 1)) ||
               Point.Contains(new Point(x, y + 1)) ||
               Point.Contains(new Point(x - 1, y)) ||
               Point.Contains(new Point(x + 1, y)) 
               )
            {
                return true;
            }
            else
            {
                if (
                (Point.Contains(new Point(x, y - 2)) ||
                Point.Contains(new Point(x, y + 2)) ||
                Point.Contains(new Point(x - 2, y)) ||
                Point.Contains(new Point(x + 2, y)) 
                ) && (type==2))
                {
                    return true;
                }
                else { return false; }
            }

        }
        private Solution Line(Point a, Point b, List<Point> ContourPoint, int dot, ref Approx T)
        {
            var LinearY = 0;
            Solution solution = new Solution();
            var coef = Coefficients(a.X, a.Y, b.X, b.Y);
            var randX = Math.Abs((a.X + b.X) / 2) + 1;
            if (coef.Item2 == 0) {LinearY = a.Y;}
            else { LinearY = (int)(((-coef.Item1 * randX) - coef.Item3) / coef.Item2);}
            Point probe = new Point(randX, LinearY);// пробная точка
            //выявление нахождения позиции точки прямая//кривая
            if (IsPoint(randX, LinearY, ContourPoint,1))
            {
                T.TypeLine = Approx.TypeApprox.Linear;
                T.P0_Start = a;
                T.P1_Bend_Point = a;
                T.P2_End = b;
                solution.Flag = true;
                solution.Point = 0;
                return solution;
            }
            else
            {
                Point bend;
                SplineServise solutionBezie = new SplineServise();
                // определение направления
                if (b.X > a.X)
                {
                    if (b.Y > a.Y)
                    {
                        bend = new Point(b.X, b.Y);
                    }
                    else
                    {
                        bend = new Point(b.X, a.Y);
                    }
                }
                else
                {
                    if (b.Y > a.Y)
                    {
                        bend = new Point(a.X, b.Y);
                    }
                    else
                    {
                        bend = new Point(a.X, a.Y);
                    }
                }
                var BezieY = solutionBezie.QuadraticityCheck(a, bend, b, randX);
                if (IsPoint(randX,BezieY,ContourPoint,2))
                {
                    T.TypeLine = Approx.TypeApprox.Bezier_Curve;
                    T.P0_Start = a;
                    T.P1_Bend_Point = bend;
                    T.P2_End = b;
                    solution.Flag = true;
                    solution.Point = 0;
                    return solution;
                }
                solution.Flag = false;
                solution.Point = dot;
                return solution;
            }
        }

        private int TestStep(List<Point> ContourPoint, Point start, double[] part, List<Point> ClustObj, ref Approx T)
        {
            int minindex = 0;
            Solution solution = new Solution();
            while (solution.Flag != true)
            {
                var minDist = part.Min();
                minindex = Array.FindLastIndex(part, delegate (double i) { return i == minDist; });
                solution = Line(start, ClustObj[minindex], ContourPoint, minindex, ref T);
                if (solution.Flag == false) part[solution.Point] = double.MaxValue;
            }
            return minindex;
        }

        private double[] GetSection(int metka, int size, List<Point> Key, List<int> OutPath)
        {
            double[] res = new double[size];

            for (int j = 0; j < size; j++)
            {
                res[j] = Math.Sqrt((float)Math.Pow(Key[j].X - Key[metka].X, 2) + Math.Pow(Key[j].Y - Key[metka].Y, 2));
            }
            foreach (var el in OutPath)
            {
                res[el] = double.MaxValue;
            }
            return res;
        }

        private List<Approx> GetLimbs(List<Point> Cluster, List<Point> ContourPoint)
        {
            var result = new List<Approx>();
            int n = 0;
            List<int> Out = new List<int>(); //лист точек в которых еще не произошел обход
            int resetVertex = Cluster.Count; // количество точек для отрезка отбора минимума part
            int metka = 0;
            int count = resetVertex - 1;// первоначальная инициализация
            Out.Add(0);
            while (Out.Count < Cluster.Count)
            {
                Approx T = new Approx();
                double[] section = GetSection(metka, Cluster.Count, Cluster, Out);
                Point startpoint = Cluster[n];
                metka = TestStep(ContourPoint, startpoint, section, Cluster, ref T);
                Out.Add(metka);
                n = metka;
                result.Add(T);
            }
            result.Add(new Approx { TypeLine = Approx.TypeApprox.Linear, P0_Start = result[result.Count - 1].P2_End, P2_End = result[0].P0_Start });
            return result;
        }
        public List<ApproxRobot> GetApprox(List<MetricaContour> Trajectory)
        {
            var Interpolation_List = new List<ApproxRobot>();
            foreach (var track in Trajectory)
            {
                var ultimate_approx = GetLimbs(track.KeysPoint, track.ContourPoint);
                ApproxRobot el = new ApproxRobot { Approxes = ultimate_approx };
                Interpolation_List.Add(el);
            }
            return Interpolation_List;
        }


    }
}
