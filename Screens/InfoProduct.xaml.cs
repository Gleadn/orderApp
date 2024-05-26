using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace orderApp.Screens
{
    /// <summary>
    /// Logique d'interaction pour InfoProduct.xaml
    /// </summary>
    public partial class InfoProduct : Window
    {
        private string connectionString = "Server=.\\SQLEXPRESS;Database=orderAppBDD;Trusted_Connection=True;";
        private int productId;
        public InfoProduct(int productId)
        {
            InitializeComponent();

            // Enregistrer l'ID du produit
            this.productId = productId;

            // Utilisez cet ID pour charger les détails du produit dans votre fenêtre
            LoadProductDetails(productId);
        }
        public void Back_Click(object sender, RoutedEventArgs e)
        {
            ProductList productlist = new ProductList();
            productlist.Show();
            this.Close();
        }

        public void LoadProductDetails(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT product.name,category,foodstuff FROM product where product.id = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            NomProduit.Text = reader.GetString(0);
                            Categorie.Text = reader.GetString(1);
                            Ingredients.Text = reader.GetString(2);
                         
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                }

                connection.Close();
            }
        }

    }
}
