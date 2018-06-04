using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MinipulVS
{
    public interface ISplineServise
    {
        int QuadraticityCheck(Point P0, Point P1, Point P2, int x);
        double PositionX(int P0, int P1, int P2, double t);
        double PositionY(double P0, double P1, double P2, double t);
        double PositionXLinear(int P0, int P1, double t);
        double PositionYLinear(double P0, double P1, double t);
    }
    class SplineServise : ISplineServise
    {
        /// <summary>
        ///  Положение по Х (кубическая траектория) 
        /// </summary>
        /// <param name="P0">Значение Х в начальной точке</param>
        /// <param name="P1">Значение Х в точке перегиба</param>
        /// <param name="P2">Значение Х в конечной точке</param>
        /// <param name="t">Момент времени</param>
        /// <returns></returns>
        public double PositionX(int P0, int P1, int P2, double t)
        {
            return Math.Pow((1 - Q_Unit(t)), 2) * P0 + 2 * Q_Unit(t) * (1 - Q_Unit(t)) * P1 + Math.Pow(Q_Unit(t), 2) * P2;
        }
        /// <summary>
        ///  Положение по Y (кубическая траектория)  
        /// </summary>
        /// <param name="P0">Значение Х в начальной точке</param>
        /// <param name="P1">Значение Х в точке перегиба</param>
        /// <param name="P2">Значение Х в конечной точке</param>
        /// <param name="t">Момент времени</param>
        /// <returns></returns>
        public double PositionY(double P0, double P1, double P2, double t)
        {
            return Math.Pow((1 - Q_Unit(t)), 2) * P0 + 2 * Q_Unit(t) * (1 - Q_Unit(t)) * P1 + Math.Pow(Q_Unit(t), 2) * P2;
        }
        private double Q_Unit(double t)
        {
            //return (Math.Log(Math.Exp(2) * Math.Exp(-2 * t) + (Math.Pow(5, 2 * t) / 25)) / (Math.Log(5) * 2 + 2)) - (Math.Log(Math.Exp(2) * Math.Exp(-2 * t) + ((Math.Pow(5, 2 * t) * Math.Exp(-2)) / 625)) / (Math.Log(5) * 2 + 2));
            return ((Math.Log(Math.Exp(4) * Math.Exp(-4 * t) + 1)) / 4) - (Math.Log(Math.Exp(-4) + Math.Exp(4) * Math.Exp(-4 * t)) / 4);
        }
        /// <summary>
        /// Возаращает значение сплайна в искомом x
        /// </summary>
        /// <param name="P0">Начальная точка</param>
        /// <param name="P1">Точка перегиба</param>
        /// <param name="P2">Конечная точка</param>
        /// <returns></returns>
        public int QuadraticityCheck(Point P0, Point P1, Point P2, int x)
        {
            int P0x = P0.X;
            int P1x = P1.X;
            int P2x = P2.X;
            double t;
            if (P0x - 2 * P1x + P2x != 0)
            {
                t = (P0x - P1x + Math.Sqrt((P0x - 2 * P1x + P2x) * x + Math.Pow(P1x, 2) - P0x * P2x)) / (P0x - 2 * P1x + P2x);
                if (double.IsNaN(t) || t < 0)
                {
                    t = (P0x - P1x - Math.Sqrt((P0x - 2 * P1x + P2x) * x + Math.Pow(P1x, 2) - P0x * P2x)) / (P0x - 2 * P1x + P2x);
                }
            }//(+-)
            else
            {
                if ((P0x - 2 * P1x + P2x == 0) || P0x != P1x) { t = (x - P0x) / 2 * (P1x - P0x); }
                else { t = Math.Sqrt((x - P0x) / (P2x - P1x)); }
            }
            int P0y = P0.Y;
            int P1y = P1.Y;
            int P2y = P2.Y;

            var By = (int)(P0y * ((t - 2) * t + 1) + P1y * (2 - 2 * t) * t + P2y * Math.Pow(t, 2));
            return By;
        }

       
        /// <summary>
        ///  Положение по Х (линейная траектория)  
        /// </summary>
        /// <param name="P0">Значение Х в начальной точке</param>
        /// <param name="P1">Значение Х в точке перегиба</param>
        /// <param name="t">Момент времени</param>
        /// <returns></returns>
        public double PositionXLinear(int P0, int P1, double t)
        {
            return (1 - Q_Unit(t)) * P0 + (Q_Unit(t)) * P1;
        }
        /// <summary>
        ///  Положение по Y (линейная траектория)  
        /// </summary>
        /// <param name="P0">Значение Х в начальной точке</param>
        /// <param name="P1">Значение Х в точке перегиба</param>
        /// <param name="t">Момент времени</param>
        /// <returns></returns>
        public double PositionYLinear(double P0, double P1, double t)
        {
            return (1 - Q_Unit(t)) * P0 + (Q_Unit(t)) * P1;
        }
    }
}
