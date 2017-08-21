using System;
using System.Windows.Forms;

namespace TestFormsApp
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            Books books = new Books();
            books.ShowDialog();
        }
    }
}
