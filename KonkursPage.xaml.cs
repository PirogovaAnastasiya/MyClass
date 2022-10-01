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
    /// Логика взаимодействия для KonkursPage.xaml
    /// </summary>
    public partial class KonkursPage : Page
    {
        public KonkursPage()
        {
            InitializeComponent();
            KonkursList();
            CLevel.ItemsSource = ClassEntities.GetContext().LevelKonkurs.ToList();
            CUch.ItemsSource = ClassEntities.GetContext().Ucheniki.ToList();
            var allLevelKonk = ClassEntities.GetContext().LevelKonkurs.ToList();
            allLevelKonk.Insert(0, new LevelKonkurs
            { 
                N_level="Все"
            });
            CSearchLevel.ItemsSource = allLevelKonk.ToList();
            CSearchLevel.SelectedIndex = 0;
            LCount.Content = "Количество записей в таблице: " + KonkursData.Items.Count;
        }
        private void KonkursList()
        {
            var db = ClassEntities.GetContext().Konkurs.ToList().Where(p => p.Klass_id == Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            KonkursData.ItemsSource = db.ToList();
        }
        private void BAddKonkurs_Click(object sender, RoutedEventArgs e)
        {
            Window windowKonk = new WindowKonkurs(null);
            windowKonk.ShowDialog();
            KonkursList();
            KonkursData.SelectedIndex = -1;
            LCount.Content = "Количество записей в таблице: " + KonkursData.Items.Count;
        }

        private void BDelKonkurs_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить запись?",
               "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            var selectedKonk = KonkursData.SelectedItem as Konkurs;
            if (selectedKonk != null)
            {
                try
                {
                    ClassEntities.GetContext().Konkurs.Remove(selectedKonk);
                    ClassEntities.GetContext().SaveChanges();
                    KonkursList();
                    KonkursData.SelectedIndex= -1;
                    MessageBox.Show("Запись удалена", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    LCount.Content = "Количество записей в таблице: " + KonkursData.Items.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void KonkursData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedKonk = KonkursData.SelectedItem as Konkurs;
            if (selectedKonk != null)
            {
                TN_konk.Text = selectedKonk.N_konk;
                TResult_konk.Text = selectedKonk.Result_konk;
                CLevel.SelectedItem= (from el in ClassEntities.GetContext().LevelKonkurs where el.ID_level == selectedKonk.Level_id select el).Single<LevelKonkurs>();
                CUch.SelectedItem= (from el in ClassEntities.GetContext().Ucheniki where el.ID_uch == selectedKonk.Uch_id select el).Single<Ucheniki>();
                DData_konk.SelectedDate = selectedKonk.Data_konk;
            }
            else
            {
                TN_konk.Text = "";
                TResult_konk.Text = "";
                CLevel.SelectedIndex = -1;
                CUch.SelectedIndex = -1;
                DData_konk.SelectedDate = null;              
            }
        }

       
        public void Filter()
        {
            var filterKonk = ClassEntities.GetContext().Konkurs.ToList().Where(p => p.Klass_id == Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            filterKonk = filterKonk.Where(p => p.N_konk.ToLower().Contains(TSearch.Text.ToLower())).ToList();
            if (CSearchLevel.SelectedIndex > 0)
            {
                filterKonk = filterKonk.Where(p => p.Level_id.ToString().Contains((CSearchLevel.SelectedItem
                    as LevelKonkurs).ID_level.ToString())).ToList();
            }
            KonkursData.ItemsSource = filterKonk.ToList();
        }

        private void TSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
            LCount.Content = "Количество записей в таблице: " + KonkursData.Items.Count;
        }

        private void CSearchLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
            LCount.Content = "Количество записей в таблице: " + KonkursData.Items.Count;
        }

        private void BRedKonk_Click(object sender, RoutedEventArgs e)
        {
            Window windowKonk = new WindowKonkurs((sender as Button).DataContext as Konkurs);
            windowKonk.ShowDialog();
            KonkursList();
            KonkursData.SelectedIndex= -1;
        }
    }
}
