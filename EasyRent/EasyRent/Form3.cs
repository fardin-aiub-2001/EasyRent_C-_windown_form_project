using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;

namespace EasyRent
{
    
    public partial class Form3 : Form
    {
        private int id,b,c;
        public Form3()
        {
            InitializeComponent();
            LoadImagesInBackground();
        }

        public Form3(int id,int b ,int c)
        {
            InitializeComponent();
            LoadImagesInBackground();
            this.id=id;
            this.b=b;
            this.c = c;
            if (this.b == 1 && this.c == 1)
            {
                button3.Hide();
                pictureBox3.Hide();
            }
            else {
                button2.Hide();
                pictureBox2.Hide();
               
            }
            
        }
        private void LoadImagesInBackground()
        {
            // Preload images into memory
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                var image = imageList1.Images[i];  // Ensure the images are loaded into memory
            }
        }
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 ff = new Form2();
            ff.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 ff = new Form2();
            ff.Show();
        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 ff = new Form4(id,2,2);
            ff.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 ff=new Form5(id,b,c,1);//a defines which button gonna work//1=view profile//2=edit profile
            ff.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private int count = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (count<9)
            {
                pictureBox1.Image = imageList1.Images[count];
                count++;
            }
            else
            {
                count = 0;
            }
        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (b == 2 && c == 2)
            {
                this.Hide();
                Form9 ff = new Form9(id,2,2);
                ff.Show();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
            if (b == 1)
            {
                this.Close();
                Form5 ff = new Form5(id,1,1);
                ff.Show();
            }
            else if (b == 2)
            {
                this.Close();
                Form5 ff = new Form5(id, 2, 2);
                ff.Show();
            }
            
        }
    }
}
