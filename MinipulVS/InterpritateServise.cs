using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinipulVS
{

    public interface IInterpritateServise
    {
        IEnumerable<RobotPosition> InterpretationOfCommands(IList<ApproxRobot> points, double Zplot, double ZplotPause,double aP, double bP);   //a=0.002 b=0.001
    }
    class InterpritateServise : IInterpritateServise
    {

        /// <summary>
        /// Интерпритация команд для робота (вычисление траекторий)
        /// </summary>
        /// <param name="points">Входной лист точек всего контура</param>
        /// <param name="Zplot">Высота на которой находится раб.обл</param>
        /// <param name="ZplotPause">Высота на которой будет происходить перемещение с контура на контур</param>
        /// <returns></returns>
        public IEnumerable<RobotPosition> InterpretationOfCommands(IList<ApproxRobot> points, double Zplot, double ZplotPause,double aP,double bP)
        {

            TimeAll Times = new TimeAll
            {
                Time = 1 * 1e-3
            };

            CommandServise Command = new CommandServise();
            // Манипулятор из точки(0,390,687) будет медленно опускаться в первую точку контура(x1, y1, z0)
            var STRUCT = Command.Start(Times.Time, Constants.StartZ, Constants.StartY, points[0].Approxes[0].P0_Start.X - 100, points[0].Approxes[0].P0_Start.Y + Constants.StartYPlot, Zplot);


            foreach (var position in STRUCT)
            {
                RobotPosition result = new RobotPosition
                {
                    x = position.x,
                    y = position.y,
                    z = position.z,
                    time = position.time
                };
                Times.Time = 0.001 + Times.Time;
                yield return result;
            }

            for (int i = 0; i < 1; i++) // Коллекция i - ых контуров ( количество найденых 0..n)
            {
                for (int m = 0; m < points[i].Approxes.Count; m++) // Обращение к элементам коллекции 
                {
                    ///рисуем контур на плоскости
                    SplineServise spline = new SplineServise();
                    var localtime = 0.0;
                    var start = (double)(points[i].Approxes[m].P0_Start.X - 100);
                    double fort = points[i].Approxes[m].P0_Start.Y + Constants.StartYPlot;
                    var end = points[i].Approxes[m].P2_End.X - 100;

                    if (start == end) // на одной линии
                    {
                        var startY = points[i].Approxes[m].P0_Start.Y + Constants.StartYPlot;
                        var endY = points[i].Approxes[m].P2_End.Y + Constants.StartYPlot;
                        var expX = 0.0;
                        var expY = 0.0;
                        while (endY < startY)
                        {
                            RobotPosition result = new RobotPosition
                            {
                                z = spline.PositionXLinear(points[i].Approxes[m].P0_Start.X - 100, points[i].Approxes[m].P2_End.X - 100, localtime),
                                x = spline.PositionYLinear(points[i].Approxes[m].P0_Start.Y + Constants.StartYPlot, points[i].Approxes[m].P2_End.Y + Constants.StartYPlot, localtime),
                                y = Zplot,
                                time = Times.Time
                            };
                            expX = start - result.z;
                            expY = startY - result.x;
                            startY = result.x;
                            localtime = 0.025 + localtime;
                            if (((Math.Abs(expX) > 0) && (Math.Abs(expX) < 0.1)) || ((Math.Abs(expY) > 0) && (Math.Abs(expY) < 0.1))) { continue; }
                            else { Times.Time = 0.001 + Times.Time; yield return result; }
                        }
                    }
                    if (end > start)  // проход влево
                    {
                        var expX = 0.0;
                        var expY = 0.0;
                        if (points[i].Approxes[m].TypeLine == Approx.TypeApprox.Linear)
                        {
                            //(1)
                            while (end > start)
                            {
                               
                                RobotPosition result = new RobotPosition
                                {
                                    z = spline.PositionXLinear(points[i].Approxes[m].P0_Start.X - 100, points[i].Approxes[m].P2_End.X - 100, localtime),
                                    x = spline.PositionYLinear(points[i].Approxes[m].P0_Start.Y + Constants.StartYPlot, points[i].Approxes[m].P2_End.Y + Constants.StartYPlot, localtime),
                                    y = Zplot,
                                    time = Times.Time
                                };
                                expX = start - result.z;
                                expY = fort - result.x;
                                start = result.z;
                                fort = result.x;
                                localtime = 0.025 + localtime;
                                if (Accuracy(expX, expY, aP, bP) == true)
                                {
                                    Times.Time = 0.001 + Times.Time;
                                    yield return result;
                                }
                                else continue;
                            }
                        }
                        if (points[i].Approxes[m].TypeLine == Approx.TypeApprox.Bezier_Curve)
                        {
                            while (end > start)
                            {
                                RobotPosition result = new RobotPosition
                                {
                                    z = spline.PositionX(points[i].Approxes[m].P0_Start.X - 100, points[i].Approxes[m].P1_Bend_Point.X - 100, points[i].Approxes[m].P2_End.X - 100, localtime),
                                    x = spline.PositionY(points[i].Approxes[m].P0_Start.Y + Constants.StartYPlot, points[i].Approxes[m].P1_Bend_Point.Y + Constants.StartYPlot, points[i].Approxes[m].P2_End.Y + Constants.StartYPlot, localtime),
                                    y = Zplot,
                                    time = Times.Time
                                };
                                expX = start - result.z;
                                expY = fort - result.x;
                                start = result.z;
                                fort = result.x;
                                localtime = 0.025 + localtime;
                                if (Accuracy(expX, expY, aP, bP) == true)
                                {
                                    Times.Time = 0.001 + Times.Time;
                                    yield return result;
                                }
                                else continue;
                            }
                        }
                    }
                    else // проход вправо
                    {
                        var expX = 0.0;
                        var expY = 0.0;
                        if (points[i].Approxes[m].TypeLine == Approx.TypeApprox.Linear)
                        {
                            while (start > end)
                            {
                                RobotPosition result = new RobotPosition
                                {
                                    z = spline.PositionXLinear(points[i].Approxes[m].P0_Start.X - 100, points[i].Approxes[m].P2_End.X - 100, localtime),
                                    x = spline.PositionYLinear(points[i].Approxes[m].P0_Start.Y + Constants.StartYPlot, points[i].Approxes[m].P2_End.Y + Constants.StartYPlot, localtime),
                                    y = Zplot,
                                    time = Times.Time
                                };
                                expX = start - result.z;
                                expY = fort - result.x;
                                start = result.z;
                                fort = result.x;
                                localtime = 0.025 + localtime;
                                if (Accuracy(expX, expY, aP, bP) == true)
                                {
                                    Times.Time = 0.001 + Times.Time;
                                    yield return result;
                                }
                                else continue;
                            }
                        }
                        if (points[i].Approxes[m].TypeLine == Approx.TypeApprox.Bezier_Curve)
                        {
                            while (start > end)
                            {
                                RobotPosition result = new RobotPosition
                                {
                                    z = spline.PositionX(points[i].Approxes[m].P0_Start.X - 100, points[i].Approxes[m].P1_Bend_Point.X - 100, points[i].Approxes[m].P2_End.X - 100, localtime),
                                    x = spline.PositionY(points[i].Approxes[m].P0_Start.Y + Constants.StartYPlot, points[i].Approxes[m].P1_Bend_Point.Y + Constants.StartYPlot, points[i].Approxes[m].P2_End.Y + Constants.StartYPlot, localtime),
                                    y = Zplot,
                                    time = Times.Time
                                };
                                expX = start - result.z;
                                expY = fort - result.x;
                                start = result.z;
                                fort = result.x;
                                localtime = 0.025 + localtime;
                                if (Accuracy(expX, expY,aP,bP) == true)
                                {
                                    Times.Time = 0.001 + Times.Time;
                                    yield return result;
                                }
                                else continue;
                            }
                        }
                    }
                }
                ///если контур последний , выход из цикла рисования
                if (i == points.Count - 1)
                {
                    break;
                }
                else
                {
                    //// поднятие рабочего органа манипулятора в точке
                    //STRUCT = Command.PenUp(Times.Time, Zplot, points[i].Approxes[points[i].Approxes.Count - 1].P2_End.X - 100, points[i].Approxes[points[i].Approxes.Count - 1].P2_End.Y + Constants.StartYPlot, Constants.timeUp, Zplot, ZplotPause);
                    //foreach (var position in STRUCT)
                    //{
                    //    RobotPosition result = new RobotPosition
                    //    {
                    //        x = position.x,
                    //        y = position.y,
                    //        z = position.z,
                    //        time = position.time
                    //    };
                    //    Times.Time = 0.001 + Times.Time;
                    //    yield return result;
                    //}

                    //// вычисление тракетории перемещения из(x1, y1, z0) в(x2, y2, z0)
                    //STRUCT = Command.PenPause(Times.Time, ZplotPause, points[i].Approxes[points[i].Approxes.Count - 1].P2_End.X - 100, points[i].Approxes[points[i].Approxes.Count - 1].P2_End.Y + Constants.StartYPlot, points[i + 1].Approxes[points[i + 1].Approxes.Count - 1].P0_Start.X - 100, points[i + 1].Approxes[points[i + 1].Approxes.Count - 1].P0_Start.Y + Constants.StartYPlot);
                    //foreach (var position in STRUCT)
                    //{
                    //    RobotPosition result = new RobotPosition
                    //    {
                    //        x = position.x,
                    //        y = position.y,
                    //        z = position.z,
                    //        time = position.time
                    //    };
                    //    Times.Time = 0.001 + Times.Time;
                    //    yield return result;
                    //}

                    //// опускание рабочего органа манипулятора в точке
                    //STRUCT = Command.PenDown(Times.Time, ZplotPause, points[i + 1].Approxes[points[i + 1].Approxes.Count - 1].P0_Start.X - 100, points[i + 1].Approxes[points[i + 1].Approxes.Count - 1].P0_Start.Y + Constants.StartYPlot, Constants.timeUp, Zplot);
                    //foreach (var position in STRUCT)
                    //{
                    //    RobotPosition result = new RobotPosition
                    //    {
                    //        x = position.x,
                    //        y = position.y,
                    //        z = position.z,
                    //        time = position.time
                    //    };
                    //    Times.Time = 0.001 + Times.Time;
                    //    yield return result;
                    //}
                }
            }
           // поднятие рабочего органа манипулятора
            //STRUCT = Command.Stop(Times.Time, Constants.StartZ, Constants.StartY, points[points.Count - 1].Approxes[points[points.Count - 1].Approxes.Count - 1].P2_End.X - 100, points[points.Count - 1].Approxes[points[points.Count - 1].Approxes.Count - 1].P2_End.Y + Constants.StartYPlot, Zplot, Constants.timeUp);
            //foreach (var position in STRUCT)
            //{
            //    RobotPosition result = new RobotPosition
            //    {
            //        x = position.x,
            //        y = position.y,
            //        z = position.z,
            //        time = position.time
            //    };
            //    Times.Time = 0.001 + Times.Time;
            //    yield return result;
            //}

            StateStorageService el = new StateStorageService();
            el.SaveMatlabTime(Times);
        }
        private bool Accuracy(double expX, double expY,double a,double b)
        {
            if (Math.Abs(expY) == 0)  // Y const
            {
                if (((Math.Abs(expX) > 0) && (Math.Abs(expX) < 0.1))) { return false; }  //0.05 горизонтальные прямые
            }
            else
            {
                if (((Math.Abs(expX) > 0) && (Math.Abs(expX) < a )))     //a=0.002 b=0.001
                {
                    return false;
                }
                else
                {
                    if (((Math.Abs(expX) > 0) && (Math.Abs(expX) < b)) || ((Math.Abs(expY) > 0) && (Math.Abs(expY) < b))) { return false; }
                }
            }
            return true;
        }
    }

}
