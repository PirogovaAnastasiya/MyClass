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
    /// Логика взаимодействия для WindowMer.xaml
    /// </summary>
    public partial class WindowMer : Window
    {
        private Meropriyatia mer = new Meropriyatia();
        public WindowMer(Meropriyatia selectedMer)
        {
            InitializeComponent();
            if (selectedMer != null)
            {
                mer = selectedMer;
            }
            DataContext = mer;
            CStatus.ItemsSource = ClassEntities.GetContext().StatusMer.ToList();
            CVidMer.ItemsSource = ClassEntities.GetContext().VidMer.ToList();
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BSaveMer_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (TNazv_meropr.Text == "" )
                errors.AppendLine("Заполните название мероприятия!");
            if (DData_meropr.SelectedDate == null )
                errors.AppendLine("Введите дату проведения мероприятия!");
            if (CVidMer.SelectedIndex == -1)
                errors.AppendLine("Выберите вид мероприятия!");
            if (CStatus.SelectedIndex == -1)
                errors.AppendLine("Выберите статус выполнения мероприятия!");           
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (mer.ID_mer== 0)
            {
                ClassEntities.GetContext().Meropriyatia.Add(mer);
            }
            mer.Klass_id = Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass;
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
    }
}
