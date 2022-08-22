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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginModel model = new LoginModel();

            if (LoginText.Text != null && Password.Password != null)
            {
                BlurEffect myEffect = new BlurEffect();
                myEffect.Radius = 10;
                MainGrid.Effect = myEffect;
                model.UserName = LoginText.Text;
                model.Password = Password.Password;
                AccountModel account = _accountViewModels.LoginHash(model);
                bool checkStudActive = _accountViewModels.CheckStudActive(model);
                if (checkStudActive)
                {
                    if ((account != null) && (account.Role == "Студент"))
                    {
                        myEffect.Radius = 0;
                        MainGrid.Effect = myEffect;
                        MainWindow m = new MainWindow(account);
                        Hide();
                        m.Show();
                    }
                    else
                    {
                        myEffect.Radius = 0;
                        MainGrid.Effect = myEffect;
                        ErrorLabel.Text = "Неверный логин или пароль";
                    }
                }
                else
                {
                    myEffect.Radius = 0;
                    MainGrid.Effect = myEffect;
                    ErrorLabel.Text = "Вас нет ни в одном списке студенческих советов. Обратитесь к председателю.";
                }
            }
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
