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
using Microsoft.Win32;

namespace MyClass
{
    /// <summary>
    /// Логика взаимодействия для WindowUch.xaml
    /// </summary>
    public partial class WindowUch : Window
    {
        private Ucheniki uchenik = new Ucheniki();
        private Roditeli rod = new Roditeli();
        private Dock docks = new Dock();
        private RodUch rd = new RodUch();
        private Family fam = new Family();
        public WindowUch(Ucheniki selectedUch)
        {
            InitializeComponent();
            if (selectedUch != null)
            {
                uchenik = selectedUch;
                var roditeli = ClassEntities.GetContext().RodUch.ToList().Where(p => p.Uch_id == selectedUch.ID_uch);
                RodData.ItemsSource = roditeli.ToList();
   
            }
            DataContext = uchenik;
            CPol_uch.ItemsSource = ClassEntities.GetContext().Pol.ToList();
            CLanguage_uch.ItemsSource = ClassEntities.GetContext().Languages.ToList();
            CKlass.ItemsSource = ClassEntities.GetContext().Klasses.ToList(); 
            
            if (uchenik.Image_uch != null)
            {
                BitmapImage im = new BitmapImage();
                im.BeginInit();
                im.UriSource = new Uri(uchenik.Image_uch, UriKind.RelativeOrAbsolute);
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
            if (Bduser.SetUser().Level != 1)
            {
                LKlass.Visibility = Visibility.Hidden;
                CKlass.Visibility = Visibility.Hidden;
            }
            CVidrods.ItemsSource = ClassEntities.GetContext().Vidrods.ToList();
            CObr.ItemsSource = ClassEntities.GetContext().Obrazovanie.ToList();
        }
        private void BSaveUch_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (TF_uch.Text == "" || TI_uch.Text == "" || TO_uch.Text == "")
                errors.AppendLine("Заполните ФИО обучающегося!");
            if (DDatarozh_uch.SelectedDate == null )
                errors.AppendLine("Выберите дату рождения обучающегося!");
            if (CPol_uch.SelectedIndex == -1 )
                errors.AppendLine("Выберите пол обучающегося!");
            if (CLanguage_uch.SelectedIndex == -1)
                errors.AppendLine("Выберите изучаемый язык обучающегося!");
            if (Bduser.SetUser().Level == 1)  {
                if (CKlass.SelectedIndex == -1)
                    errors.AppendLine("Выберите класс обучающегося!"); }
            if (TPhone_uch.Text!="")  {
                if (!ulong.TryParse(TPhone_uch.Text, out ulong a))
                    errors.AppendLine("Номер телефона введен некорректно!");  }
            if (TNum_svid.Text != "")  {
                if (!uint.TryParse(TNum_svid.Text, out uint a))
                    errors.AppendLine("Номер свидетельства о рождении введен некорректно!");  }
            if (TNum_pass.Text != "") {
                if (!uint.TryParse(TNum_pass.Text, out uint a))
                    errors.AppendLine("Номер паспорта введен некорректно!");  }
            if (TSeriya_pass.Text != "")  {
                if (!uint.TryParse(TSeriya_pass.Text, out uint a))
                    errors.AppendLine("Серия паспорта введена некорректно!");  }
            if (TINN.Text != "")  {
                if (!ulong.TryParse(TINN.Text, out ulong a))
                    errors.AppendLine("ИНН введен некорректно!"); }
            if (TSnils.Text != "")    {
                if (!ulong.TryParse(TSnils.Text, out ulong a))
                    errors.AppendLine("Снилс введен некорректно!"); }
            if (TMedpolis.Text != "")  {
                if (!ulong.TryParse(TMedpolis.Text, out ulong a))
                    errors.AppendLine("Номер медицинского полиса введен некорректно!");  }
            if (errors.Length > 0) 
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            if (uchenik.ID_uch == 0)
            {
                ClassEntities.GetContext().Ucheniki.Add(uchenik);
                ClassEntities.GetContext().Dock.Add(docks);
                ClassEntities.GetContext().Family.Add(fam);
                uchenik.Fam_id = fam.ID_fam;
                uchenik.Dock_id = docks.ID_dock;
            }
            if (Bduser.SetUser().Level != 1)
            {
                uchenik.Klass_id = Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass;
            }
            else {
                uchenik.Klass_id = (CKlass.SelectedItem as Klasses).ID_klass;
            }          
            uchenik.Image_uch = IImageUch.Source.ToString();       
            docks.Seriya_svid = TSeriya_svid.Text;
            docks.Num_svid = TNum_svid.Text;
            docks.Datavid_svid = DDatavid_svid.SelectedDate;
            docks.Kemvid_svid = TKem_svid.Text;
            docks.Seriya_pass = TSeriya_pass.Text;
            docks.Num_pass = TNum_pass.Text;
            docks.Datavid_pass = DDatavid_pass.SelectedDate;
            docks.Kemvid_pass = TKem_pass.Text;
            docks.INN = TINN.Text;
            docks.Snils = TSnils.Text;
            docks.Medpolis = TMedpolis.Text;
            if (ChFull.IsChecked == true) {
                fam.Full_fam = true; }
            else
                fam.Full_fam = false;
            if (ChSirota.IsChecked == true)  {
                fam.Sirota_fam = true;    }
            else
                fam.Sirota_fam = false;
            if (ChLowincome.IsChecked == true)  {
                fam.Lowincome_fam = true; }
            else
                fam.Lowincome_fam = false;
            if (ChDysfunctional.IsChecked == true) {
                fam.Dysfunctional_fam = true;}
            else
                fam.Dysfunctional_fam = false;
            if (ChFoster.IsChecked == true){
                fam.Foster_fam = true; }
            else
                fam.Foster_fam = false;
            if (ChLarge.IsChecked == true)  {
                fam.Large_fam = true; }
            else
                fam.Large_fam = false;
            try
            {
                ClassEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ExpDock_Expanded(object sender, RoutedEventArgs e)
        {
            ExpRod.IsExpanded = false;
            ExpFam.IsExpanded = false;
        }
        private void ExpRod_Expanded(object sender, RoutedEventArgs e)
        {
            ExpDock.IsExpanded = false;
            ExpFam.IsExpanded = false;
        }
        private void BAddRod_Click(object sender, RoutedEventArgs e)
        {  
            CVidrods.SelectedIndex = -1;
            CObr.SelectedIndex = -1;
            TF_rod.Text = "";
            TI_rod.Text = "";
            TO_rod.Text = "";
            TPhone_rod.Text = "";
            TMestorab_rod.Text = "";
            RodData.SelectedIndex = -1;
        }

        private void BSaveRod_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (TF_rod.Text == "" || TI_rod.Text == "" || TO_rod.Text == "")
                errors.AppendLine("Заполните ФИО родителя!");
            if (CVidrods.SelectedIndex == -1 )
                errors.AppendLine("Выберите степень родства!");
            if (CObr.SelectedIndex == -1)
                errors.AppendLine("Выберите образование родителя!");
            if (TPhone_rod.Text == "")
                errors.AppendLine("Введите номер телефона родителя!");
           
            if (!ulong.TryParse(TPhone_rod.Text, out ulong a))
                errors.AppendLine("Номер телефона введен некорректно!");          
            if (uchenik.ID_uch == 0)
                errors.AppendLine("Необходимо добавить обучающегося!");
            if (errors.Length > 0) {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var selectedRod = RodData.SelectedItem as RodUch;
            if (selectedRod == null) {
                ClassEntities.GetContext().Roditeli.Add(rod);
                ClassEntities.GetContext().RodUch.Add(rd);
                rod.F_rod = TF_rod.Text;
                rod.I_rod = TI_rod.Text;
                rod.O_rod = TO_rod.Text;
                rod.Mestorab_rod = TMestorab_rod.Text;
                rod.Phone_rod = TPhone_rod.Text;         
                rod.Obr_id = (CObr.SelectedItem as Obrazovanie).ID_obr;                        
                rod.Vidrod_id = (CVidrods.SelectedItem as Vidrods).ID_vidrod;
            }
            else  {
                selectedRod.Roditeli.F_rod = TF_rod.Text;
                selectedRod.Roditeli.I_rod = TI_rod.Text;
                selectedRod.Roditeli.O_rod = TO_rod.Text;
                selectedRod.Roditeli.Mestorab_rod = TMestorab_rod.Text;
                selectedRod.Roditeli.Phone_rod = TPhone_rod.Text;
                selectedRod.Roditeli.Obr_id = (CObr.SelectedItem as Obrazovanie).ID_obr;
                selectedRod.Roditeli.Vidrod_id = (CVidrods.SelectedItem as Vidrods).ID_vidrod;
            }
            rd.Rod_id = rod.ID_rod;
            rd.Uch_id = uchenik.ID_uch;
            try  {
                ClassEntities.GetContext().SaveChanges();
                var roditeli = ClassEntities.GetContext().RodUch.ToList().Where(p => p.Uch_id == uchenik.ID_uch);
                RodData.ItemsSource = roditeli.ToList();
                MessageBox.Show("Родитель сохранен!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BDelRod_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить родителя?",
              "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            var selectedRd = RodData.SelectedItem as RodUch;
            var rod = ClassEntities.GetContext().Roditeli.ToList().Where(p => p.ID_rod == selectedRd.Rod_id).First();
            if (selectedRd != null)
            {
                try
                {
                    ClassEntities.GetContext().RodUch.Remove(selectedRd);
                    ClassEntities.GetContext().Roditeli.Remove(rod);
                    ClassEntities.GetContext().SaveChanges();
                    var roditeli = ClassEntities.GetContext().RodUch.ToList().Where(p => p.Uch_id == uchenik.ID_uch);
                    RodData.ItemsSource = roditeli.ToList();
                    MessageBox.Show("Родитель удален!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;
            if (ofdPicture.ShowDialog() == true)
                IImageUch.Source =
                    new BitmapImage(new Uri(ofdPicture.FileName));
        }

      
        private void RodData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRod = RodData.SelectedItem as RodUch;
            if (selectedRod != null)
            {
                TF_rod.Text = selectedRod.Roditeli.F_rod;
                TI_rod.Text = selectedRod.Roditeli.I_rod;
                TO_rod.Text = selectedRod.Roditeli.O_rod;
                TPhone_rod.Text = selectedRod.Roditeli.Phone_rod;
                TMestorab_rod.Text = selectedRod.Roditeli.Mestorab_rod;
                CVidrods.SelectedItem = (from el in ClassEntities.GetContext().Vidrods where el.ID_vidrod==selectedRod.Roditeli.Vidrod_id select el).Single<Vidrods>();
               
                    CObr.SelectedItem = (from el in ClassEntities.GetContext().Obrazovanie where el.ID_obr == selectedRod.Roditeli.Obr_id select el).Single<Obrazovanie>();
                      
            }
            else
            {
                TF_rod.Text = "";
                TI_rod.Text = "";
                TO_rod.Text = "";
                TPhone_rod.Text = "";
                TMestorab_rod.Text = "";
                CVidrods.SelectedIndex = -1;
                CObr.SelectedIndex = -1;
            }
        }

      

        private void ExpFam_Expanded(object sender, RoutedEventArgs e)
        {
            ExpRod.IsExpanded = false;
            ExpDock.IsExpanded = false;
        }
    }
}
