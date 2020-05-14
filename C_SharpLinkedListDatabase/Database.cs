using System;
using System.IO;
using System.Text.RegularExpressions;

namespace C_SharpLinkedListDatabase
{
    /// <summary>
    /// The driver of the program, the controller of the linked list database.
    /// This is where tables are initialized, operated on, and printed.
    /// </summary>
    /// 
    /// Author: Kevin Filanowski
    /// Version: 05/13/2020
    public class Database
    {
        /// <summary>
        /// The number of fields that are needed in one table record.
        /// </summary>
        private const int FieldNumber = 7;

        /// <summary>
        /// Checks for empty record fields. The method will throw an exception
        /// if there is not enough information to fill a record in the table.
        /// It does this by counting every element in the file, and then knowing
        /// that there should be 'FIELD_NUMBER' elements per record in a table,
        /// performs a modulus operator to determine if there exists at least
        /// one element that is missing.
        /// </summary>
        /// <param name="filename">The file's name which will be scanned.</param>
        private static void CheckForMissingElements(string filename)
        {
            // Read the lines in the file.
            string[] lines = File.ReadAllLines(filename);

            // Formatted line.
            String fLine;

            // Keep count of the line number we're scanning.
            int counter = 0;

            // Count all of the fields in each line. If there are not enough of
            // those fields, then we throw an exception.
            foreach (string line in lines)
            {
                counter++;

                // Format the line by removing excess characters.
                fLine = Regex.Replace(line, "([\t]+|[ ]+)", " ");

                // Check length requirement.
                if (fLine.Split(' ').Length - FieldNumber < 0)
                {
                    throw new DataMisalignedException("There are missing fields" +
                        " in the database file: '" + filename + "' on line number: " +
                        counter);
                }
            }
        }

        /// <summary>
        /// This method is used to check if an element is an integer. If the
        /// element is in fact an integer, it will return the element.If it
        /// is not an integer, it will throw an exception.
        /// </summary>
        /// <param name="element">The string element to check.</param>
        /// <returns>The same element passed in if the element is a number,
        /// throws an exception otherwise.</returns>
        private static string CheckInteger(string element)
        {
            // Check to see if we can convert the element to an integer,
            // throw an exception if we cant.
            long.Parse(element);

            // Exception test passed, we continue.
            return element;
        }

        /// <summary>
        /// Similar to checkInteger, this method will check an element to ensure
        /// that it contains only letters, and no numerical digits.
        /// This method will throw an exception if it finds any numerical digits
        /// in the String 'element', and return the element back if it does not.
        /// </summary>
        /// <param name="element">The string element to check for digits.</param>
        /// <returns>The same element passed in if the element does not contain any
        /// digits, throws an exception otherwise.</returns>
        private static string CheckString(string element)
        {

            Regex rx = new Regex(".*[\\d+|\\W].*");
            if (rx.IsMatch(element))
            {
                throw new ArgumentException("Field argument: '" + element +
                    "' should not contain numerical digits or non-letter characters.");
            }
            return element;
        }

