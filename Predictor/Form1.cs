using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Predictor
{
    public partial class Form1 : Form
    {
        private const string FORM_NAME = "PREDICTOR";
        private readonly string PREDICTIONS_CONFIG_PATH = $"{Environment.CurrentDirectory}\\Predictions.txt";
        private string[] _predictions;
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private async void ButtonPredict_Click(object sender, EventArgs e)
        {
            buttonPredict.Enabled = false;

            if (ClarifyInformation() == DialogResult.No)
            {
                buttonPredict.Enabled = true;
                return;
            }

            await ExecutePrediction();

            MessageBox.Show(GetPrediction());

            buttonPredict.Enabled = true;
            progressBar1.Value = 0;
            this.Text = FORM_NAME;
        }

        private async Task ExecutePrediction()
        {
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
        }

        private static DialogResult ClarifyInformation()
        {
            string message = "The answer to  your question must be clearly defined: 'Yes' or 'No'!" +
                             "\n\nAre you ready?";

            DialogResult dialogResult = MessageBox.Show(
                message, "Important.",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            return dialogResult;
        }

        private string GetPrediction()
        {
            int index = _random.Next(_predictions.Length);
            return _predictions[index];
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

            try
            {
                string predictionsData = File.ReadAllText(PREDICTIONS_CONFIG_PATH);
                _predictions = predictionsData.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            finally
            {
                if (_predictions.Length == 0)
                {
                    MessageBox.Show("There are no predictions for you now. :(");
                    this.Close();
                }
            }

            CleanStringsInArray(_predictions);
        }

        private void CleanStringsInArray(string[] array)
        {
            foreach (string eachString in array)
                eachString.Trim('\r', ' ');
        }
    }
}
