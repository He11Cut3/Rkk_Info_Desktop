using RkkInfo.Vacancy;
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

namespace RkkInfo.Dismis
{
    /// <summary>
    /// Логика взаимодействия для Dism_Edit.xaml
    /// </summary>
    public partial class Dism_Edit : Window
    {
        private RkkInfo_dbEntities _context;
        private Dismis_UC dismis_UC;
        private RkkInfo_Dismissal rkkInfo_Dismissal;

        public Dism_Edit(RkkInfo_dbEntities rkkInfo_DbEntities, object o, Dismis_UC dismiss_UC)
        {
            InitializeComponent();
            _context = rkkInfo_DbEntities;
            rkkInfo_Dismissal = (o as Button).DataContext as RkkInfo_Dismissal;
            dismis_UC = dismiss_UC;

            myComboBox.Text = rkkInfo_Dismissal.RkkInfo_Dismissal_Name;
            Last_Name.Text = rkkInfo_Dismissal.RkkInfo_Dismissal_Last_Name;
            First_Name.Text = rkkInfo_Dismissal.RkkInfo_Dismissal_First_Name;
            Position.Text = rkkInfo_Dismissal.RkkInfo_Dismissal_Position;
            Date.Text = rkkInfo_Dismissal.RkkInfo_Dismissal_Date;
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
                    rkkInfo_Dismissal.RkkInfo_Dismissal_Name = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                    rkkInfo_Dismissal.RkkInfo_Dismissal_Last_Name = Last_Name.Text;
                    rkkInfo_Dismissal.RkkInfo_Dismissal_First_Name = First_Name.Text;
                    rkkInfo_Dismissal.RkkInfo_Dismissal_Position = Position.Text;
                    rkkInfo_Dismissal.RkkInfo_Dismissal_Date = Date.SelectedDate?.ToString("dd.MM.yyyy");
                    rkkInfo_Dismissal.RkkInfo_Dismissal_Files = imageBytes;

                    _context.SaveChanges();
                    dismis_UC.Update_Dis();
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
