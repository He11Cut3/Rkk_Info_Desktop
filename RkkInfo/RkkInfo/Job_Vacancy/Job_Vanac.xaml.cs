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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RkkInfo.Job_Vacancy
{
    /// <summary>
    /// Логика взаимодействия для Job_Vanac.xaml
    /// </summary>
    public partial class Job_Vanac : UserControl
    {

        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        List<RkkInfo_Jobs_Vacancy> _list = new List<RkkInfo_Jobs_Vacancy>();

        public Job_Vanac(RkkInfo_dbEntities rkkInfo_DbEntities)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            LV_.ItemsSource = _context.RkkInfo_Jobs_Vacancy.OrderBy(t => t.RkkInfo_Jobs_Vacancy_id).ToList();
        }

        private void Finder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = Finder.Text;
            var query = from emp in _context.RkkInfo_Jobs_Vacancy
                        where emp.RkkInfo_Jobs_Vacancy_Name.Contains(searchText)
                            || emp.RkkInfo_Jobs_Vacancy_First_Name.Contains(searchText)
                            || emp.RkkInfo_Jobs_Vacancy_Last_Name.Contains(searchText)
                            || emp.RkkInfo_Jobs_Vacancy_Patronymic.Contains(searchText)
                            || emp.RkkInfo_Jobs_Vacancy_Position.Contains(searchText)
                            || emp.RkkInfo_Jobs_Vacancy_Date.Contains(searchText)
                            || emp.RkkInfo_Jobs_Vacancy_Status.Contains(searchText)
                        select emp;

            LV_.ItemsSource = query.ToList();
        }

        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = ((ComboBoxItem)myComboBox.SelectedItem).Content.ToString();

            var sortedQuery = from emp in _context.RkkInfo_Jobs_Vacancy
                              select emp;

            switch (selectedValue)
            {
                case "Наименование":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Vacancy_Name);
                    break;
                case "Имя":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Vacancy_First_Name);
                    break;
                case "Фамилия":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Vacancy_Last_Name);
                    break;
                case "Отчество":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Vacancy_Patronymic);
                    break;
                case "Должность":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Vacancy_Position);
                    break;
                case "Дата":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Vacancy_Date);
                    break;
                case "Статус":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Jobs_Vacancy_Status);
                    break;

                default:
                    break;
            }

            LV_.ItemsSource = sortedQuery.ToList();
        }

        public void Update_Jobs_Vac()
        {
            _list = _context.RkkInfo_Jobs_Vacancy.ToList();
            LV_.ItemsSource = _list;
        }

        private void Vac_Del_Click(object sender, RoutedEventArgs e)
        {

            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var item = button.DataContext as RkkInfo_Jobs_Vacancy;
                _context.RkkInfo_Jobs_Vacancy.Remove(item);
                _context.SaveChanges();
                Update_Jobs_Vac();
            }
        }

        private void Vac_Down_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую нажали
            Button button = sender as Button;

            // Получаем элемент списка, связанный с кнопкой
            var item = button.DataContext as RkkInfo_Jobs_Vacancy;

            // Получаем данные файла из базы данных

            string fileName = item.RkkInfo_Jobs_Vacancy_Name + ".docx";
            byte[] fileData = item.RkkInfo_Jobs_Vacancy_Files;

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
    }
}
