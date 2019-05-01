using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TV_Translator
{
    public partial class Form1 : Form
    {
        String filename;
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.ActiveControl = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "(*.srt, *.txt)|*.srt;*.txt";

            if(openDlg.ShowDialog() == DialogResult.OK)
            {
                int length = openDlg.FileName.Split('\\').Length;
                tbFile.Text = openDlg.FileName.Split('\\')[length - 1];
                this.filename = openDlg.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(this.filename,FileMode.Open,FileAccess.Read);
            StreamReader sr = new StreamReader(fs,Encoding.Default,true);
            String content = sr.ReadToEnd();

            fs.Close();
            sr.Close();

            content = content.Replace('þ', 'ț');
            content = content.Replace('º', 'ș');
            content = content.Replace('Þ', 'Ț');
            content = content.Replace('ª', 'Ș');

            FileStream sfs = new FileStream(this.filename, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(sfs,Encoding.UTF8);
        
            sw.Write(content);

            sw.Close();
            sfs.Close();

            MessageBox.Show("Subtitle was corrected!");
        }
    }
}
