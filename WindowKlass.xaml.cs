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
    /// Логика взаимодействия для WindowKlass.xaml
    /// </summary>
    public partial class WindowKlass : Window
    {
        private Klasses klass = new Klasses();
        public WindowKlass(Klasses selectedKlass)
        {
            InitializeComponent();
            if (selectedKlass != null)
            {
                klass = selectedKlass;
                TKlruk.Text = selectedKlass.KlRuks.F_klruk + " " + selectedKlass.KlRuks.I_klruk + " " + selectedKlass.KlRuks.O_klruk;
            }
            DataContext = klass;
            KData.ItemsSource = ClassEntities.GetContext().KlRuks.ToList();
        }

        private void BSaveKlass_Click(object sender, RoutedEventArgs e)
        {
            var selectedKlRuk = KData.SelectedItem as KlRuks;
            StringBuilder errors = new StringBuilder();
            if (TN_klass.Text == ""  )
                errors.AppendLine("Введите название класса!");
            if (TKlruk.Text == "")
                errors.AppendLine("Выберите классного руководителя!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectedKlRuk != null)
            {
                try
                {
                    var existKlRuk = (from el in ClassEntities.GetContext().Klasses where el.KlRuk_id == selectedKlRuk.ID_klruk select el).First();
                    MessageBox.Show("Данный классный руководитель уже занят!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch
                {
                    if (klass.ID_klass == 0)
                    {
                        ClassEntities.GetContext().Klasses.Add(klass);
                    }
                    klass.N_klass = TN_klass.Text;
                    klass.KlRuk_id = selectedKlRuk.ID_klruk;
                    ClassEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные сохранены!", "Сохранение",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void KData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selektedKlruk = KData.SelectedItem as KlRuks;
            if (selektedKlruk != null)
            {
                TKlruk.Text = selektedKlruk.ToString();
            }
            else
            {
                TKlruk.Text= "";
            }
        }
    }
}
