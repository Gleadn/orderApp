using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour CreateProduct.xaml
    /// </summary>
    public partial class CreateProduct : Window
    {
        private string connectionString = "Server=.\\SQLEXPRESS;Database=orderAppBDD;Trusted_Connection=True;";

        public ObservableCollection<string> Foodstuffs { get; set; }

        public int ID = 1;
        public CreateProduct()
        {
            InitializeComponent();
            LoadFoddstuff();
        }

        public void NextWindow_Click(object sender, RoutedEventArgs e)
        {
            string productName = name.Text;
            string productDescription = description.Text;
            string productCategory = category.Text;
            string productPrice = price.Text;
            List<string> selectedFoodstuffs = new List<string>();
            foreach (var item in foodstuffsListBox.SelectedItems)
            {
                selectedFoodstuffs.Add(item.ToString());
            }
            string foodstuffs = string.Join(", ", selectedFoodstuffs);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO product (Id, Name, Description, Category, Price, Foodstuff, ReceipeID) VALUES (@id, @name, @description, @category, @price, @foodstuff, @receipid)";

                if (validInput(productName, productDescription, productCategory, productPrice, foodstuffs))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        float floatPrice = float.Parse(productPrice);
                        command.Parameters.AddWithValue("@id", ID);
                        command.Parameters.AddWithValue("@name", productName);
                        command.Parameters.AddWithValue("@description", productDescription);
                        command.Parameters.AddWithValue("@category", productCategory);
                        command.Parameters.AddWithValue("@price", productPrice);
                        command.Parameters.AddWithValue("@foodstuff", foodstuffs);
                        command.Parameters.AddWithValue("@receipid", ID);
                        command.ExecuteNonQuery();
                        ID++;
                    }
                    connection.Close();
                    CreateReceipe createReceipe = new CreateReceipe(ID);
                    createReceipe.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("A field might be empty or the value is wrong");
                }
            }
        }

        public bool validInput(string name, string description, string category, string price, string foodstuffs)
        {
            if (name == "" || description == "" || category == "" || price == "" || foodstuffs == "")
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

        public void LoadFoddstuff()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Name FROM foodstuff WHERE Status = 'Stocked'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Foodstuffs = new ObservableCollection<string>();
                        while (reader.Read())
                        {
                            Foodstuffs.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            this.foodstuffsListBox.ItemsSource = Foodstuffs;
        }
    }
}
