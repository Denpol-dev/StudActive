using StudActive.Entities;
using StudActive.Models;
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
using System.Windows.Shapes;
using StudActive.ViewModels;
using System.Diagnostics;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Windows.Threading;
using System.ComponentModel;

namespace StudActive.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlurEffect myEffect = new BlurEffect();
        StudentsViewModel studentsViewModel = new StudentsViewModel();
        DutyListViewModel dutyListViewModel = new DutyListViewModel();
        AccountModel account = new AccountModel();
        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }
        public MainWindow(AccountModel accountModel) : this()
        {
            account = accountModel;
        }

        #region Загрузка окна
        /// <summary>
        /// Загрузка всех полей на форме
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserNameTextBlock.Text = account.FirstName + " " + account.MiddleName;

            //Личный кабинет
            UserNamePersonalArea.Text = account.LastName + " " + account.FirstName + " " + account.MiddleName;
            StudentsActiveModel userInfo = studentsViewModel.GetStudentActive(account.Id);
            PhoneNumberPersonalArea.Text = userInfo.MobilePhone;
            RolePersonalArea.Text = userInfo.Role;
            VKLink.NavigateUri = new Uri(userInfo.VkLink);
            RunVKLink.Text = userInfo.VkLink;
            CouncilName.Text = userInfo.CouncilName;

            //Панель студентов
            StudActivesDataGrid.ItemsSource = studentsViewModel.GetStudentsActive(account.Id);
            StudentGridReg.ItemsSource = studentsViewModel.GetAllStudents();
            cbGroupName.ItemsSource = studentsViewModel.GetAllGroups()/*.Select(x => x.GroupName)*/;
            cbGroupName.DisplayMemberPath = "GroupName";
            cbRoleActive.ItemsSource = studentsViewModel.GetAllRolesStudActiveModel().OrderBy(x => x.NameRU);

            //График дежурств
            DutyListDataGrid.ItemsSource = dutyListViewModel.GetDutyList();
        }
        #endregion

        #region Панель управления окном
        private void CloseWin_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
        }

        private void CollapseWin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void RestoreWin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            MaxWin.Visibility = Visibility.Visible;
            RestoreWin.Visibility = Visibility.Collapsed;
        }
        
        private void MaxWin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            MaxWin.Visibility = Visibility.Collapsed;
            RestoreWin.Visibility = Visibility.Visible;
        }

        private void Window_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            DragMove();
        }

        private void CanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void CanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private void CloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void MaximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void MinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void RestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }


        private void ShowSystemMenu(object sender, ExecutedRoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element == null)
                return;

            var point = WindowState == WindowState.Maximized ? new Point(0, element.ActualHeight)
                : new Point(Left + BorderThickness.Left, element.ActualHeight + Top + BorderThickness.Top);
            point = element.TransformToAncestor(this).Transform(point);
            SystemCommands.ShowSystemMenu(this, point);
        }

        #endregion

        #region Гамбургер меню
        private void DarkModeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DarkMode.IsChecked.Value)
                DarkMode.IsChecked = false;
            else
                DarkMode.IsChecked = true;
        }

        private void DarkMode_Unchecked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml",
                UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries[0] = dictionary;
            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }

        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml",
                UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries[0] = dictionary;
            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }

        private void Main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            myEffect.Radius = 10;
            MainGrid.Effect = myEffect;
        }

        private void PersonalAreaButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalArea.Visibility = Visibility.Visible;
            StudentsGrid.Visibility = Visibility.Collapsed;
            DutyListGrid.Visibility = Visibility.Collapsed;

            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalArea.Visibility = Visibility.Collapsed;
            StudentsGrid.Visibility = Visibility.Visible;
            DutyListGrid.Visibility = Visibility.Collapsed;

            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }

        private void DutyListButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalArea.Visibility = Visibility.Collapsed;
            StudentsGrid.Visibility = Visibility.Collapsed;
            DutyListGrid.Visibility = Visibility.Visible;

            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }
        #endregion

        #region Панель студентов
        private void StudentsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void CreateStudActive_Click(object sender, RoutedEventArgs e)
        {
            CreateStudActiveStack.Visibility = Visibility.Visible;
            StudActiveGridPanel.Visibility = Visibility.Collapsed;
        }

        private void BackToStudActivesGrid_Click(object sender, RoutedEventArgs e)
        {
            CreateStudActiveStack.Visibility = Visibility.Collapsed;
            StudActiveGridPanel.Visibility = Visibility.Visible;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            List<RegistrationStudActiveModel> regModel = new List<RegistrationStudActiveModel>();

            if (StudentIdText.Text is not null)
            {
                StudentsActiveModel userInfo = studentsViewModel.GetStudentActive(account.Id);
                RolesStudActiveModel cbRoleActiveSelected = (RolesStudActiveModel)cbRoleActive.SelectedItem;

                Guid? studentCouncilId = studentsViewModel.GetStudentCouncilId(userInfo.Id);

                regModel.Add(new RegistrationStudActiveModel
                {
                    StudentId = new Guid(StudentIdText.Text),
                    IsArchive = false,
                    RoleActive = cbRoleActiveSelected.RoleId,
                    VkLink = VkLink.Text,
                    StudentCouncilId = account.Id
                });
                studentsViewModel.CreateAgainStudentActive();
            }
            else
            {
                regModel.Add(new RegistrationStudActiveModel
                {

                });
                studentsViewModel.CreateFullNewStudentActive();
            }
        }

        private void SelectStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentsDataGridStack.Visibility = Visibility.Visible;

            BlurEffect effect = new BlurEffect();
            effect.Radius = 10;
            NewStudentStack.Effect = effect;
        }

        private void StudentGridReg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StudentGridReg.SelectedItem != null)
            {
                var row = (DataGridRow)sender;
                if (row.DataContext is not StudentsModel context) return;
                if (context.Fio != "Пусто")
                {
                    string[] fio = context.Fio.Split();
                    GroupsModel group = new GroupsModel();
                    group.GroupId = context.GroupId;
                    group.GroupName = context.GroupNumber;

                    int sex = context.Sex;
                    DateTime? birthDate = context.BirthDate;
                    string mobilePhone = context.MobilePhone;

                    FirstName.Text = fio[0];
                    MiddleName.Text = fio[1];
                    LastName.Text = fio[2];

                    entryDatePicker.SelectedDate = DateTime.Now;

                    Phone.Text = mobilePhone;

                    birthDatePicker.SelectedDate = birthDate;

                    cbGroupName.Text = group.GroupName;

                    StudentIdText.Text = context.Id.ToString();
                }
                else
                {
                    StudentIdText.Text = null;
                }

                BlurEffect effect = new BlurEffect();
                effect.Radius = 0;
                NewStudentStack.Effect = effect;
                StudentsDataGridStack.Visibility = Visibility.Collapsed;
            }
        }
        public string SelectedUnit
        {
            set
            {

            }
        }
        private void cbGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            cbGroupName.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(cbGroupName.ItemsSource);
            cv.Filter = s =>
            {
                var group = (GroupsModel)s;
                return group.GroupName.Contains(cbGroupName.Text, StringComparison.CurrentCultureIgnoreCase);
            };
        }
        #endregion

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
        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            if (NewPasswordChange.Password == NewRepeatPasswordChange.Password)
            {
                bool result = studentsViewModel.ChangePass(NewPasswordChange.Password, account.Id);

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

        #region График дежурств
        private void DutyListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (DutyListDataGrid.SelectedItem != null)
            //{
            //    Guid id;
            //    var row = (DataGridRow)sender;
            //    if (row.DataContext is not Guid context) return;
            //    id = context.;

            //    bool check = dutyListViewModel.SaveDuty(id);
            //    if (check)
            //    {
            //        MessageBox.Show("Сохранение успешно!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //}
        }        

        #endregion

        #region Личный кабинет

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(VKLink.NavigateUri.ToString()) { UseShellExecute = true });
        }
        #endregion

        private void CloseStudentsDataGridStack_Click(object sender, RoutedEventArgs e)
        {
            StudentsDataGridStack.Visibility = Visibility.Collapsed;

            BlurEffect effect = new BlurEffect();
            effect.Radius = 0;
            NewStudentStack.Effect = effect;
        }
    }
}
