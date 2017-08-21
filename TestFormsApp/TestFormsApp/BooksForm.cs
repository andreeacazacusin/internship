using System;
using System.Windows.Forms;

namespace TestFormsApp
{
    public partial class BooksForm : Form
    {
        public BooksForm()
        {
            InitializeComponent();
        }

        private void Books_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'booksDataSet.Books' table. You can move, or remove it, as needed.
            this.booksTableAdapter.Fill(this.booksDataSet.Books);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddOrEditBookForm addBook = new AddOrEditBookForm();
            addBook.ShowDialog();
        }
    }
}
