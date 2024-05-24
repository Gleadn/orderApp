using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace orderApp.Screens
{
    public partial class Orders : Window
    {
        private string connectionString = "Server=.\\SQLEXPRESS;Database=orderAppBDD;Trusted_Connection=True;";
        public Orders()
        {
            InitializeComponent();
            ListOrders();
        }

        public void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void ListOrders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Id, Name, Price FROM orders", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        var ordersDict = new Dictionary<int, List<string>>();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);

                            if (!ordersDict.ContainsKey(id))
                            {
                                ordersDict[id] = new List<string>();
                            }
                            ordersDict[id].Add(name);
                        }

                        foreach (var kvp in ordersDict)
                        {
                            int id = kvp.Key;
                            List<string> names = kvp.Value;

                            StackPanel itemPanel = new StackPanel
                            {
                                Orientation = Orientation.Horizontal,
                                Margin = new Thickness(0, 5, 0, 5)
                            };

                            Label label = new Label
                            {
                                Content = $"ID: {id}",
                                Width = 100,
                                VerticalAlignment = VerticalAlignment.Center,
                                Foreground = System.Windows.Media.Brushes.White
                            };
                            itemPanel.Children.Add(label);

                            ComboBox comboBox = new ComboBox
                            {
                                Width = 150,
                                Margin = new Thickness(10, 0, 0, 0)
                            };

                            foreach (var name in names)
                            {
                                comboBox.Items.Add(name);
                            }

                            if (comboBox.Items.Count > 0)
                            {
                                comboBox.SelectedIndex = 0;
                            }

                            itemPanel.Children.Add(comboBox);
                            ItemsPanel.Children.Add(itemPanel);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No rows found.");
                    }
                }

                connection.Close();
            }
        }
    }

    public class OrdersList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
