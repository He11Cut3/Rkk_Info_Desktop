using MailKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Windows.Shapes;
using System.Windows.Threading;
using RkkInfo.Authorization;
using MaterialDesignThemes.Wpf;
using System.Runtime.Remoting.Contexts;
using RkkInfo.Emp;
using RkkInfo.Job_Opening;
using RkkInfo.Job_Vacancy;
using RkkInfo.Files_Rkk;
using RkkInfo.Vacancy;
using RkkInfo.Dismis;

namespace RkkInfo.Main_Win
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private string _branchName = "";
        RkkInfo_dbEntities _context = new RkkInfo_dbEntities();
        private string branchName;

        public Main()
        {
            InitializeComponent();

        }

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        //===================================>

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            //Theme Code ========================>
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
            //===================================>
        }

        public void But_Spawn(string branchName) // добавляем параметр branchName
        {
            uc_spawn_stack.Children.Clear();
            var buttonData = _context.RkkInfo_Branch.ToList();
            foreach (var data in buttonData)
            {
                var button = new System.Windows.Controls.Button
                {
                    Name = "button_" + data.RkkInfo_Branch_id.ToString(),
                    Content = data.RkkInfo_Branch_Name,
                    Style = (Style)FindResource("Button_Style_Spawn"),
                    Tag = new Employ(branchName, _context)
                    
                };
                button.Click += Button_Click;
                uc_spawn_stack.Children.Add(button);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Получить уникальный идентификатор филиала из имени кнопки
                string buttonId = button.Name.Split('_').Last();

                // Использовать идентификатор, чтобы получить филиал из базы данных
                int branchId = int.Parse(buttonId);
                var branch = _context.RkkInfo_Branch.FirstOrDefault(b => b.RkkInfo_Branch_id == branchId);

                // Получить наименование кнопки и сохранить его в переменную branchName
                branchName = button.Content.ToString();

                // Удалить предыдущий UserControl, если он был добавлен ранее
                Empl_UC.Children.Clear();

                // Создать новый экземпляр UserControl и добавить его на StackPanel
                var userControl = new Employ(branchName, _context);
                userControl.Branch = branch;
                userControl.BranchName = branchName; // передать значение branchName в UserControl
                userControl.UpdateLayout(); // Обновить визуальный интерфейс пользовательского элемента управления
                Empl_UC.Children.Add(userControl);
            }
        }


        private void Employyes_Click(object sender, RoutedEventArgs e)
        {
            But_Spawn(_branchName);

            Dism.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();
            Rkk_File.Children.Clear();
            Vacansy.Children.Clear();
        }

        private void Jobs_Opening_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();

            Jobs_Ops jobs_Ops = new Jobs_Ops(_context);
            Jobs_Opening_UC.Children.Add(jobs_Ops);
        }

        private void Jobs_Vacancy_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Opening_UC.Children.Clear();

            Job_Vanac job_Vanac = new Job_Vanac(_context);
            Jobs_Vacancy_UC.Children.Add(job_Vanac);
        }

        private void Files_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();

            Files_UC files_UC = new Files_UC(_context);
            Rkk_File.Children.Add(files_UC);

        }

        private void Vacancy_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();

            Vacancy_UC vacancy_UC = new Vacancy_UC(_context);
            Vacansy.Children.Add(vacancy_UC);
        }

        private void Dismissal_Click(object sender, RoutedEventArgs e)
        {
            Dism.Children.Clear();
            Vacansy.Children.Clear();
            Rkk_File.Children.Clear();
            uc_spawn_stack.Children.Clear();
            Empl_UC.Children.Clear();
            Jobs_Opening_UC.Children.Clear();
            Jobs_Vacancy_UC.Children.Clear();

            Dismis_UC dismis_UC = new Dismis_UC();
            Dism.Children.Add(dismis_UC);
        }

        private void ComeBack_Click(object sender, RoutedEventArgs e)
        {
            Authorization_Window authorization_Window = new Authorization_Window();
            this.Close();
            authorization_Window.ShowDialog();
        }

        private void Add_Employ_Click(object sender, RoutedEventArgs e)
        {
            Add_Depart add_Depart = new Add_Depart(_context, this);
            add_Depart.ShowDialog();
        }

        private void FAQ_Click(object sender, RoutedEventArgs e)
        {
            FAQ fAQ = new FAQ();
            fAQ.ShowDialog();
        }
    }
}
