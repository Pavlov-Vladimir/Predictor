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
        private const string FORM_NAME = "PREDICATOR";

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
            this.Text = FORM_NAME;
        }
                
        private void UpdateProgressBar(int i)
        {
            #region Crutch for correct progressBar animation.
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
            #endregion

            progressBar1.Value = i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = FORM_NAME;
        }
    }
}
