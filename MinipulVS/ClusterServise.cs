using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Accord;
using Accord.MachineLearning;

namespace MinipulVS
{
    /// <summary>
    /// Класс кластера
    /// </summary>
    public class Metrica
    {
        public List<System.Drawing.Point> KeysPoint;
    }
    public class MetricaContour : Metrica, IComparable<MetricaContour>
    {
        public List<System.Drawing.Point> ContourPoint;
        public int CompareTo(MetricaContour obj)
        {
            if (this.ContourPoint.Count > obj.ContourPoint.Count)
                return 1;
            if (this.ContourPoint.Count < obj.ContourPoint.Count)
                return -1;
            else return 0;
        }
    }
    public class Approx : Metrica
    {
        /// <summary>
        /// Тип для интерполяции
        /// </summary>
        public enum TypeApprox { Linear, Bezier_Curve };
        /// <summary>
        /// 0 - Линейная , 1 - Безье
        /// </summary>
        public TypeApprox TypeLine;
        /// <summary>
        /// Точка перегиба
        /// </summary>
        public System.Drawing.Point P1_Bend_Point;
        //public List<System.Drawing.Point> KeysPoint;
        public System.Drawing.Point P0_Start;
        public System.Drawing.Point P2_End;
        public int distance_module;

    }
    public class RobotPosition
    {
        public double x; // = z
        public double y; // = x
        public double z; // = y
        public double time;
    }
    
    public class ApproxRobot
    {
        public List<Approx> Approxes;
    }

    public class Constants
    {
        /// <summary>
        /// Ноль манипулятора (Z)
        /// </summary>
        public const double StartZ = 686.6;
        /// <summary>
        /// Ноль манипулятора (Y)
        /// </summary>
        public const double StartY = 390;
        /// <summary>
        /// Коэфициент для перевода точки на точку плоскости
        /// </summary>
        public const double StartYPlot = 500.000025;
        /// <summary>
        /// Время для опускания/поднятия
        /// </summary>
        public const double timeUp = 1 * 1e-3;
    }
    public class TimeAll
    {
        public double Time;
    }
    /// <summary>
    /// Время для реализации всего контура от опускания до поднятия манипулятора
    /// </summary>
   // public static TimeAll AllTime;


    public interface IClusterServise
    {
        double[][] GetPoint(List<Accord.IntPoint> data);
        List<System.Drawing.Point> GetPointList(List<Accord.IntPoint> data);
        double[][] GetPoint(List<System.Drawing.Point> data);
        List<Metrica> Clustering(double[][] data, int count);
        List<System.Drawing.Point> Comparison(List<System.Drawing.Point> Key, List<System.Drawing.Point> Contrs);

    }
    public class ClusterServise : IClusterServise
    {

        /// <summary>
        ///   Распределение ключевых точек изображения в отдельные кластеры
        /// </summary>
        /// <param name="data">Коллекция точек </param>
        /// <param name="metriks">Массив меток для каждой точки</param>
        /// <returns>Список кластеров с соответствующими точками</returns>
        private List<Metrica> Sort(double[][] data, int[] metriks)
        {
            var res = new List<Metrica>(); // результирующий
            var countCluster = metriks.Distinct().ToArray(); // количество кластеров
            int n = 0;
            int k = 0;
            foreach (var numClust in countCluster)
            {
                var temp = new List<System.Drawing.Point>();
                for (int j = 0; j < metriks.Length; j++)
                {
                    if (metriks[j] == numClust)
                    {
                        temp.Add(new System.Drawing.Point((int)data[n][0], (int)data[n][1]));
                    }
                    n++;
                }
                Metrica el = new Metrica
                {
                    KeysPoint = temp,
                };
                res.Add(el);
                n = 0;
                k++;
            }
            return res;
        }

        /// <summary>
        ///  Кластеризация
        /// </summary>
        /// <param name="data">Массив ключевых точек</param>
        /// <param name="count">Количество желаемых класетров в выборке ключевых точек</param>
        /// <returns>Лист кластеризованных контуров</returns>
        public List<Metrica> Clustering(double[][] data, int count)
        {
            Accord.Math.Random.Generator.Seed = 0;
            KMeans kmeans = new KMeans(k: count); // создать обьект результата k-means
            var clusters = kmeans.Learn(data); // обработка выборки
            // массив меток для каждой точки
            int[] metriks = clusters.Decide(data);
            var res = Sort(data, metriks); // сортировка 
            return res;
        }

        /// <summary>
        /// Получить матрицу ключевых точек
        /// </summary>
        /// <param name="data">Массив точек</param>
        /// <returns>Матрица ключевых точек для k-means</returns>
        public double[][] GetPoint(List<IntPoint> data)
        {
            double[][] res = new double[data.Count][];
            int n = 0;
            foreach (var el in data)
            {
                double[] temp = new double[2];
                temp[0] = el.X;
                temp[1] = el.Y;
                res[n] = temp;
                n++;
            }
            return res;
        }

        public double[][] GetPoint(List<System.Drawing.Point> data)
        {
            double[][] res = new double[data.Count][];
            int n = 0;
            foreach (var el in data)
            {
                double[] temp = new double[2];
                temp[0] = el.X;
                temp[1] = el.Y;
                res[n] = temp;
                n++;
            }
            return res;

        }

        /// <summary>
        ///  Получить лист контрольных точек
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<System.Drawing.Point> GetPointList(List<IntPoint> data)
        {
            List<System.Drawing.Point> res = new List<System.Drawing.Point>();
            foreach (var el in data)
            {
                res.Add(new System.Drawing.Point(el.X, el.Y));
            }
            return res;

        }

        public List<System.Drawing.Point> Comparison(List<System.Drawing.Point> Key, List<System.Drawing.Point> Contrs)
        {
            var res = new List<System.Drawing.Point>(); // результирующий
            foreach (var dotKey in Key)
            {
                if (Contrs.Contains(new System.Drawing.Point(dotKey.X, dotKey.Y)) ||
                Contrs.Contains(new System.Drawing.Point(dotKey.X, dotKey.Y - 1)) ||
                Contrs.Contains(new System.Drawing.Point(dotKey.X, dotKey.Y + 1)) ||
                Contrs.Contains(new System.Drawing.Point(dotKey.X-1, dotKey.Y )) ||
                Contrs.Contains(new System.Drawing.Point(dotKey.X+1, dotKey.Y)) )
                { res.Add(dotKey); }
            }
            return res;
        }
    }
}
