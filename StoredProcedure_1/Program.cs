using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
class StoredProcedure_1
{
    const string connectionString = "server=localhost;user=root;password=Cadhla24!;database=employeedb;";

    static void Main()
    {
        Console.WriteLine("What would you like to do? (CREATE, READ, UPDATE OR DELETE)");
        var userInput = Console.ReadLine();
        DecideAction(userInput);
    }

    static void DecideAction(string? userInput)
    {
        switch (userInput)
        {
            case "CREATE":
                CreateEmployee();
                break;
            case "READ":
                ReadEmployee();
                break;
            case "UPDATE":
                UpdateEmployee();
                break;
            case "DELETE":
                DeleteEmployee();
                break;
            default:
                Console.WriteLine("Invalid action. Please try again.");
                break;
        }
    }

    static void CreateEmployee()
    {

        string procedureCode =
        @$"INSERT INTO Employees (FirstName, LastName, Age, Gender, Department, Position, Salary) 
                    VALUES (@firstName, @lastName, @age, @gender, @department, @position, @salary);";

        Console.WriteLine("Enter your first name: ");
        var firstName = Console.ReadLine();
        Console.WriteLine("Enter your last name: ");
        var lastName = Console.ReadLine();
        Console.WriteLine("Enter your age: ");
        var age = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter your Gender: ");
        var gender = Console.ReadLine();
        Console.WriteLine("Enter your department");
        var department = Console.ReadLine();
        Console.WriteLine("Enter your position: ");
        var position = Console.ReadLine();
        Console.WriteLine("Enter your salary: ");
        var salary = Convert.ToDecimal(Console.ReadLine());


        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            try
            {
                MySqlCommand cmd = new MySqlCommand(procedureCode, connection);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Parameters.AddWithValue("@salary", salary);

                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine($"Received the following error when attempting to insert data into Employee table: {e}");
            }


        }
    }

    static void ReadEmployee()
    {
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();


                string getText = "SELECT * FROM Employees;";
                MySqlCommand cmd = new MySqlCommand(getText, connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string getFirstName = reader.GetString("firstName");
                        string getLastName = reader.GetString("lastName");
                        int getAge = reader.GetInt32("age");
                        string getGender = reader.GetString("gender");
                        string getDepartment = reader.GetString("department");
                        string getPosition = reader.GetString("position");
                        decimal getSalary = reader.GetDecimal("salary");

                        Console.WriteLine($"{getFirstName}, {getLastName}, {getAge}, {getGender}, {getDepartment}, {getPosition}, {getSalary}");
                    }
                }
            }
        }
    }
    static void UpdateEmployee()
    {
        string updateKey =
        @$"UPDATE Employees SET FirstName = @firstName, LastName = @lastName WHERE EmployeeID = @employeeId;";

        Console.WriteLine("Which ID would you like to update: ");
        var employeeId = Console.ReadLine();
        Console.WriteLine("Enter the new first name: ");
        var firstName = Console.ReadLine();
        Console.WriteLine("Enter the new last name: ");
        var lastName = Console.ReadLine();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            MySqlCommand cmd = new MySqlCommand(updateKey, connection);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@employeeId", employeeId);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Update complete.");
        }

    }

    static void DeleteEmployee()
    {
        string deleteKey =
        @$"DELETE FROM Employees WHERE EmployeeID = @employeeId";

        Console.WriteLine("Which employee would you like to delete: ");
        var employeeId = Console.ReadLine();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(deleteKey, connection);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                var rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Received the following error when attempting to delete data from Employee table: {e}");
            }
        }
    }
}
