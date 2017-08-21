using System;
using System.Linq;
using System.Windows.Forms;

namespace TestFormsApp
{
    public partial class BooksForm : Form
    {
        public BooksForm()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {
            LoadBooks();
        }

        private void Books_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            using (var context = new LibraryEntities())
            {
                var books = context.Books.Select(x => new
                {
                    Id = x.Id,
                    InventoryNumber = x.InventoryNumber,
                    Title = x.Title,
                    Author = x.Author,
                    Description = x.Description
                }).ToList();
                this.dataGridView1.DataSource = books;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddOrEditBookForm addBook = new AddOrEditBookForm(this);
            addBook.ShowDialog();
        }
    }
}
