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

namespace Example_HcaptchaSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private static string Result;
        private void SolveHcaptcha()
        {
            HcaptchaSolverNET.Solver Hsolver = new HcaptchaSolverNET.Solver();
            Hsolver.apikey = thirteenTextBox1.Text;
            string SiteKey = thirteenTextBox2.Text;
            string Domain = thirteenTextBox3.Text;

            if ((!string.IsNullOrEmpty(Hsolver.apikey)) && (!string.IsNullOrEmpty(SiteKey)) && (!string.IsNullOrEmpty(Domain)))
            {
                listBox1.Items.Add("Fields validated");
                listBox1.Items.Add("Attempting to get 2captcha ID");
                string ID = Hsolver.GetCaptchaToken(SiteKey, Domain, out bool IDstatus);
                if (IDstatus == true)
                {
                    listBox1.Items.Add("2cap task ID = " + ID);
                    listBox1.Items.Add("Attempting to get captcha result / solve");
                    string CaptchaValue = Hsolver.GetSolvedCaptchaID(ID, out bool CaptchaSolvedResult);
                    if (CaptchaSolvedResult == true)
                    {
                        listBox1.Items.Add("Got value = " + CaptchaValue);
                        Result = CaptchaValue;
                    }
                    else
                    {
                        listBox1.Items.Add("Failed to grab result of captcha");
                    }
                }
                else
                {
                    listBox1.Items.Add("Failed to grab 2captcha task ID");
                }
            }
            else
            {
                MessageBox.Show("Please validate input fields - error", "Error");
            }
        }
        private void thirteenButton2_Click(object sender, EventArgs e)
        {
            Thread Hcap = new Thread(SolveHcaptcha);
            Hcap.Start();
        }

        private void thirteenButton1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Result);
        }
    }
}
