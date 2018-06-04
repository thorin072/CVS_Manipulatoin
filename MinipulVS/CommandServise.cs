using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinipulVS
{
    public interface ICommandServise
    {
        IEnumerable<RobotPosition> Start(double time, double ZHight, double YHight, double x1, double y1, double Zplot);
        IEnumerable<RobotPosition> PenUp(double time, double ZHight, int x1, double y1, double timeUP, double Zplot, double ZplotPause);
        IEnumerable<RobotPosition> PenPause(double time, double ZHight, double x1, double y1, double x2, double y2);
        IEnumerable<RobotPosition> PenDown(double time, double ZHight, int x1, double y1, double timeUP, double Zplot);
        IEnumerable<RobotPosition> Stop(double time, double ZHight, double YHight, double x1, double y1, double Zplot, double timeUP);

      //  RobotPosition Line(int P0x, int P1x, double P0y, double P1y, double time);
    }
    class CommandServise : ICommandServise
    {
        /// <summary>
        /// Старт, опускание рабочего органа манипулятора в первую точку контура
        /// </summary>
        /// <param name="time">Начальное время</param>
        /// <param name="ZHight">Ноль манипулятора по Y</param>
        /// <param name="YHight">Ноль манипулятора по Х </param>
        /// <param name="x1">Первая точка контура</param>
        /// <param name="y1">Первая точка конутра</param>
        /// <param name="Zplot">Высота на которой находится раб.обл</param>
        /// <returns></returns>
        public IEnumerable<RobotPosition> Start(double time, double ZHight, double YHight, double x1, double y1, double Zplot)
        {
            var aX = (2 * (y1 - YHight) / Math.Pow(10, 6)); // ускорение по оси Х
            var aY = Math.Abs((2 * (Zplot - ZHight)) / Math.Pow(10, 6)); // ускорение по оси Y
            var aZ = (2 * (x1 - 0)) / Math.Pow(10, 6) * (-1); // ускорение по оси Z

            while (ZHight >= Zplot)
            {
                RobotPosition result = new RobotPosition
                {
                    time = time,
                    z = 0 - (aZ * Math.Pow(time * 1000, 2)) / 2,
                    x = Constants.StartY + (aX * Math.Pow(time * 1000, 2)) / 2
                };
                ZHight = Constants.StartZ - (aY * Math.Pow((1000 * time), 2)) / 2;
                result.y = ZHight;
                time = 0.001 + time;
                ZHight--;
                yield return result;
            }
        }

        /// <summary>
        /// Поднятие рабочего органа манипулятора
        /// </summary>
        /// <param name="time">Время</param>
        /// <param name="ZHight">Высота для поднятия манипулятора</param>
        /// <param name="x1">Координата в которой осуществляется подьем</param>
        /// <param name="y1">Координата в которой осуществляется подьем</param>
        /// <param name="timeUP">Время для поднятия пера</param>
        /// <param name="Zplot">Высота на которой находится раб.обл</param>
        /// <param name="ZplotPause">Высота на которой будет происходить перемещение с контура на контур</param>
        /// <returns></returns>
        public IEnumerable<RobotPosition> PenUp(double time, double ZHight, int x1, double y1, double timeUP, double Zplot, double ZplotPause)
        {
            var aY = Math.Abs((2 * (ZplotPause - ZHight)) / Math.Pow(10, 6)) * 12; // ускорение по оси Y
            while (ZHight <= 215)
            {
                RobotPosition result = new RobotPosition();
                ZHight = Zplot + (aY * Math.Pow((1000 * timeUP), 2)) / 2;
                result.y = ZHight;
                result.time = time;
                result.z = x1;
                result.x = y1;
                time = 0.001 + time;
                timeUP = 0.001 + timeUP;
                yield return result;
            }
        }

        /// <summary>
        /// Манипулятор остается на заданой высоте, происзодит перемещение по траектории от контура к контуру 
        /// </summary>
        /// <param name="time">Время</param>
        /// <param name="ZHight">Высота на которой происходит перемещение (постоянна)</param>
        /// <param name="x1">Последняя точка первого контура</param>
        /// <param name="y1">Последняя точка первого контура</param>
        /// <param name="x2">Первая точка нового контура</param>
        /// <param name="y2">Первая точка нового контура</param>
        /// <returns></returns>
        public IEnumerable<RobotPosition> PenPause(double time, double ZHight, double x1, double y1, double x2, double y2)
        {
            var coef = Coefficients(x1, y1, x2, y2); // коэффициенты для прямой перехода
            for (double i = x1; i < x2; i++) // вычисление точек траектории
            {
                RobotPosition result = new RobotPosition();
                var Yzn = ((-coef.Item1 * i) - coef.Item3) / coef.Item2;
                result.time = time;
                result.z = i;
                result.x = Yzn;
                result.y = ZHight;
                time = 0.001 + time;
                yield return result;
            }

        }

        /// <summary>
        /// Опускание рабочего органа манипулятора
        /// </summary>
        /// <param name="time">Время</param>
        /// <param name="ZHight">Высота с которой опускают</param>
        /// <param name="x1">Точка в которой осуществляется опускание</param>
        /// <param name="y1">Точка в которой осуществляется опускание</param>
        /// <param name="timeUP">Время для опускания</param>
        /// <param name="Zplot">Высота на которой находится раб.обл</param>
        /// <returns></returns>
        public IEnumerable<RobotPosition> PenDown(double time, double ZHight, int x1, double y1, double timeUP, double Zplot)
        {
            var aY = Math.Abs((2 * (Zplot - ZHight)) / Math.Pow(10, 6)) * 12; // ускорение по оси Y
            while (ZHight >= 200)
            {
                RobotPosition result = new RobotPosition();
                ZHight = 215 - (aY * Math.Pow((1000 * timeUP), 2)) / 2;
                result.y = ZHight;
                result.time = time;
                result.z = x1;
                result.x = y1;
                time = 0.001 + time;
                timeUP = 0.001 + timeUP;
                yield return result;
            }
        }

        /// <summary>
        /// Поднятие рабочего органа манипулятора
        /// </summary>
        /// <param name="time">Время</param>
        /// <param name="ZHight">Высота на которую поднимают (Z)</param>
        /// <param name="YHight">Координата манипулятора (Y)</param>
        /// <param name="x1">Последняя координата контура</param>
        /// <param name="y1">Последняя координата контура</param>
        /// <param name="Zplot">Высота рабочей области</param>
        /// <param name="timeUP">Время подьема</param>
        /// <returns></returns>
        public IEnumerable<RobotPosition> Stop(double time, double ZHight, double YHight, double x1, double y1, double Zplot, double timeUP)
        {
            var aX = (2 * (YHight - y1) / Math.Pow(10, 6)); // ускорение по оси Х
            var aY = Math.Abs((2 * (ZHight - Zplot)) / Math.Pow(10, 6)); // ускорение по оси Y
            var aZ = (2 * (0 - x1)) / Math.Pow(10, 6) * (-1); // ускорение по оси Z

            var a = Zplot;
            while (a <= Constants.StartZ)
            {
                RobotPosition result = new RobotPosition
                {
                    time = time,
                    z = x1 - (aZ * Math.Pow(timeUP * 1000, 2)) / 2,
                    x = y1 + (aX * Math.Pow(timeUP * 1000, 2)) / 2
                };
                a = Zplot + (aY * Math.Pow((1000 * timeUP), 2)) / 2;
                result.y = a;
                timeUP = 0.001 + timeUP;
                time = 0.001 + time;
                a++;
                yield return result;
            }
            var g = (int)(time + 1);
            while (time <= g)
            {

                RobotPosition result = new RobotPosition
                {
                    time = time,
                    z = 0,
                    x = YHight,
                    y = ZHight
                };
                time = 0.001 + time;
                yield return result;
            }
        }
        private Tuple<double, double, double> Coefficients(double x1, double y1, double x2, double y2)
        {
            //(y1-y2)*x+(x2-x1)*y+(x1y2-x2y1) = 0
            //Ax+By+C=0
            var A = (y1 - y2);
            var B = (x2 - x1);
            var C = (x1 * y2 - x2 * y1);
            return Tuple.Create(A, B, C);
        }
    }
}
