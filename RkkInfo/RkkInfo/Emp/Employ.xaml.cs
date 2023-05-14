using MaterialDesignColors;
using RkkInfo.Job_Opening;
using System;
using System.Collections.Generic;
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
        private string _login;

        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        List<RkkInfo_Branch> _list = new List<RkkInfo_Branch>();

        public Employ(string branchName, RkkInfo_dbEntities rkkInfo_DbEntities, string login)
        {
            InitializeComponent();
            this._context = rkkInfo_DbEntities;
            _branchName = branchName;
            _login = login;

            var result = from item in _context.RkkInfo_Employees
                         where item.RkkInfo_Employees_Department.Contains(_branchName)
                         select item;
            LV_.ItemsSource = result.ToList();

            if (!_login.Contains("_admin"))
            {
                New_Emp.Visibility = Visibility.Collapsed;
            }
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
            if (!_login.Contains("_admin"))
            {
                System.Windows.MessageBox.Show("У вас нету доступа к этой функции");
            }
            else
            {
                Edit_Del_Employ edit_Del_Employ = new Edit_Del_Employ(_context, sender, this);
                edit_Del_Employ.ShowDialog();
            }
        }

        private void New_Emp_Click(object sender, RoutedEventArgs e)
        {
            if (!_login.Contains("_admin"))
            {
                System.Windows.MessageBox.Show("У вас нету доступа к этой функции");
            }
            else
            {
                New_Employ new_Employ = new New_Employ(_branchName, _context, this);
                new_Employ.ShowDialog();
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (!_login.Contains("_admin"))
            {
                System.Windows.MessageBox.Show("У вас нету доступа к этой функции");
            }
            else
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

        private void Finder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = Finder.Text;
            var query = from emp in _context.RkkInfo_Employees
                        where emp.RkkInfo_Employees_Last_Name.Contains(searchText)
                            || emp.RkkInfo_Employees_First_Name.Contains(searchText)
                            || emp.RkkInfo_Employees_Patronymic.Contains(searchText)
                            || emp.RkkInfo_Employees_Position.Contains(searchText)
                            || emp.RkkInfo_Employees_Department.Contains(searchText)
                            || emp.RkkInfo_Employees_Is_Active.Contains(searchText)
                        select emp;

            LV_.ItemsSource = query.ToList();
        }

        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = ((ComboBoxItem)myComboBox.SelectedItem).Content.ToString();

            var sortedQuery = from emp in _context.RkkInfo_Employees
                              select emp;

            switch (selectedValue)
            {
                case "Фамилия":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Employees_Last_Name);
                    break;
                case "Имя":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Employees_First_Name);
                    break;
                case "Отчество":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Employees_Patronymic);
                    break;
                case "Должность":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Employees_Position);
                    break;
                case "Статус":
                    sortedQuery = sortedQuery.OrderBy(emp => emp.RkkInfo_Employees_Is_Active);
                    break;
                default:
                    break;
            }

            LV_.ItemsSource = sortedQuery.ToList();
        }
    }
}
