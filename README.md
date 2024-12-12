# Desciption
The following program contains the code for creating a Stored Procedure called `SetupTable`. `SetupTable`, when executed, creates a new table called `Employees` and populates it with 5 rows of data. In the end, it also returns the `FirstName`, `LastName`, `Position` and `Salary` of the top three employees based on the `Salary` column.

This program already creates a stored procedure when it starts. Your task is to write the code for executing the `SetupTable` and displaying the three rows returned after the execution. Following should be the output of your program:
```
Name, Position, Salary
Bob Jones, Marketing Manager, 75000
Jane Smith, HR Manager, 70000
John Doe, Software Engineer, 60000
```

### Hints
> **Hint:** Since this procedure returns data, you need to use `ExecuteReader` for reading the data.
