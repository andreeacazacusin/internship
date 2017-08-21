using System;
using System.Linq;
using System.Windows.Forms;

namespace TestFormsApp
{
    public partial class AddOrEditBookForm : Form
    {
        private BooksForm _parentForm;
        private int _bookId;
        private bool isEditForm;

        public AddOrEditBookForm(BooksForm booksForm)
        {
            InitializeComponent();
            _parentForm = booksForm;
        }

        public AddOrEditBookForm(BooksForm booksForm, int bookId)
        {
            InitializeComponent();
            _parentForm = booksForm;
            _bookId = bookId;
            isEditForm = true;
            InitializeBookDetails(bookId);
        }

        private void InitializeBookDetails(int bookId)
        {
            using (var context = new LibraryEntities())
            {
                var book = context.Books.FirstOrDefault(b => b.Id == bookId);
                if(book != null)
                {
                    textBoxInventoryNumber.Text = book.InventoryNumber;
                    textBoxInventoryNumber.ReadOnly = true;
                    textBoxTitle.Text = book.Title;
                    textBoxAuthor.Text = book.Author;
                    textBoxDescription.Text = book.Description;
                }
            }
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
                        if (!isEditForm)
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
                        }
                        else
                        {
                            var book = connection.Books.FirstOrDefault(b => b.Id == _bookId);
                            if(book == null)
                                MessageBox.Show("An error occured while saving the book");
                            else
                            {
                                book.InventoryNumber = textBoxInventoryNumber.Text;
                                book.Author = textBoxAuthor.Text;
                                book.Title = textBoxTitle.Text;
                                book.Description = textBoxDescription.Text;
                                connection.SaveChanges();
                            }
                        }
                        this.Dispose();
                        MessageBox.Show("Book was saved successfully");
                    }
                    catch(Exception exception)
                    {
                        this.Dispose();
                        MessageBox.Show("An error occured while saving the book");
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

            if(!isEditForm && InventoryNumberUsed(textBoxInventoryNumber.Text))
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
