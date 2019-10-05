using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slide_Imgae
{
    public partial class Form1 : Form
    {
        private string[] folderFile = null;
        private int selected = 0;
        private int end = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] part1 = null,  part2 = null;

                part1 = System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.jpg");
                part2 = System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.png");
                folderFile = new string[part1.Length + part2.Length];

                Array.Copy(part1, 0, folderFile, 0, part1.Length);
                Array.Copy(part2, 0, folderFile, part1.Length, part2.Length);
                selected = 0;
                end = folderFile.Length;
                showImage(folderFile[selected]);

                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnPlay.Enabled = true;
            }
        }
        private void showImage(string path)
        {
            Image folderAnh = Image.FromFile(path);

            pictureBox1.Image = folderAnh;
        }
        private void prevImage()
        {
            if (selected == 0)
            {
                selected = folderFile.Length - 1;
                showImage(folderFile[selected]);
            }
            else
            {
                selected = selected - 1;
                showImage(folderFile[selected]);
            }
        }

        private void nextImage()
        {
            if (selected == folderFile.Length - 1)
            {
                selected = 0;
                showImage(folderFile[selected]);
            }
            else
            {
                selected = selected + 1;
                showImage(folderFile[selected]);
            }
        }
        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            prevImage();
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            nextImage();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            nextImage();
        }

        private void btnPlay_Click(object sender, System.EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
                btnPlay.Text = "START";
                //btnPlay.Image = Image.FromFile()
            }
            else
            {
                timer1.Enabled = true;
                btnPlay.Text = "STOP";
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            btnPlay.Enabled = false;
        }

    }
}
