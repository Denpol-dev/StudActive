using StudActive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Effects;
using StudActive.ViewModels;
using System.Diagnostics;
using System.Data;
using System.Collections.ObjectModel;
using StudActive.Entities;

namespace StudActive.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonalArea.xaml
    /// </summary>
    public partial class PersonalArea : Page
    {
        BlurEffect myEffect = new BlurEffect();
        StudentsViewModel studentsViewModel = new StudentsViewModel();
        DutyListViewModel dutyListViewModel = new DutyListViewModel();
        AccountModel account = new AccountModel();
        public PersonalArea()
        {
            InitializeComponent();
        }

        public PersonalArea(AccountModel accountModel) : this()
        {
            account = accountModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Личный кабинет
            UserNamePersonalArea.Text = account.LastName + " " + account.FirstName + " " + account.MiddleName;
            var userInfo = await StudentsViewModel.GetStudentActive(account.Id);
            PhoneNumberPersonalArea.Text = userInfo.MobilePhone;
            RolePersonalArea.Text = userInfo.Role;
            VKLink.NavigateUri = new Uri(userInfo.VkLink);
            RunVKLink.Text = userInfo.VkLink;
            CouncilName.Text = userInfo.CouncilName;
        }

        #region Смена пароля

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            BlurEffect effect = new BlurEffect();
            effect.Radius = 10;
            PersonalAreaStack.Effect = effect;

            ChangePasswordStack.Visibility = Visibility.Visible;
        }
        private void CloseChangePassword_Click(object sender, RoutedEventArgs e)
        {
            BlurEffect effect = new BlurEffect();
            effect.Radius = 0;
            PersonalAreaStack.Effect = effect;

            ChangePasswordStack.Visibility = Visibility.Collapsed;
        }
        private async void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            if (NewPasswordChange.Password == NewRepeatPasswordChange.Password)
            {
                var student = new StudentsViewModel();
                bool result = await student.ChangePass(NewPasswordChange.Password, account.Id);

                if (result)
                {
                    OldPasswordChange.Password = null;
                    NewPasswordChange.Password = null;
                    NewRepeatPasswordChange = null;
                    ChangePasswordStack.Visibility = Visibility.Collapsed;
                    BlurEffect effect = new BlurEffect();
                    effect.Radius = 0;
                    PersonalAreaStack.Effect = effect;
                    MessageBox.Show("Пароль успешно изменен.", "Изменение пароля", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка в изменении пароля, попробуйте позже.", "Изменение пароля", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        #endregion

        #region Личный кабинет

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(VKLink.NavigateUri.ToString()) { UseShellExecute = true });
        }
        #endregion
    }
}
