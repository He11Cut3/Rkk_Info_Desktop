using RkkInfo.Vacancy;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace RkkInfo.Dismis
{
    /// <summary>
    /// Логика взаимодействия для Dism_Add.xaml
    /// </summary>
    public partial class Dism_Add : Window
    {
        private RkkInfo_dbEntities _context;
        private Dismis_UC _uc;
        private string _login;

        public Dism_Add(RkkInfo_dbEntities rkkInfo_DbEntities, Dismis_UC dismis_UC, string login)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            _uc = dismis_UC;
            _login = login;


            RkkInfo_Users _user = _context.RkkInfo_Users.SingleOrDefault(u => u.RkkInfo_Users_Login == login);

            if (_user != null) // make sure that the user was found
            {
                // access the user's properties
                First_Name.Text = _user.RkkInfo_Users_Login_First_Name;
                Last_Name.Text = _user.RkkInfo_Users_Login_Last_Name;
                Patronymic.Text = _user.RkkInfo_Users_Login_Patronymic;



                First_Name.IsEnabled = false;
                Last_Name.IsEnabled = false;
                Patronymic.IsEnabled = false;


                var Find_Last_Name = Last_Name.Text;
                var Find_First_Name = First_Name.Text;
                var Find_Patronymic = Patronymic.Text;

                var find = _context.RkkInfo_Employees.Where(x =>
                                                             x.RkkInfo_Employees_Last_Name == Find_Last_Name &&
                                                             x.RkkInfo_Employees_First_Name == Find_First_Name &&
                                                             x.RkkInfo_Employees_Patronymic == Find_Patronymic).ToList();

                foreach (var finder in find)
                {
                    Position.Text = finder.RkkInfo_Employees_Position.ToString();
                    myComboBox.Text = finder.RkkInfo_Employees_Is_Active.ToString();
                }

                Position.IsEnabled = false;

            }
        }
        private void ComeBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_Dism_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Word Files (*.docx)|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    _context.RkkInfo_Dismissal.Add(new RkkInfo_Dismissal()
                    {
                        RkkInfo_Dismissal_Name = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                        RkkInfo_Dismissal_First_Name = First_Name.Text,
                        RkkInfo_Dismissal_Last_Name = Last_Name.Text,
                        RkkInfo_Dismissal_Patronymic = Patronymic.Text,
                        RkkInfo_Dismissal_Position = Position.Text,
                        RkkInfo_Dismissal_Date = Date.SelectedDate?.ToString("dd.MM.yyyy"),
                        RkkInfo_Dismissal_Files = imageBytes,
                        RkkInfo_Dismissal_Status = "В процессе обработки",

                    });
                    _context.SaveChanges();
                    _uc.Update_Dis();
                    this.Close();
                }
            }
        }
    }
}
