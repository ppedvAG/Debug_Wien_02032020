using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloAsnyc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(300);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value = i; });
                    Thread.Sleep(10);
                }
                progressBar1.Invoke((MethodInvoker)delegate { button2.Enabled = !false; });
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            button3.Enabled = false;

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Factory.StartNew(() => progressBar1.Value = i,
                                                cts.Token, TaskCreationOptions.None, ts);
                    Thread.Sleep(200);
                    if (cts.Token.IsCancellationRequested)
                        break;
                }
                Task.Factory.StartNew(() => button3.Enabled = !false,
                                              CancellationToken.None, TaskCreationOptions.None, ts);
            });
        }
        CancellationTokenSource cts = null;

        private void button6_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            cts = new CancellationTokenSource();

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    progressBar1.Value = i;
                    await Task.Delay(20, cts.Token);
                }
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show("Erfolgreich abgebrochen");
            }
            button4.Enabled = !false;

        }

        private async void button5_Click(object sender, EventArgs e)
        {
            //Debug.Assert(DateTime.Now.DayOfWeek == DayOfWeek.Friday);

#if DEBUG
            MessageBox.Show("Debug");
#else
            MessageBox.Show("Release");
#endif

#if Knödel
            MessageBox.Show("Knödel");
#endif

            var conString = "Server=(localdb)\\bla;Database=Northwind;Trusted_Connection=true;";
            using (var con = new SqlConnection(conString))
            {
                await con.OpenAsync();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Employees;WAITFOR DELAY '0:0:5'";
                    var count = await cmd.ExecuteScalarAsync();
                    MessageBox.Show($"{count} Employees in DB found");
                }
            }
        }
    }
}
