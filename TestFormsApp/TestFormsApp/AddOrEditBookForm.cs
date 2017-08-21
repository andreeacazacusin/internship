using System;
using System.Configuration;
using System.Data.SqlClient;
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
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestFormsApp.Properties.Settings.libraryConnectionString"].ToString());
                try
                {
                    sqlConnection.Open();
                    string sql = $"insert into Books(InventoryNumber, Author, Title, Description) VALUES('{textBoxInventoryNumber.Text}', '{textBoxAuthor.Text}', '{textBoxTitle.Text}', '{textBoxDescription.Text}')";
                    var sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Book was added successfully");
                }
                catch (Exception exception) {
                    MessageBox.Show("An error occured while adding a new book");
                }
                finally
                {
                    sqlConnection.Close();                    
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
