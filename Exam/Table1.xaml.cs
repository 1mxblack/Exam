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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.IO;

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для Table1.xaml
    /// </summary>
    public partial class Table1 : Page
    {
        public Table1()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            if (Number_floor.Text == "" || Number_pavilion.Text == "" || Square.Text == "" || Status.Text == "" || Ratio.Text == "" || Price.Text == "")
            {
                
            }
            else
            {
                try
                {
                    Manager.connection.Open();
                    string registration = "insert into Pavilions VALUES(@Number_SC_value, @Number_pavilion_value, @Number_floor_value, @Status_value, @Square_value, @Price_value, @Ratio_value)";
                    SqlCommand cmd = new SqlCommand(registration, Manager.connection);
                    SqlParameter Number_SC_param = new SqlParameter("@Number_SC_value", Number_SC.Text);
                    cmd.Parameters.Add(Number_SC_param);
                    SqlParameter Number_pavilion_param = new SqlParameter("@Number_pavilion_value", Number_pavilion.Text);
                    cmd.Parameters.Add(Number_pavilion_param);
                    SqlParameter Number_floor_param = new SqlParameter("@Number_floor_value", Number_floor.Text);
                    cmd.Parameters.Add(Number_floor_param);
                    SqlParameter Status_param = new SqlParameter("@Status_value", Status.Text);
                    cmd.Parameters.Add(Status_param);
                    SqlParameter Square_param = new SqlParameter("@Square_value", Square.Text);
                    cmd.Parameters.Add(Square_param);
                    SqlParameter Price_param = new SqlParameter("@Price_value", Price.Text);
                    cmd.Parameters.Add(Price_param);
                    SqlParameter Ratio_param = new SqlParameter("@Ratio_value", Ratio.Text);
                    cmd.Parameters.Add(Ratio_param);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.Message);
                }
                Manager.connection.Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (Number_floor.Text == "" || Number_pavilion.Text == "" || Square.Text == "" || Status.Text == "" || Ratio.Text == "" || Price.Text == "")
                {
                    Notify.Content = "Пожалуйста, заполните все поля!!!";
                }
                else
                {
                    try
                    {
                        Manager.connection.Open();
                        string registration = "UPDATE Pavilions SET id_shop_center = @Number_SC_value, id_pavilion = @Number_pavilion_value, " +
                            "floor = @Number_floor_value, status = @Status_value, area = @Square_value, price_metr = @Price_value, " +
                            "var_coefficient = @Ratio_value)";
                        SqlCommand cmd = new SqlCommand(registration, Manager.connection);
                        SqlParameter Number_SC_param = new SqlParameter("@Number_SC_value", Number_SC.Text);
                        cmd.Parameters.Add(Number_SC_param);
                        SqlParameter Number_pavilion_param = new SqlParameter("@Number_pavilion_value", Number_pavilion.Text);
                        cmd.Parameters.Add(Number_pavilion_param);
                        SqlParameter Number_floor_param = new SqlParameter("@Number_floor_value", Number_floor.Text);
                        cmd.Parameters.Add(Number_floor_param);
                        SqlParameter Status_param = new SqlParameter("@Status_value", Status.Text);
                        cmd.Parameters.Add(Status_param);
                        SqlParameter Square_param = new SqlParameter("@Square_value", Square.Text);
                        cmd.Parameters.Add(Square_param);
                        SqlParameter Price_param = new SqlParameter("@Price_value", Price.Text);
                        cmd.Parameters.Add(Price_param);
                        SqlParameter Ratio_param = new SqlParameter("@Ratio_value", Ratio.Text);
                        cmd.Parameters.Add(Ratio_param);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException err)
                    {
                        MessageBox.Show(err.Message);
                    }
                    Manager.connection.Close();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Remove_Pavilion_Text.Text == "")
            {
                Notify.Content = "Пожалуйста, заполните поле для удаления!!!";
            }
            else
            {
                try
                {
                    Manager.connection.Open();
                    string Delete = "DELETE FROM Pavilions WHERE id_pavilion = (@id_pavilion)";
                    SqlCommand cmd = new SqlCommand(Delete, Manager.connection);
                    SqlParameter Delete_param = new SqlParameter("@id_pavilion", Remove_Pavilion_Text.Text);
                    cmd.Parameters.Add(Delete_param);
                    cmd.ExecuteNonQuery();
                    Notify.Content = "Павильон удален!!!";
                }
                catch
                {

                    Notify.Content = "Невозможно удалить павильон";
                }
                Manager.connection.Close();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Status_Search.Text != "" && MinPrice.Text != "" && MaxPrice.Text != "" && Floor.Text != "")
                {
                    Manager.connection.Open();
                    string Search = "SELECT floor, status, area, price_metr, var_coefficient " +
                    "FROM Pavilions WHERE status = @status_value AND price_metr BETWEEN @MinPrice_value AND @MaxPrice_value AND floor = @floor_value";
                    SqlCommand cmd = new SqlCommand(Search, Manager.connection);
                    SqlParameter Search_param = new SqlParameter("@status_value", Status_Search.Text);
                    cmd.Parameters.Add(Search_param);
                    SqlParameter MinPrice_param = new SqlParameter("@MinPrice_value", MinPrice.Text);
                    cmd.Parameters.Add(MinPrice_param);
                    SqlParameter MaxPrice_param = new SqlParameter("@MaxPrice_value", MaxPrice.Text);
                    SqlParameter Floor_param = new SqlParameter("@floor_value", Floor.Text);
                    cmd.Parameters.Add(Floor_param);
                    cmd.Parameters.Add(MaxPrice_param);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter dataAdp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Pavilions"); // В скобках указываем название таблицы
                    dataAdp.Fill(dt);
                    DataGridView.ItemsSource = dt.DefaultView; // Сам вывод 
                }
            }
            catch (SqlException er)
            {

                MessageBox.Show(er.Number + " " + er.Message);
            }
            Manager.connection.Close();
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Table1());
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Table3());
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.connection.Open();
            string cmd = "SELECT dbo.Shoping_Center.shop_center_name AS [Название торгового центра], dbo.Shoping_Center.status AS [Статус торгового центра], dbo.Pavilions.floor AS Этаж, " +
                "dbo.Pavilions.area AS [Площадь павильона], dbo.Pavilions.status AS [Статус павильона], dbo.Pavilions.var_coefficient AS [кдс], " +
                "dbo.Pavilions.price_metr AS [Цена за метр] FROM Pavilions INNER JOIN " +
                "dbo.Shoping_Center ON id_shop_center = Shoping_Center.shop_center_id"; // Из какой таблицы нужен вывод 
            SqlCommand createCommand = new SqlCommand(cmd, Manager.connection);
            createCommand.ExecuteNonQuery();

            SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
            DataTable dt = new DataTable("Pavilions"); // В скобках указываем название таблицы
            dataAdp.Fill(dt);
            DataGridView.ItemsSource = dt.DefaultView; // Сам вывод 
            Manager.connection.Close();
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = System.IO.Path.Combine(Environment.CurrentDirectory, "rent.txt");
            DirectoryInfo dirInfo = new DirectoryInfo(fileName);
            using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.Default))
            {
                sw.WriteLineAsync("Номер клиента");
                sw.WriteLine(Client_id_TextBox.Text.ToString());
                sw.WriteLineAsync("Номер автомобиля");
                sw.WriteLine(Car_id_TextBox.Text.ToString());
                sw.WriteLineAsync("Дата начала проката");
                sw.WriteLine(Date_start.SelectedDate.ToString());
                sw.WriteLineAsync("Дата окончания проката");
                sw.WriteLine(Date_end.SelectedDate.ToString());
            }
        }
    }
}
