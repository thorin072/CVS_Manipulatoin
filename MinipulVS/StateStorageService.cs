using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MinipulVS
{
    public interface IStateStorageService
    {
        void SaveImg(Bitmap img);
        Bitmap GetImg();
        void SaveKeyArr(double[][] array);
        double[][] GetKeyArr();
        void SaveApproxList(List<ApproxRobot> List);
        List<ApproxRobot> GetApproxList();
        void SaveClusterList(List<Metrica> List);
        List<Metrica> GetClusterList();
        double GetMatlabTime();
        void SaveMatlabTime(TimeAll Mtime);
    }
    class StateStorageService : IStateStorageService
    {
        #region Глобальные переменные
        /// <summary>
        /// Исходное изображение
        /// </summary>
        private static Bitmap InputImg;
        /// <summary>
        /// Массив ключевых точек
        /// </summary>
        private static double[][] KeyPoint;
        /// <summary>
        ///  Лист точек интерполяции
        /// </summary>
        private static List<ApproxRobot> Interpolation_List;
        /// <summary>
        ///   Лист кластеров ключевых точек
        /// </summary>
        private static List<Metrica> ClusterKeyPoint;
        public static TimeAll MatlabTime;
        #endregion

        #region Методы сохранения и возврата
        public Bitmap GetImg()
        {
            return InputImg;
        }
        public double[][] GetKeyArr()
        {
            return KeyPoint;
        }
        public List<ApproxRobot> GetApproxList()
        {
            return Interpolation_List;
        }
        public void SaveApproxList(List<ApproxRobot> List)
        {
            Interpolation_List = List;
        }
        public List<Metrica> GetClusterList()
        {
            return ClusterKeyPoint;
        }
        public void SaveClusterList(List<Metrica> List)
        {
            ClusterKeyPoint = List;
        }
        public void SaveImg(Bitmap img)
        {
            InputImg = img;
        }
        public void SaveKeyArr(double[][] array)
        {
            KeyPoint = array;
        }
        public double GetMatlabTime()
        {
            return MatlabTime.Time;
        }
        public void SaveMatlabTime(TimeAll Mtime)
        {
            MatlabTime = Mtime;
        }
        #endregion
    }
}
