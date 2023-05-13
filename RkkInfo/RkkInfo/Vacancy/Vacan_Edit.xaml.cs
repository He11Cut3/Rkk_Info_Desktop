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

namespace RkkInfo.Vacancy
{
    /// <summary>
    /// Логика взаимодействия для Vacan_Edit.xaml
    /// </summary>
    public partial class Vacan_Edit : Window
    {
        private RkkInfo_dbEntities _context;
        private Vacancy_UC vacancy_UC;
        private RkkInfo_Vacation rkkInfo_Vacation;

        public Vacan_Edit(RkkInfo_dbEntities rkkInfo_DbEntities, object o, Vacancy_UC vacancyc_UC)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            rkkInfo_Vacation = (o as Button).DataContext as RkkInfo_Vacation;
            vacancy_UC = vacancyc_UC;

            myComboBox.Text = rkkInfo_Vacation.RkkInfo_Vacation_Name;
            Last_Name.Text = rkkInfo_Vacation.RkkInfo_Vacation_Last_Name;
            First_Name.Text = rkkInfo_Vacation.RkkInfo_Vacation_First_Name;
            Patronymic.Text = rkkInfo_Vacation.RkkInfo_Vacation_Patronymic;
            Position.Text = rkkInfo_Vacation.RkkInfo_Vacation_Position;
            Date_Start.Text = rkkInfo_Vacation.RkkInfo_Vacation_Start_Date;
            Date_End.Text = rkkInfo_Vacation.RkkInfo_Vacation_End_Date;

            Last_Name.IsEnabled = false;
            First_Name.IsEnabled = false;
            Patronymic.IsEnabled = false;
            Position.IsEnabled = false;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Word Files (*.docx)|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if ((MessageBox.Show("Вы уверены, что хотите изменить информацию?", "Изменение", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    rkkInfo_Vacation.RkkInfo_Vacation_Name = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                    rkkInfo_Vacation.RkkInfo_Vacation_Last_Name = Last_Name.Text;
                    rkkInfo_Vacation.RkkInfo_Vacation_First_Name = First_Name.Text;
                    rkkInfo_Vacation.RkkInfo_Vacation_Position = Position.Text;
                    rkkInfo_Vacation.RkkInfo_Vacation_Patronymic = Patronymic.Text;
                    rkkInfo_Vacation.RkkInfo_Vacation_Start_Date = Date_Start.SelectedDate?.ToString("dd.MM.yyyy");
                    rkkInfo_Vacation.RkkInfo_Vacation_End_Date = Date_Start.SelectedDate?.ToString("dd.MM.yyyy");
                    rkkInfo_Vacation.RkkInfo_Vacation_Files = imageBytes;

                    _context.SaveChanges();
                    vacancy_UC.Update_VAC();
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