        /// <summary>
        /// The visual and logic part of the menu. The menu has several options
        /// that the user can choose from:
        /// 0) Quit       		- Quits the program
        /// 1) Intersect  		- Finds the intersection between two tables
        /// 2) Difference 		- Finds the difference between two tables
        /// 3) Union		 		- Finds the union between two tables
        /// 4) Select     		- Select a specified attribute and value in a table
        /// 5) Remove     		- Removes a row from a table
        /// 6) Print both tables - Prints both tables
        /// </summary>
        /// <param name="faculty">The first table, which consists of faculty members.</param>
        /// <param name="admin">The second table, which consists of administrative members.</param>
        private static void Menu(Table<Employee> faculty, Table<Employee> admin)
        {
            // Option denotes a menu option, this variable saves user selection.
            String option = "";

            Console.WriteLine("Welcome to Database Deluxe 5000\n");

            // Loop the menu until the user wants to exit.
            while (!option.Equals("0"))
            {
                Console.Write("\nPlease make choice:" +
                        "\n\t0) Quit" +
                        "\n\t1) Intersect" +
                        "\n\t2) Difference" +
                        "\n\t3) Union" +
                        "\n\t4) Select" +
                        "\n\t5) Remove" +
                        "\n\t6) Print both tables" +
                        "\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ");
                option = Console.ReadLine();

                // Handle the menu option chosen by the user.
                switch (option)
                {
                    case "0":
                        {
                            Console.WriteLine("\nGoodbye!");
                            Environment.Exit(0);
                        }
                        break;
                    case "1":
                        { // 1) Intersect
                          // Variables declared to save user responses.
                            string attribute, value;
                            PrintAttributes();
                            Console.Write("Enter attribute >");
                            attribute = Console.ReadLine();
                            Console.Write("Enter value >");
                            value = Console.ReadLine();

                            PrintTable(faculty.Intersect(attribute, value, admin));
                        }
                        break;
                    case "2":
                        { // 2) Difference
                          // Variable declared to save user response.
                            string table;
                            Console.Write("Enter table (F/A) >");
                            table = Console.ReadLine();
                            switch (table)
                            {
                                case "F":
                                case "f":
                                    PrintTable(faculty.Difference(admin));
                                    break;
                                case "A":
                                case "a":
                                    PrintTable(admin.Difference(faculty));
                                    break;
                                default:
                                    Console.WriteLine("Invalid input. "
                                   + "Must choose table 'F' or 'A'.");
                                    break;
                            }
                        }
                        break;
                    case "3":
                        { // 3) Union
                            PrintTable(faculty.Union(admin));
                        }
                        break;
                    case "4":
                        { // 4) Select
                          // Variables declared to save user responses.
                            string table, attribute, value;
                            Console.Write("Enter table (F/A) >");
                            table = Console.ReadLine();
                            PrintAttributes();
                            Console.Write("Enter attribute >");
                            attribute = Console.ReadLine();
                            Console.Write("Enter value >");
                            value = Console.ReadLine();

                            switch (table)
                            {
                                case "F":
                                case "f":
                                    PrintTable(faculty.Select(attribute, value));
                                    break;
                                case "A":
                                case "a":
                                    PrintTable(admin.Select(attribute, value));
                                    break;
                                default:
                                    Console.WriteLine("Invalid input. "
                                   + "Must choose table 'F' or 'A'.");
                                    break;
                            }
                        }
                        break;
                    case "5":
                        { // 5) Remove
                          // Variables declared to save user responses.
                            string table, value;
                            Console.Write("Enter table (F/A) >");
                            table = Console.ReadLine();
                            Console.Write("Enter ID of employee to remove >");
                            value = Console.ReadLine();

                            switch (table)
                            {
                                case "F":
                                case "f":
                                    faculty.Remove(value);
                                    break;
                                case "A":
                                case "a":
                                    admin.Remove(value);
                                    break;
                                default:
                                    Console.WriteLine("Invalid input. "
                                   + "Must choose table 'F' or 'A'.");
                                    break;
                            }
                        }
                        break;
                    case "6":
                        { // 6) Print Both Tables
                            PrintTable(faculty);
                            PrintTable(admin);
                        }
                        break;
                    default:
                        { // Anything other than 0-6
                          // Default is reached if the user entered an option outside
                          // the range of choices.
                            Console.WriteLine("Option " + option + " does not exist.");
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Prints the available attributes we can interact with.
        /// </summary>
        private static void PrintAttributes()
        {
            Console.WriteLine("Available attributes: id, division, years, first, last, status.");
        }

        /// <summary>
        /// This method populates a table with information from a text file.
        /// </summary>
        /// <param name="table">table The table to fill in with records.</param>
        /// <param name="filename">The name of the file to read information from.</param>
        private static void PopulateTable(Table<Employee> table, string filename)
        {
            // Read all of the lines in the file.
            string[] lines = File.ReadAllLines(filename);

            // Counter to progress through each element.
            int index = 0;

            // Each invidividual element.
            String[] e;

            // Keep grabbing values as long as they exist.
            foreach (string line in lines)
            {
                // Split each element.
                e = Regex.Replace(line, "([\t]+|[ ]+)", " ").Split(' ');

                // Create a new person object.
                Person person = new Person(CheckString(e[index++]),
                        CheckString(e[index++]), new Status(e[index++]));

                // Create a new employee object.
                Employee employee = new Employee(CheckInteger(e[index++]),
                        CheckInteger(e[index++]), CheckInteger(e[index++]),
                        CheckInteger(e[index++]), person, table.Title);

                // Add the employee to the table.
                table.Insert(employee);

                // Reset the index for the next person.
                index = 0;
            }
        }

        /// <summary>
        /// Simple function that calls the toString method of the table and prints
        /// that out.
        /// </summary>
        /// <param name="table">table The table to be printed.</param>
        private static void PrintTable(Table<Employee> table)
        {
            Console.WriteLine(table);
        }

        /// <summary>
        /// The main driver. Declares and initializes two tables, and then 
        /// populates them with information from two separate text files. Calls
        /// upon the menu for input commands.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {

            // Declaring and Initializing two tables, Faculty and Admin.
            Table<Employee> faculty = new Table<Employee>("Faculty");
            Table<Employee> admin = new Table<Employee>("Admin");

            // Check to ensure that the database has no missing elements.
            // Populate the tables with information inside the file.
            // Try-Catch handles the exception if the file does not exist.
            try
            {
                CheckForMissingElements("faculty.txt");
                CheckForMissingElements("admin.txt");
                PopulateTable(faculty, "faculty.txt");
                PopulateTable(admin, "admin.txt");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Cannot read file " + ex.Message
                + ".\nProgram exiting.");
                Environment.Exit(1);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid field, expected an integer,"
                                    + "\nor the integer is too long.\n");
                Console.WriteLine(ex.Message + "\nProgram Exiting.");
                Environment.Exit(1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message + "\nProgram Exiting.");
                Environment.Exit(1);
            }
            catch (DataMisalignedException ex)
            {
                Console.WriteLine(ex.Message + "\nProgram Exiting.");
                Environment.Exit(1);
            }

            Menu(faculty, admin);
        }
    }
}
