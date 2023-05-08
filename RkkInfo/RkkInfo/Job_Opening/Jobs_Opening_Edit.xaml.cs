using RkkInfo.Emp;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RkkInfo.Job_Opening
{
    /// <summary>
    /// Логика взаимодействия для Jobs_Opening_Edit.xaml
    /// </summary>
    public partial class Jobs_Opening_Edit : Window
    {
        private RkkInfo_dbEntities _context;
        private Jobs_Ops jobs_Ops;
        private RkkInfo_Jobs_Opening rkkInfo_Jobs_Opening;

        public Jobs_Opening_Edit(RkkInfo_dbEntities context, object o, Jobs_Ops jobs_)
        {
            InitializeComponent();
            _context = context;
            rkkInfo_Jobs_Opening = (o as Button).DataContext as RkkInfo_Jobs_Opening;
            jobs_Ops = jobs_;

            Name.Text = rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Name;
            Date.Text = rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Date;
            myComboBox.Text = rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Status;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Вы уверены, что хотите изменить информацию?", "Изменение", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                if (myComboBox.SelectedIndex == -1 && Date.SelectedDate == null)
                {
                    MessageBox.Show("Не заполнена дата или статус");
                }
                else
                {
                    rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Name = Name.Text;
                    rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Date = Date.SelectedDate?.ToString("dd.MM.yyyy");
                    rkkInfo_Jobs_Opening.RkkInfo_Jobs_Opening_Status = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

                    _context.SaveChanges();
                    jobs_Ops.Update_Jobs_Open();
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
