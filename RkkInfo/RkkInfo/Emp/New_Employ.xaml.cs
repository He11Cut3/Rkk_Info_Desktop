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

            var entities = from e in _context.RkkInfo_Users_Post
                           select e;

            // Преобразуем список объектов в список строк
            List<string> items = entities.Select(e => e.RkkInfo_Users_Post_Name).ToList();

            // Устанавливаем источник данных для ComboBox
            Position.ItemsSource = items;

        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = true;
            }
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
                    string lastName = Last_Name.Text;
                    string firstName = First_Name.Text;
                    string patronymic = Patronymic.Text;

                    // Проверка наличия сотрудников с таким же именем, фамилией и отчеством
                    int count = _context.RkkInfo_Employees
                        .Count(A => A.RkkInfo_Employees_Last_Name == lastName &&
                                    A.RkkInfo_Employees_First_Name == firstName &&
                                    A.RkkInfo_Employees_Patronymic == patronymic);

                    if (count > 0)
                    {
                        // Добавление к фамилии символа "_" и идентификатора
                        lastName += "_" + count;
                    }

                    _context.RkkInfo_Employees.Add(new RkkInfo_Employees()
                    {
                        RkkInfo_Employees_First_Name = firstName,
                        RkkInfo_Employees_Last_Name = lastName,
                        RkkInfo_Employees_Patronymic = Patronymic.Text,
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
