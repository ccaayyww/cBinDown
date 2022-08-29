namespace cBinDown
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClosePort = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusTick = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarInfo = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonOpenPort = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClosePort);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.buttonOpenPort);
            this.panel1.Controls.Add(this.buttonDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxPath);
            this.panel1.Controls.Add(this.buttonSelectFile);
            this.panel1.Controls.Add(this.comboBoxPort);
            this.panel1.Controls.Add(this.textBoxInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 326);
            this.panel1.TabIndex = 0;
            // 
            // buttonClosePort
            // 
            this.buttonClosePort.Location = new System.Drawing.Point(391, 13);
            this.buttonClosePort.Name = "buttonClosePort";
            this.buttonClosePort.Size = new System.Drawing.Size(87, 23);
            this.buttonClosePort.TabIndex = 11;
            this.buttonClosePort.Text = "关闭串口";
            this.buttonClosePort.UseVisualStyleBackColor = true;
            this.buttonClosePort.Click += new System.EventHandler(this.buttonClosePort_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(107, 80);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(86, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "关闭软件";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusTick,
            this.toolStripProgressBarInfo,
            this.toolStripStatusTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 304);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(501, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusTick
            // 
            this.toolStripStatusTick.AutoSize = false;
            this.toolStripStatusTick.Name = "toolStripStatusTick";
            this.toolStripStatusTick.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusTick.Text = "0";
            this.toolStripStatusTick.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripProgressBarInfo
            // 
            this.toolStripProgressBarInfo.Name = "toolStripProgressBarInfo";
            this.toolStripProgressBarInfo.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripStatusTime
            // 
            this.toolStripStatusTime.Name = "toolStripStatusTime";
            this.toolStripStatusTime.Size = new System.Drawing.Size(15, 17);
            this.toolStripStatusTime.Text = "0";
            // 
            // buttonOpenPort
            // 
            this.buttonOpenPort.Location = new System.Drawing.Point(287, 12);
            this.buttonOpenPort.Name = "buttonOpenPort";
            this.buttonOpenPort.Size = new System.Drawing.Size(87, 23);
            this.buttonOpenPort.TabIndex = 7;
            this.buttonOpenPort.Text = "打开串口";
            this.buttonOpenPort.UseVisualStyleBackColor = true;
            this.buttonOpenPort.Click += new System.EventHandler(this.buttonOpenPort_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(390, 80);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(87, 23);
            this.buttonDown.TabIndex = 6;
            this.buttonDown.Text = "下载";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "文件路径";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "串口号";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(107, 46);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(267, 26);
            this.textBoxPath.TabIndex = 3;
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Location = new System.Drawing.Point(391, 48);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(87, 23);
            this.buttonSelectFile.TabIndex = 2;
            this.buttonSelectFile.Text = "选择文件";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(107, 15);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(117, 24);
            this.comboBoxPort.TabIndex = 1;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(32, 109);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxInfo.Size = new System.Drawing.Size(445, 177);
            this.textBoxInfo.TabIndex = 0;
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(501, 326);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Text = "固件下载";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.Button buttonOpenPort;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTick;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTime;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonClosePort;
    }
}

