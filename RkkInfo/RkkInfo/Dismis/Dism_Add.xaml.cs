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
using System.Windows.Shapes;

namespace RkkInfo.Dismis
{
    /// <summary>
    /// Логика взаимодействия для Dism_Add.xaml
    /// </summary>
    public partial class Dism_Add : Window
    {
        private RkkInfo_dbEntities _context;
        private Dismis_UC _uc;

        public Dism_Add(RkkInfo_dbEntities rkkInfo_DbEntities, Dismis_UC dismis_UC)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            _uc = dismis_UC;
        }
        private void ComeBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_Dism_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Word Files (*.docx)|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    _context.RkkInfo_Dismissal.Add(new RkkInfo_Dismissal()
                    {
                        RkkInfo_Dismissal_Name = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                        RkkInfo_Dismissal_First_Name = First_Name.Text,
                        RkkInfo_Dismissal_Last_Name = Last_Name.Text,
                        RkkInfo_Dismissal_Position = Position.Text,
                        RkkInfo_Dismissal_Date = Date.SelectedDate?.ToString("dd.MM.yyyy"),
                        RkkInfo_Dismissal_Files = imageBytes,
                        RkkInfo_Dismissal_Status = "В процессе обработки",

                    });
                    _context.SaveChanges();
                    _uc.Update_Dis();
                    this.Close();
                }
            }
        }
    }
}
