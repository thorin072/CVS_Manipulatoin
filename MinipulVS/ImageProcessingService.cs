using Accord.Imaging;
using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Shape;
using Accord;
using System.Drawing.Imaging;

namespace MinipulVS
{
    public interface IImageProccesingServise
    {
        List<Accord.IntPoint> KeyPoint(double sigma, float k, float threshold, Bitmap img); // перегрузка для точек Accord
        Tuple<int, int> ResizeImg(int nWidth, int nHeight);// изменение размера изображения
        Bitmap KeyPoint(double sigma, float k, float threshold, Bitmap img, int Height, int Width); // проецирование ключ-точек на изображение
        Image<Gray, byte> CannyFilter(int tresch, int tresch2, int rH, int rW, Image<Bgr, byte> orign); // фильтр Кэнни
        List<MetricaContour> СontourInСluster(Bitmap img, List<System.Drawing.Point> Key); // лист с контурами и ключ - точками входящими в него
        Bitmap GetInputImg(string file); // инициализация из файла изображения 
        Bitmap Clone(int h, int w, Bitmap img); // клонирование изображения
        List<MetricaContour> GetTrajectory(List<Metrica> ClusterKeyPoint, Bitmap img);// получить лист контур+ключи
    }
    class ImageProccesingServise : IImageProccesingServise
    {
        /// <summary>
        ///  Детектор Канни
        /// </summary>
        /// <param name="tresch">Граница левая</param>
        /// <param name="tresch2">Граница правая</param>
        /// <param name="rH">Высота</param>
        /// <param name="rW">Ширина</param>
        /// <param name="orign">Изображение</param>
        /// <returns>Бинарное изображение</returns>
        public Image<Gray, byte> CannyFilter(int tresch, int tresch2, int rH, int rW, Image<Bgr, byte> orign)
        {
            Image<Bgr, byte> newinput = orign.Resize(rW, rH, Inter.Linear);
            Image<Gray, byte> _imgCanny = new Image<Gray, byte>(newinput.Width, newinput.Width, new Gray(0)); // создать новый обьект изображения Canny
            _imgCanny = newinput.Canny(tresch, tresch2); // вызов Canny из библиотеки
            newinput = newinput.Rotate(180, new Bgr(255, 255, 255), false);
            newinput = newinput.Flip(FlipType.Horizontal);
            _imgCanny = newinput.Canny(tresch, tresch2); // вызов Canny из библиотеки
            return _imgCanny;
        }

