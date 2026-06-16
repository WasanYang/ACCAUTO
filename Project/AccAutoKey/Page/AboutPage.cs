using AccAutoKey.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AccAutoKey.Page
{
    public partial class AboutPage : UserControl
    {
        XmlSerializer xs;
        List<AboutModel> ls;
        private string configPath = Path.Combine(Application.StartupPath, "config", "about.xml");
        public AboutPage()
        {
            InitializeComponent();
            ls = new List<AboutModel>();
            xs = new XmlSerializer(typeof(List<AboutModel>));
        }

        private void RemarkPage_Load(object sender, EventArgs e)
        {
            this.LoadPage();
        }

        private void LoadPage()
        {
            if (!File.Exists(configPath))
            {
                MessageBox.Show($"ไม่พบไฟล์ config: {configPath}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var fs = new FileStream(configPath, FileMode.Open, FileAccess.Read))
                ls = (List<AboutModel>)xs.Deserialize(fs);

            var selected = ls[0];
            Address.Text = selected.Address;
            description.Text = selected.Description;
            startDate.Text = selected.StartDate;
            endDate.Text = selected.EndDate;
            location.Text = selected.Location;
            record.Text = selected.Record;
            snBox.Text = selected.SN;

            AddResourcePathRow();
        }

        private void AddResourcePathRow()
        {
            var panel = new Panel { Size = new System.Drawing.Size(514, 39) };

            var label = new Label
            {
                AutoSize = true,
                Font = new Font("Angsana New", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 222),
                Location = new System.Drawing.Point(6, 6),
                Text = "โฟลเดอร์โปรแกรม"
            };

            var textBox = new TextBox
            {
                Enabled = false,
                Font = new Font("Angsana New", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 222),
                Location = new System.Drawing.Point(141, 3),
                Size = new System.Drawing.Size(370, 33),
                Text = ConfigurationManager.AppSettings["resource_path"] ?? "resources"
            };

            panel.Controls.Add(label);
            panel.Controls.Add(textBox);

            int logoIndex = flowLayoutPanel1.Controls.IndexOf(logo);
            flowLayoutPanel1.Controls.Add(panel);
            flowLayoutPanel1.Controls.SetChildIndex(panel, logoIndex);
        }
    }
}
