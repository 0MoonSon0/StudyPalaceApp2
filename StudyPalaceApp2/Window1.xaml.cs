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

namespace StudyPalaceApp2
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        STUDY_PALACEEntities2 db;
        public Window1()
        {
            InitializeComponent();
            combobind();
        }

        public List<Client> Client { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new STUDY_PALACEEntities2();
            dgP.ItemsSource = db.Client.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == true)
                {
                    pd.PrintVisual(dgP, "Проект");
                }
            }
            finally
            { this.IsEnabled = true; }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgP.ItemsSource = db.Client.Where(x => x.full_name.Contains(tbN.Text)).ToList();
        }

        private void cbP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgP.ItemsSource = db.Client.Where(x => x.id == cbP.SelectedIndex + 1).ToList();
        }

        private void combobind()
        {
            STUDY_PALACEEntities2 db = new STUDY_PALACEEntities2();
            var Item = db.Client.ToList();
            Client = Item;
            DataContext = Client;
        }

        private void dgP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

