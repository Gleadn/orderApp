using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour NewItems.xaml
    /// </summary>
    public partial class NewItems : Window
    {
        public NewItems()
        {
            InitializeComponent();
        }

        public void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("New Product");
        }

        public void NewFoodstuff_Click(object sender, RoutedEventArgs e)
        {
            CreateFoodstuff createFoodstuff = new CreateFoodstuff();
            createFoodstuff.Show();
            this.Close();
        }
    }
}
