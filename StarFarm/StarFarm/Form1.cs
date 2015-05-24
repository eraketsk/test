using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace StarFarm
{
    public partial class Form1 : Form
    {
        Class1 _instance;

        public Form1()
        {
            InitializeComponent();
            textEdit1.Text = Properties.Settings.Default.LastPath;
            textEdit2.Text = Properties.Settings.Default.LastNickName;
            _instance = new Class1();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;
                result = folderBrowserDialog1.ShowDialog();
                switch (result)
                {
                    case DialogResult.OK:
                        textEdit1.Text = folderBrowserDialog1.SelectedPath;
                        Properties.Settings ps = Properties.Settings.Default;
                        ps.LastPath = folderBrowserDialog1.SelectedPath;
                        ps.Save();
                        break;
                    default:
                        textEdit1.Text = Properties.Settings.Default.LastPath;
                        break;
                }
            }
            finally 
            {
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Properties.Settings ps = Properties.Settings.Default;
            ps.LastNickName = textEdit2.Text;
            ps.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instance.Close();
        }
    }
}
