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
                SqlCommand command = new SqlCommand("SELECT Id, Name, Price, Status FROM orders", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        var ordersDict = new Dictionary<int, (List<string> names, string status)>();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string status = reader.GetString(3);

                            if (!ordersDict.ContainsKey(id))
                            {
                                ordersDict[id] = (new List<string>(), status);
                            }
                            ordersDict[id].names.Add(name);
                        }

                        foreach (var kvp in ordersDict)
                        {
                            int id = kvp.Key;
                            List<string> names = kvp.Value.names;
                            string currentStatus = kvp.Value.status;

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
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Foreground = System.Windows.Media.Brushes.White
                            };
                            itemPanel.Children.Add(label);

                            ComboBox comboBox = new ComboBox
                            {
                                Width = 150,
                                HorizontalAlignment = HorizontalAlignment.Center,
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

                            ComboBox statusComboBox = new ComboBox
                            {
                                Width = 100,
                                HorizontalAlignment = HorizontalAlignment.Right,
                                Margin = new Thickness(10, 0, 0, 0),
                                ItemsSource = new List<string> { "Pending", "Shipped", "Ordered", "Completed" },
                                SelectedValue = currentStatus
                            };

                            statusComboBox.SelectionChanged += (s, e) =>
                            {
                                string newStatus = (string)statusComboBox.SelectedValue;
                                UpdateOrderStatus(id, newStatus);
                            };

                            itemPanel.Children.Add(statusComboBox);
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
        private void UpdateOrderStatus(int id, string newStatus)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE orders SET Status = @Status WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Status", newStatus);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
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
