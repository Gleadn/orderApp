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
        public CreateProduct()
        {
            InitializeComponent();
            LoadFoddstuff();
        }

        public void NextWindow_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = foodstuffsListBox.SelectedItems.Cast<string>().ToList();
            MessageBox.Show("Selected items: " + string.Join(", ", selectedItems));
            Console.WriteLine("NextWindow_Click");
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
