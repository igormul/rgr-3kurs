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
    /// Interaction logic for Update_window.xaml
    /// </summary>
    public partial class Update_window : MetroWindow
    {
        DataBase DB = new DataBase();
        DataTable DT = new DataTable();
        public Update_window()
        {
            InitializeComponent();
        }
        public Update_window(DataRowView row)
        {
            InitializeComponent();
            Search_n.Text = row[0].ToString();
            Search_s.Text = row[1].ToString();
            DT = DB.Ex_Select_Comm("SELECT Name, Sequel FROM Main WHERE Name = '" + row[0].ToString() + "' AND Sequel = '" + row[1].ToString() + "'");
            if (!(DT.Rows.Count == 0))
            {
                Message.Content = "Success!";
                Price.IsEnabled = true;
                Studio.IsEnabled = true;
                Gathered.IsEnabled = true;
                Year.IsEnabled = true;
                Description.IsEnabled = true;
                Image.IsEnabled = true;
                Genre.IsEnabled = true;
                AD_ex.IsEnabled = true;
                UpGrid.DataContext = DB.Ex_Select_Comm("Select m.Price, s.Studio, m.Gathered, m.Year, m.Description, m.Image, g.Genre FROM Main AS m JOIN Genre AS g ON m.Genre_id = g.Id JOIN Studio_info AS s ON m.Studio_id = s.Id WHERE Name = '" + Search_n.Text + "';");

            }
            else Message.Content = "Not found :(";
        }
        private void Add_Studio_Click(object sender, RoutedEventArgs e)
        {
            DataTable DT = new DataTable();
            DT = DB.Ex_Select_Comm("SELECT Id FROM Studio_info WHERE Studio = '" + Studio_adv.Text + "'");
            if (DT.Rows.Count == 0)
            {
                DB.InsertComm("INSERT INTO Studio_info (Studio, Year, Country) VALUES ('" + Studio_adv.Text + "', " + Year_stud.Text + "', " + Country.Text + "');");
            }
            else DB.InsertComm("UPDATE Studio_info SET Year = '" + Year_stud.Text + "', Country = '" + Country.Text + "'  WHERE Studio = '" + Studio_adv.Text + "';");
        
        }

        private void SearchB_Click(object sender, RoutedEventArgs e)
        {
                DT = DB.Ex_Select_Comm("SELECT Name, Sequel FROM Main WHERE Name = '" + Search_n.Text + "' AND Sequel = '" + Search_s.Text + "'");
                if (!(DT.Rows.Count == 0))
                {
                    Message.Content = "Success!";
                    Price.IsEnabled = true;
                    Studio.IsEnabled = true;
                    Gathered.IsEnabled = true;
                    Year.IsEnabled = true;
                    Description.IsEnabled = true;
                    Image.IsEnabled = true;
                    Genre.IsEnabled = true;
                    AD_ex.IsEnabled = true;
                    UpGrid.DataContext = DB.Ex_Select_Comm("Select m.Price, s.Studio, m.Gathered, m.Year, m.Description, m.Image, g.Genre FROM Main AS m JOIN Genre AS g ON m.Genre_id = g.Id JOIN Studio_info AS s ON m.Studio_id = s.Id");
                }
                else Message.Content = "Not found :(";
         }

        private void UpdateB_Click(object sender, RoutedEventArgs e)
        {
            DB.InsertComm("UPDATE Main SET Price = " + Price.Text + ", Gathered = " + Gathered.Text + ", Year = '" + Year.Text + "', Description = '" + Description.Text + "', Image = '" + Image.Text + "' WHERE Name = '" + Search_n.Text + "';");
            DT = DB.Ex_Select_Comm("SELECT Id FROM Studio_info WHERE Studio = '" + Studio.Text + "'");
            if (!(DT.Rows.Count == 0))
            {
                DB.InsertComm("UPDATE Main SET Studio_id = " + DT.Rows[0][0].ToString() + " WHERE Name = '" + Search_n.Text + "'");
            }
            else
            {
                DB.InsertComm("INSERT INTO Studio_info (Studio) VALUES ('" + Studio.Text + "')");
                DT = DB.Ex_Select_Comm("SELECT Id FROM Studio_info WHERE Studio = '" + Studio.Text + "'");
                DB.InsertComm("UPDATE Main SET Studio_id = " + DT.Rows[0][0].ToString() + " WHERE Name = '" + Search_n.Text + "'");
            }
            DT = DB.Ex_Select_Comm("SELECT Id FROM Genre WHERE Genre = '" + Genre.Text + "'");
            if (!(DT.Rows.Count == 0))
            {
                DB.InsertComm("UPDATE Main SET Genre_id = " + DT.Rows[0][0].ToString() + " WHERE Name = '" + Search_n.Text + "'");
            }
            else
            {
                DB.InsertComm("INSERT INTO Genre (Genre) VALUES ('" + Genre.Text + "')");
                DT = DB.Ex_Select_Comm("SELECT Id FROM Genre WHERE Genre = '" + Genre.Text + "'");
                DB.InsertComm("UPDATE Main SET Genre_id = " + DT.Rows[0][0].ToString() + " WHERE Name = '" + Search_n.Text + "'");
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DG.ItemsSource = DB.Ex_Select_Comm("Select Studio, Country, Year From Studio_info").DefaultView;
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)DG.SelectedItems[0];
            DB.InsertComm("Delete From Studio_info WHERE Studio = '" + row[0].ToString() + "';");
            DG.ItemsSource = DB.Ex_Select_Comm("Select Studio, Country, Year From Studio_info").DefaultView;
        }
    }
}
