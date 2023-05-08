using RkkInfo.Main_Win;
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
    /// Логика взаимодействия для Add_Depart.xaml
    /// </summary>
    public partial class Add_Depart : Window
    {

        private RkkInfo_dbEntities _context;
        private Main _uc;

        public Add_Depart(RkkInfo_dbEntities rkkInfo_DbEntities, Main main)
        {
            InitializeComponent();
            this._context = rkkInfo_DbEntities;
            this._uc = main;
        }

        private void ComeBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_Dep_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Вы уверены, что хотите добавить отдел?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var branchName = Depart.Text;
                var branch = _context.RkkInfo_Branch.FirstOrDefault(b => b.RkkInfo_Branch_Name == branchName);

                if (branch != null)
                {
                    MessageBox.Show("Данная компания уже существует");
                }
                else
                {
                    _context.RkkInfo_Branch.Add(new RkkInfo_Branch()
                    {
                        RkkInfo_Branch_Name = Depart.Text,
                    });


                    _context.SaveChanges();
                    _uc.But_Spawn(branchName);
                    this.Close();
                }
            }
        }
    }
}
