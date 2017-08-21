using System;
using System.Linq;
using System.Windows.Forms;

namespace TestFormsApp
{
    public partial class AddOrEditBookForm : Form
    {
        private BooksForm _parentForm;
        public AddOrEditBookForm(BooksForm booksForm)
        {
            InitializeComponent();
            _parentForm = booksForm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {   
                using (var connection = new LibraryEntities())
                {
                    try
                    {
                        var book = new Books
                        {
                            InventoryNumber = textBoxInventoryNumber.Text,
                            Title = textBoxTitle.Text,
                            Author = textBoxAuthor.Text,
                            Description = textBoxDescription.Text
                        };
                        connection.Books.Add(book);
                        connection.SaveChanges();
                        this.Dispose();
                        MessageBox.Show("Book was added successfully");
                    }
                    catch(Exception exception)
                    {
                        this.Dispose();
                        MessageBox.Show("An error occured while adding a new book");
                    }
                    
                }
            }
        }

        private bool InventoryNumberUsed(string inventoryNumber)
        {
            using(var content = new LibraryEntities())
            {
                var book = content.Books.Where(x => x.InventoryNumber.Equals(inventoryNumber)).ToList();
                if (book.Count > 0)
                    return true;
                else return false;
            }
        }
        public bool IsValid()
        {
            bool isValid = true;

            if(textBoxInventoryNumber.Text == "")
            {
                textBoxInventoryNumber.Focus();
                isValid = false;
                bookErrors.SetError(textBoxInventoryNumber, "Please add the inventory number");
            }

            if(InventoryNumberUsed(textBoxInventoryNumber.Text))
            {
                if (isValid) textBoxInventoryNumber.Focus();
                isValid = false;
                bookErrors.SetError(textBoxInventoryNumber, "Inventory number in use");
            }

            if(textBoxTitle.Text == "")
            {
                if(isValid) textBoxTitle.Focus();
                isValid = false;
                bookErrors.SetError(textBoxTitle, "Please add the title");
            }

            if(textBoxAuthor.Text == "")
            {                
                if(isValid) textBoxAuthor.Focus();
                isValid = false;
                bookErrors.SetError(textBoxAuthor, "Please add the author");
            }

            if (isValid)
                bookErrors.Clear();
            return isValid;
        }

        private void AddOrEditBookForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parentForm.RefreshData();
        }
    }
}
