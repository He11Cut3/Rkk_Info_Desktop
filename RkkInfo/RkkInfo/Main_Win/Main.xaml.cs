using MailKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Windows.Shapes;
using System.Windows.Threading;
using RkkInfo.Authorization;
using MaterialDesignThemes.Wpf;
using System.Runtime.Remoting.Contexts;
using RkkInfo.Emp;
using RkkInfo.Job_Opening;
using RkkInfo.Job_Vacancy;
using RkkInfo.Files_Rkk;
using RkkInfo.Vacancy;
using RkkInfo.Dismis;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using OfficeOpenXml;
using System.IO;

namespace RkkInfo.Main_Win
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private string _branchName = "";
        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        private string branchName;


        private RkkInfo_Users _user;

        public Main(RkkInfo_Users user)
        {
            InitializeComponent();
            _user = user;
            CheckUserRole(_user.RkkInfo_Users_Login);
        }
        
        public void CheckUserRole(string login)
        {
            if (!login.Contains("_admin"))
            {
                Add_Employ.Visibility = Visibility.Collapsed;
            }
        }




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

        public void But_Spawn(string branchName) // добавляем параметр branchName
        {
            uc_spawn_stack.Children.Clear();
            var buttonData = _context.RkkInfo_Branch.ToList();
            string login = _user.RkkInfo_Users_Login;
            foreach (var data in buttonData)
            {
                var button = new System.Windows.Controls.Button
                {
                    Name = "button_" + data.RkkInfo_Branch_id.ToString(),
                    Content = data.RkkInfo_Branch_Name,
                    Style = (Style)FindResource("Button_Style_Spawn"),
                    Tag = new Employ(branchName, _context, login)
                    
                };
                button.Click += Button_Click;
                uc_spawn_stack.Children.Add(button);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Получить уникальный идентификатор филиала из имени кнопки
                string buttonId = button.Name.Split('_').Last();

                // Использовать идентификатор, чтобы получить филиал из базы данных
                int branchId = int.Parse(buttonId);
                var branch = _context.RkkInfo_Branch.FirstOrDefault(b => b.RkkInfo_Branch_id == branchId);

                // Получить наименование кнопки и сохранить его в переменную branchName
                branchName = button.Content.ToString();

                // Удалить предыдущий UserControl, если он был добавлен ранее
                Empl_UC.Children.Clear();

                string login = _user.RkkInfo_Users_Login;

                // Создать новый экземпляр UserControl и добавить его на StackPanel
                var userControl = new Employ(branchName, _context, login);
                userControl.Branch = branch;
                userControl.BranchName = branchName; // передать значение branchName в UserControl
                userControl.UpdateLayout(); // Обновить визуальный интерфейс пользовательского элемента управления

                
                Empl_UC.Children.Add(userControl);
            }
        }


        private void Employyes_Click(object sender, RoutedEventArgs e)
        {
            But_Spawn(_branchName);

            Dism.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();
            Rkk_File.Children.Clear();
            Vacansy.Children.Clear();
        }

        private void Jobs_Opening_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();


            string login = _user.RkkInfo_Users_Login;
            Jobs_Ops jobs_Ops = new Jobs_Ops(_context, login);
            Jobs_Opening_UC.Children.Add(jobs_Ops);

        }

        private void Jobs_Vacancy_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Opening_UC.Children.Clear();

            Job_Vanac job_Vanac = new Job_Vanac(_context);
            Jobs_Vacancy_UC.Children.Add(job_Vanac);
        }

        private void Files_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();

            Files_UC files_UC = new Files_UC(_context);
            Rkk_File.Children.Add(files_UC);

        }

        private void Vacancy_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();

            string login = _user.RkkInfo_Users_Login;
            Vacancy_UC vacancy_UC = new Vacancy_UC(_context, login, _user);
            Vacansy.Children.Add(vacancy_UC);
        }

        private void Dismissal_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();

            string login = _user.RkkInfo_Users_Login;
            Dismis_UC dismis_UC = new Dismis_UC(login, _user);
            Dism.Children.Add(dismis_UC);
        }

        private void ComeBack_Click(object sender, RoutedEventArgs e)
        {
            Authorization_Window authorization_Window = new Authorization_Window();
            this.Close();
            authorization_Window.ShowDialog();
        }

        private void Add_Employ_Click(object sender, RoutedEventArgs e)
        {
            Add_Depart add_Depart = new Add_Depart(_context, this);
            add_Depart.ShowDialog();
        }

        private void FAQ_Click(object sender, RoutedEventArgs e)
        {
            FAQ fAQ = new FAQ();
            fAQ.ShowDialog();
        }

        private void Emp_Report_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите сформировать отчёт?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                // Получаем список всех отделов
                List<string> departments = _context.RkkInfo_Employees.Select(c => c.RkkInfo_Employees_Department).Distinct().ToList();

                // Определяем наименования столбцов
                string[] columnNames = new string[] { "Фамилия", "Имя", "Отчество", "Должность", "Дата приема на работу", "Активен" };
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                // Создаем новый файл Excel
                using (ExcelPackage package = new ExcelPackage())
                {
                    foreach (string department in departments)
                    {
                        // Создаем новый лист для отдела
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(department);

                        // Записываем название отдела в первую строку
                        worksheet.Cells[1, 1].Value = "Отдел: " + department;

                        // Записываем наименования столбцов
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            worksheet.Cells[2, i + 1].Value = columnNames[i];
                        }

                        // Получаем список всех сотрудников для текущего отдела
                        List<RkkInfo_Employees> data = _context.RkkInfo_Employees.Where(f => f.RkkInfo_Employees_Department == department).ToList();

                        // Записываем данные
                        for (int i = 0; i < data.Count; i++)
                        {
                            worksheet.Cells[i + 3, 1].Value = data[i].RkkInfo_Employees_Last_Name;
                            worksheet.Cells[i + 3, 2].Value = data[i].RkkInfo_Employees_First_Name;
                            worksheet.Cells[i + 3, 3].Value = data[i].RkkInfo_Employees_Patronymic;
                            worksheet.Cells[i + 3, 4].Value = data[i].RkkInfo_Employees_Position;
                            worksheet.Cells[i + 3, 5].Value = data[i].RkkInfo_Employees_Start_Date;
                            worksheet.Cells[i + 3, 6].Value = data[i].RkkInfo_Employees_Is_Active;
                        }
                    }

                    // Сохраняем файл
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Отчёт_Сотрудники.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                    System.Windows.MessageBox.Show("Успешно!");
                }
            }
        }

        private void Job_Open_Report_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите сформировать отчёт?", "Отчёт", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                List<RkkInfo_Jobs_Opening> data = _context.RkkInfo_Jobs_Opening.ToList();

                // Определяем наименования столбцов
                string[] columnNames = new string[] { "Наименование", "Дата", "Статус"};
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                // Создаем новый файл Excel
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Добавляем лист
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Отчёт_Вакансии");

                    // Записываем наименования столбцов
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columnNames[i];
                    }

                    // Записываем данные
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].RkkInfo_Jobs_Opening_Name;
                        worksheet.Cells[i + 2, 2].Value = data[i].RkkInfo_Jobs_Opening_Date;
                        worksheet.Cells[i + 2, 3].Value = data[i].RkkInfo_Jobs_Opening_Status;
                    }

                    // Сохраняем файл
                    File.WriteAllBytes("Отчёт_Вакансии.xlsx", package.GetAsByteArray());
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Отчёт_Вакансии.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());

                    System.Windows.MessageBox.Show("Успешно!");
                }
            }
        }

        private void Job_Vac_Report_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите сформировать отчёт?", "Отчёт", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                List<RkkInfo_Jobs_Vacancy> data = _context.RkkInfo_Jobs_Vacancy.ToList();

                // Определяем наименования столбцов
                string[] columnNames = new string[] { "Наименование", "Фамилия", "Имя", "Отчество","Должность", "Дата", "Статус" };
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                // Создаем новый файл Excel
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Добавляем лист
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Отчёт_Отклик");

                    // Записываем наименования столбцов
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columnNames[i];
                    }

                    // Записываем данные
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].RkkInfo_Jobs_Vacancy_Name;
                        worksheet.Cells[i + 2, 2].Value = data[i].RkkInfo_Jobs_Vacancy_Last_Name;
                        worksheet.Cells[i + 2, 3].Value = data[i].RkkInfo_Jobs_Vacancy_First_Name;
                        worksheet.Cells[i + 2, 4].Value = data[i].RkkInfo_Jobs_Vacancy_Patronymic;
                        worksheet.Cells[i + 2, 5].Value = data[i].RkkInfo_Jobs_Vacancy_Position;
                        worksheet.Cells[i + 2, 6].Value = data[i].RkkInfo_Jobs_Vacancy_Date;
                        worksheet.Cells[i + 2, 7].Value = data[i].RkkInfo_Jobs_Vacancy_Status;
                    }

                    // Сохраняем файл
                    File.WriteAllBytes("Отчёт_Отклик.xlsx", package.GetAsByteArray());
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Отчёт_Отклик.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                    System.Windows.MessageBox.Show("Успешно!");
                }
            }
        }

        private void Vac_Report_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите сформировать отчёт?", "Отчёт", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                List<RkkInfo_Vacation> data = _context.RkkInfo_Vacation.ToList();

                // Определяем наименования столбцов
                string[] columnNames = new string[] { "Наименование", "Фамилия", "Имя", "Отчество", "Должность", "Дата начала", "Дата окончания",  "Статус" };
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                // Создаем новый файл Excel
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Добавляем лист
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Отчёт_Отклик");

                    // Записываем наименования столбцов
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columnNames[i];
                    }

                    // Записываем данные
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].RkkInfo_Vacation_Name;
                        worksheet.Cells[i + 2, 2].Value = data[i].RkkInfo_Vacation_Last_Name;
                        worksheet.Cells[i + 2, 3].Value = data[i].RkkInfo_Vacation_First_Name;
                        worksheet.Cells[i + 2, 4].Value = data[i].RkkInfo_Vacation_Patronymic;
                        worksheet.Cells[i + 2, 5].Value = data[i].RkkInfo_Vacation_Position;
                        worksheet.Cells[i + 2, 6].Value = data[i].RkkInfo_Vacation_Start_Date;
                        worksheet.Cells[i + 2, 7].Value = data[i].RkkInfo_Vacation_End_Date;
                        worksheet.Cells[i + 2, 8].Value = data[i].RkkInfo_Vacation_Status;
                    }

                    // Сохраняем файл
                    File.WriteAllBytes("Отчёт_Отпуск.xlsx", package.GetAsByteArray());
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Отчёт_Отпуск.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                    System.Windows.MessageBox.Show("Успешно!");
                }
            }
        }

        private void Dism_Report_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите сформировать отчёт?", "Отчёт", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                List<RkkInfo_Dismissal> data = _context.RkkInfo_Dismissal.ToList();

                // Определяем наименования столбцов
                string[] columnNames = new string[] { "Наименование", "Фамилия", "Имя", "Отчество", "Должность", "Дата", "Статус" };
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                // Создаем новый файл Excel
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Добавляем лист
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Отчёт_Отклик");

                    // Записываем наименования столбцов
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columnNames[i];
                    }

                    // Записываем данные
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].RkkInfo_Dismissal_Name;
                        worksheet.Cells[i + 2, 2].Value = data[i].RkkInfo_Dismissal_Last_Name;
                        worksheet.Cells[i + 2, 3].Value = data[i].RkkInfo_Dismissal_First_Name;
                        worksheet.Cells[i + 2, 4].Value = data[i].RkkInfo_Dismissal_Patronymic;
                        worksheet.Cells[i + 2, 5].Value = data[i].RkkInfo_Dismissal_Position;
                        worksheet.Cells[i + 2, 6].Value = data[i].RkkInfo_Dismissal_Date;
                        worksheet.Cells[i + 2, 7].Value = data[i].RkkInfo_Dismissal_Status;
                    }

                    // Сохраняем файл
                    File.WriteAllBytes("Отчёт_Увольнение.xlsx", package.GetAsByteArray());
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Отчёт_Увольнение.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                    System.Windows.MessageBox.Show("Успешно!");
                }
            }
        }
    }
}
