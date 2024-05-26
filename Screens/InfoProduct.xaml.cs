using orderApp.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
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
                SqlCommand command = new SqlCommand("SELECT product.name,category,foodstuff,receipeID FROM product where product.id = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", productId);
                int receipeID = 0;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            

                            NomProduit.Text = reader.GetString(0);
                            Categorie.Text = reader.GetString(1);
                            Ingredients.Text = reader.GetString(2);
                            receipeID = reader.GetInt32(3);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    
                }
                SqlCommand command2 = new SqlCommand("SELECT steps FROM receipe where id = @receipid", connection);
                command2.Parameters.AddWithValue("@receipid", receipeID);
                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Steps.Text = reader.GetString(0);
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

        public void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM product WHERE id = @id";
                string query2 = "DELETE FROM receipe WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", productId);
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@id", productId);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

            ProductList productlist = new ProductList();
            productlist.Show();
            this.Close();
        }
    }
}