        public Bitmap Clone(int h, int w, Bitmap img)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, w, h);
            Bitmap CopyImg = img.Clone(sourceRectangle, PixelFormat.DontCare);
            CopyImg.RotateFlip(RotateFlipType.Rotate180FlipX);
            return CopyImg;
        }

        public void FoudForDraw(Metrica data, ref Bitmap demo, ref Color color, ref int count2)
        {
            Random rand = new Random();
            color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            SolidBrush brush = new SolidBrush(color);
            Pen pen = new Pen(brush);
            int n = rand.Next(0, data.KeysPoint.Count);
            System.Drawing.Point cur = new System.Drawing.Point();
            cur.X = data.KeysPoint[n].X;
            cur.Y = data.KeysPoint[n].Y;
            var g = Graphics.FromImage(demo);
            g.FillRectangle(brush, cur.X, cur.Y, 12, 12);
            string drawString = count2.ToString();
            Font drawFont = new Font("Arial", 12, style: FontStyle.Bold);

            SolidBrush drawBrush = new SolidBrush(Color.Red);
            StringFormat drawFormat = new StringFormat();

            g.DrawString(drawString, drawFont, drawBrush, cur.X, cur.Y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            g.Dispose();
            count2++;

        }

        /// <summary>
        ///   Установка Bitmap изображения
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Bitmap GetInputImg(string file)
        {
            var inputImg = new Bitmap(file);
            return inputImg;
        }

        /// <summary>
        ///  Поиск ключевых точек
        /// </summary>
        /// <param name="sigma">Сигма</param>
        /// <param name="k">k</param>
        /// <param name="threshold">Граница</param>
        /// <param name="img">Изображение</param>
        /// <returns>Лист ключевых точек</returns>
        public List<IntPoint> KeyPoint(double sigma, float k, float threshold, Bitmap img)
        {
            HarrisCornersDetector harris = new HarrisCornersDetector(k)
            {
                Threshold = threshold,
                Sigma = sigma
            };
            var OriginCorner = harris.ProcessImage(img);
            return OriginCorner;
        }

        /// <summary>
        /// Поиск клювых точек и получение изображения с маркерами
        /// </summary>
        /// <param name="sigma">Сигма</param>
        /// <param name="k">k</param>
        /// <param name="threshold">Граница</param>
        /// <param name="img">Изображение</param>
        /// <param name="Height">Высота</param>
        /// <param name="Width">Ширина</param>
        /// <returns>Изображение с маркерами</returns>
        public Bitmap KeyPoint(double sigma, float k, float threshold, Bitmap img, int Height, int Width)
        {
            HarrisCornersDetector harris = new HarrisCornersDetector(k)
            {
                Threshold = threshold,
                Sigma = sigma
            };
            CornersMarker corners = new CornersMarker(harris, Color.DeepSkyBlue);
            var CornerRrev = corners.Apply(img);
            var DemoSize = ResizeImg(Height, Width);
            var DemoImg = new Bitmap(CornerRrev, DemoSize.Item2, DemoSize.Item1);
            return DemoImg;
        }

        /// <summary>
        /// Масшабирование
        /// </summary>
        /// <param name="nWidth">Новая ширина</param>
        /// <param name="nHeight">Новая высота</param>
        /// <returns></returns>
        public Tuple<int, int> ResizeImg(int nWidth, int nHeight)
        {
            System.Drawing.Image result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                g.Dispose();
            }
            return Tuple.Create(result.Height, result.Width);
        }

        /// <summary>
        /// Генерирует лист с траекториями и соответствующими контрольными точками
        /// </summary>
        /// <param name="img">Изображение</param>
        /// <param name="Key">Лист ключ-точек</param>
        /// <returns></returns>
        public List<MetricaContour> СontourInСluster(Bitmap img, List<System.Drawing.Point> Key)
        {
            var res = GetAllPoints(img, Key);
            return res;
        }

        private List<MetricaContour> GetAllPoints(Bitmap img, List<System.Drawing.Point> Key)
        {
            List<MetricaContour> TotalResult = new List<MetricaContour>();
            Image<Bgr, byte> imgIn = new Image<Bgr, byte>(img);
            imgIn = imgIn.Rotate(180, new Bgr(255, 255, 255), false);
            imgIn = imgIn.Flip(FlipType.Horizontal);

            // создать новый обьект изображения Canny
            Image<Gray, byte> _imgCanny = new Image<Gray, byte>(img.Width, img.Width, new Gray(0));
            // вызов Canny из библиотеки
            _imgCanny = imgIn.Canny(100, 150);
            // выделение массива для хранения контуров
            Mat hierarchy = new Mat();
            // количество ключ-точек для контура                                                                            
            List<int> countDot = new List<int>();
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(_imgCanny, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxNone);//поиск контуров 
                {
                    for (int i = 0; i < contours.Size; i++)
                    {
                        // ищем i-тый контур в коллекции всех контуров 
                        using (VectorOfPoint contour = contours[i])
                        {
                            var ContrList = new List<System.Drawing.Point>();
                            ContrList.AddRange(contour.ToArray());
                            ClusterServise Clproc = new ClusterServise();
                            var NewKey = Clproc.Comparison(Key, ContrList);// поиск ключевых в контуре 
                            if (NewKey.Count != 0)
                            {
                                MetricaContour el = new MetricaContour
                                {
                                    ContourPoint = ContrList,
                                    KeysPoint = NewKey.OrderBy(p => p.X).ToList()
                                };

                                TotalResult.Add(el);
                                if (!countDot.Contains(NewKey.Count)) { countDot.Add(NewKey.Count); }
                            }
                            else continue;
                        }
                    }
                }
                //сортировка для контуров с одинаковым количеством ключ-точек
                foreach (var index in countDot)
                {
                    var countrepeat = TotalResult.Count(i => i.KeysPoint.Count == index);
                    if (countrepeat >= 2)     // 2
                    {
                        var min = (from x in TotalResult where x.KeysPoint.Count == index select x).Min();
                        var indexmin = TotalResult.IndexOf(min);
                        TotalResult.RemoveAt(indexmin);
                    }
                    else { continue; }
                }

                if (countDot.Sum() == Key.Count)  
                {
                    return TotalResult;
                }
                else  // если фигура не имеет внутренних контуров
                {
                    //сортировка для нахождения максимального
                    List<MetricaContour> TotalResult_Max = new List<MetricaContour>();
                    foreach (var index in countDot)
                    {
                        var max = (from x in TotalResult where x.KeysPoint.Count >= index select x).Max();
                        var indexmin = TotalResult.IndexOf(max);
                        if (TotalResult_Max.Contains(max)) { continue; } else { TotalResult_Max.Add(max); }
                    }
                    return TotalResult_Max;
                }

            }
        }

        public List<MetricaContour> GetTrajectory(List<Metrica> ClusterKeyPoint, Bitmap img)
        {
            ImageProccesingServise IVs = new ImageProccesingServise();
            var Trajectory = new List<MetricaContour>();
            foreach (var clust in ClusterKeyPoint)
            {
                var temp = IVs.СontourInСluster(img, clust.KeysPoint);
                Trajectory.AddRange(temp);
            }
            return Trajectory;
        }
    }
}


