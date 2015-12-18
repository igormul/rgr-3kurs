using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static int access = 145;
        DataBase Data = new DataBase();
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Update_main(object sender, System.EventArgs e)
        {
            DG.ItemsSource = Data.Ex_Select_Comm("Select Name, Sequel, Genre, Studio, Country, Price From V_main").DefaultView; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DG.ItemsSource = Data.Ex_Select_Comm("Select Name, Sequel, Genre, Studio, Country, Price From V_main").DefaultView;
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)DG.SelectedItems[0];
            Info_window info = new Info_window(row);
            info.Show();

        }

        private void InsertB_Click(object sender, RoutedEventArgs e)
        {
            DataBase Data = new DataBase();
            DG.ItemsSource = Data.Ex_Select_Comm("Select Name, Sequel, Genre, Studio, Country, Price From V_main").DefaultView;
            Insert_Wind insert_w = new Insert_Wind(Data);
            insert_w.Closed += Update_main;
            insert_w.Show();
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)DG.SelectedItems[0];
            Data.InsertComm("Delete From main where Name = '" + row[0].ToString() + "' AND Sequel = " + row[1].ToString() + ";");
            DG.ItemsSource = Data.Ex_Select_Comm("Select Name, Sequel, Genre, Studio, Country, Price From V_main").DefaultView; 
        }

        private void UpdateB_Click(object sender, RoutedEventArgs e)
        {
            Update_window update_w = new Update_window();
            update_w.Closed += Update_main;
            update_w.Show();
        }

        private void MenuItem2_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)DG.SelectedItems[0];
            Update_window update_w = new Update_window(row);
            update_w.Closed += Update_main;
            update_w.Show();
        }

        private void MenuItem3_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)DG.SelectedItems[0];
            Data.InsertComm("Delete From main where Name = '" + row[0].ToString() + "' AND Sequel = " + row[1].ToString() + ";");
            DG.ItemsSource = Data.Ex_Select_Comm("Select Name, Sequel, Genre, Studio, Country, Price From V_main").DefaultView;
        }

        private void SingB_Click(object sender, RoutedEventArgs e)
        {
            access = Data.Access(LoginBox.Text, PassBox.Text);
            switch (access)
            {
                case 0:
                    {
                        DG.IsEnabled = true;
                        InsertB.IsEnabled = true;
                        DeleteB.IsEnabled = true;
                        UpdateB.IsEnabled = true;
                        LoginBox.Visibility = Visibility.Hidden;
                        PassBox.Visibility = Visibility.Hidden;
                        SignB.Visibility = Visibility.Hidden;
                        SignOutB.Visibility = Visibility.Visible;
                        break;
                    }
                case 1:
                    {
                        DG.IsEnabled = true;
                        MenuUpdate.IsEnabled = false;
                        MenuDelete.IsEnabled = false;
                        LoginBox.Visibility = Visibility.Hidden;
                        PassBox.Visibility = Visibility.Hidden;
                        SignB.Visibility = Visibility.Hidden;
                        SignOutB.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show("Wrong login or password!");
                        break;
                    }
            }
        }
        private void SingOut_Click(object sender, RoutedEventArgs e)
        {
            DG.IsEnabled = false;
            InsertB.IsEnabled = false;
            DeleteB.IsEnabled = false;
            UpdateB.IsEnabled = false;
            LoginBox.Visibility = Visibility.Visible;
            PassBox.Visibility = Visibility.Visible;
            SignB.Visibility = Visibility.Visible;
            SignOutB.Visibility = Visibility.Hidden;
            LoginBox.Text = "Login";
            PassBox.Text = "Password";
            access = 6;
        }

        private void RegisterB_Click(object sender, RoutedEventArgs e)
        {
            Registation_Window reg_w = new Registation_Window();
            reg_w.Show();
        }
    }
    public class InsertComm
    {
        public string Title;
        public string Sequel;
        public string Studio_id;
        public string Genre_id;
        public string Year;
        public string Description;
        public string Image;
        public string Price;
        public string Gathered;

        public InsertComm()
        {

        }
    }
}
