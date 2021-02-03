namespace LogAnalysisClustering.DataProcess.Ip2VecClustering
{
    partial class Ip2VecSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ThreatNameTargetSelector = new System.Windows.Forms.Button();
            this.SourceIpTargetSelector = new System.Windows.Forms.Button();
            this.SourcePortTargetSelector = new System.Windows.Forms.Button();
            this.TargetIpTargetSelector = new System.Windows.Forms.Button();
            this.TargetPortTargetSelector = new System.Windows.Forms.Button();
            this.ProtocolTargetSelector = new System.Windows.Forms.Button();
            this.TargetSelectorGroup = new System.Windows.Forms.GroupBox();
            this.ThreatNameContextGroup = new System.Windows.Forms.GroupBox();
            this.SourceIpContextGroup = new System.Windows.Forms.GroupBox();
            this.SourcePortContextGroup = new System.Windows.Forms.GroupBox();
            this.TargetIpContextGroup = new System.Windows.Forms.GroupBox();
            this.TargetPortContextGroup = new System.Windows.Forms.GroupBox();
            this.ProtocolContextGroup = new System.Windows.Forms.GroupBox();
            this.Submit = new System.Windows.Forms.Button();
            this.TargetSelectorGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ThreatNameTargetSelector
            // 
            this.ThreatNameTargetSelector.BackColor = System.Drawing.SystemColors.Control;
            this.ThreatNameTargetSelector.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.ThreatNameTargetSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ThreatNameTargetSelector.Location = new System.Drawing.Point(6, 20);
            this.ThreatNameTargetSelector.Name = "ThreatNameTargetSelector";
            this.ThreatNameTargetSelector.Size = new System.Drawing.Size(75, 29);
            this.ThreatNameTargetSelector.TabIndex = 1;
            this.ThreatNameTargetSelector.Text = "威胁名称";
            this.ThreatNameTargetSelector.UseVisualStyleBackColor = false;
            this.ThreatNameTargetSelector.Click += new System.EventHandler(this.ThreatNameTargetSelector_Click);
            // 
            // SourceIpTargetSelector
            // 
            this.SourceIpTargetSelector.BackColor = System.Drawing.SystemColors.Control;
            this.SourceIpTargetSelector.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.SourceIpTargetSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SourceIpTargetSelector.Location = new System.Drawing.Point(6, 84);
            this.SourceIpTargetSelector.Name = "SourceIpTargetSelector";
            this.SourceIpTargetSelector.Size = new System.Drawing.Size(75, 29);
            this.SourceIpTargetSelector.TabIndex = 2;
            this.SourceIpTargetSelector.Text = "源IP";
            this.SourceIpTargetSelector.UseVisualStyleBackColor = false;
            this.SourceIpTargetSelector.Click += new System.EventHandler(this.SourceIpTargetSelector_Click);
            // 
            // SourcePortTargetSelector
            // 
            this.SourcePortTargetSelector.BackColor = System.Drawing.SystemColors.Control;
            this.SourcePortTargetSelector.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.SourcePortTargetSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SourcePortTargetSelector.Location = new System.Drawing.Point(6, 148);
            this.SourcePortTargetSelector.Name = "SourcePortTargetSelector";
            this.SourcePortTargetSelector.Size = new System.Drawing.Size(75, 29);
            this.SourcePortTargetSelector.TabIndex = 3;
            this.SourcePortTargetSelector.Text = "源端口";
            this.SourcePortTargetSelector.UseVisualStyleBackColor = false;
            this.SourcePortTargetSelector.Click += new System.EventHandler(this.SourcePortTargetSelector_Click);
            // 
            // TargetIpTargetSelector
            // 
            this.TargetIpTargetSelector.BackColor = System.Drawing.SystemColors.Control;
            this.TargetIpTargetSelector.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.TargetIpTargetSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TargetIpTargetSelector.Location = new System.Drawing.Point(6, 212);
            this.TargetIpTargetSelector.Name = "TargetIpTargetSelector";
            this.TargetIpTargetSelector.Size = new System.Drawing.Size(75, 29);
            this.TargetIpTargetSelector.TabIndex = 4;
            this.TargetIpTargetSelector.Text = "目标IP";
            this.TargetIpTargetSelector.UseVisualStyleBackColor = false;
            this.TargetIpTargetSelector.Click += new System.EventHandler(this.TargetIpTargetSelector_Click);
            // 
            // TargetPortTargetSelector
            // 
            this.TargetPortTargetSelector.BackColor = System.Drawing.SystemColors.Control;
            this.TargetPortTargetSelector.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.TargetPortTargetSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TargetPortTargetSelector.Location = new System.Drawing.Point(6, 276);
            this.TargetPortTargetSelector.Name = "TargetPortTargetSelector";
            this.TargetPortTargetSelector.Size = new System.Drawing.Size(75, 29);
            this.TargetPortTargetSelector.TabIndex = 5;
            this.TargetPortTargetSelector.Text = "目标端口";
            this.TargetPortTargetSelector.UseVisualStyleBackColor = false;
            this.TargetPortTargetSelector.Click += new System.EventHandler(this.TargetPortTargetSelector_Click);
            // 
            // ProtocolTargetSelector
            // 
            this.ProtocolTargetSelector.BackColor = System.Drawing.SystemColors.Control;
            this.ProtocolTargetSelector.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.ProtocolTargetSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProtocolTargetSelector.Location = new System.Drawing.Point(6, 340);
            this.ProtocolTargetSelector.Name = "ProtocolTargetSelector";
            this.ProtocolTargetSelector.Size = new System.Drawing.Size(75, 29);
            this.ProtocolTargetSelector.TabIndex = 6;
            this.ProtocolTargetSelector.Text = "协议";
            this.ProtocolTargetSelector.UseVisualStyleBackColor = false;
            this.ProtocolTargetSelector.Click += new System.EventHandler(this.ProtocolTargetSelector_Click);
            // 
            // TargetSelectorGroup
            // 
            this.TargetSelectorGroup.Controls.Add(this.ThreatNameTargetSelector);
            this.TargetSelectorGroup.Controls.Add(this.ProtocolTargetSelector);
            this.TargetSelectorGroup.Controls.Add(this.SourceIpTargetSelector);
            this.TargetSelectorGroup.Controls.Add(this.TargetPortTargetSelector);
            this.TargetSelectorGroup.Controls.Add(this.SourcePortTargetSelector);
            this.TargetSelectorGroup.Controls.Add(this.TargetIpTargetSelector);
            this.TargetSelectorGroup.Location = new System.Drawing.Point(12, 12);
            this.TargetSelectorGroup.Name = "TargetSelectorGroup";
            this.TargetSelectorGroup.Size = new System.Drawing.Size(93, 378);
            this.TargetSelectorGroup.TabIndex = 7;
            this.TargetSelectorGroup.TabStop = false;
            this.TargetSelectorGroup.Text = "目标词选择";
            // 
            // ThreatNameContextGroup
            // 
            this.ThreatNameContextGroup.Location = new System.Drawing.Point(121, 12);
            this.ThreatNameContextGroup.Name = "ThreatNameContextGroup";
            this.ThreatNameContextGroup.Size = new System.Drawing.Size(495, 58);
            this.ThreatNameContextGroup.TabIndex = 13;
            this.ThreatNameContextGroup.TabStop = false;
            this.ThreatNameContextGroup.Text = "上下文";
            // 
            // SourceIpContextGroup
            // 
            this.SourceIpContextGroup.Location = new System.Drawing.Point(121, 76);
            this.SourceIpContextGroup.Name = "SourceIpContextGroup";
            this.SourceIpContextGroup.Size = new System.Drawing.Size(495, 58);
            this.SourceIpContextGroup.TabIndex = 14;
            this.SourceIpContextGroup.TabStop = false;
            this.SourceIpContextGroup.Text = "上下文";
            // 
            // SourcePortContextGroup
            // 
            this.SourcePortContextGroup.Location = new System.Drawing.Point(121, 140);
            this.SourcePortContextGroup.Name = "SourcePortContextGroup";
            this.SourcePortContextGroup.Size = new System.Drawing.Size(495, 58);
            this.SourcePortContextGroup.TabIndex = 15;
            this.SourcePortContextGroup.TabStop = false;
            this.SourcePortContextGroup.Text = "上下文";
            // 
            // TargetIpContextGroup
            // 
            this.TargetIpContextGroup.Location = new System.Drawing.Point(121, 204);
            this.TargetIpContextGroup.Name = "TargetIpContextGroup";
            this.TargetIpContextGroup.Size = new System.Drawing.Size(495, 58);
            this.TargetIpContextGroup.TabIndex = 15;
            this.TargetIpContextGroup.TabStop = false;
            this.TargetIpContextGroup.Text = "上下文";
            // 
            // TargetPortContextGroup
            // 
            this.TargetPortContextGroup.Location = new System.Drawing.Point(121, 268);
            this.TargetPortContextGroup.Name = "TargetPortContextGroup";
            this.TargetPortContextGroup.Size = new System.Drawing.Size(495, 58);
            this.TargetPortContextGroup.TabIndex = 15;
            this.TargetPortContextGroup.TabStop = false;
            this.TargetPortContextGroup.Text = "上下文";
            // 
            // ProtocolContextGroup
            // 
            this.ProtocolContextGroup.Location = new System.Drawing.Point(121, 332);
            this.ProtocolContextGroup.Name = "ProtocolContextGroup";
            this.ProtocolContextGroup.Size = new System.Drawing.Size(495, 58);
            this.ProtocolContextGroup.TabIndex = 16;
            this.ProtocolContextGroup.TabStop = false;
            this.ProtocolContextGroup.Text = "上下文";
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(275, 405);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(75, 23);
            this.Submit.TabIndex = 17;
            this.Submit.Text = "开始";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // Ip2VecSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 440);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.ProtocolContextGroup);
            this.Controls.Add(this.TargetPortContextGroup);
            this.Controls.Add(this.TargetIpContextGroup);
            this.Controls.Add(this.SourcePortContextGroup);
            this.Controls.Add(this.SourceIpContextGroup);
            this.Controls.Add(this.ThreatNameContextGroup);
            this.Controls.Add(this.TargetSelectorGroup);
            this.Name = "Ip2VecSettings";
            this.Text = "Ip2Vec设置";
            this.Load += new System.EventHandler(this.Ip2VecSettings_Load);
            this.TargetSelectorGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ThreatNameTargetSelector;
        private System.Windows.Forms.Button SourceIpTargetSelector;
        private System.Windows.Forms.Button SourcePortTargetSelector;
        private System.Windows.Forms.Button TargetIpTargetSelector;
        private System.Windows.Forms.Button TargetPortTargetSelector;
        private System.Windows.Forms.Button ProtocolTargetSelector;
        private System.Windows.Forms.GroupBox TargetSelectorGroup;
        private System.Windows.Forms.GroupBox ThreatNameContextGroup;
        private System.Windows.Forms.GroupBox SourceIpContextGroup;
        private System.Windows.Forms.GroupBox SourcePortContextGroup;
        private System.Windows.Forms.GroupBox TargetIpContextGroup;
        private System.Windows.Forms.GroupBox TargetPortContextGroup;
        private System.Windows.Forms.GroupBox ProtocolContextGroup;
        private System.Windows.Forms.Button Submit;
    }
}