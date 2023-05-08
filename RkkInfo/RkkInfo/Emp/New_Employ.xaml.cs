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
using static System.Windows.Forms.LinkLabel;

namespace RkkInfo.Emp
{
    /// <summary>
    /// Логика взаимодействия для New_Employ.xaml
    /// </summary>
    public partial class New_Employ : Window
    {
        private RkkInfo_dbEntities _context;
        private Employ _uc;
        public RkkInfo_Branch Branch { get; set; }
        public string BranchName { get; set; } // новое свойство
        private string _branchName;

        public New_Employ(string branchName, RkkInfo_dbEntities rkkInfo_DbEntities, Employ employ)
        {
            InitializeComponent();
            _branchName = branchName;
            this._context = rkkInfo_DbEntities;
            this._uc = employ;
            _branchName = branchName;
        }

        private void New_Employs_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите добавить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                if (myComboBox.SelectedIndex == -1 && Date.SelectedDate == null)
                {
                    MessageBox.Show("Не заполнена дата или статус");
                }
                else
                {
                    _context.RkkInfo_Employees.Add(new RkkInfo_Employees()
                    {
                        RkkInfo_Employees_First_Name = First_Name.Text,
                        RkkInfo_Employees_Last_Name = Last_Name.Text,
                        RkkInfo_Employees_Position = Position.Text,
                        RkkInfo_Employees_Department = _branchName,
                        RkkInfo_Employees_Start_Date = Date.SelectedDate?.ToString("dd.MM.yyyy"),
                        RkkInfo_Employees_Is_Active = (myComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                });
                    _context.SaveChanges();
                    _uc.Update_Emp();
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
