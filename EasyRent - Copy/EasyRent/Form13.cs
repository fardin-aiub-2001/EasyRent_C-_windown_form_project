using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyRent
{
    public partial class Form13 : Form
    {
        private int id,a,s,u,t;

        public Form13()
        {
            InitializeComponent();
        }

        public Form13(int id,int a,int s,int u,int t)
        {
            this.id = id;
            this.s = s;
            this.u = u;
            this.a = a; 
            this.t = t;

            InitializeComponent();
            LoadComponent();
        }

        public void LoadComponent()
        {
            try
            {
                label35.Text =a.ToString();
                label36.Text = s.ToString();
                label37.Text = u.ToString();
                label38.Text=t.ToString();
                label39.Text=id.ToString();
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                MessageBox.Show($"An error occurred while loading components: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void panel1_MouseHover(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap panelBitmap = new Bitmap(panel6.Width, panel6.Height);
            panel6.DrawToBitmap(panelBitmap, new Rectangle(0, 0, panel6.Width, panel6.Height));
            e.Graphics.DrawImage(panelBitmap, 0, 0);
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
            {
                printDocument2.Print();
            }
        }

        private void Form13_Load(object sender, EventArgs e)
        {

        }

        private void label30_Click_1(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

