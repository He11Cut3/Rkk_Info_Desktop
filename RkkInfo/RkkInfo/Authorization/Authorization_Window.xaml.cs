using MaterialDesignThemes.Wpf;
using RkkInfo.Dismis;
using RkkInfo.Emp;
using RkkInfo.Job_Opening;
using RkkInfo.Main_Win;
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

namespace RkkInfo.Authorization
{
    /// <summary>
    /// Логика взаимодействия для Authorization_Window.xaml
    /// </summary>
    public partial class Authorization_Window : Window
    {
        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();

        public string Login { get; set; }

        public Authorization_Window()
        {
            InitializeComponent();
        }

        //Theme Code ========================>
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        //===================================>

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            //Theme Code ========================>
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
            //===================================>
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = txtUsername.Text;
            string password = txtPassword.Password;
            var user = _context.RkkInfo_Users.FirstOrDefault(u => u.RkkInfo_Users_Login == login && u.RkkInfo_Users_Password == password);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
            }
            else
            {
                Main main = new Main(user);
                main.CheckUserRole(login);
                this.Close();
                main.ShowDialog();
            }
        }
    }
}
