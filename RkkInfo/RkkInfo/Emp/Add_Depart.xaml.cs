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
                var branchName = Depart_add.Text;
                var branch = _context.RkkInfo_Branch.FirstOrDefault(b => b.RkkInfo_Branch_Name == branchName);

                if (branch != null)
                {
                    MessageBox.Show("Данный отдел уже существует");
                }
                else
                {
                    _context.RkkInfo_Branch.Add(new RkkInfo_Branch()
                    {
                        RkkInfo_Branch_Name = Depart_add.Text,
                    });

                    Depart_add.Text = "";
                    _context.SaveChanges();
                }
            }
        }


        private void New_Post_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Вы уверены, что хотите добавить должность?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var branchName = Post_add.Text;
                var branch = _context.RkkInfo_Users_Post.FirstOrDefault(b => b.RkkInfo_Users_Post_Name == branchName);

                if (branch != null)
                {
                    MessageBox.Show("Данная должность уже существует");
                }
                else
                {
                    _context.RkkInfo_Users_Post.Add(new RkkInfo_Users_Post()
                    {
                        RkkInfo_Users_Post_Name = Post_add.Text,
                    });

                    Post_add.Text = "";
                    _context.SaveChanges();
                }
            }
        }

        private void Del_Dep_Click(object sender, RoutedEventArgs e)
        {
            

            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                // Получаем выбранную запись
                string selectedBranchName = Depart_del.SelectedItem.ToString();
                var branchToDelete = _context.RkkInfo_Branch.FirstOrDefault(b => b.RkkInfo_Branch_Name == selectedBranchName);

                if (branchToDelete != null)
                {
                    // Удаляем запись из контекста базы данных
                    _context.RkkInfo_Branch.Remove(branchToDelete);

                    // Сохраняем изменения в базе данных
                    _context.SaveChanges();

                    var entities = from f in _context.RkkInfo_Branch
                                   select f;



                    // Преобразуем список объектов в список строк
                    List<string> items = entities.Select(f => f.RkkInfo_Branch_Name).ToList();

                    // Устанавливаем источник данных для ComboBox
                    Depart_del.ItemsSource = items;
                }

            }
        }

        private void Del_Post_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                // Получаем выбранную запись
                string selectedBranchName = Post_del.SelectedItem.ToString();
                var branchToDelete = _context.RkkInfo_Users_Post.FirstOrDefault(b => b.RkkInfo_Users_Post_Name == selectedBranchName);

                if (branchToDelete != null)
                {
                    // Удаляем запись из контекста базы данных
                    _context.RkkInfo_Users_Post.Remove(branchToDelete);

                    // Сохраняем изменения в базе данных
                    _context.SaveChanges();

                    var entities1 = from A in _context.RkkInfo_Users_Post
                                    select A;

                    List<string> items1 = entities1.Select(A => A.RkkInfo_Users_Post_Name).ToList();

                    // Устанавливаем источник данных для ComboBox
                    Post_del.ItemsSource = items1;
                }

            }
        }






        private void New_Depart_c_Click(object sender, RoutedEventArgs e)
        {

            Name_Text.Text = "";

            Depart_add.Visibility = Visibility.Collapsed;
            Depart_del.Visibility = Visibility.Collapsed;
            Post_add.Visibility = Visibility.Collapsed;
            Post_del.Visibility = Visibility.Collapsed;
            Post_Edit.Visibility = Visibility.Collapsed;
            Depart_Edit.Visibility = Visibility.Collapsed;
            Edit_B_Dep.Visibility = Visibility.Collapsed;
            Edit_B_Post.Visibility = Visibility.Collapsed;
            New_Dep.Visibility = Visibility.Collapsed;
            New_Post.Visibility = Visibility.Collapsed;
            Del_Dep.Visibility = Visibility.Collapsed;
            Del_Post.Visibility = Visibility.Collapsed;
            Edit_Post.Visibility = Visibility.Collapsed;
            Edit_Dep.Visibility = Visibility.Collapsed;

            Depart_add.Visibility = Visibility.Visible;
            New_Dep.Visibility = Visibility.Visible;

            Name_Text.Text = "Наименование отдела";
        }

        private void New_Post_C_Click(object sender, RoutedEventArgs e)
        {
            Name_Text.Text = "";

            Depart_add.Visibility = Visibility.Collapsed;
            Depart_del.Visibility = Visibility.Collapsed;
            Post_add.Visibility = Visibility.Collapsed;
            Post_del.Visibility = Visibility.Collapsed;
            Post_Edit.Visibility = Visibility.Collapsed;
            Depart_Edit.Visibility = Visibility.Collapsed;
            Edit_B_Dep.Visibility = Visibility.Collapsed;
            Edit_B_Post.Visibility = Visibility.Collapsed;
            New_Dep.Visibility = Visibility.Collapsed;
            New_Post.Visibility = Visibility.Collapsed;
            Del_Dep.Visibility = Visibility.Collapsed;
            Del_Post.Visibility = Visibility.Collapsed;
            Edit_Post.Visibility = Visibility.Collapsed;
            Edit_Dep.Visibility = Visibility.Collapsed;

            New_Post.Visibility = Visibility.Visible;
            Post_add.Visibility = Visibility.Visible;

            Name_Text.Text = "Наименование должности";
        }

        private void Del_Depart_C_Click(object sender, RoutedEventArgs e)
        {
            Name_Text.Text = "";

            var entities = from f in _context.RkkInfo_Branch
                           select f;



            // Преобразуем список объектов в список строк
            List<string> items = entities.Select(f => f.RkkInfo_Branch_Name).ToList();

            // Устанавливаем источник данных для ComboBox
            Depart_del.ItemsSource = items;

            Depart_add.Visibility = Visibility.Collapsed;
            Depart_del.Visibility = Visibility.Collapsed;
            Post_add.Visibility = Visibility.Collapsed;
            Post_del.Visibility = Visibility.Collapsed;
            Post_Edit.Visibility = Visibility.Collapsed;
            Depart_Edit.Visibility = Visibility.Collapsed;
            Edit_B_Dep.Visibility = Visibility.Collapsed;
            Edit_B_Post.Visibility = Visibility.Collapsed;
            New_Dep.Visibility = Visibility.Collapsed;
            New_Post.Visibility = Visibility.Collapsed;
            Del_Dep.Visibility = Visibility.Collapsed;
            Del_Post.Visibility = Visibility.Collapsed;
            Edit_Post.Visibility = Visibility.Collapsed;
            Edit_Dep.Visibility = Visibility.Collapsed;

            Del_Dep.Visibility = Visibility.Visible;
            Depart_del.Visibility = Visibility.Visible;

            Name_Text.Text = "Наименование отдела";

        }

        private void Del_Post_C_Click(object sender, RoutedEventArgs e)
        {
            Name_Text.Text = "";

            var entities1 = from A in _context.RkkInfo_Users_Post
                            select A;

            List<string> items1 = entities1.Select(A => A.RkkInfo_Users_Post_Name).ToList();

            // Устанавливаем источник данных для ComboBox
            Post_del.ItemsSource = items1;

            Depart_add.Visibility = Visibility.Collapsed;
            Depart_del.Visibility = Visibility.Collapsed;
            Post_add.Visibility = Visibility.Collapsed;
            Post_del.Visibility = Visibility.Collapsed;
            Post_Edit.Visibility = Visibility.Collapsed;
            Depart_Edit.Visibility = Visibility.Collapsed;
            Edit_B_Dep.Visibility = Visibility.Collapsed;
            Edit_B_Post.Visibility = Visibility.Collapsed;
            New_Dep.Visibility = Visibility.Collapsed;
            New_Post.Visibility = Visibility.Collapsed;
            Del_Dep.Visibility = Visibility.Collapsed;
            Del_Post.Visibility = Visibility.Collapsed;
            Edit_Post.Visibility = Visibility.Collapsed;
            Edit_Dep.Visibility = Visibility.Collapsed;

            Del_Post.Visibility = Visibility.Visible;
            Post_del.Visibility = Visibility.Visible;

            Name_Text.Text = "Наименование должности";

        }
        private void Edit_Dep_C_Click(object sender, RoutedEventArgs e)
        {
            var entities = from post in _context.RkkInfo_Branch

                           select post.RkkInfo_Branch_Name;

            List<string> postList = entities.ToList();

            // Устанавливаем источник данных для ComboBox
            Depart_Edit.ItemsSource = postList;
            Name_Text.Text = "";

            Depart_add.Visibility = Visibility.Collapsed;
            Depart_del.Visibility = Visibility.Collapsed;
            Post_add.Visibility = Visibility.Collapsed;
            Post_del.Visibility = Visibility.Collapsed;
            Post_Edit.Visibility = Visibility.Collapsed;
            Depart_Edit.Visibility = Visibility.Collapsed;
            Edit_B_Dep.Visibility = Visibility.Collapsed;
            Edit_B_Post.Visibility= Visibility.Collapsed;

            New_Dep.Visibility = Visibility.Collapsed;
            New_Post.Visibility = Visibility.Collapsed;
            Del_Dep.Visibility = Visibility.Collapsed;
            Del_Post.Visibility = Visibility.Collapsed;
            Edit_Post.Visibility = Visibility.Collapsed;
            Edit_Dep.Visibility = Visibility.Collapsed;

            Edit_Dep.Visibility = Visibility.Visible;
            Depart_Edit.Visibility = Visibility.Visible;
            Edit_B_Dep.Visibility = Visibility.Visible;


            Name_Text.Text = "Наименование отдела (изм)";
        }

        private void Edit_Post_C_Click(object sender, RoutedEventArgs e)
        {
            var entities = from post in _context.RkkInfo_Users_Post
                           select post.RkkInfo_Users_Post_Name;

            List<string> postList = entities.ToList();

            // Устанавливаем источник данных для ComboBox
            Post_Edit.ItemsSource = postList;

            Name_Text.Text = "";

            Depart_add.Visibility = Visibility.Collapsed;
            Depart_del.Visibility = Visibility.Collapsed;
            Post_add.Visibility = Visibility.Collapsed;
            Post_del.Visibility = Visibility.Collapsed;
            Post_Edit.Visibility = Visibility.Collapsed;
            Depart_Edit.Visibility = Visibility.Collapsed;

            New_Dep.Visibility = Visibility.Collapsed;
            New_Post.Visibility = Visibility.Collapsed;
            Del_Dep.Visibility = Visibility.Collapsed;
            Del_Post.Visibility = Visibility.Collapsed;
            Edit_Post.Visibility = Visibility.Collapsed;
            Edit_Dep.Visibility = Visibility.Collapsed;

            Edit_Post.Visibility = Visibility.Visible;
            Post_Edit.Visibility = Visibility.Visible;
            Edit_B_Post.Visibility = Visibility.Visible;

            Name_Text.Text = "Наименование должности (изм)";
        }

        private void Edit_B_Post_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите сохранить изменения?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                // Получаем выбранный элемент в ComboBox
                string selectedPost = Post_Edit.SelectedItem.ToString();

                // Получаем запись из базы данных, соответствующую выбранному элементу
                var postToUpdate = _context.RkkInfo_Users_Post.FirstOrDefault(b => b.RkkInfo_Users_Post_Name == selectedPost);

                if (postToUpdate != null)
                {
                    // Обновляем данные выбранной записи с новыми значениями из TextBox
                    postToUpdate.RkkInfo_Users_Post_Name = Edit_Post.Text;

                    // Сохраняем изменения в базе данных
                    _context.SaveChanges();

                    var entities = from post in _context.RkkInfo_Users_Post
                                   select post.RkkInfo_Users_Post_Name;

                    List<string> postList = entities.ToList();
                    Post_Edit.ItemsSource = postList;
                    Edit_Post.Text = "";

                    System.Windows.MessageBox.Show("Изменения сохранены!");
                }
            }
        }
        private void Post_Edit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, есть ли выбранный элемент
            if (Post_Edit.SelectedItem != null)
            {
                // Получаем выбранный элемент в ComboBox
                string selectedPost = Post_Edit.SelectedItem.ToString();

                // Устанавливаем выбранное значение в TextBox
                Edit_Post.Text = selectedPost;
            }
        }

        private void Edit_B_Dep_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите сохранить изменения?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                // Получаем выбранный элемент в ComboBox
                string selectedPost = Depart_Edit.SelectedItem.ToString();

                // Получаем запись из базы данных, соответствующую выбранному элементу
                var postToUpdate = _context.RkkInfo_Branch.FirstOrDefault(b => b.RkkInfo_Branch_Name == selectedPost);

                if (postToUpdate != null)
                {
                    // Обновляем данные выбранной записи с новыми значениями из TextBox
                    postToUpdate.RkkInfo_Branch_Name = Edit_Dep.Text;

                    // Сохраняем изменения в базе данных
                    _context.SaveChanges();

                    var entities = from post in _context.RkkInfo_Branch
                                   select post.RkkInfo_Branch_Name;

                    List<string> postList = entities.ToList();
                    Depart_Edit.ItemsSource = postList;
                    Edit_Dep.Text = "";

                    System.Windows.MessageBox.Show("Изменения сохранены!");
                }
            }
        }

        private void Depart_Edit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, есть ли выбранный элемент
            if (Depart_Edit.SelectedItem != null)
            {
                // Получаем выбранный элемент в ComboBox
                string selectedPost = Depart_Edit.SelectedItem.ToString();

                // Устанавливаем выбранное значение в TextBox
                Edit_Dep.Text = selectedPost;
            }
        }

        
    }
}
