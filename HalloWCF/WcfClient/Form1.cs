using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfClient.ServiceReference1;

namespace WcfClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var client = new ServiceClient();
            var result = await client.GetDataAsync((int)numericUpDown1.Value);
            label1.Text = result;

        }
        public BooksResult result = null;
        private async void button2_Click(object sender, EventArgs e)
        {
            var web = new HttpClient();
            var json = await web.GetStringAsync("https://www.googleapis.com/books/v1/volumes?q=wien");

            result = JsonConvert.DeserializeObject<BooksResult>(json);

            MessageBox.Show("Test");


            result.items.First().volumeInfo.title = "LALAALALA";

            //throw new EvaluateException();

            dataGridView1.DataSource = result.items.Select(x => x.volumeInfo).ToList();

        }
    }
}
