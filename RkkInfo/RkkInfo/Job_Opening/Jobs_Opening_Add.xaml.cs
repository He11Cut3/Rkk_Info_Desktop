using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.LinkLabel;

namespace RkkInfo.Job_Opening
{
    /// <summary>
    /// Логика взаимодействия для Jobs_Opening_Add.xaml
    /// </summary>
    public partial class Jobs_Opening_Add : Window
    {
        private RkkInfo_dbEntities _context;
        private Jobs_Ops _uc;

        public Jobs_Opening_Add( RkkInfo_dbEntities rkkInfo_DbEntities, Jobs_Ops jobs_Ops)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            _uc = jobs_Ops;
        }

        private void New_Jobs_Opening_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {
                    _context.RkkInfo_Jobs_Opening.Add(new RkkInfo_Jobs_Opening()
                    {
                        RkkInfo_Jobs_Opening_Name = Name.Text,
                        RkkInfo_Jobs_Opening_Date = Date.SelectedDate?.ToString("dd.MM.yyyy"),
                        RkkInfo_Jobs_Opening_Files = imageBytes,
                        RkkInfo_Jobs_Opening_Status = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),

                    });
                    _context.SaveChanges();
                    _uc.Update_Jobs_Open();
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
