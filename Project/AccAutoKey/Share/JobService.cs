using AccAutoKey.Models;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AccAutoKey.Share
{
    public class JobService
    {
        public JobService() { }

        private string GetResourcePath()
        {
            string configured = ConfigurationManager.AppSettings["resource_path"];
            if (string.IsNullOrEmpty(configured))
                return Path.Combine(Application.StartupPath, "resources");
            return Path.IsPathRooted(configured)
                ? configured
                : Path.Combine(Application.StartupPath, configured);
        }

        public Process startProcess(FileConfig config)
        {
            if (config == null) return null;

            string path = Path.IsPathRooted(config.FileName)
                ? config.FileName
                : Path.Combine(GetResourcePath(), config.FileName);

            if (!File.Exists(path))
            {
                MessageBox.Show("ไม่พบไฟล์ กรุณาตรวจสอบการเชื่อมต่ออินเตอร์เน็ตหรือ Google Drive\nตำแหน่งไฟล์ : " + path, "ข้อผิดพลาด");
                return null;
            }

            try
            {
                return Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nตำแหน่งไฟล์ : " + path, "ข้อผิดพลาด");
                return null;
            }
        }

        public void setSelectJob(Button btnJob, Button jobBtn)
        {
            btnJob.BackColor = Color.SkyBlue;
            jobBtn.Enabled = true;
            jobBtn.BackColor = Color.Lime;
        }

        public void setDisableBtn(Button btnStart)
        {
            if (btnStart != null)
            {
                btnStart.Enabled = false;
                btnStart.BackColor = Color.Silver;
            }
        }
    }
}
