using RkkInfo.Emp;
using RkkInfo.Job_Vacancy;
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

namespace RkkInfo.Vacancy
{
    /// <summary>
    /// Логика взаимодействия для Vacan_Add.xaml
    /// </summary>
    public partial class Vacan_Add : Window
    {
        private RkkInfo_dbEntities _context;
        private Vacancy_UC _uc;

        public Vacan_Add(RkkInfo_dbEntities rkkInfo_DbEntities, Vacancy_UC vacancy_UC)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            _uc = vacancy_UC;

        }

        private void ComeBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_Vaca_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Word Files (*.docx)|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    _context.RkkInfo_Vacation.Add(new RkkInfo_Vacation()
                    {
                        RkkInfo_Vacation_Name = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                        RkkInfo_Vacation_First_Name = First_Name.Text,
                        RkkInfo_Vacation_Last_Name = Last_Name.Text,
                        RkkInfo_Vacation_Position = Position.Text,
                        RkkInfo_Vacation_Start_Date = Date_Start.SelectedDate?.ToString("dd.MM.yyyy"),
                        RkkInfo_Vacation_End_Date = Date_End.SelectedDate?.ToString("dd.MM.yyyy"),
                        RkkInfo_Vacation_Files = imageBytes,
                        RkkInfo_Vacation_Status = "В процессе обработки",

                    });
                    _context.SaveChanges();
                    _uc.Update_VAC();
                    this.Close();
                }
            }
        }
    }
}
