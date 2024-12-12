using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

class StoredProcedure_1
{

    static void Main(string[] args)
    {
        string connectionString = "server=localhost;user=root;password=Cadhla24!;database=employeedb;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            
            string queryTxt = "SELECT * FROM Employees;";
            MySqlDataAdapter adapter = new MySqlDataAdapter(queryTxt, connection);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);

            DataTable table = dataset.Tables[0];

            foreach(DataRow row in table.Rows)
            {
                int id = (int)row["EmployeeID"];
                string first_name = (string)row["FirstName"];
                string last_name = (string)row["LastName"];
                int age = (int)row["Age"];
                string gender = (string)row["Gender"];
                string department = (string)row["Department"];
                string position = (string)row["Position"];
                decimal salary = (decimal)row["Salary"];

                    Console.WriteLine($"{id}, {first_name}, {last_name}, {age}, {gender}, {department}, {position}, {salary}");
            }
        }
    }
    }