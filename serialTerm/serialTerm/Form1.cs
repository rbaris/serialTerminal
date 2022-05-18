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
using System.IO.Ports;

namespace serialTerm
{
    public partial class Form1 : Form
    {

        private SerialPort _serialPort;

        
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Visible = false;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            

            foreach (string port in ports)
            {
                cbPort1.Items.Add(port);
                cbPort2.Items.Add(port);
            }
            if (ports.Count() == 0)
            {
                cbPort1.Enabled = false;
                cbPort2.Enabled = false;
            }
        }

        private void btnConnect1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            try
            {
                serialPort1.PortName = cbPort1.Text;
                serialPort1.BaudRate = Convert.ToInt16(cbRate1.Text);

                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                    
                    lblStatus1.Text = "Connection established.";
                    lblStatus1.ForeColor = Color.Green;
                    richTextBox1.Visible = true;
                }
            }
            catch (Exception)
            {

                
                lblStatus1.Text = "Connection could not be established";
                lblStatus1.ForeColor = Color.Red;
            }

            try
            {
                if (!serialPort1.IsOpen)
                {
                    richTextBox1.Visible = true;
                    serialPort1.WriteLine(richTextBox1.Text);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Port connection not found");
            }
        }

        private void btnDisconnect1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            serialPort1.Close();
            
            lblStatus1.Text = "Connection terminated";
            lblStatus1.ForeColor = Color.Red;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {

                string info = (serialPort1.ReadExisting());
                richTextBox1.Visible = true;
                richTextBox1.Text = info.ToString();
                System.Threading.Thread.Sleep(1200);
            }
            catch (Exception)
            {
            }
        }

        private void btnConnect2_Click(object sender, EventArgs e)
        {
            timer2.Start();
            try
            {
                serialPort2.PortName = cbPort2.Text;
                serialPort2.BaudRate = Convert.ToInt16(cbRate2.Text);

                if (!serialPort2.IsOpen)
                {
                    serialPort2.Open();
                    
                    lblStatus2.Text = "Connection established.";
                    lblStatus2.ForeColor = Color.Green;
                }
            }
            catch (Exception)
            {

               
                lblStatus2.Text = "Connection could not be established";
                lblStatus2.ForeColor = Color.Red;
            }

            try
            {
                if (!serialPort2.IsOpen)
                {
                    richTextBox1.Visible = true;
                    serialPort2.WriteLine(richTextBox1.Text);

                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Port connection not found");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                string info = (serialPort2.ReadExisting());
                richTextBox1.Visible = true;
                richTextBox1.Text = info.ToString();
                System.Threading.Thread.Sleep(1200);
            }
            catch (Exception)
            {
            }

        }

        private void btnDisconnect2_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            serialPort2.Close();
            
            lblStatus2.Text = "Connection terminated";
            lblStatus2.ForeColor = Color.Red;
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            richTextBox1.Visible = true;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          
                int saveloop = Convert.ToInt32(cbFiles.SelectedIndex);
                string fpath = folderBrowserDialog1.SelectedPath;
                string fullpath = string.Concat(fpath + "\\" + DateTime.Now.ToString().Replace(':', '_'));
             
                for (int i = 0; i < saveloop+1; i++)
                {
                    richTextBox1.SaveFile(fullpath + @".rtf");
                }    
        }

        private void cbSeconds_SelectedIndexChanged(object sender, EventArgs e)
        {
            int recordTime = Convert.ToInt32(cbSeconds.SelectedIndex);
            double ms = Math.Pow(10, 3);
            int ms_int = Convert.ToInt32(ms);
            timer1.Interval = (recordTime + 1) * ms_int;
            timer2.Interval = (recordTime+1) * ms_int;
        }         
    }
}
