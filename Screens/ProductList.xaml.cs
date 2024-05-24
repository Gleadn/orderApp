using orderApp.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace orderApp.Screens
{
    /// <summary>
    /// Logique d'interaction pour ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        private string connectionString = "Server=.\\SQLEXPRESS;Database=orderAppBDD;Trusted_Connection=True;";
        List<Product> products = new List<Product>();
        public ProductList()
        {
            InitializeComponent();
            ListProduct();
        }

        public void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int productId = (int)button.Tag;
            InfoProduct infoproduct = new InfoProduct(productId);
            infoproduct.Show();
            this.Close();
        }

        public void ListProduct()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id, name FROM product", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        var productList = new List<ProductItem>(); // Liste pour stocker les produits

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            Console.WriteLine(id.ToString(), name);
                            var product = new ProductItem
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                               
                            };

                            productList.Add(product);
                        }

                        // Liaison de la liste de produits à votre interface utilisateur
                        ProductListItems.ItemsSource = productList;
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                }

                connection.Close();
            }
        }
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            int productId = (int)button.Tag;

           
        }





    }
}
public class ProductItem
{
    public int Id { get; set; }
    public string Name { get; set; }
}
