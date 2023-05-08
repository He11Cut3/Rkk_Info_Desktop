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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RkkInfo.Emp
{
    /// <summary>
    /// Логика взаимодействия для Employ.xaml
    /// </summary>
    public partial class Employ : UserControl
    {
        public RkkInfo_Branch Branch { get; set; }
        public string BranchName { get; set; } // новое свойство
        private string _branchName;

        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        List<RkkInfo_Branch> _list = new List<RkkInfo_Branch>();

        public Employ(string branchName, RkkInfo_dbEntities rkkInfo_DbEntities)
        {
            InitializeComponent();
            this._context = rkkInfo_DbEntities;
            _branchName = branchName;

            var result = from item in _context.RkkInfo_Employees
                         where item.RkkInfo_Employees_Department.Contains(_branchName)
                         select item;
            LV_.ItemsSource = result.ToList();

        }

        public void Update_Emp()
        {
            var result = from item in _context.RkkInfo_Employees
                         where item.RkkInfo_Employees_Department.Contains(_branchName)
                         select item;
            LV_.ItemsSource = result.ToList();
        }


        private void Emd_Del_Click(object sender, RoutedEventArgs e)
        {
            Edit_Del_Employ edit_Del_Employ = new Edit_Del_Employ(_context, sender, this);
            edit_Del_Employ.ShowDialog();
        }

        private void New_Emp_Click(object sender, RoutedEventArgs e)
        {
            New_Employ new_Employ = new New_Employ(_branchName, _context, this);
            new_Employ.ShowDialog();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var item = button.DataContext as RkkInfo_Employees;
                _context.RkkInfo_Employees.Remove(item);
                _context.SaveChanges();
                Update_Emp();
            }
        }
    }
}
