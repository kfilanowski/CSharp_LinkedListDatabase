using System;
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
            string[] lines = System.IO.File.ReadAllLines(filename);

            // Keep count of the line number we're scanning.
            int counter = 0;

            // Count all of the fields in each line. If there are not enough of
            // those fields, then we throw an exception.
            foreach (string line in lines)
            {
                counter++;
                if (line.Split(' ').Length - FieldNumber < 0)
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
            // TODO: using namespaces broken on my laptop for some reason.
            return element;
        }


        //private static void menu(Table<Employee> faculty, Table<Employee> admin)
        //{

        //}

    }
}
