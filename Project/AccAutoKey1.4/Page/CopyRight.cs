using AccAutoKey4.Models;
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
    public partial class CopyRight : UserControl
    {
        XmlSerializer xs;
        List<CopyRightModel> ls;
        private string configPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\config\\copyRight.xml";
        public CopyRight()
        {
            InitializeComponent();
            ls = new List<CopyRightModel>();
            xs = new XmlSerializer(typeof(List<CopyRightModel>));
        }

        private void CopyRight_Load(object sender, EventArgs e)
        {
            controlPanel.Controls.Clear();
            FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.Read);
            ls = (List<CopyRightModel>)xs.Deserialize(fs);
            fs.Close();
            for (int i = 0; i < ls.Count(); i++)
            { 
                Label lb = new Label();
                lb.Location = new Point(30, 7 + (i * 45));
                lb.Text = ls[i].Title;
                lb.Font = new Font("Angsana New", 14, FontStyle.Regular);
                lb.Size = new Size(210, 39);
                controlPanel.Controls.Add(lb);
            }
        }

        private void controlPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
