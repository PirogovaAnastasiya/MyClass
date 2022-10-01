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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyClass
{
    /// <summary>
    /// Логика взаимодействия для KlRuksPage.xaml
    /// </summary>
    public partial class KlRuksPage : Page
    {
        public KlRuksPage()
        {
            InitializeComponent();
            KlRukData.ItemsSource = ClassEntities.GetContext().KlRuks.ToList();
            LCount.Content = "Количество записей в таблице: " + KlRukData.Items.Count;
        }

        private void BAddKlRuk_Click(object sender, RoutedEventArgs e)
        {
            Window windowKlRuk = new WindowKlRuk(null);
            windowKlRuk.ShowDialog();
            KlRukData.ItemsSource = ClassEntities.GetContext().KlRuks.ToList();
            KlRukData.SelectedIndex = -1;
            LCount.Content = "Количество записей в таблице: " + KlRukData.Items.Count;
        }

        private void BDelKlRuk_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить запись?",
               "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            var selectedKlRuk = KlRukData.SelectedItem as KlRuks;
            var existUser = (from el in ClassEntities.GetContext().Users where el.ID_user == selectedKlRuk.User_id select el).First();
            if (selectedKlRuk != null)
            {               
                try
                {
                    var existruk = (from el in ClassEntities.GetContext().Klasses where el.KlRuk_id == selectedKlRuk.ID_klruk select el).First();
                    MessageBox.Show("Запись удалить нельзя, так как этот классный руководитель привязан к классу!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch 
                {
                    ClassEntities.GetContext().KlRuks.Remove(selectedKlRuk);
                    ClassEntities.GetContext().Users.Remove(existUser);
                    ClassEntities.GetContext().SaveChanges();
                    KlRukData.ItemsSource = ClassEntities.GetContext().KlRuks.ToList();
                    KlRukData.SelectedIndex = -1;
                    MessageBox.Show("Запись удалена!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    LCount.Content = "Количество записей в таблице: " + KlRukData.Items.Count;
                }
            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void KlRukData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedKlRuk = KlRukData.SelectedItem as KlRuks;
            if (selectedKlRuk != null)
            {
                TF_klruk.Text = selectedKlRuk.F_klruk;
                TI_klruk.Text = selectedKlRuk.I_klruk;
                TO_klruk.Text = selectedKlRuk.O_klruk;
                TLogin.Text = selectedKlRuk.Users.Login;
                TPassword.Text = selectedKlRuk.Users.Password;              
            }
            else
            {
                TF_klruk.Text = "";
                TI_klruk.Text = "";
                TO_klruk.Text = "";
                TLogin.Text = "";
                TPassword.Text = "";
            }
        }
     
        private void TSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filterKlRuk = ClassEntities.GetContext().KlRuks.ToList();
            filterKlRuk = filterKlRuk.Where(p => p.F_klruk.ToLower().Contains(TSearch.Text.ToLower()) ||
                 p.I_klruk.ToLower().Contains(TSearch.Text.ToLower()) ||
                  p.O_klruk.ToLower().Contains(TSearch.Text.ToLower())).ToList();

            KlRukData.ItemsSource = filterKlRuk.ToList();
            LCount.Content = "Количество записей в таблице: " + KlRukData.Items.Count;
        }

        private void BRedKlruk_Click(object sender, RoutedEventArgs e)
        {
            Window windowKlRuk = new WindowKlRuk((sender as Button).DataContext as KlRuks);
            windowKlRuk.ShowDialog();
            KlRukData.ItemsSource = ClassEntities.GetContext().KlRuks.ToList();
            KlRukData.SelectedIndex = -1;
        }
    }
}
