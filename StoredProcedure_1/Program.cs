using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
class StoredProcedure_1
{
    const string connectionString = "server=localhost;user=root;password=Cadhla24!;database=employeedb;";
    static void Main()
    {
        DecideAction("CREATE", "READ", "UPDATE", "DELETE");
        
        static void DecideAction(string replyA, string replyB, string replyC, string replyD)
        {

            Console.WriteLine("What would you like to do? (CREATE, READ, UPDATE OR DELETE)");
            string userInput = Console.ReadLine();

            if (userInput == ("CREATE")) 
            {
                CreateEmployee("Jane", "Smith", 35, "Female", "HR", "HR Manager", (decimal)70000.00);

                static void CreateEmployee(string firstName, string lastName, int age, string gender, string department, string position, decimal salary)
                {
                    string procedureCode =
                    @$"INSERT INTO Employees (FirstName, LastName, Age, Gender, Department, Position, Salary) 
                    VALUES (@firstName, @lastName, @age, @gender, @department, @position, @salary);";

                    Console.WriteLine("Enter your first name: ");
                    firstName = Console.ReadLine();
                    Console.WriteLine("Enter your last name: ");
                    lastName = Console.ReadLine();
                    Console.WriteLine("Enter your age: ");
                    age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter your Gender: ");
                    gender = Console.ReadLine();
                    Console.WriteLine("Enter your department");
                    department = Console.ReadLine();
                    Console.WriteLine("Enter your position: ");
                    position = Console.ReadLine();
                    Console.WriteLine("Enter your salary: ");
                    salary = Convert.ToDecimal(Console.ReadLine());


                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        try
                        {
                            MySqlCommand cmd1 = new MySqlCommand(procedureCode, connection);
                            cmd1.Parameters.AddWithValue("@firstName", firstName);
                            cmd1.Parameters.AddWithValue("@lastName", lastName);
                            cmd1.Parameters.AddWithValue("@age", age);
                            cmd1.Parameters.AddWithValue("@gender", gender);
                            cmd1.Parameters.AddWithValue("@department", department);
                            cmd1.Parameters.AddWithValue("@position", position);
                            cmd1.Parameters.AddWithValue("@salary", salary);
                            cmd1.ExecuteNonQuery();
                
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Received the following error when attempting to insert data into Employee table: {e}");
                        }
                
                    }
                }
            }
            else if (userInput == "READ")
            {
                ReadEmployee("Jane", "Smith", 35, "Female", "HR", "HR Manager", (decimal)70000.00);

                static void ReadEmployee(string firstName, string lastName, int age, string gender, string department, string position, decimal salary)
                {       
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();


                        string getText = "SELECT * FROM Employees;";
                        MySqlCommand cmd2 = new MySqlCommand(getText, connection);

                        using (MySqlDataReader reader = cmd2.ExecuteReader())
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
            else if (userInput == "UPDATE") 
            {   
                UpdateEmployee("Jane", "Smith", 35, "Female", "HR", "HR Manager", (decimal)70000.00);

                static void UpdateEmployee(string firstName, string lastName, int age, string gender, string department, string position, decimal salary)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
            
                        string updateText = "UPDATE Employees SET FirstName = 'Alex' WHERE FirstName = 'Alice'";
                        MySqlCommand cmd3 = new MySqlCommand(updateText, connection);
                        cmd3.ExecuteNonQuery();

                        Console.WriteLine("Update complete.");
                    }
                }
            }
            else if (userInput == "DELETE")
            {        
                DeleteEmployee("Jane", "Smith", 35, "Female", "HR", "HR Manager", (decimal)70000.00);

                static void DeleteEmployee(string firstName, string lastName, int age, string gender, string department, string position, decimal salary)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string deleteText = "DELETE FROM Employees WHERE Salary > 70000";
                        MySqlCommand cmd4 = new MySqlCommand(deleteText, connection);
                        cmd4.ExecuteNonQuery();

                        Console.WriteLine("Deletion actioned.");
                    }
                } 
            }
            else 
            {
                Console.WriteLine("Invalid action. Please try again.");
            }
        }
    }
}