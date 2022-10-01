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
    /// Логика взаимодействия для MerPage.xaml
    /// </summary>
    public partial class MerPage : Page
    {
        public MerPage()
        {
            InitializeComponent();
            CStatus.ItemsSource = ClassEntities.GetContext().StatusMer.ToList();
            CVidMer.ItemsSource = ClassEntities.GetContext().VidMer.ToList();
            var allStatus = ClassEntities.GetContext().StatusMer.ToList();
            allStatus.Insert(0, new StatusMer
            { 
               N_status= "Все"
            });
            CSearchStatus.ItemsSource = allStatus.ToList();
            CSearchStatus.SelectedIndex = 0;
            var allVidMer = ClassEntities.GetContext().VidMer.ToList();
            allVidMer.Insert(0, new VidMer
            {
                N_vidmer="Все"
            });
            CSearchVidMer.ItemsSource = allVidMer.ToList();
            CSearchVidMer.SelectedIndex = 0;
            MerList();
            LCount.Content = "Количество записей в таблице: " + MerData.Items.Count;
        }
        private void MerList()
        {
            var db = ClassEntities.GetContext().Meropriyatia.ToList().Where(p => p.Klass_id == Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            MerData.ItemsSource = db.ToList();
        }
        private void BAddMer_Click(object sender, RoutedEventArgs e)
        {
            Window windowMer = new WindowMer(null);
            windowMer.ShowDialog();
            MerList();
            MerData.SelectedIndex = -1;
            LCount.Content = "Количество записей в таблице: " + MerData.Items.Count;
        }
        private void BDelMer_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить запись?",
               "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }          
            var selectedMer = MerData.SelectedItem as Meropriyatia;
            if (selectedMer != null)
            {
                try
                {
                    ClassEntities.GetContext().Meropriyatia.Remove(selectedMer);
                    ClassEntities.GetContext().SaveChanges();
                    MerList();
                    MerData.SelectedIndex = -1;
                    MessageBox.Show("Запись удалена!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    LCount.Content = "Количество записей в таблице: " + MerData.Items.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

      

        private void MerData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMer = MerData.SelectedItem as Meropriyatia;
            if (selectedMer != null)
            {
                DData_meropr.SelectedDate = selectedMer.Data_mer;
                TNazv_meropr.Text = selectedMer.N_mer;
                TOpisanie.Text = selectedMer.Opisanie;
                CStatus.SelectedItem= (from el in ClassEntities.GetContext().StatusMer where el.ID_status == selectedMer.Status_id select el).Single<StatusMer>();
                CVidMer.SelectedItem = (from el in ClassEntities.GetContext().VidMer where el.ID_vidmer==selectedMer.Vidmer_id select el).Single<VidMer>();
            }
            else
            {
                DData_meropr.SelectedDate = null;
                TNazv_meropr.Text = "";
                TOpisanie.Text = "";
                CStatus.SelectedIndex = -1;
                CVidMer.SelectedIndex = -1;
            }
        }
        public void Filter()
        {
            var filterMer = ClassEntities.GetContext().Meropriyatia.ToList().Where(p => p.Klass_id == Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            filterMer = filterMer.Where(p => p.N_mer.ToLower().Contains(TSearchN.Text.ToLower())).ToList();
            if (CSearchStatus.SelectedIndex > 0)
            {
                filterMer = filterMer.Where(p => p.Status_id.ToString().Contains((CSearchStatus.SelectedItem
                    as StatusMer).ID_status.ToString())).ToList();
            }
            if (CSearchVidMer.SelectedIndex > 0)
            {
                filterMer = filterMer.Where(p => p.Vidmer_id.ToString().Contains((CSearchVidMer.SelectedItem
                    as VidMer).ID_vidmer.ToString())).ToList();
            }
            MerData.ItemsSource = filterMer.ToList();
        }

        private void TSearchN_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
            LCount.Content = "Количество записей в таблице: " + MerData.Items.Count;
        }

        private void CSearchStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
            LCount.Content = "Количество записей в таблице: " + MerData.Items.Count;
        }

        private void BRedMer_Click(object sender, RoutedEventArgs e)
        {
            Window windowMer = new WindowMer((sender as Button).DataContext as Meropriyatia);
            windowMer.ShowDialog();
            MerList();
            MerData.SelectedIndex = -1;
        }

        private void CSearchVidMer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
            LCount.Content = "Количество записей в таблице: " + MerData.Items.Count;
        }
    }
}
