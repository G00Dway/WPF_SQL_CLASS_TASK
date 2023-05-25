using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Forms;

namespace WPF_SQL_25._05._2023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //string connectionString = null;
        string sql = null;
        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Library;Integrated Security=True";
        //sql = "INSERT INTO Main ([First Name], [Last Name]) values(@first,@last)";
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sql = "INSERT INTO Authors ([First Name], [Last Name]) values(@first,@last)";
            #region
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        
                        cmd.Parameters.Add("@first", SqlDbType.NVarChar).Value = FirstName.Text;
                        cmd.Parameters.Add("@last", SqlDbType.NVarChar).Value = LastName.Text;

                        
                        int rowsAdded = cmd.ExecuteNonQuery();
                        if (rowsAdded > 0)
                            System.Windows.MessageBox.Show("Success");
                        else
                            System.Windows.MessageBox.Show("No Success");

                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("ERROR: " + ex.Message);
                }
            }
            string query = "SELECT FirstName, LastName FROM Authors"; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.GetString(0);
                        string lastName = reader.GetString(1);
                        string fullName = $"{firstName} {lastName}";

                        ListBoxPersons.Items.Add(fullName);
                    }
                }
            }
            #endregion
        }
    }
}
