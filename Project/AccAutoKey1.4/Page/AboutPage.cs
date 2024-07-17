using AccAutoKey4.Models;
using AccAutoKey4.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AccAutoKey4.Page
{
    public partial class AboutPage : UserControl
    {
        XmlSerializer xs;
        List<AboutModel> ls;
        private string configPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\config\\about.xml";
        public AboutPage()
        {
            InitializeComponent();
            ls = new List<AboutModel>();
            xs = new XmlSerializer(typeof(List<AboutModel>));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RemarkPage_Load(object sender, EventArgs e)
        {
            this.LoadPage();
        }
        private void LoadPage()
        {
            FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.Read);
            ls = (List<AboutModel>)xs.Deserialize(fs);
            fs.Close();

            var selected = ls[0];
            Address.Text = selected.Address;
            description.Text = selected.Description;
            startDate.Text = selected.StartDate;
            endDate.Text = selected.EndDate;
            location.Text = selected.Location;
            record.Text = selected.Record;
            snBox.Text = selected.SN;
        }

        private void logo_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void snBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
