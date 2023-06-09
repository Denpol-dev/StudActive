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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudActive.Entities;
using StudActive.Models;
using StudActive.ViewModels;
using StudActive.Views;

namespace StudActive
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        AccountViewModel _accountViewModels = new AccountViewModel();
        public Login()
        {
            InitializeComponent();
        }

        private void CloseWin_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
        }

        private void CollapseWin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var model = new LoginModel();

            if (LoginText.Text != null && Password.Password != null)
            {
                RoundLoader.Visibility = Visibility.Visible;
                var myEffect = new BlurEffect();
                myEffect.Radius = 10;
                MainGrid.Effect = myEffect;
                model.UserName = LoginText.Text;
                model.Password = Password.Password;
                var account = await _accountViewModels.LoginHash(model);
                bool checkStudActive = _accountViewModels.CheckStudActive(model);
                if (account != null)
                {
                    if (checkStudActive)
                    {
                        if ((account.Id != Guid.Empty)/* && (account.Role == "Студент")*/)
                        {
                            RoundLoader.Visibility = Visibility.Collapsed;
                            myEffect.Radius = 0;
                            MainGrid.Effect = myEffect;
                            var m = new MainWindow(account);
                            Hide();
                            m.Show();
                        }
                        else
                        {
                            RoundLoader.Visibility = Visibility.Collapsed;
                            myEffect.Radius = 0;
                            MainGrid.Effect = myEffect;
                            ErrorLabel.Text = "Неверный логин или пароль";
                            Password.Password = "";
                        }
                    }
                    else
                    {
                        RoundLoader.Visibility = Visibility.Collapsed;
                        myEffect.Radius = 0;
                        MainGrid.Effect = myEffect;
                        ErrorLabel.Text = "Вас нет ни в одном списке студенческих советов. Обратитесь к председателю.";
                        Password.Password = "";
                    }
                }
                else
                {
                    RoundLoader.Visibility = Visibility.Collapsed;
                    myEffect.Radius = 0;
                    MainGrid.Effect = myEffect;
                    ErrorLabel.Text = "Нет доступа к базе данных, проверьте подключение к интернету.";
                    Password.Password = "";
                }
            }
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
