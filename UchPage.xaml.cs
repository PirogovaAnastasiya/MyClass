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
using Microsoft.Win32;

namespace MyClass
{
    /// <summary>
    /// Логика взаимодействия для UchPage.xaml
    /// </summary>
    public partial class UchPage : Page
    {
        public UchPage()
        {
            InitializeComponent();
            CPol_uch.ItemsSource = ClassEntities.GetContext().Pol.ToList();
            CLanguage_uch.ItemsSource = ClassEntities.GetContext().Languages.ToList();
            var allPols= ClassEntities.GetContext().Pol.ToList();
            allPols.Insert(0, new Pol
            {
                N_pol = "Все"
            });
            CSearchPol.ItemsSource = allPols.ToList();
            CSearchPol.SelectedIndex = 0;
            var allLang = ClassEntities.GetContext().Languages.ToList();
            allLang.Insert(0, new Languages
            {
                N_lang = "Все"
            });
            CSearchLang.ItemsSource = allLang.ToList();
            CSearchLang.SelectedIndex = 0;
            UchList();
            LCount.Content ="Количество записей в таблице: "+ UchData.Items.Count;
        }       
        private void UchData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUch = UchData.SelectedItem as Ucheniki;
            if (selectedUch != null)
            {
                TF_uch.Text = selectedUch.F_uch;
                TI_uch.Text = selectedUch.I_uch;
                TO_uch.Text = selectedUch.O_uch;
                DDatarozh_uch.SelectedDate = selectedUch.Datarozh_uch;
                CPol_uch.SelectedItem = (from el in ClassEntities.GetContext().Pol where 
                    el.ID_pol == selectedUch.Pol_id select el).Single<Pol>();
              
                    CLanguage_uch.SelectedItem = (from el in ClassEntities.GetContext().Languages where el.ID_lang == selectedUch.Language_id select el).Single<Languages>();
                
                
                TPhone_uch.Text = selectedUch.Phone_uch;
                TAdres_uch.Text = selectedUch.Adres_uch;
                TNacion_uch.Text = selectedUch.Nation_uch;
                TZach_uch.Text = selectedUch.Zach_uch;
                TOtch_uch.Text = selectedUch.Otch_uch;
                if (selectedUch.Image_uch != null)
                {
                    BitmapImage im = new BitmapImage();
                    im.BeginInit();
                    im.UriSource = new Uri(selectedUch.Image_uch, UriKind.RelativeOrAbsolute);
                    IImageUch.Source = im;
                    im.EndInit();
                }
                else
                {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(@"image\defaultimageuser.png", UriKind.RelativeOrAbsolute);
                    IImageUch.Source = img;
                    img.EndInit();
                } 
                TSeriya_svid.Text = selectedUch.Dock.Seriya_svid;
                TNum_svid.Text = selectedUch.Dock.Num_svid;
                TKem_svid.Text = selectedUch.Dock.Kemvid_svid;
                DDatavid_svid.SelectedDate = selectedUch.Dock.Datavid_svid;
                TSeriya_pass.Text = selectedUch.Dock.Seriya_pass;
                TNum_pass.Text = selectedUch.Dock.Num_pass;
                TKem_pass.Text = selectedUch.Dock.Kemvid_pass;
                DDatavid_pass.SelectedDate = selectedUch.Dock.Datavid_pass;
                TINN.Text = selectedUch.Dock.INN;
                TSnils.Text = selectedUch.Dock.Snils;
                TMedpolis.Text = selectedUch.Dock.Medpolis;
                var roditeli = ClassEntities.GetContext().RodUch.ToList().Where(p => p.Uch_id == selectedUch.ID_uch);
                RodData.ItemsSource = roditeli.ToList();
                ChFull.IsChecked = selectedUch.Family.Full_fam;
                ChSirota.IsChecked = selectedUch.Family.Sirota_fam;
                ChLowincome.IsChecked = selectedUch.Family.Lowincome_fam;
                ChDysfunctional.IsChecked = selectedUch.Family.Dysfunctional_fam;
                ChFoster.IsChecked = selectedUch.Family.Foster_fam;
                ChLarge.IsChecked = selectedUch.Family.Large_fam;
            }
            else            
            {
                TF_uch.Text = "";
                TI_uch.Text = "";
                TO_uch.Text = "";
                DDatarozh_uch.SelectedDate = null;
                CPol_uch.SelectedIndex = -1;
                CLanguage_uch.SelectedIndex = -1;
                TPhone_uch.Text = "";
                TAdres_uch.Text = "";
                TNacion_uch.Text = "";
                TZach_uch.Text = "";
                TOtch_uch.Text = "";
                TSeriya_svid.Text = "";
                TNum_svid.Text = "";
                TKem_svid.Text = "";
                DDatavid_svid.SelectedDate = null;
                TSeriya_pass.Text = "";
                TNum_pass.Text = "";
                TKem_pass.Text = "";
                DDatavid_pass.SelectedDate = null;
                TINN.Text = "";
                TSnils.Text = "";
                TMedpolis.Text = "";
                ChFull.IsChecked = false;
                ChSirota.IsChecked = false;
                ChLowincome.IsChecked = false;
                ChDysfunctional.IsChecked = false;
                ChFoster.IsChecked = false;
                ChLarge.IsChecked = false;
            }
        }
        private void BAddUch_Click(object sender, RoutedEventArgs e)
        {
            Window windowAddUch = new WindowUch(null);
            windowAddUch.ShowDialog();
            UchList();
            UchData.SelectedIndex= -1;
            LCount.Content = "Количество записей в таблице: " + UchData.Items.Count;
        }
        private void BRedUch_Click(object sender, RoutedEventArgs e)
        {
            Window windowAddUch = new WindowUch((sender as Button).DataContext as Ucheniki);
            windowAddUch.ShowDialog();
            UchList();
            UchData.SelectedIndex = -1;
        }
        private void UchList()
        {
            var uchList = ClassEntities.GetContext().Ucheniki.ToList().Where(p => p.Klass_id == Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            UchData.ItemsSource = uchList.ToList();
        }
        private void BDelUch_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить запись?",
               "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            var selectedUch = UchData.SelectedItem as Ucheniki;
            var dock = ClassEntities.GetContext().Dock.Where(p => p.ID_dock == selectedUch.Dock_id).First();
            var family = ClassEntities.GetContext().Family.Where(p => p.ID_fam == selectedUch.Fam_id).First();
            var allRd = ClassEntities.GetContext().RodUch.ToList().Where(p => p.Uch_id == selectedUch.ID_uch);
            var allKonk = ClassEntities.GetContext().Konkurs.Where(p => p.Uch_id == selectedUch.ID_uch);
            if (selectedUch != null)
            {
                try
                {
                    if (allRd != null) {
                        foreach (var rd in allRd) {
                            var allRod = ClassEntities.GetContext().Roditeli.ToList().Where(p => p.ID_rod == rd.Rod_id);
                            if (allRod != null) {
                                foreach (var rod in allRod)  {
                                    ClassEntities.GetContext().Roditeli.Remove(rod);  }  }
                            ClassEntities.GetContext().RodUch.Remove(rd);  } }
                    if (allKonk != null)  {
                        foreach (var konk in allKonk) {
                            ClassEntities.GetContext().Konkurs.Remove(konk); }
                    }
                    ClassEntities.GetContext().Dock.Remove(dock);
                    ClassEntities.GetContext().Family.Remove(family);
                    ClassEntities.GetContext().Ucheniki.Remove(selectedUch);
                    ClassEntities.GetContext().SaveChanges();
                    UchList();
                    MessageBox.Show("Запись удалена!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    LCount.Content = "Количество записей в таблице: " + UchData.Items.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public void Filter()
        {
            var filterUch = ClassEntities.GetContext().Ucheniki.ToList().Where(p => p.Klass_id
                == Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            filterUch= filterUch.Where(p => p.F_uch.ToLower().Contains(TSearchFIO.Text.ToLower()) ||
                p.I_uch.ToLower().Contains(TSearchFIO.Text.ToLower()) ||
                 p.O_uch.ToLower().Contains(TSearchFIO.Text.ToLower())).ToList();
            if (CSearchPol.SelectedIndex > 0)
            {
                filterUch = filterUch.Where(p => p.Pol_id.ToString().Contains((CSearchPol.SelectedItem
                    as Pol).ID_pol.ToString())).ToList();
            }
            if (CSearchLang.SelectedIndex > 0)
            {
                filterUch = filterUch.Where(p => p.Language_id.ToString().Contains((CSearchLang.SelectedItem
                    as Languages).ID_lang.ToString())).ToList();
            }
            UchData.ItemsSource = filterUch.ToList();
            RodData.ItemsSource = null;
        }
        private void TSearchFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
            LCount.Content = "Количество записей в таблице: " + UchData.Items.Count;
        }
        private void CSearchPol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
            LCount.Content = "Количество записей в таблице: " + UchData.Items.Count;
        }
        private void CSearchLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
            LCount.Content = "Количество записей в таблице: " + UchData.Items.Count;
        }
       
    }
}
