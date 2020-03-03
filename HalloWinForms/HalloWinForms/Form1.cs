using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Trace.AutoFlush = true;
            Trace.Listeners.Add(new EventLogTraceListener("Application"));


            Debug.Listeners.Add(new TextWriterTraceListener(System.Console.Out));

            Trace.WriteLine("Form ctor");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Trace.WriteLine("Button clicked!");

            MessageBox.Show("Test");

            StreamWriter sw = new StreamWriter("lala.txt");
            sw.Close();
            //StreamReader sr = new StreamReader("lala.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine( "lalal");
        }
    }
}
