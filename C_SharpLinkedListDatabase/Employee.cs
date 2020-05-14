using System;
using System.Collections.Generic;

namespace C_SharpLinkedListDatabase
{
    /// <summary>
    /// An Employee represents a row in a table.
    /// This will contain certain attributes pertaining to employees.
    /// </summary>
    /// 
    /// Author: Kevin Filanowski
    /// Version: 05/13/2020
    public class Employee : IAttribute
    {
        /// <summary>
        /// Identification Number.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Phone Number.
        /// </summary>
        string Phone { get; set; }

        /// <summary>
        /// Division within the institution.
        /// </summary>
        string Division { get; set; }

        /// <summary>
        /// Number of years employed.
        /// </summary>
        string Years { get; set; }

        /// <summary>
        /// Personal Information
        /// </summary>
        Person Person { get; set; }

        /// <summary>
        /// Current department or classification 
        /// </summary>
        string Department { get; set; }

        /// <summary>
        /// Constructor initializing the fields of the employee.
        /// </summary>
        /// <param name="id">The identification number of the employee.</param>
        /// <param name="phone">The phone number of the employee.</param>
        /// <param name="division">The division number of the employee.</param>
        /// <param name="years">How many years the employee has worked at the company.</param>
        /// <param name="person">The personal information of the employee,
        /// this includes their first and last name, and their marital status.</param>
        /// <param name="department">The department that the employee works at.</param>
        public Employee(string id, string phone, string division, string years, Person person, string department)
        {
            Id = id;
            Phone = phone;
            Division = division;
            Years = years;
            Person = person;
            Department = department;
        }

        /// <summary>
        /// Check to see if a record has an attribute containing a specific value.
        /// </summary>
        /// <param name="attribute">The attribute to check that value for.</param>
        /// <param name="value">The value to check with the attribute.</param>
        /// <returns>True if there exists an attribute with the value specified,
        /// false otherwise. </returns>
        public bool Check(string attribute, string value)
        {
            switch (attribute)
            {
                case "ID":
                case "id":
                case "Id": return Id.Equals(value);
                case "phone": return Phone.Equals(value);
                case "division": return Division.Equals(value);
                case "years": return Years.Equals(value);
                case "department": return Department.Equals(value, StringComparison.OrdinalIgnoreCase);
                case "first": return Person.First.Equals(value, StringComparison.OrdinalIgnoreCase);
                case "last": return Person.Last.Equals(value, StringComparison.OrdinalIgnoreCase);
                case "status": return Person.Status.ToString().Equals(value, StringComparison.OrdinalIgnoreCase);
                default: return false;
            }
        }

        /// <summary>
        /// Change the value of a specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute to change.</param>
        /// <param name="value">The new value to assign to the specified attribute.</param>
        /// <returns>True if a change was made, false otherwise.</returns>
        public bool Change(string attribute, string value)
        {
            switch (attribute)
            {
                case "ID":
                case "id":
                case "Id": Id = value; return true;
                case "phone": Phone = value; return true;
                case "division": Division = value; return true;
                case "years": Years = value; return true;
                case "department": Department = value; return true;
                case "first": Person.First = value; return true;
                case "last": Person.Last = value; return true;
                case "status": Person.Status = new Status(value); return true;
                default: return false;
            }
        }

        /// <summary>
        /// Equals method compares this employee object with another to determine
        /// if they are equal.They are considered equal if all of the fields are
        /// equivalent, except the department field.This is because the same
        /// person can be in multiple departments at one time.
        /// </summary>
        /// <param name="obj">The object to compare this employee with.</param>
        /// <returns>True if the two objects contain the same fields.</returns>
        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   Id == employee.Id &&
                   Phone == employee.Phone &&
                   Division == employee.Division &&
                   Years == employee.Years &&
                   EqualityComparer<Person>.Default.Equals(Person, employee.Person);
        }

        /// <summary>
        /// Makes a deep copy of the employee object without using clone()
        /// or a copy constructor.
        /// </summary>
        /// <returns>A deep copy of the current employee object.</returns>
        public IAttribute MakeCopy()
        {
            IAttribute copy = new Employee(Id, Phone, Division, Years, new Person(Person.First, Person.Last, Person.Status.ToString()), Department);
            return copy;
        }

        /// <summary>
        /// Returns all of the employees information in a formatted string.
        /// </summary>
        /// <returns>A string containing all of the employee's information.</returns>
        public override string ToString()
        {
            return "Employee(" + Id + "): " + Person + "\n\tRecord: "
                + Years + " years in division " + "[" + Division
                + "] -- Dept: " + Department;
        }
    }
}
