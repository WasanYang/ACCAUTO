using AccAutoKey4.Page;
using System;
using System.Configuration;
using System.Windows.Forms;
namespace AccAutoKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.setupPage();
        }

        private void setupPage()
        {
            NewPage1 page = new NewPage1();
            selectPage(page);

            this.Text = ConfigurationManager.AppSettings["form_title"];
            this.btnpage1.Text = ConfigurationManager.AppSettings["page1_text"];
            this.btnpage2.Text = ConfigurationManager.AppSettings["page2_text"];
            this.btnpage3.Text = ConfigurationManager.AppSettings["page3_text"];
            this.btnpage4.Text = ConfigurationManager.AppSettings["page4_text"];
            this.btnpage5.Text = ConfigurationManager.AppSettings["page5_text"];

        }
        private void selectPage(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            panelControl.Controls.Clear();
            panelControl.Controls.Add(control);
            control.BringToFront();
        }

        private void clearBtnPageColor()
        {
            this.btnpage1.BackColor = System.Drawing.Color.LightGray;
            this.btnpage2.BackColor = System.Drawing.Color.LightGray;
            this.btnpage3.BackColor = System.Drawing.Color.LightGray;
            this.btnpage4.BackColor = System.Drawing.Color.LightGray;
            this.btnpage5.BackColor = System.Drawing.Color.LightGray;
        }
        private void page1Btn(object sender, EventArgs e)
        {
            NewPage1 page = new NewPage1();
            selectPage(page);
        }
        private void btn_page2(object sender, EventArgs e)
        {
            NewPage2 page = new NewPage2();
            selectPage(page);
        }

        private void btn_page3(object sender, EventArgs e)
        {
            NewPage3 page = new NewPage3();
            selectPage(page);
        }

        private void btn_page4(object sender, EventArgs e)
        {
            NewPage4 page = new NewPage4();
            selectPage(page);
        }

        private void btn_page5(object sender, EventArgs e)
        {
            NewPage5 page = new NewPage5();
            selectPage(page);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CopyRight page = new CopyRight();
            selectPage(page);
        }

        private void panelControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemarkPage page = new RemarkPage();
            selectPage(page);
        }
    }
}
