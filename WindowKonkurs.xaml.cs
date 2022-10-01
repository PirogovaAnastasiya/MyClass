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
    /// Логика взаимодействия для WindowKonkurs.xaml
    /// </summary>
    public partial class WindowKonkurs : Window
    {
        private Konkurs konk = new Konkurs();
        public WindowKonkurs(Konkurs selectedKonk)
        {
            InitializeComponent();
            if (selectedKonk != null)
            {
                konk = selectedKonk;
            }
            DataContext = konk;
            CLevel.ItemsSource = ClassEntities.GetContext().LevelKonkurs.ToList();
            var db = ClassEntities.GetContext().Ucheniki.ToList().Where(p => p.Klass_id == Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            CUch.ItemsSource = db.ToList();
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BSaveKonkurs_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (TN_konk.Text == "" )
                errors.AppendLine("Введите название конкурса!");
            if (DData_konk.SelectedDate == null )
                errors.AppendLine("Введите дату конкурса!");
            if (CLevel.SelectedIndex == -1)
                errors.AppendLine("Выберите уровень конкурса!");
            if (TResult_konk.Text == "")
                errors.AppendLine("Введите результат конкурса!");
            if (CUch.SelectedIndex== -1)
                errors.AppendLine("Выберите ученика, учавствовавшего в конкурса!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (konk.ID_konk == 0)
            {
                ClassEntities.GetContext().Konkurs.Add(konk);
            }
            konk.Klass_id= Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass;
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
