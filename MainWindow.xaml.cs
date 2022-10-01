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
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MyClass
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entities = new Entities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            if (TLogin.Text == "" || PPassword.Password == "") //проверка заполняемости полей
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {       //проверка существования записи в БД
                    Users _user = (from el in entities.Users where 
                        el.Login == TLogin.Text && el.Password == PPassword.Password select el).First();
                    Bduser.user = _user;
                    if (_user.Level == 2)
                    {
                        var klruk = (from el in entities.KlRuks where el.User_id == _user.ID_user select el).Single<KlRuks>();
                        try
                        {
                            var klass = (from el in entities.Klasses where el.KlRuk_id == klruk.ID_klruk select el).First();
                            MessageBox.Show("Авторизация прошла успешно!", "Авторизация",
                                MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            Window window_main = new MainMenu();
                            this.Close();
                            window_main.ShowDialog();        
                        }
                        catch 
                        {
                            MessageBox.Show("На данный момент Вы не можете войти в систему!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Авторизация прошла успешно!", "Авторизация",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        Window window_adm = new MainMenu();
                        this.Close();
                        window_adm.ShowDialog();
                    }
                }
                catch
                {                
                        MessageBox.Show("Неправильный логин или пароль. \n Пожалуйста, введите данные заново",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);    
                }
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox.IsChecked.Value)
            {
                TPassword.Text = PPassword.Password;
                TPassword.Visibility = Visibility.Visible;
                PPassword.Visibility = Visibility.Hidden;
            }
            else
            {
                PPassword.Password = TPassword.Text;
                PPassword.Visibility = Visibility.Visible;
                TPassword.Visibility = Visibility.Hidden;
            }
        }
    }
}
