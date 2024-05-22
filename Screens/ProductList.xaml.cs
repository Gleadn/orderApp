using orderApp.Class;
using System.Collections.Generic;
using System.Windows;

namespace orderApp.Screens
{
    /// <summary>
    /// Logique d'interaction pour ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        public ProductList()
        {
            InitializeComponent();
        }

        public void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
