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
    /// Logique d'interaction pour CreateFoodstuff.xaml
    /// </summary>
    public partial class CreateFoodstuff : Window
    {
        public CreateFoodstuff()
        {
            InitializeComponent();
        }

        public void CreateFoodstuff_Click(object sender, RoutedEventArgs e)
        {
            string name = this.name.Text;
            int quantity = int.Parse(this.quantity.Text);

            Class.Foodstuff foodstuff = new Class.Foodstuff(name, quantity, "stock");

            NewItems newItems = new NewItems();
            newItems.Show();
            this.Close();
        }

        public void Back_Click(object sender, RoutedEventArgs e)
        {
            NewItems newItems = new NewItems();
            newItems.Show();
            this.Close();
        }
    }
}
