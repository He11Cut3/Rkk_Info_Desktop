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

namespace RkkInfo.Emp
{
    /// <summary>
    /// Логика взаимодействия для Edit_Del_Employ.xaml
    /// </summary>
    public partial class Edit_Del_Employ : Window
    {
        private RkkInfo_dbEntities _context;
        private Employ employ;
        private RkkInfo_Employees _employees;

        public Edit_Del_Employ(RkkInfo_dbEntities context, object o, Employ employees)
        {
            InitializeComponent();
            _context = context;
            _employees = (o as Button).DataContext as RkkInfo_Employees;
            employ = employees;

            First_Name.Text = _employees.RkkInfo_Employees_First_Name;
            Last_Name.Text = _employees.RkkInfo_Employees_Last_Name;
            Position.Text = _employees.RkkInfo_Employees_Position;
            Date.Text = _employees.RkkInfo_Employees_Start_Date;
            myComboBox.Text = _employees.RkkInfo_Employees_Is_Active;

            
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
                    _employees.RkkInfo_Employees_First_Name = First_Name.Text;
                    _employees.RkkInfo_Employees_Last_Name = Last_Name.Text;
                    _employees.RkkInfo_Employees_Position = Position.Text;
                    _employees.RkkInfo_Employees_Start_Date = Date.SelectedDate?.ToString("dd.MM.yyyy");
                    _employees.RkkInfo_Employees_Is_Active = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

                    _context.SaveChanges();
                    employ.Update_Emp();
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
