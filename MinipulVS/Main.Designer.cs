namespace MinipulVS
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Setting = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.clust = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Graph = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.approx = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.parTh = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.parsK = new System.Windows.Forms.NumericUpDown();
            this.parSigma = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.butOpenImg = new System.Windows.Forms.ToolStripButton();
            this.butFoundKey = new System.Windows.Forms.ToolStripButton();
            this.butClustering = new System.Windows.Forms.ToolStripButton();
            this.butGetGraph = new System.Windows.Forms.ToolStripButton();
            this.butPaint = new System.Windows.Forms.ToolStripButton();
            this.butRobotFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.imgstat = new System.Windows.Forms.ToolStripLabel();
            this.Кластеры = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.checkwithapprox = new System.Windows.Forms.RadioButton();
            this.checknoapprox = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.countCluster = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.VisualImg = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.Setting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clust)).BeginInit();
            this.Graph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parTh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parsK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parSigma)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.Кластеры.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countCluster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VisualImg)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Setting);
            this.tabControl1.Controls.Add(this.Graph);
            this.tabControl1.Location = new System.Drawing.Point(298, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(442, 463);
            this.tabControl1.TabIndex = 2;
            // 
            // Setting
            // 
            this.Setting.BackColor = System.Drawing.Color.White;
            this.Setting.Controls.Add(this.label5);
            this.Setting.Controls.Add(this.clust);
            this.Setting.Location = new System.Drawing.Point(4, 22);
            this.Setting.Name = "Setting";
            this.Setting.Padding = new System.Windows.Forms.Padding(3);
            this.Setting.Size = new System.Drawing.Size(434, 437);
            this.Setting.TabIndex = 0;
            this.Setting.Text = "Кластеризация ключевых точек";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(229, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Визуализация обнаруженых кластеров:";
            // 
            // clust
            // 
            this.clust.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.clust.BackImageTransparentColor = System.Drawing.Color.LightGray;
            chartArea1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            chartArea1.Name = "ChartArea1";
            this.clust.ChartAreas.Add(chartArea1);
            this.clust.Location = new System.Drawing.Point(-8, 19);
            this.clust.Name = "clust";
            this.clust.Size = new System.Drawing.Size(451, 403);
            this.clust.TabIndex = 5;
            this.clust.Text = "chart1";
            // 
            // Graph
            // 
            this.Graph.Controls.Add(this.label7);
            this.Graph.Controls.Add(this.approx);
            this.Graph.Location = new System.Drawing.Point(4, 22);
            this.Graph.Name = "Graph";
            this.Graph.Padding = new System.Windows.Forms.Padding(3);
            this.Graph.Size = new System.Drawing.Size(434, 437);
            this.Graph.TabIndex = 1;
            this.Graph.Text = "Спланированные траектории";
            this.Graph.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(6, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "Результат обхода графа для каждого обьекта:";
            // 
            // approx
            // 
            chartArea2.AxisX.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisY.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.Name = "ChartArea1";
            this.approx.ChartAreas.Add(chartArea2);
            this.approx.Location = new System.Drawing.Point(-18, 17);
            this.approx.Name = "approx";
            this.approx.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.approx.Size = new System.Drawing.Size(446, 400);
            this.approx.TabIndex = 3;
            this.approx.Text = "chart1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(90, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "sigma = ";
            // 
            // parTh
            // 
            this.parTh.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.parTh.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.parTh.Location = new System.Drawing.Point(211, 26);
            this.parTh.Maximum = new decimal(new int[] {
            25000,
            0,
            0,
            0});
            this.parTh.Name = "parTh";
            this.parTh.Size = new System.Drawing.Size(52, 20);
            this.parTh.TabIndex = 3;
            this.parTh.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(9, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "k =";
            // 
            // parsK
            // 
            this.parsK.DecimalPlaces = 3;
            this.parsK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.parsK.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.parsK.Location = new System.Drawing.Point(34, 26);
            this.parsK.Name = "parsK";
            this.parsK.Size = new System.Drawing.Size(52, 20);
            this.parsK.TabIndex = 1;
            // 
            // parSigma
            // 
            this.parSigma.DecimalPlaces = 2;
            this.parSigma.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.parSigma.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.parSigma.Location = new System.Drawing.Point(130, 26);
            this.parSigma.Name = "parSigma";
            this.parSigma.Size = new System.Drawing.Size(52, 20);
            this.parSigma.TabIndex = 2;
            this.parSigma.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.butOpenImg,
            this.butFoundKey,
            this.butClustering,
            this.butGetGraph,
            this.butPaint,
            this.butRobotFile,
            this.toolStripSeparator1,
            this.imgstat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 481);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(751, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // butOpenImg
            // 
            this.butOpenImg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butOpenImg.Image = global::MinipulVS.Properties.Resources.file;
            this.butOpenImg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butOpenImg.Name = "butOpenImg";
            this.butOpenImg.Size = new System.Drawing.Size(23, 22);
            this.butOpenImg.Text = "Открыть изображение";
            // 
            // butFoundKey
            // 
            this.butFoundKey.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butFoundKey.Image = global::MinipulVS.Properties.Resources.pointkeys;
            this.butFoundKey.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butFoundKey.Name = "butFoundKey";
            this.butFoundKey.Size = new System.Drawing.Size(23, 22);
            this.butFoundKey.Text = "Поиск ключевых точек";
            this.butFoundKey.ToolTipText = "Приминить детектор Харриса";
            // 
            // butClustering
            // 
            this.butClustering.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butClustering.Image = global::MinipulVS.Properties.Resources.clusters;
            this.butClustering.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butClustering.Name = "butClustering";
            this.butClustering.Size = new System.Drawing.Size(23, 22);
            this.butClustering.Text = "Кластеризовать";
            // 
            // butGetGraph
            // 
            this.butGetGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butGetGraph.Image = global::MinipulVS.Properties.Resources.graphimg;
            this.butGetGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butGetGraph.Name = "butGetGraph";
            this.butGetGraph.Size = new System.Drawing.Size(23, 22);
            this.butGetGraph.Text = "toolStripButton4";
            this.butGetGraph.ToolTipText = "Упорядочить точки обьектов";
            // 
            // butPaint
            // 
            this.butPaint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butPaint.Image = global::MinipulVS.Properties.Resources.paint;
            this.butPaint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butPaint.Name = "butPaint";
            this.butPaint.Size = new System.Drawing.Size(23, 22);
            this.butPaint.Text = "toolStripButton1";
            this.butPaint.ToolTipText = "Построить базовое решение";
            // 
            // butRobotFile
            // 
            this.butRobotFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butRobotFile.Image = global::MinipulVS.Properties.Resources.excel;
            this.butRobotFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butRobotFile.Name = "butRobotFile";
            this.butRobotFile.Size = new System.Drawing.Size(23, 22);
            this.butRobotFile.Text = "toolStripButton1";
            this.butRobotFile.ToolTipText = "Генерировать файл для манипулятора";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // imgstat
            // 
            this.imgstat.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgstat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.imgstat.Name = "imgstat";
            this.imgstat.Size = new System.Drawing.Size(183, 22);
            this.imgstat.Text = "Ожидание ввода изображения...";
            // 
            // Кластеры
            // 
            this.Кластеры.Controls.Add(this.tabPage1);
            this.Кластеры.Location = new System.Drawing.Point(12, 295);
            this.Кластеры.Name = "Кластеры";
            this.Кластеры.SelectedIndex = 0;
            this.Кластеры.Size = new System.Drawing.Size(280, 184);
            this.Кластеры.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.checkwithapprox);
            this.tabPage1.Controls.Add(this.checknoapprox);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.countCluster);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.parTh);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.parsK);
            this.tabPage1.Controls.Add(this.parSigma);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(272, 158);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Настройки:";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label13.Location = new System.Drawing.Point(6, 136);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(259, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "---------------------------------------------------------------------------------" +
    "---";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(9, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(136, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Тип графика траектории:";
            this.toolTip1.SetToolTip(this.label12, "Варьируя параметры детектора, кол-во контрольных точек будет изменятся");
            // 
            // checkwithapprox
            // 
            this.checkwithapprox.AutoSize = true;
            this.checkwithapprox.ForeColor = System.Drawing.SystemColors.Desktop;
            this.checkwithapprox.Location = new System.Drawing.Point(12, 121);
            this.checkwithapprox.Name = "checkwithapprox";
            this.checkwithapprox.Size = new System.Drawing.Size(120, 17);
            this.checkwithapprox.TabIndex = 8;
            this.checkwithapprox.TabStop = true;
            this.checkwithapprox.Text = "с аппроксимацией";
            this.checkwithapprox.UseVisualStyleBackColor = true;
            // 
            // checknoapprox
            // 
            this.checknoapprox.AutoSize = true;
            this.checknoapprox.ForeColor = System.Drawing.SystemColors.Desktop;
            this.checknoapprox.Location = new System.Drawing.Point(133, 121);
            this.checknoapprox.Name = "checknoapprox";
            this.checknoapprox.Size = new System.Drawing.Size(126, 17);
            this.checknoapprox.TabIndex = 9;
            this.checknoapprox.TabStop = true;
            this.checknoapprox.Text = "без аппроксимации";
            this.checknoapprox.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label11.Location = new System.Drawing.Point(6, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(259, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "---------------------------------------------------------------------------------" +
    "---";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label10.Location = new System.Drawing.Point(6, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(259, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "---------------------------------------------------------------------------------" +
    "---";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(9, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Кластеризация:";
            this.toolTip1.SetToolTip(this.label9, "Варьируя параметры детектора, кол-во контрольных точек будет изменятся");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(9, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Параметры детектора Харриса:";
            this.toolTip1.SetToolTip(this.label8, "Варьируя параметры детектора, кол-во контрольных точек будет изменятся");
            // 
            // countCluster
            // 
            this.countCluster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.countCluster.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.countCluster.Location = new System.Drawing.Point(111, 72);
            this.countCluster.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.countCluster.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countCluster.Name = "countCluster";
            this.countCluster.Size = new System.Drawing.Size(56, 20);
            this.countCluster.TabIndex = 8;
            this.countCluster.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(171, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(9, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Кол-во кластеров:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(181, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "TH = ";
            // 
            // VisualImg
            // 
            this.VisualImg.BackColor = System.Drawing.Color.LightSteelBlue;
            this.VisualImg.BackgroundImage = global::MinipulVS.Properties.Resources.pic_img;
            this.VisualImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.VisualImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VisualImg.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.VisualImg.Location = new System.Drawing.Point(12, 12);
            this.VisualImg.Name = "VisualImg";
            this.VisualImg.Size = new System.Drawing.Size(280, 280);
            this.VisualImg.TabIndex = 1;
            this.VisualImg.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(751, 506);
            this.Controls.Add(this.Кластеры);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.VisualImg);
            this.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "СТЗ";
            this.tabControl1.ResumeLayout(false);
            this.Setting.ResumeLayout(false);
            this.Setting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clust)).EndInit();
            this.Graph.ResumeLayout(false);
            this.Graph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parTh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parsK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parSigma)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Кластеры.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countCluster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VisualImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox VisualImg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Setting;
        private System.Windows.Forms.TabPage Graph;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton butOpenImg;
        private System.Windows.Forms.ToolStripButton butFoundKey;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel imgstat;
        private System.Windows.Forms.NumericUpDown parTh;
        private System.Windows.Forms.NumericUpDown parSigma;
        private System.Windows.Forms.NumericUpDown parsK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart clust;
        private System.Windows.Forms.TabControl Кластеры;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown countCluster;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripButton butClustering;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton butGetGraph;
        private System.Windows.Forms.ToolStripButton butPaint;
        private System.Windows.Forms.ToolStripButton butRobotFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataVisualization.Charting.Chart approx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton checkwithapprox;
        private System.Windows.Forms.RadioButton checknoapprox;
        private System.Windows.Forms.Label label13;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

