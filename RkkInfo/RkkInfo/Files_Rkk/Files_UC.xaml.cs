using RkkInfo.Job_Vacancy;
using System;
using System.Collections;
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

namespace RkkInfo.Files_Rkk
{
    /// <summary>
    /// Логика взаимодействия для Files_UC.xaml
    /// </summary>
    public partial class Files_UC : UserControl
    {
        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        List<RkkInfo_Files> _list = new List<RkkInfo_Files>();

        public Files_UC(RkkInfo_dbEntities rkkInfo_DbEntities)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            LV_.ItemsSource = _context.RkkInfo_Files.OrderBy(t => t.RkkInfo_Files_id).ToList();
        }

        public void Update_Files()
        {
            _list = _context.RkkInfo_Files.ToList();
            LV_.ItemsSource = _list;
        }

        private void Vac_Files_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую нажали
            Button button = sender as Button;

            // Получаем элемент списка, связанный с кнопкой
            var item = button.DataContext as RkkInfo_Files;

            // Получаем данные файла из базы данных

            string fileName = item.RkkInfo_Files_Name;
            byte[] fileData = item.RkkInfo_Files_Files;

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

        private void Vac_Del_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var item = button.DataContext as RkkInfo_Files;
                _context.RkkInfo_Files.Remove(item);
                _context.SaveChanges();
                Update_Files();
            }
        }

        private void New_Files_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.SafeFileName;
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить файл?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    _context.RkkInfo_Files.Add(new RkkInfo_Files()
                    {
                        RkkInfo_Files_Name = fileName,
                        RkkInfo_Files_Data = DateTime.Now.ToString("dd.MM.yyyy"),
                        RkkInfo_Files_Files = imageBytes,

                    });
                    _context.SaveChanges();
                    Update_Files();
                }
            }
        }
    }
}
