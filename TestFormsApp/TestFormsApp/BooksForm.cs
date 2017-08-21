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
                DataGridViewButtonColumn buttonColEdit = new DataGridViewButtonColumn();
                buttonColEdit.Name = "Edit";
                buttonColEdit.Text = "Edit";
                buttonColEdit.UseColumnTextForButtonValue = true;
                DataGridViewButtonColumn buttonColDelete = new DataGridViewButtonColumn();
                buttonColDelete.Name = "Delete";
                buttonColDelete.Text = "Delete";
                buttonColDelete.UseColumnTextForButtonValue = true;

                booksGridView.Columns.Add(buttonColEdit);
                booksGridView.Columns.Add(buttonColDelete);
                this.booksGridView.DataSource = books;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                MessageBox.Show("EDIT button clicked at row: " + this.booksGridView.Rows[e.RowIndex].Cells[2].Value);
                
            }
            else
            {
                if (e.ColumnIndex == 1)
                {
                    DeleteBook((int)booksGridView.Rows[e.RowIndex].Cells["Id"].Value);
                    RefreshData();
                    MessageBox.Show("Book deleted!");
                }                
            }
        }

        private void DeleteBook(int bookId)
        {
            using (var context = new LibraryEntities())
            {
                var book = context.Books.FirstOrDefault(b => b.Id == bookId);
                context.Books.Remove(book);
                context.SaveChanges();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddOrEditBookForm addBook = new AddOrEditBookForm(this);
            addBook.ShowDialog();
        }
    }
}
