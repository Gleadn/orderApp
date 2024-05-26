using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
using System.Data.SqlClient;

namespace orderApp.Screens
{
    /// <summary>
    /// Logique d'interaction pour CreateReceipe.xaml
    /// </summary>
    public partial class CreateReceipe : Window
    {
        private string connectionString = "Server=.\\SQLEXPRESS;Database=orderAppBDD;Trusted_Connection=True;";
        private int stepCount = 2;
        public int ID = 0;
        public CreateReceipe(int id)
        {
            InitializeComponent();
            ID = id;
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            stepCount++;
            AddNewStep(stepCount);
        }
        private void AddNewStep(int stepNumber)
        {
            Label newStepLabel = new Label
            {
                Content = $"Step {stepNumber}",
                Foreground = System.Windows.Media.Brushes.White
            };

            TextBox newStepTextBox = new TextBox
            {
                Name = $"step{stepNumber}",
                Margin = new Thickness(0, 5, 0, 5)
            };

            StepsPanel.Children.Add(newStepLabel);
            StepsPanel.Children.Add(newStepTextBox);
        }

        public void CreateReceipe_Click(object sender, RoutedEventArgs e)
        {
            string steps = "";
            foreach (var item in StepsPanel.Children)
            {
                if (item is TextBox)
                {
                    steps += (item as TextBox).Text + "; ";
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO receipe (Id, Steps) VALUES (@id, @steps)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", ID);
                    command.Parameters.AddWithValue("@steps", steps);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


    }
}
