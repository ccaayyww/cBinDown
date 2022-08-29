using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cBinDown
{
    public partial class FormMain : Form
    {
        private string m_strOp = "";
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonOpenPort_Click(object sender, EventArgs e)
        {
            string s = comboBoxPort.Text;
            if (string.IsNullOrWhiteSpace(s)) return;
            HelperPort.PortOpen(s);
            comboBoxPort.Enabled=!HelperPort.PortIsOpen();
        }
        private void buttonClosePort_Click(object sender, EventArgs e)
        {
            HelperPort.PortClose();
            comboBoxPort.Enabled = !HelperPort.PortIsOpen();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                FunPort();
                HelperPort.Start();
                timerMain.Start();
            }
            catch { }
        }
        


        private void FunPort()
        {
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey hs = hklm.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM");
            if (hs==null|| hs.ValueCount < 1) return;
            string[] values = new string[hs.ValueCount];
            for (int i = 0; i < hs.ValueCount; i++)
            {
                values[i] = hs.GetValue(hs.GetValueNames()[i]).ToString();
            }
            comboBoxPort.Items.Clear();
            foreach(string s in values)
            {
                comboBoxPort.Items.Add(s);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                HelperPort.Stop();
            }
            catch { }
            
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            try
            {
                CommParam.timerTick++;
                toolStripStatusTick.Text = HelperPort.GetProcessCurrentLen();
                toolStripStatusTime.Text = DateTime.Now.ToString();
                toolStripProgressBarInfo.Value = (int)HelperPort.GetProcessInfo();
                
                if (m_strOp!=HelperPort.GetOpStep())
                {
                    m_strOp = HelperPort.GetOpStep();
                    if(!string.IsNullOrWhiteSpace(m_strOp))
                        textBoxInfo.Text += DateTime.Now.ToString("[hh:mm:ss]")+ m_strOp + "\r\n";
                }
            }
            catch { }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if(!HelperPort.PortIsOpen())
            {
                textBoxInfo.Text = "请选择选择串口并打开";
                return;
            }
            string strBinPath = textBoxPath.Text;
            if(string.IsNullOrEmpty(strBinPath))
            {
                textBoxInfo.Text = "请选择下载文件";
                return;
            }
            textBoxInfo.Text = "";
            Task.Run(()=> HelperPort.PortInitBL(strBinPath));
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".bin"; // Default file extension
            dlg.Filter = "firmware file (.bin)|*.bin"; // Filter files by extension

            DialogResult result= dlg.ShowDialog();
            if(result==DialogResult.OK)
            {
                textBoxPath.Text = dlg.FileName;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
