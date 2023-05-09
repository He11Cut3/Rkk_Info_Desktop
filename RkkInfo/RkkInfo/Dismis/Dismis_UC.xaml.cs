using RkkInfo.Vacancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace RkkInfo.Dismis
{
    /// <summary>
    /// Логика взаимодействия для Dismis_UC.xaml
    /// </summary>
    public partial class Dismis_UC : UserControl
    {
        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        List<RkkInfo_Dismissal> _list = new List<RkkInfo_Dismissal>();
        private string _login;

        public Dismis_UC(string login)
        {
            InitializeComponent();
            _login = login;
            LV_.ItemsSource = _context.RkkInfo_Dismissal.OrderBy(t => t.RkkInfo_Dismissal_id).ToList();
        }

        

        public void Update_Dis()
        {
            _list = _context.RkkInfo_Dismissal.ToList();
            LV_.ItemsSource = _list;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Dism_Edit dism_Edit = new Dism_Edit(_context, sender, this);
            dism_Edit.ShowDialog(); 
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var item = button.DataContext as RkkInfo_Dismissal;
                _context.RkkInfo_Dismissal.Remove(item);
                _context.SaveChanges();
                Update_Dis();
            }
        }

        private void New_Vac_Click(object sender, RoutedEventArgs e)
        {
            Dism_Add dism_Add = new Dism_Add(_context, this);
            dism_Add.ShowDialog();
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
                    var item = button.DataContext as RkkInfo_Dismissal;

                    item.RkkInfo_Dismissal_Status = "Одобрено✓";
                    _context.SaveChanges();
                    Update_Dis();
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
                    var item = button.DataContext as RkkInfo_Dismissal;

                    item.RkkInfo_Dismissal_Status = "Отказано✖";
                    _context.SaveChanges();
                    Update_Dis();
                }
            }
        }

        private void DownLoad_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую нажали
            Button button = sender as Button;

            // Получаем элемент списка, связанный с кнопкой
            var item = button.DataContext as RkkInfo_Dismissal;

            // Получаем данные файла из базы данных

            string fileName = item.RkkInfo_Dismissal_Name + "_" + item.RkkInfo_Dismissal_Last_Name + "_" + item.RkkInfo_Dismissal_First_Name + ".docx";
            byte[] fileData = item.RkkInfo_Dismissal_Files;

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
