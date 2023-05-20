using MaterialDesignThemes.Wpf;
using RkkInfo.Job_Vacancy;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RkkInfo.Job_Opening
{
    /// <summary>
    /// Логика взаимодействия для Jobs_Ops.xaml
    /// </summary>
    public partial class Jobs_Ops : UserControl
    {
        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        List<RkkInfo_Jobs_Opening> _list = new List<RkkInfo_Jobs_Opening>();
        List<RkkInfo_Jobs_Vacancy> _list1 = new List<RkkInfo_Jobs_Vacancy>();

        private RkkInfo_Users _user;

        private string _login;

        public Jobs_Ops(RkkInfo_dbEntities context, string login)
        {
            InitializeComponent();
            _context = context;
            _login = login;
            LV_1.ItemsSource = _context.RkkInfo_Jobs_Opening.OrderBy(t => t.RkkInfo_Jobs_Opening_id).ToList();
            if (!_login.Contains("_admin"))
            {
                New_Vacan.Visibility = Visibility.Collapsed;
            }
            
        }

        private void Finder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = Finder.Text;
            var query = from emp in _context.RkkInfo_Jobs_Opening
                        where emp.RkkInfo_Jobs_Opening_Name.Contains(searchText)
                            || emp.RkkInfo_Jobs_Opening_Date.Contains(searchText)
                            || emp.RkkInfo_Jobs_Opening_Status.Contains(searchText)
                        select emp;

            LV_1.ItemsSource = query.ToList();
        }

        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = ((ComboBoxItem)myComboBox.SelectedItem).Content.ToString();

            var sortedQuery = from emp in _context.RkkInfo_Jobs_Opening
                              select emp;

            switch (selectedValue)
            {
                case "Имя":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Opening_Name);
                    break;
                case "Дата":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Opening_Date);
                    break;
                case "Статус":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Opening_Status);
                    break;
                default:
                    break;
            }

            LV_1.ItemsSource = sortedQuery.ToList();
        }



        public void Update_Jobs_Open()
        {
            _list = _context.RkkInfo_Jobs_Opening.ToList();
            LV_1.ItemsSource = _list;
        }

        public void Update_Jobs_Vac()
        {
            _list1 = _context.RkkInfo_Jobs_Vacancy.ToList();
            LV_1.ItemsSource = _list1;
        }

        private void New_Vacan_Click(object sender, RoutedEventArgs e)
        {
            Jobs_Opening_Add jobs_Opening_Add = new Jobs_Opening_Add(_context, this);
            jobs_Opening_Add.ShowDialog();
        }

        private void Vac_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!_login.Contains("_admin"))
            {
                System.Windows.MessageBox.Show("У вас нету доступа к этой функции");
            }
            else
            {
                Jobs_Opening_Edit jobs_Opening_Edit = new Jobs_Opening_Edit(_context, sender, this);
                jobs_Opening_Edit.ShowDialog();
            }
        }

        private void Vac_Del_Click(object sender, RoutedEventArgs e)
        {
            if (!_login.Contains("_admin"))
            {
                System.Windows.MessageBox.Show("У вас нету доступа к этой функции");
            }
            else
            {
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    var button = sender as Button;
                    var item = button.DataContext as RkkInfo_Jobs_Opening;
                    _context.RkkInfo_Jobs_Opening.Remove(item);
                    _context.SaveChanges();
                    Update_Jobs_Open();
                }
            }
        }

        private void Vac_Files_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую нажали
            Button button = sender as Button;

            // Получаем элемент списка, связанный с кнопкой
            var item = button.DataContext as RkkInfo_Jobs_Opening;

            // Получаем данные файла из базы данных

            string fileName = item.RkkInfo_Jobs_Opening_Name + ".pdf";
            byte[] fileData = item.RkkInfo_Jobs_Opening_Files;

            // Если данные файла есть, то открываем файл
            if (fileData != null && fileData.Length > 0)
            {
                // Получаем путь к рабочему столу
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                // Создаем путь для сохранения файла на рабочем столе
                string filePath = System.IO.Path.Combine(desktopPath, fileName);

                // Сохраняем файл на рабочий стол
                File.WriteAllBytes(filePath, fileData);
            }
        }

        private void Vac_Vac_Click(object sender, RoutedEventArgs e)
        {
            if (_login.Contains("_admin"))
            {
                System.Windows.MessageBox.Show("У вас нету доступа к этой функции");
                
            }
            else
            {
                Job_Vac_Add job_Vac_Add = new Job_Vac_Add(_context, sender, this, _login);
                job_Vac_Add.ShowDialog();
            }
            
        }

        
    }
}
