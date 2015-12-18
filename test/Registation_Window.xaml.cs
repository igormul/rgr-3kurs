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
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// Interaction logic for Registation_Window.xaml
    /// </summary>
    public partial class Registation_Window : MetroWindow
    {
        DataBase Data = new DataBase();
        public Registation_Window()
        {
            InitializeComponent();
            if (MainWindow.access == 0)
            {
                IsAdmin.IsEnabled = true;
            }
        }

        private void OKB_Click(object sender, RoutedEventArgs e)
        {
            DataTable DT = new DataTable();
            DT = Data.Ex_Select_LoginComm("SELECT Login FROM Admin WHERE Login = '" + Login.Text + "'");
            if (Password.Text == Repeat.Text)
            {
                if (DT.Rows.Count == 0)
                {
                    DT = Data.Ex_Select_LoginComm("SELECT Login FROM Users WHERE Login = '" + Login.Text + "'");
                    if (DT.Rows.Count == 0)
                    {
                        if (IsAdmin.IsChecked == true)
                        {
                            Data.LoginComm("INSERT INTO Admin (Login, Password) VALUES ('" + Login.Text +"', '" + Password.Text + "')");
                        }
                        else Data.LoginComm("INSERT INTO Users (Login, Password) VALUES ('" + Login.Text + "', '" + Password.Text + "')");
                    }
                    else MessageBox.Show("This login already exists!");
                }
                else MessageBox.Show("This login already exists!");
            }
            else MessageBox.Show("Passwords do NOT match!");
        }
    }
}
