using System;
using System.Windows.Forms;

namespace TestFormsApp
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            BooksForm books = new BooksForm();
            books.ShowDialog();
        }
    }
}
