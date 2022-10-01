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

namespace MyClass
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;
            if (Bduser.SetUser().Level == 1)
            {
                LabelUser.Content = "Пользователь: Администратор";                
                BUch.Visibility = Visibility.Hidden;
                BMer.Visibility = Visibility.Hidden;
                BKonkurs.Visibility = Visibility.Hidden;
                BOtchets.Visibility = Visibility.Hidden;
            }
            if (Bduser.SetUser().Level != 1)
            {
                LabelUser.Content = "Пользователь: " + Bduser.SetUser().KlRuks.First().F_klruk +
                    " " + Bduser.SetUser().KlRuks.First().I_klruk + " " + Bduser.SetUser().KlRuks.First().O_klruk;
                LabelKlass.Content = "Класс: " + Bduser.SetUser().KlRuks.First().Klasses.First().N_klass;                   
                BKlasses.Visibility = Visibility.Hidden;
                BKlRuks.Visibility = Visibility.Hidden;  
                BUchForAdmin.Visibility = Visibility.Hidden;
            }          
        }

        private void BUch_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UchPage());
        }
        private void BOtchets_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new OtchetsPage());
        }
        private void BMer_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MerPage());
        }
        private void BKonkurs_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new KonkursPage());
        }
        private void BKlasses_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new KlassesPage());
        }

        private void BKlRuks_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new KlRuksPage());
        }

        private void BUchForAdmin_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UchForAdminPage());
        }

        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            Window exit = new MainWindow();
            this.Close();
            exit.ShowDialog();
        }
    }
}
