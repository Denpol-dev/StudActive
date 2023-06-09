using StudActive.Models;
using System;
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

namespace StudActive.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<GroupsModel> groups = new();
        private readonly ObservableCollection<RolesStudActiveModel> roles = new();
        private readonly ObservableCollection<StudentsActiveModel> studActives = new();
        private readonly ObservableCollection<StudentsModel> nonRegistrStudActives = new();
        private readonly ObservableCollection<InventoryModel> inventory = new();
        private readonly ObservableCollection<DutyListModel> dutyList = new();

        StudentsActiveModel selectedStudActive = new();
        DutyListModel SelectedDutyList = new();

        BlurEffect myEffect = new();
        StudentsViewModel studentsViewModel = new();
        DutyListViewModel dutyListViewModel = new();
        AccountModel account = new();

        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            cbGroupName.ItemsSource = groups;
            cbGroupNameChange.ItemsSource = groups;

            cbRoleActive.ItemsSource = roles;
            cbRoleActiveChange.ItemsSource = roles;

            StudActivesDataGrid.ItemsSource = studActives;
            StudentGridReg.ItemsSource = nonRegistrStudActives;

            DutyListDataGrid.ItemsSource = dutyList;

            InventoryDataGrid.ItemsSource = inventory;
        }
        public MainWindow(AccountModel accountModel) : this()
        {
            account = accountModel;
            App.userId = account.Id;
        }

        #region Загрузка окна
        /// <summary>
        /// Загрузка всех полей на форме
        /// </summary>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var userInfo = await StudentsViewModel.GetStudentActive(account.Id);
            if (account.Role == "Студент")
            {
                string[] Role = { "Председатель", "Зам. председателя" };
                if (Role.Contains(userInfo.Role))
                {
                    StudentsStackHumburg.Visibility = Visibility.Visible;
                    DutyListStackHumburg.Visibility = Visibility.Visible;
                }
            }
            else
            {
                StudentsStackHumburg.Visibility = Visibility.Visible;
                DutyListStackHumburg.Visibility = Visibility.Visible;
            }

            UserNameTextBlock.Text = account.FirstName + " " + account.MiddleName;
            //Личный кабинет
            UserNamePersonalArea.Text = account.LastName + " " + account.FirstName + " " + account.MiddleName;
            PhoneNumberPersonalArea.Text = userInfo.MobilePhone;
            RolePersonalArea.Text = userInfo.Role;
            VKLink.NavigateUri = new Uri(userInfo.VkLink);
            RunVKLink.Text = userInfo.VkLink;
            CouncilName.Text = userInfo.CouncilName;

            //Панель студентов
            var studentsActives = await StudentsViewModel.GetStudentsActiveIsNotArchive(account.Id);
            studentsActives.ForEach(s=> studActives.Add(s));

            var studentsNonReg = studentsViewModel.GetNonRegistratedStrudents();
            studentsNonReg.ForEach(s => nonRegistrStudActives.Add(s));

            studentsViewModel.GetAllGroups().ForEach(gr => groups.Add(gr));
            groups.OrderBy(x => x.GroupName);

            studentsViewModel.GetAllRolesStudActiveModel().ForEach(r=> roles.Add(r));
            roles.OrderBy(x => x.NameRU);

            //График дежурств
            var duty = await DutyListViewModel.GetDutyList();
            duty.ForEach(d=> dutyList.Add(d));

            //Инвентарь
            var inventoryList = await InventoryViewModel.GetInventory();
            inventoryList.ForEach(i => inventory.Add(i));
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
            InventoryGrid.Visibility = Visibility.Collapsed;

            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalArea.Visibility = Visibility.Collapsed;
            StudentsGrid.Visibility = Visibility.Visible;
            DutyListGrid.Visibility = Visibility.Collapsed;
            InventoryGrid.Visibility = Visibility.Collapsed;

            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }

        private void DutyListButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalArea.Visibility = Visibility.Collapsed;
            StudentsGrid.Visibility = Visibility.Collapsed;
            DutyListGrid.Visibility = Visibility.Visible;
            InventoryGrid.Visibility = Visibility.Collapsed;

            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalArea.Visibility = Visibility.Collapsed;
            StudentsGrid.Visibility = Visibility.Collapsed;
            DutyListGrid.Visibility = Visibility.Collapsed;
            InventoryGrid.Visibility = Visibility.Visible;

            myEffect.Radius = 0;
            MainGrid.Effect = myEffect;
        }
        #endregion

        #region Панель студентов
        private void StudentsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StudActiveGridPanel.Visibility = Visibility.Collapsed;
            ChangeStudentActive.Visibility = Visibility.Visible;
            ChangeStudentActive.IsEnabled= true;

            if (StudActivesDataGrid.SelectedItem is not null)
            {
                var row = (DataGridRow)sender;
                if (row.DataContext is not StudentsActiveModel context) return;
                Guid groupId = context.GroupId;
                Guid roleId = context.RoleId;
                StudentActiveIdTextChange.Text = context.Id.ToString();
                StudentIdTextChange.Text = context.StudentId.ToString();

                NameStudent.Text = context.Fio;
                string[] fio = context.Fio.Split();
                FirstNameChange.Text = fio[0];
                MiddleNameChange.Text = fio[1];
                LastNameChange.Text = fio[2];

                entryDatePickerChange.SelectedDate = context.EntryDate;
                birthDatePickerChange.SelectedDate = context.BirthDate;

                VkLinkChange.Text = context.VkLink;

                string mobilePhoneOld = context.MobilePhone;
                char[] delSymb = { '(', ')', '+' };
                string mobilePhone = mobilePhoneOld.TrimStart(delSymb);
                PhoneNumberChange.Text = mobilePhone.Remove(0, 2);

                cbGroupNameChange.SelectedItem = groups.First(x => x.GroupId == groupId);
                cbRoleActiveChange.SelectedItem = roles.First(x => x.RoleId == roleId);
            }
        }

        private void StudentGridReg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StudentGridReg.SelectedItem is not null)
            {
                DataGridRow row = (DataGridRow)sender;
                if (row.DataContext is not StudentsModel context) return;
                if (context.Fio != "Пусто")
                {
                    Guid groupId = context.GroupId;
                    string[] fio = context.Fio.Split();

                    FirstName.Text = fio[0];
                    MiddleName.Text = fio[1];
                    LastName.Text = fio[2];
                    entryDatePicker.SelectedDate = DateTime.Now;

                    string mobilePhoneOld = context.MobilePhone;
                    char[] delSymb = { '(', ')', '+' };
                    string mobilePhone = mobilePhoneOld.TrimStart(delSymb);
                    Phone.Text = mobilePhone.Remove(0, 2);

                    birthDatePicker.SelectedDate = context.BirthDate;
                    StudentIdText.Text = context.Id.ToString();
                    cbGroupName.SelectedItem = groups.First(x => x.GroupId == groupId);
                    cbRoleActiveChange.SelectedItem = roles.First(x => x.RoleId == Guid.Parse("356DC01F-165E-452B-BB91-BF0E0D536564"));
                    cbSex.SelectedIndex = context.Sex.ToString() == "1" ? cbSex.SelectedIndex = 0 : cbSex.SelectedIndex = 1;
                }
                else
                {
                    StudentIdText.Text = null;
                    FirstName.Text = null;
                    MiddleName.Text = null;
                    LastName.Text = null;
                    entryDatePicker.SelectedDate = DateTime.Now;
                    Phone.Text = null;
                    birthDatePicker.SelectedDate = null;
                    cbGroupName.SelectedItem = null;
                    cbRoleActiveChange.SelectedItem = null;
                    cbSex.SelectedItem = null;
                }

                BlurEffect effect = new BlurEffect();
                effect.Radius = 0;
                NewStudentStack.Effect = effect;
                StudentsDataGridStack.Visibility = Visibility.Collapsed;
            }
        }

        private void CreateStudActive_Click(object sender, RoutedEventArgs e)
        {
            CreateStudActiveStack.IsEnabled = true;
            CreateStudActiveStack.Visibility = Visibility.Visible;
            StudActiveGridPanel.Visibility = Visibility.Collapsed;
        }

        private void BackToStudActivesGrid_Click(object sender, RoutedEventArgs e)
        {
            CreateStudActiveStack.IsEnabled= false;
            CreateStudActiveStack.Visibility = Visibility.Collapsed;
            StudActiveGridPanel.Visibility = Visibility.Visible;
        }

        private async void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text != "" || MiddleName.Text != "" || LastName.Text != "") 
            {
                if(cbGroupName.SelectedItem != null)
                {
                    var userInfo = await StudentsViewModel.GetStudentActive(account.Id);
                    var cbRoleActiveSelected = (RolesStudActiveModel)cbRoleActive.SelectedItem;
                    var roleActive = cbRoleActiveSelected != null ? cbRoleActiveSelected.RoleId : Guid.Parse("356DC01F-165E-452B-BB91-BF0E0D536564");
                    var cbGroupNameSelected = (GroupsModel)cbGroupName.SelectedItem;
                    var groupId = cbGroupNameSelected.GroupId;
                    var studentCouncilId = studentsViewModel.GetStudentCouncilId(userInfo.Id);
                    var studActiveId = StudentIdText.Text != "" ? new Guid(StudentIdText.Text) : Guid.Empty;

                    RegistrationStudActiveModel regModel = new RegistrationStudActiveModel
                    {
                        FirstName = FirstName.Text,
                        MiddleName = MiddleName.Text,
                        LastName = LastName.Text,
                        StudActiveId = Guid.NewGuid(),
                        EntryDate = DateTime.Today,
                        IsArchive = false,
                        RoleActive = roleActive,
                        VkLink = VkLink.Text,
                        StudentId = studActiveId,
                        StudentCouncilId = studentCouncilId.Value,
                        GroupId = groupId,
                        Sex = cbSex.SelectedIndex
                    };

                    bool res = regModel.StudentId != Guid.Empty ? studentsViewModel.CreateAgainStudentActive(regModel) : studentsViewModel.CreateFullNewStudentActive(regModel);//Проверка на то, есть уже этот студент в системе, или нет

                    if (res)
                    {
                        StudentIdText.Text = null;
                        CreateStudActiveStack.Visibility = Visibility.Collapsed;
                        StudActiveGridPanel.Visibility = Visibility.Visible;
                        MessageBox.Show("Регистрация успешна.");
                    }
                    else
                    {
                        MessageBox.Show("Регистрация не удалась.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    UpdateStudActiveDataGrid();
                }
            }
        }

        private async void ChangeStudActive_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameChange.Text != "" || MiddleNameChange.Text != "" || LastNameChange.Text != "")
            {
                if (cbGroupNameChange.SelectedItem != null)
                {
                    var userInfo = await StudentsViewModel.GetStudentActive(account.Id);
                    var cbRoleActiveSelected = (RolesStudActiveModel)cbRoleActiveChange.SelectedItem;
                    var roleActive = cbRoleActiveSelected != null ? cbRoleActiveSelected.RoleId : Guid.Parse("356DC01F-165E-452B-BB91-BF0E0D536564");
                    var cbGroupNameSelected = (GroupsModel)cbGroupNameChange.SelectedItem;
                    var groupId = cbGroupNameSelected.GroupId;
                    var studentCouncilId = studentsViewModel.GetStudentCouncilId(userInfo.Id);

                    var regModel = new RegistrationStudActiveModel
                    {
                        FirstName = FirstNameChange.Text,
                        MiddleName = MiddleNameChange.Text,
                        LastName = LastNameChange.Text,
                        StudActiveId = Guid.Parse(StudentActiveIdTextChange.Text),
                        EntryDate = entryDatePickerChange.SelectedDate,
                        IsArchive = false,
                        RoleActive = roleActive,
                        VkLink = VkLinkChange.Text,
                        StudentId = Guid.Parse(StudentIdTextChange.Text),
                        StudentCouncilId = studentCouncilId.Value,
                        GroupId = groupId,
                        MobilePhoneNumber = PhoneNumberChange.Text,
                        Sex = cbSexChange.SelectedIndex
                    };

                    var res = await regModel.ChangeStudentActive();

                    if (res)
                    {
                        StudentIdText.Text = null;
                        CreateStudActiveStack.Visibility = Visibility.Collapsed;
                        StudActiveGridPanel.Visibility = Visibility.Visible;
                        MessageBox.Show("Изменение успешно.");
                    }
                    else
                    {
                        MessageBox.Show("Изменение не удалось.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    UpdateStudActiveDataGrid();
                }
            }
        }

        private void SelectStudent_Click(object sender, RoutedEventArgs e)
        {
            StudentsDataGridStack.Visibility = Visibility.Visible;

            BlurEffect effect = new BlurEffect();
            effect.Radius = 10;
            NewStudentStack.Effect = effect;
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

        private void UpdateStudActive_Click(object sender, RoutedEventArgs e)
        {

            UpdateStudActiveDataGrid();
        }
        public async void UpdateStudActiveDataGrid()
        {
            studActives.Clear();
            var st = IsArchive.IsChecked == true ? await StudentsViewModel.GetStudentsActiveIsArchiveToo(account.Id) : await StudentsViewModel.GetStudentsActiveIsNotArchive(account.Id);
            st.ForEach(s => studActives.Add(s));
        }

        private void SaveStudActive_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void IsArchive_Checked(object sender, RoutedEventArgs e)
        {
            studActives.Clear();
            var st = await StudentsViewModel.GetStudentsActiveIsArchiveToo(account.Id);
            st.ForEach(s => studActives.Add(s));
        }

        private async void IsArchive_Unchecked(object sender, RoutedEventArgs e)
        {
            studActives.Clear();
            var st = await StudentsViewModel.GetStudentsActiveIsNotArchive(account.Id);
            st.ForEach(s => studActives.Add(s));
        }

        private async void AddInArchive_Click(object sender, RoutedEventArgs e)
        {
            await StudentsViewModel.ChangeIsArchive(selectedStudActive.Id);
            UpdateStudActiveDataGrid();
        }

        private void StudentsDataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            if (row.DataContext is StudentsActiveModel context)
            {
                selectedStudActive = context;
            }
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

        #region График дежурств
        private async void DutyListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DutyListDataGrid.SelectedItem != null)
            {
                Guid id;
                var row = (DataGridRow)sender;
                if (row.DataContext is DutyListModel context)
                {
                    id = context.Id;
                    bool check = dutyListViewModel.SaveDuty(id);
                    if (check)
                    {
                        dutyList.Clear();
                        var duty = await DutyListViewModel.GetDutyList();
                        duty.ForEach(d => dutyList.Add(d));
                        MessageBox.Show("Сохранение успешно!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private async void Missed_Click(object sender, RoutedEventArgs e)
        {
            if (DutyListDataGrid.SelectedItem != null)
            {
                Guid id;

                id = SelectedDutyList.Id;
                bool check = dutyListViewModel.MissedDuty(id);
                if (check)
                {
                    dutyList.Clear();
                    var duty = await DutyListViewModel.GetDutyList();
                    duty.ForEach(d => dutyList.Add(d));
                    MessageBox.Show("Сохранение успешно!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DutyListDataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            if (row.DataContext is DutyListModel context)
            {
                SelectedDutyList = context;
            }
        }

        #endregion

        #region Инвентарь
        private void InventoryGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void CreateNewInventory_Click(object sender, RoutedEventArgs e)
        {

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

        private void closeChangeStudent_Click(object sender, RoutedEventArgs e)
        {
            ChangeStudentActive.Visibility = Visibility.Collapsed;
            StudActiveGridPanel.Visibility = Visibility.Visible;
        }
    }
}
