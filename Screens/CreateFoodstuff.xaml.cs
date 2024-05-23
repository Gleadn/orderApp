using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace orderApp.Screens
{
    /// <summary>
    /// Logique d'interaction pour CreateFoodstuff.xaml
    /// </summary>
    public partial class CreateFoodstuff : Window
    {
        private string connectionString = "Server=.\\SQLEXPRESS;Database=orderAppBDD;Trusted_Connection=True;";

        public CreateFoodstuff()
        {
            InitializeComponent();
        }

        public void CreateFoodstuff_Click(object sender, RoutedEventArgs e)
        {
            string name = this.name.Text;
            string quantity = this.quantity.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO foodstuff (Name, Quantity, Status) VALUES (@name, @quantity, @status)";

                if (validInput(name, quantity))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int quantityQuery = int.Parse(quantity);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@status", "Stocked");
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    NewItems newItems = new NewItems();
                    newItems.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("A field might be empty or the value is wrong");
                }
            }
        }

        public bool validInput(string name, string quantity)
        {
            if (name == "" || quantity == "")
            {
                return false;
            } else if (int.TryParse(quantity, out int n) == false)
            {
                return false;
            }
            return true;
        }

        public void Back_Click(object sender, RoutedEventArgs e)
        {
            NewItems newItems = new NewItems();
            newItems.Show();
            this.Close();
        }
    }
}
