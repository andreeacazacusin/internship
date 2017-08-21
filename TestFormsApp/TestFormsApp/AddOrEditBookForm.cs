using System;
using System.Windows.Forms;

namespace TestFormsApp
{
    public partial class AddOrEditBookForm : Form
    {
        public AddOrEditBookForm()
        {
            InitializeComponent();
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
                        MessageBox.Show("Book was added successfully");
                    }
                    catch(Exception exception)
                    {
                        MessageBox.Show("An error occured while adding a new book");
                    }
                    
                }
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
    }
}
