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
    /// Логика взаимодействия для Insert_Wind.xaml
    /// </summary>
    public partial class Insert_Wind : MetroWindow
    {
        InsertComm com;
        DataBase DB;
        public Insert_Wind(DataBase Db)
        {
            DB = Db;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DG.ItemsSource = DB.Ex_Select_Comm("Select Studio, Country, Year From Studio_info").DefaultView;
        }
        private void Add_Record()
        {
            DataTable DT = new DataTable();
            DT = DB.Ex_Select_Comm("SELECT Id FROM Studio_info WHERE Studio = '" + Studio.Text + "';");
            if (DT.Rows.Count == 0)
            {
                DB.InsertComm("INSERT INTO Studio_info (Studio) VALUES ('" + Studio.Text + "');");
                com.Studio_id = DB.Ex_Select_Comm("SELECT Id FROM Studio_info WHERE Studio = '" + Studio.Text + "';").Rows[0][0].ToString();
            }
            else  com.Studio_id = DT.Rows[0][0].ToString();

            DT = DB.Ex_Select_Comm("SELECT Id FROM Genre WHERE Genre = '" + Genre.Text + "' ;");
            if (DT.Rows.Count == 0)
            {
                DB.InsertComm(@"Insert into Genre (Genre) 
                                values ('" + Genre.Text + "' );");
                com.Genre_id = DB.Ex_Select_Comm("SELECT Id FROM Genre WHERE Genre = '" + Genre.Text + "';").Rows[0][0].ToString();
            }
            else
                com.Genre_id = DT.Rows[0][0].ToString();

            DB.InsertComm(@"INSERT INTO Main (Year, Name, Studio_id, Genre_id, Description, Sequel, Image, Gathered, Price) 
                                   VALUES ('" + com.Year + "', '" + com.Title + "', " + com.Studio_id +", " + com.Genre_id + ", '" + com.Description + "', "
                                             + com.Sequel + ", '" + com.Image + "', " +
                                             com.Gathered + ", " + com.Price + ") ;");
        }
        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            com = new InsertComm()
            {
                Sequel = Sequel.Text,
                Title = Title.Text,
                Description = Description.Text,
                Price = Price.Text,
                Image = Image.Text,
                Gathered = Gathered.Text,
                Year = Year.Text
            };
            Add_Record();
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

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)DG.SelectedItems[0];
            DB.InsertComm("Delete From Studio_info WHERE Studio = '" + row[0].ToString() + "';");
            DG.ItemsSource = DB.Ex_Select_Comm("Select Studio, Country, Year From Studio_info").DefaultView;
        }

    }

}