using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Predictor
{
    public partial class Form1 : Form
    {
        private string formName = "PREDICATOR";

        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonPredict_Click(object sender, EventArgs e)
        {
            buttonPredict.Enabled = false;
            await Task.Run(() =>
            {
                for (int i = 1; i <= progressBar1.Maximum; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateProgressBar(i);
                        this.Text = $"{i} %";
                    }));
                    Thread.Sleep(20);
                }
            });
            MessageBox.Show("Prediction!");
            buttonPredict.Enabled = true;
            progressBar1.Value = 0;
            this.Text = formName;
        }

        /// <summary>
        /// Костыль для обработки анимации progressBar'а
        /// </summary>
        /// <param name="i"></param>
        private void UpdateProgressBar(int i)
        {
            if (i == progressBar1.Maximum)
            {
                progressBar1.Maximum = i + 1;
                progressBar1.Value = i + 1;
                progressBar1.Maximum = i;
            }
            else
            {
                progressBar1.Value = i + 1;
            }
            progressBar1.Value = i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = formName;
        }
    }
}
