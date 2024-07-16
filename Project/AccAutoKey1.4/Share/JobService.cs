using AccAutoKey4.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace AccAutoKey4.Share
{
    public class JobService
    {
        private string resourcePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\resources";

        public JobService() { }
        public void startProcess(FileConfig config)
        {
            if (config != null)
            {
                try
                {
                    Process.Start(resourcePath + "\\" + config.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "\n" + "File name : " + config.FileName, "Error !");
                }
            }
        }

        public void setSelectJob(Button btnJob, Button jobBtn)
        {
            btnJob.BackColor = Color.SkyBlue;
            jobBtn.Enabled = true;
            jobBtn.BackColor = System.Drawing.Color.Lime;

        }
        public void setDisableBtn(Button btnStart)
        {
            if (btnStart != null)
            {
                btnStart.Enabled = false;
                btnStart.BackColor = System.Drawing.Color.Silver;
            }
        }
    }
}
