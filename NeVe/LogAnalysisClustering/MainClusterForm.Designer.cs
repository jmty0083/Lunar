namespace LogAnalysisClustering
{
    partial class MainClusterForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.TimeUsageLabel = new System.Windows.Forms.Label();
            this.TimeEstimationLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PercentageLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.EmbeddingButton = new System.Windows.Forms.Button();
            this.KDisCalcButton = new System.Windows.Forms.Button();
            this.DBScanButton = new System.Windows.Forms.Button();
            this.Ip2VecEmbeddingButton = new System.Windows.Forms.RadioButton();
            this.EmbeddingBox = new System.Windows.Forms.GroupBox();
            this.NeVeEmbeddingButton = new System.Windows.Forms.RadioButton();
            this.MeasureButton = new System.Windows.Forms.Button();
            this.UniversalProcessButton = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.GroupBox();
            this.WorkerLabel = new System.Windows.Forms.Label();
            this.String = new System.Windows.Forms.RadioButton();
            this.RepetitionNumericCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tSNEButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.EmbeddingBox.SuspendLayout();
            this.StatusBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepetitionNumericCount)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 73);
            this.ProgressBar.Maximum = 10000;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(709, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(10, 54);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(41, 12);
            this.ProgressLabel.TabIndex = 1;
            this.ProgressLabel.Text = "label1";
            // 
            // TimeUsageLabel
            // 
            this.TimeUsageLabel.AutoSize = true;
            this.TimeUsageLabel.Location = new System.Drawing.Point(10, 103);
            this.TimeUsageLabel.Name = "TimeUsageLabel";
            this.TimeUsageLabel.Size = new System.Drawing.Size(41, 12);
            this.TimeUsageLabel.TabIndex = 2;
            this.TimeUsageLabel.Text = "label1";
            // 
            // TimeEstimationLabel
            // 
            this.TimeEstimationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeEstimationLabel.Location = new System.Drawing.Point(478, 103);
            this.TimeEstimationLabel.Name = "TimeEstimationLabel";
            this.TimeEstimationLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TimeEstimationLabel.Size = new System.Drawing.Size(243, 12);
            this.TimeEstimationLabel.TabIndex = 3;
            this.TimeEstimationLabel.Text = "label1fdsafds";
            this.TimeEstimationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(10, 30);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(41, 12);
            this.StatusLabel.TabIndex = 4;
            this.StatusLabel.Text = "label1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据ToolStripMenuItem
            // 
            this.数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFileToolStripMenuItem,
            this.openDirectoryToolStripMenuItem});
            this.数据ToolStripMenuItem.Name = "数据ToolStripMenuItem";
            this.数据ToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.数据ToolStripMenuItem.Text = "Data";
            // 
            // AddFileToolStripMenuItem
            // 
            this.AddFileToolStripMenuItem.Name = "AddFileToolStripMenuItem";
            this.AddFileToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.AddFileToolStripMenuItem.Text = "Open File";
            this.AddFileToolStripMenuItem.Click += new System.EventHandler(this.AddFileToolStripMenuItem_Click);
            // 
            // openDirectoryToolStripMenuItem
            // 
            this.openDirectoryToolStripMenuItem.Name = "openDirectoryToolStripMenuItem";
            this.openDirectoryToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.openDirectoryToolStripMenuItem.Text = "Open Directory";
            this.openDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryToolStripMenuItem_Click);
            // 
            // PercentageLabel
            // 
            this.PercentageLabel.Location = new System.Drawing.Point(510, 54);
            this.PercentageLabel.Name = "PercentageLabel";
            this.PercentageLabel.Size = new System.Drawing.Size(211, 12);
            this.PercentageLabel.TabIndex = 8;
            this.PercentageLabel.Text = "label1";
            this.PercentageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(277, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // EmbeddingButton
            // 
            this.EmbeddingButton.Location = new System.Drawing.Point(115, 142);
            this.EmbeddingButton.Name = "EmbeddingButton";
            this.EmbeddingButton.Size = new System.Drawing.Size(75, 23);
            this.EmbeddingButton.TabIndex = 10;
            this.EmbeddingButton.Text = "Vectorize";
            this.EmbeddingButton.UseVisualStyleBackColor = true;
            this.EmbeddingButton.Click += new System.EventHandler(this.EmbeddingButton_Click);
            // 
            // KDisCalcButton
            // 
            this.KDisCalcButton.Location = new System.Drawing.Point(115, 171);
            this.KDisCalcButton.Name = "KDisCalcButton";
            this.KDisCalcButton.Size = new System.Drawing.Size(75, 23);
            this.KDisCalcButton.TabIndex = 11;
            this.KDisCalcButton.Text = "KDisCalc";
            this.KDisCalcButton.UseVisualStyleBackColor = true;
            this.KDisCalcButton.Click += new System.EventHandler(this.KDisCalcButton_Click);
            // 
            // DBScanButton
            // 
            this.DBScanButton.Location = new System.Drawing.Point(115, 200);
            this.DBScanButton.Name = "DBScanButton";
            this.DBScanButton.Size = new System.Drawing.Size(75, 23);
            this.DBScanButton.TabIndex = 12;
            this.DBScanButton.Text = "DBScan";
            this.DBScanButton.UseVisualStyleBackColor = true;
            this.DBScanButton.Click += new System.EventHandler(this.DBScanButton_Click);
            // 
            // Ip2VecEmbeddingButton
            // 
            this.Ip2VecEmbeddingButton.Location = new System.Drawing.Point(6, 20);
            this.Ip2VecEmbeddingButton.Name = "Ip2VecEmbeddingButton";
            this.Ip2VecEmbeddingButton.Size = new System.Drawing.Size(59, 16);
            this.Ip2VecEmbeddingButton.TabIndex = 13;
            this.Ip2VecEmbeddingButton.Text = "Ip2Vec";
            this.Ip2VecEmbeddingButton.UseVisualStyleBackColor = true;
            // 
            // EmbeddingBox
            // 
            this.EmbeddingBox.Controls.Add(this.String);
            this.EmbeddingBox.Controls.Add(this.NeVeEmbeddingButton);
            this.EmbeddingBox.Controls.Add(this.Ip2VecEmbeddingButton);
            this.EmbeddingBox.Location = new System.Drawing.Point(15, 135);
            this.EmbeddingBox.Name = "EmbeddingBox";
            this.EmbeddingBox.Size = new System.Drawing.Size(82, 88);
            this.EmbeddingBox.TabIndex = 14;
            this.EmbeddingBox.TabStop = false;
            this.EmbeddingBox.Text = "Embeddings";
            // 
            // NeVeEmbeddingButton
            // 
            this.NeVeEmbeddingButton.AutoSize = true;
            this.NeVeEmbeddingButton.Checked = true;
            this.NeVeEmbeddingButton.Location = new System.Drawing.Point(6, 42);
            this.NeVeEmbeddingButton.Name = "NeVeEmbeddingButton";
            this.NeVeEmbeddingButton.Size = new System.Drawing.Size(47, 16);
            this.NeVeEmbeddingButton.TabIndex = 14;
            this.NeVeEmbeddingButton.Text = "NeVe";
            this.NeVeEmbeddingButton.UseVisualStyleBackColor = true;
            // 
            // MeasureButton
            // 
            this.MeasureButton.Location = new System.Drawing.Point(196, 142);
            this.MeasureButton.Name = "MeasureButton";
            this.MeasureButton.Size = new System.Drawing.Size(75, 23);
            this.MeasureButton.TabIndex = 15;
            this.MeasureButton.Text = "Measure";
            this.MeasureButton.UseVisualStyleBackColor = true;
            this.MeasureButton.Click += new System.EventHandler(this.MeasureButton_Click);
            // 
            // UniversalProcessButton
            // 
            this.UniversalProcessButton.Location = new System.Drawing.Point(277, 200);
            this.UniversalProcessButton.Name = "UniversalProcessButton";
            this.UniversalProcessButton.Size = new System.Drawing.Size(75, 23);
            this.UniversalProcessButton.TabIndex = 16;
            this.UniversalProcessButton.Text = "EasyAccess";
            this.UniversalProcessButton.UseVisualStyleBackColor = true;
            this.UniversalProcessButton.Click += new System.EventHandler(this.UniversalProcessButton_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.Controls.Add(this.WorkerLabel);
            this.StatusBox.Location = new System.Drawing.Point(576, 135);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(145, 38);
            this.StatusBox.TabIndex = 17;
            this.StatusBox.TabStop = false;
            this.StatusBox.Text = "STATUS";
            // 
            // WorkerLabel
            // 
            this.WorkerLabel.Location = new System.Drawing.Point(6, 15);
            this.WorkerLabel.Name = "WorkerLabel";
            this.WorkerLabel.Size = new System.Drawing.Size(134, 16);
            this.WorkerLabel.TabIndex = 0;
            this.WorkerLabel.Text = "Idle";
            this.WorkerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // String
            // 
            this.String.AutoSize = true;
            this.String.Location = new System.Drawing.Point(6, 64);
            this.String.Name = "String";
            this.String.Size = new System.Drawing.Size(59, 16);
            this.String.TabIndex = 15;
            this.String.Text = "String";
            this.String.UseVisualStyleBackColor = true;
            // 
            // RepetitionNumericCount
            // 
            this.RepetitionNumericCount.Location = new System.Drawing.Point(233, 202);
            this.RepetitionNumericCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RepetitionNumericCount.Name = "RepetitionNumericCount";
            this.RepetitionNumericCount.Size = new System.Drawing.Size(38, 21);
            this.RepetitionNumericCount.TabIndex = 18;
            this.RepetitionNumericCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "次数:";
            // 
            // tSNEButton
            // 
            this.tSNEButton.Location = new System.Drawing.Point(196, 171);
            this.tSNEButton.Name = "tSNEButton";
            this.tSNEButton.Size = new System.Drawing.Size(75, 23);
            this.tSNEButton.TabIndex = 20;
            this.tSNEButton.Text = "SaveTSNE";
            this.tSNEButton.UseVisualStyleBackColor = true;
            this.tSNEButton.Click += new System.EventHandler(this.tSNEButton_Click);
            // 
            // MainClusterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 239);
            this.Controls.Add(this.tSNEButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RepetitionNumericCount);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.UniversalProcessButton);
            this.Controls.Add(this.MeasureButton);
            this.Controls.Add(this.EmbeddingBox);
            this.Controls.Add(this.DBScanButton);
            this.Controls.Add(this.KDisCalcButton);
            this.Controls.Add(this.EmbeddingButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PercentageLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.TimeEstimationLabel);
            this.Controls.Add(this.TimeUsageLabel);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainClusterForm";
            this.Text = "Main Controller";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.EmbeddingBox.ResumeLayout(false);
            this.EmbeddingBox.PerformLayout();
            this.StatusBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RepetitionNumericCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.Label TimeUsageLabel;
        private System.Windows.Forms.Label TimeEstimationLabel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryToolStripMenuItem;
        private System.Windows.Forms.Label PercentageLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button EmbeddingButton;
        private System.Windows.Forms.Button KDisCalcButton;
        private System.Windows.Forms.Button DBScanButton;
        private System.Windows.Forms.RadioButton Ip2VecEmbeddingButton;
        private System.Windows.Forms.GroupBox EmbeddingBox;
        private System.Windows.Forms.RadioButton NeVeEmbeddingButton;
        private System.Windows.Forms.Button MeasureButton;
        private System.Windows.Forms.Button UniversalProcessButton;
        private System.Windows.Forms.GroupBox StatusBox;
        private System.Windows.Forms.Label WorkerLabel;
        private System.Windows.Forms.RadioButton String;
        private System.Windows.Forms.NumericUpDown RepetitionNumericCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button tSNEButton;
    }
}

