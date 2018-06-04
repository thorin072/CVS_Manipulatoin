using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MinipulVS
{
   
    public interface IFormView
    {
        void ViewImgInput(Bitmap img);// выгрузизка изображения в picturebox
        void ChangeStatus(string status);// изменение статус-сообщения
        void Visual(Metrica data, ref int count);
        void AddtoList(List<Metrica> Cluster);
        void VisualGraph(ApproxRobot data, ref int count);
        void VisualGraphKey(Metrica data, ref int count);
        void VisualGraphLinear(ApproxRobot data, ref int count);
    }
    public partial class Main : Form ,IFormView
    {
        private readonly Controller _controller;

        public Main()
        {
            InitializeComponent();
            butOpenImg.Click += ButOpenImg_Click;
            butClustering.Click += ButClustering_Click;
            butFoundKey.Click += ButFoundKey_Click;
            butGetGraph.Click += ButGetGraph_Click;
            butPaint.Click += ButPaint_Click;
            butRobotFile.Click += ButRobotFile_Click;
            checknoapprox.Checked = true;
            parSigma.Value = 1;
            checkwithapprox.Click += Checkwithapprox_Click;
            checknoapprox.Click += Checknoapprox_Click;

            //backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            //backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            //backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;

           _controller = new Controller(this);
        }

        //private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    ProgressBar.Value = e.ProgressPercentage;
        //}

        //private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    for (int i = 0; i <= 100; i++)
        //    {
        //       backgroundWorker1.ReportProgress(i);
        //    }

        //}

        //public Main(Controller controller)
        //{
        //    InitializeComponent();
        //    butOpenImg.Click += ButOpenImg_Click;
        //    butClustering.Click += ButClustering_Click;
        //    butFoundKey.Click += ButFoundKey_Click;
        //    butGetGraph.Click += ButGetGraph_Click;
        //    butPaint.Click += ButPaint_Click;
        //    butRobotFile.Click += ButRobotFile_Click;
        //    _controller = controller;
        //}

        #region Проброс кнопок
        private void Checknoapprox_Click(object sender, EventArgs e) => ChangeStatus("Изменен способ отображения: без аппроксимации. Обновите график");
        private void Checkwithapprox_Click(object sender, EventArgs e) => ChangeStatus("Изменен способ отображения: с аппроксимацией. Обновите график");
        private void ButRobotFile_Click(object sender, EventArgs e) => _controller.GetFile();

        private void ButPaint_Click(object sender, EventArgs e)
        {
            approx.Series.Clear();
            if (checkwithapprox.Checked)
            {
                _controller.PrintGraphApprox();
            }
            else
            {
              _controller.PrintGraph();
            }
            
        }
        private void ButGetGraph_Click(object sender, EventArgs e) => _controller.GetApproxTrack();
        private void ButFoundKey_Click(object sender, EventArgs e)
        {
           _controller.GetImageWithKeys((double)parSigma.Value, (float)parsK.Value, (float)parTh.Value,VisualImg.Height, VisualImg.Width);
            _controller.GetKeyToArr((double)parSigma.Value, (float)parsK.Value, (float)parTh.Value, VisualImg.Height, VisualImg.Width);
        }
        private void ButClustering_Click(object sender, EventArgs e)
        {
            clust.Series.Clear();
            _controller.GetClustList((int)countCluster.Value);

        }
        private void ButOpenImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); // диалог открытия изображения
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var path = ofd.FileName;
                _controller.InputImg(path);
            }
        }
        #endregion

        #region IFormView
        /// <summary>
        /// Вывести изображение в picturebox
        /// </summary>
        /// <param name="img"></param>
        public void ViewImgInput(Bitmap img)
        {
            VisualImg.Image = img;
        }
        /// <summary>
        /// Изменить статус актуального события
        /// </summary>
        /// <param name="status">Статус</param>
        public void ChangeStatus(string status) => imgstat.Text = status;
        /// <summary>
        /// Визуализация графика кластеров
        /// </summary>
        /// <param name="data">Кластеры</param>
        /// <param name="count">Количество</param>
        public void Visual(Metrica data, ref int count)
        {
            var x = data.KeysPoint.Select(_ => _.X).ToArray();
            var y = data.KeysPoint.Select(_ => _.Y).ToArray();
            clust.Series.Add("Series" + count.ToString());
            clust.Series["Series" + count.ToString()].ChartArea = "ChartArea1";
            clust.Series["Series" + count.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            clust.Series[count].Points.DataBindXY(x, y);
            count++;
        }
        public void VisualGraphKey(Metrica data, ref int count)
        {
            var x = data.KeysPoint.Select(_ => _.X).ToArray();
            var y = data.KeysPoint.Select(_ => _.Y).ToArray();
            approx.Series.Add("Series" + count.ToString());
            approx.Series["Series" + count.ToString()].ChartArea = "ChartArea1";
            approx.Series["Series" + count.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            approx.Series[count].Points.DataBindXY(x, y);
            count++;
        }
        public void VisualGraph(ApproxRobot data, ref int count)
        {

            List<Point> list = new List<Point>();
            foreach (var el in data.Approxes)
            {
                list.Add(new Point(el.P0_Start.X, el.P0_Start.Y));
                list.Add(new Point(el.P2_End.X, el.P2_End.Y));
            }
            var x = list.Select(_ => _.X).ToArray();
            var y = list.Select(_ => _.Y).ToArray();
            approx.Series.Add("Series" + count.ToString());
            approx.Series["Series" + count.ToString()].ChartArea = "ChartArea1";
            approx.Series["Series" + count.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            approx.Series[count].Points.DataBindXY(x, y);
            count++;
        }

        public void VisualGraphLinear(ApproxRobot data, ref int count)
        {

            List<Point> list = new List<Point>();
            foreach (var el in data.Approxes)
            {
                if (el.TypeLine == Approx.TypeApprox.Bezier_Curve)
                {
                    list.Add(new Point(el.P0_Start.X, el.P0_Start.Y));
                    list.Add(new Point(el.P2_End.X, el.P2_End.Y));
                }
            }
            var x = list.Select(_ => _.X).ToArray();
            var y = list.Select(_ => _.Y).ToArray();
            approx.Series.Add("Series" + count.ToString());
            approx.Series["Series" + count.ToString()].ChartArea = "ChartArea1";
            approx.Series["Series" + count.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            approx.Series[count].Points.DataBindXY(x, y);
            count++;
        }
        public void AddtoList(List<Metrica> Cluster)
        {
            System.Object[] ItemObject = new System.Object[Cluster.Count];
            for (int i = 0; i < Cluster.Count; i++)
            {
                ItemObject[i] = "Кластер " + i;
            }
            //boxCl.Items.AddRange(ItemObject);
        }
        #endregion

    }
}
