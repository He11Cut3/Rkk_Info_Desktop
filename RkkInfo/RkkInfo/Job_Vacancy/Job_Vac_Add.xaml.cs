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

        public Job_Vac_Add(RkkInfo_dbEntities context, object o, Jobs_Ops jobs_)
        {
            InitializeComponent();
            _context = context;
            rkkInfo_Jobs_Opening = (o as Button).DataContext as RkkInfo_Jobs_Opening;
            job_Vanac = jobs_;

            Name.Text = rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Name + "_Вакансия";
            Date.Text = rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Date;

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
                        RkkInfo_Jobs_Vacancy_Position = Position.Text,
                        RkkInfo_Jobs_Vacancy_Date = Date.SelectedDate?.ToString("dd.MM.yyyy"),
                        RkkInfo_Jobs_Vacancy_Files = imageBytes,
                        RkkInfo_Jobs_Vacancy_Status = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),

                    });
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
