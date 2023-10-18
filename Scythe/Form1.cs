using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scythe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private static string dllp { get; set; }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"C:\";
                ofd.Title = "Select DLL File For Injection";
                ofd.DefaultExt = "dll";
                ofd.Filter = "DLL Files (*.dll|*.dll)";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.ShowDialog();

                textBox1.Text = ofd.FileName;
                dllp = ofd.FileName;
            }

            catch (Exception ed)
            {
                MessageBox.Show(ed.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dllp = textBox1.Text;
        }
    }
}
