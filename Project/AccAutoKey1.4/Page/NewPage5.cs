using AccAutoKey4.Models;
using AccAutoKey4.Share;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace AccAutoKey4.Page
{
    public partial class NewPage5 : UserControl
    {
        XmlSerializer xs;
        List<FileConfig> ls;
        private string configPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\config\\page5.xml";
        JobService jobService;
        public NewPage5()
        {
            InitializeComponent();
            ls = new List<FileConfig>();
            xs = new XmlSerializer(typeof(List<FileConfig>));
            jobService = new JobService();
        }

        private void LoadPage()
        {
            controlPanel.Controls.Clear();

            FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.Read);
            ls = (List<FileConfig>)xs.Deserialize(fs);
            fs.Close();

            for (int i = 0; i < ls.Count(); i++)
            {
                Button btn = new Button();
                btn.Location = new Point(30, 7 + (i * 45));
                btn.Text = ls[i].Name;
                btn.Font = new Font("Angsana New", 14, FontStyle.Regular);
                btn.Size = new Size(210, 39);
                btn.BackColor = Color.Gainsboro;

                Button startBtn = new Button();
                startBtn.Location = new Point(245, 7 + (i * 45));
                startBtn.Text = "START";
                startBtn.Font = new Font("Arial Rounded MT", 12, FontStyle.Bold);
                startBtn.Size = new Size(78, 39);
                startBtn.Enabled = false;
                startBtn.BackColor = Color.Gainsboro;
                startBtn.Name = ls[i].Id.ToString();
                btn.Click += (sender1, e1) =>
                {
                    this.clearBtn();
                    jobService.setSelectJob(btn, startBtn);
                };

                startBtn.Click += (_, __) =>
                {
                    jobService.startProcess(ls[Convert.ToInt16(startBtn.Name) - 1]);
                };
                controlPanel.Controls.Add(btn);
                controlPanel.Controls.Add(startBtn);

            }
        }
        private void clearBtn()
        {
            foreach (Control x in controlPanel.Controls)
            {
                if (x is Button)
                {
                    if (x.Text.Equals("START"))
                    {
                        x.BackColor = Color.Gainsboro;
                        x.Enabled = false;
                    }
                    else
                    {
                        x.BackColor = Color.Gainsboro;
                    }
                }
            }
        }
        private void OnLoadPage(object sender, EventArgs e)
        {
            this.LoadPage();
        }
    }
}
