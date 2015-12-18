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
    /// Interaction logic for Info_window.xaml
    /// </summary>
    public partial class Info_window : MetroWindow
    {
        public Info_window()
        {
            InitializeComponent();
        }
        public Info_window(DataRowView row)
        {
            InitializeComponent();
            InfoGrid.DataContext = row;
            DataBase DB = new DataBase();
            if (row[1].ToString() == "1")
            {
                InfoGrid.DataContext = DB.Ex_Select_Comm("select Image, Name, Genre from V_main where Name = '" + row[0].ToString() + "'");
            }
            else InfoGrid.DataContext = DB.Ex_Select_Comm("select Image, Name, Sequel, Genre from V_main where Name = '" + row[0].ToString() + "'");
            Studio.Content = "Studio: " + DB.Ex_Select_Comm("select Studio from V_main where Name = '" + row[0].ToString() + "'").Rows[0][0].ToString();
            Year.Content = DB.Ex_Select_Comm("select Year from Studio_info where Studio = (select Studio from V_main where Name = '"+ row[0].ToString() + "')").Rows[0][0].ToString();
            Price.Content = "Price: " + DB.Ex_Select_Comm("select Price from V_main where Name = '" + row[0].ToString() + "'").Rows[0][0].ToString() + "$";
            BoxOffice.Content = "Gathered: " + DB.Ex_Select_Comm("select Gathered from Main where Name = '" + row[0].ToString() + "'").Rows[0][0].ToString();
            MainText.Text = DB.Ex_Select_Comm("select Description from Main where Name = '" + row[0].ToString() + "'").Rows[0][0].ToString();
        }
    }
}
