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
    /// Логика взаимодействия для KlassesPage.xaml
    /// </summary>
    public partial class KlassesPage : Page
    {
        public KlassesPage()
        {
            InitializeComponent();
            var allKlruks = ClassEntities.GetContext().KlRuks.ToList();
            allKlruks.Insert(0, new KlRuks
            { 
                F_klruk="Все"
            });
            CSearchKlruk.ItemsSource = allKlruks.ToList();
            CSearchKlruk.SelectedIndex = 0;
            KlassesData.ItemsSource = ClassEntities.GetContext().Klasses.ToList();
            LCount.Content = "Количество записей в таблице: " + KlassesData.Items.Count;
        }

        private void BAddKlass_Click(object sender, RoutedEventArgs e)
        {
            Window windowKlass = new WindowKlass(null);
            windowKlass.ShowDialog();
            KlassesData.ItemsSource = ClassEntities.GetContext().Klasses.ToList();
            KlassesData.SelectedIndex = -1;
            LCount.Content = "Количество записей в таблице: " + KlassesData.Items.Count;
        }

        private void BDelKlass_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить класс?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            var selectedKlass = KlassesData.SelectedItem as Klasses;
            var existMer = ClassEntities.GetContext().Meropriyatia.Where(p => p.ID_mer == selectedKlass.ID_klass);
            if (selectedKlass != null)
            {
                try
                {
                    var existuch = (from el in ClassEntities.GetContext().Ucheniki where el.Klass_id == selectedKlass.ID_klass select el).First();
                    MessageBox.Show("Данный класс удалить нельзя, так как в классе есть ученики!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch 
                {
                    if (existMer != null)
                    {
                        foreach (var mer in existMer)
                        {
                            ClassEntities.GetContext().Meropriyatia.Remove(mer);
                        }
                    }
                    ClassEntities.GetContext().Klasses.Remove(selectedKlass);
                    ClassEntities.GetContext().SaveChanges();
                    KlassesData.ItemsSource = ClassEntities.GetContext().Klasses.ToList();
                    MessageBox.Show("Класс удален!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    LCount.Content = "Количество записей в таблице: " + KlassesData.Items.Count;
                }
            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);          
        }

        private void KlassesData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedKlass = KlassesData.SelectedItem as Klasses;
            if (selectedKlass!= null)
            {
                TN_klass.Text = selectedKlass.N_klass;
                TKlRuk.Text = selectedKlass.KlRuks.ToString();
            }
            else
            {
                TN_klass.Text = "";
                TKlRuk.Text = "";
            }
        }
        private void CSearchKlruk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filterKlass = ClassEntities.GetContext().Klasses.ToList();
            if (CSearchKlruk.SelectedIndex > 0)
            {
                filterKlass = filterKlass.Where(p => p.KlRuk_id.ToString().Contains((CSearchKlruk.SelectedItem
                    as KlRuks).ID_klruk.ToString())).ToList();
            }
            KlassesData.ItemsSource = filterKlass.ToList();
            LCount.Content = "Количество записей в таблице: " + KlassesData.Items.Count;
        }

        private void BRedKlass_Click(object sender, RoutedEventArgs e)
        {
            Window windowKlass = new WindowKlass((sender as Button).DataContext as Klasses);
            windowKlass.ShowDialog();
            KlassesData.ItemsSource = ClassEntities.GetContext().Klasses.ToList();
            KlassesData.SelectedIndex = -1;
        }
    }
}
