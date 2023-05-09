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

namespace RkkInfo.Vacancy
{
    /// <summary>
    /// Логика взаимодействия для Vacancy_UC.xaml
    /// </summary>
    public partial class Vacancy_UC : UserControl
    {
        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        List<RkkInfo_Vacation> _list = new List<RkkInfo_Vacation>();
        private string _login;

        public Vacancy_UC(RkkInfo_dbEntities rkkInfo_DbEntities, string login)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            _login = login;
            LV_.ItemsSource = _context.RkkInfo_Vacation.OrderBy(t => t.RkkInfo_Vacation_id).ToList();
        }

        public void Update_VAC()
        {
            _list = _context.RkkInfo_Vacation.ToList();
            LV_.ItemsSource = _list;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Vacan_Edit vacan_Edit = new Vacan_Edit(_context, sender, this);
            vacan_Edit.ShowDialog();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var item = button.DataContext as RkkInfo_Vacation;
                _context.RkkInfo_Vacation.Remove(item);
                _context.SaveChanges();
                Update_VAC();
            }
        }

        private void New_Vac_Click(object sender, RoutedEventArgs e)
        {
            Vacan_Add vacan_Add = new Vacan_Add(_context, this);
            vacan_Add.ShowDialog();
        }

        private void Utv_Click(object sender, RoutedEventArgs e)
        {
            if (!_login.Contains("_admin"))
            {
                System.Windows.MessageBox.Show("У вас нету доступа к этой функции");
            }
            else
            {
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите утвердить?", "Утвердить", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    var button = sender as Button;
                    var item = button.DataContext as RkkInfo_Vacation;

                    item.RkkInfo_Vacation_Status = "Одобрено✓";
                    _context.SaveChanges();
                    Update_VAC();
                }
            }
        }

        private void Des_Click(object sender, RoutedEventArgs e)
        {
            if (!_login.Contains("_admin"))
            {
                System.Windows.MessageBox.Show("У вас нету доступа к этой функции");
            }
            else
            {
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите отказать?", "Отказ", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    var button = sender as Button;
                    var item = button.DataContext as RkkInfo_Vacation;

                    item.RkkInfo_Vacation_Status = "Отказано✖";
                    _context.SaveChanges();
                    Update_VAC();
                }
            }
        }

        private void DownLoad_Click(object sender, RoutedEventArgs e)
        {

            // Получаем кнопку, на которую нажали
            Button button = sender as Button;

            // Получаем элемент списка, связанный с кнопкой
            var item = button.DataContext as RkkInfo_Vacation;

            // Получаем данные файла из базы данных

            string fileName = item.RkkInfo_Vacation_Name + "_" + item.RkkInfo_Vacation_Last_Name + "_" + item.RkkInfo_Vacation_First_Name + ".docx";
            byte[] fileData = item.RkkInfo_Vacation_Files;

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
