using RkkInfo.Emp;
using RkkInfo.Job_Opening;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace RkkInfo.Job_Vacancy
{
    /// <summary>
    /// Логика взаимодействия для Job_Vac_Add.xaml
    /// </summary>
    public partial class Job_Vac_Add : Window
    {
        private RkkInfo_dbEntities _context;
        private Jobs_Ops job_Vanac;
        private RkkInfo_Jobs_Vacancy rkkInfo_Jobs_Vacancy;
        private RkkInfo_Jobs_Opening rkkInfo_Jobs_Opening;
        private RkkInfo_Employees rkkInfo_Employees;
        private RkkInfo_Users _user;
        private string _login;

        public Job_Vac_Add(RkkInfo_dbEntities context, object o, Jobs_Ops jobs_, string login)
        {
            InitializeComponent();
            _context = context;
            rkkInfo_Jobs_Opening = (o as Button).DataContext as RkkInfo_Jobs_Opening;
            job_Vanac = jobs_;
            _login = login;
            Check_and_Update(_login);
        }

        public void Check_and_Update(string login)
        {
            Name.Text = rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Name + "_Вакансия";
            Date.Text = DateTime.Now.ToString("dd.MM.yyyy");

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

                Name.IsEnabled = false;

                myComboBox.IsEnabled = false;

                Date.IsEnabled = false;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Word Files (*.docx)|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите откликнуться на вакансию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    _context.RkkInfo_Jobs_Vacancy.Add(new RkkInfo_Jobs_Vacancy()
                    {
                        RkkInfo_Jobs_Vacancy_Name = Name.Text,
                        RkkInfo_Jobs_Vacancy_First_Name = First_Name.Text,
                        RkkInfo_Jobs_Vacancy_Last_Name = Last_Name.Text,
                        RkkInfo_Jobs_Vacancy_Patronymic = Patronymic.Text,
                        RkkInfo_Jobs_Vacancy_Position = Position.Text,
                        RkkInfo_Jobs_Vacancy_Date = Date.Text,
                        RkkInfo_Jobs_Vacancy_Files = imageBytes,
                        RkkInfo_Jobs_Vacancy_Status = myComboBox.Text,

                    });

                    var find = _context.RkkInfo_Jobs_Opening.Where(x => x.RkkInfo_Jobs_Opening_Name.Replace("_Вакансия", "") == Name.Text.Replace("_Вакансия", "")).ToList();
                    foreach (var finder in find)
                    {
                        finder.RkkInfo_Jobs_Opening_Status = "В процессе обработки";
                    }
                    job_Vanac.Update_Jobs_Open();

                    _context.SaveChanges();
                    job_Vanac.Update_Jobs_Open();
                    this.Close();
                }
            }
        }

        private void ComeBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
