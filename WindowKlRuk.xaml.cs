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
    /// Логика взаимодействия для WindowKlRuk.xaml
    /// </summary>
    public partial class WindowKlRuk : Window
    {
        private KlRuks ruk = new KlRuks();
        private Users user = new Users();
        public WindowKlRuk(KlRuks selectedKlruk)
        {
            InitializeComponent();
            if (selectedKlruk != null)
            {
                ruk = selectedKlruk;
            }
            DataContext = ruk;
        }

        private void BSaveKlRuk_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (TF_klruk.Text == "" || TI_klruk.Text == "" || TO_klruk.Text == "" || TLogin.Text == "" || TPassword.Text == "")
                errors.AppendLine("Заполните все поля!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ruk.ID_klruk == 0)
            {
                ClassEntities.GetContext().Users.Add(user);
                ClassEntities.GetContext().KlRuks.Add(ruk);
                ruk.User_id = user.ID_user;
            }
            user.Login = TLogin.Text;
            user.Password = TPassword.Text;
            user.Level = 2;
            ruk.F_klruk = TF_klruk.Text;
            ruk.I_klruk = TI_klruk.Text;
            ruk.O_klruk = TO_klruk.Text;          
            try {
                ClassEntities.GetContext().SaveChanges();
                MessageBox.Show("Классный руководитель добавлен", "Сохранение",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
    }
}
