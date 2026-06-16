using AccAutoKey.Models;
using AccAutoKey.Share;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AccAutoKey.Page
{
    public partial class JobPage : UserControl
    {
        private readonly string configPath;
        private List<FileConfig> items;
        private readonly XmlSerializer xs = new XmlSerializer(typeof(List<FileConfig>));
        private readonly JobService jobService = new JobService();

        public JobPage(int pageNumber)
        {
            InitializeComponent();
            configPath = Path.Combine(Application.StartupPath, "config", $"page{pageNumber}.xml");
        }

        private void JobPage_Load(object sender, System.EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            controlPanel.Controls.Clear();

            if (!File.Exists(configPath))
            {
                MessageBox.Show($"ไม่พบไฟล์ config: {configPath}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var fs = new FileStream(configPath, FileMode.Open, FileAccess.Read))
                items = (List<FileConfig>)xs.Deserialize(fs);

            int row = 0;
            foreach (var item in items)
            {
                if (!item.Show) continue;

                var btn = new Button
                {
                    Location = new Point(30, 7 + (row * 45)),
                    Text = item.Name,
                    Font = new Font("Angsana New", 14, FontStyle.Regular),
                    Size = new Size(210, 39),
                    BackColor = Color.Gainsboro
                };

                var startBtn = new Button
                {
                    Location = new Point(245, 7 + (row * 45)),
                    Text = "เริ่ม",
                    Font = new Font("Arial Rounded MT", 12, FontStyle.Bold),
                    Size = new Size(78, 39),
                    Enabled = false,
                    BackColor = Color.Gainsboro
                };

                var captured = item;
                btn.Click += (s, ev) =>
                {
                    ClearButtons();
                    jobService.setSelectJob(btn, startBtn);
                };
                startBtn.Click += (s, ev) =>
                {
                    var process = jobService.startProcess(captured);
                    if (process != null)
                        Application.Exit();
                };

                controlPanel.Controls.Add(btn);
                controlPanel.Controls.Add(startBtn);
                row++;
            }
        }

        private void ClearButtons()
        {
            foreach (Control c in controlPanel.Controls)
            {
                if (c is Button b)
                {
                    b.BackColor = Color.Gainsboro;
                    if (b.Text == "เริ่ม") b.Enabled = false;
                }
            }
        }
    }
}
