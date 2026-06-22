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
            programNameBox.Text = selected.ProgramName;
            addressBox.Text = selected.Address;
            telBox.Text = selected.Tel;
            contactBox.Text = selected.Contact;
            startDate.Text = selected.StartDate;
            endDate.Text = selected.EndDate;
            location.Text = selected.Location;
            record.Text = selected.Record;
            snBox.Text = selected.SN;

            LoadCopyRightPanel();
            AddResourcePathRow();
        }

        private void LoadCopyRightPanel()
        {
            string crPath = Path.Combine(Application.StartupPath, "config", "copyRight.xml");
            if (!File.Exists(crPath)) return;

            List<CopyRightModel> crList;
            var crXs = new XmlSerializer(typeof(List<CopyRightModel>));
            using (var fs = new FileStream(crPath, FileMode.Open, FileAccess.Read))
                crList = (List<CopyRightModel>)crXs.Deserialize(fs);

            var labels = new System.Windows.Forms.Label[] { lblCr1, lblCr2, lblCr3, lblCr4, lblCr5 };
            for (int i = 0; i < labels.Length && i < crList.Count; i++)
                labels[i].Text = crList[i].Title;
        }

        private void AddResourcePathRow()
        {
            pathBox.Text = ConfigurationManager.AppSettings["resource_path"] ?? "resources";
        }
    }
}
