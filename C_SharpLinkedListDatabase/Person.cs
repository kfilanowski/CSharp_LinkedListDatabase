using System;
using System.Collections.Generic;

namespace C_SharpLinkedListDatabase
{
    /// <summary>
    /// The person class is used to hold personal information about someone.
    /// </summary>
    /// 
    /// Author: Kevin Filanowski
    /// Version: 05/13/2020
    public class Person
    {
        /// <summary>
        /// First name of a person.
        /// </summary>
        public string First { get; set; }

        /// <summary>
        /// Last name of a person.
        /// </summary>
        public string Last { get; set; }

        /// <summary>
        /// Martial Status of a person.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Constructor. Initializes a person object.
        /// </summary>
        /// <param name="first">The first name of the person.</param>
        /// <param name="last">The last name of the person.</param>
        /// <param name="status">The marital status of the person.</param>
        public Person(string first, string last, Status status)
        {
            First = first;
            Last = last;
            Status = status;
        }

        /// <summary>
        /// Constructor. Initializes a person object.
        /// </summary>
        /// <param name="first">The first name of the person.</param>
        /// <param name="last">The last name of the person.</param>
        /// <param name="status">The marital status of the person.</param>
        public Person(string first, string last, string status)
        {
            First = first;
            Last = last;
            Status = new Status(status);
        }

        /// <summary>
        /// Equals method compares two objects to check if they are the same.
        /// The two objects are considered equal if their fields contain
        /// the same values.
        /// </summary>
        /// <param name="obj">The object to compare the person with.</param>
        /// <returns>True if these two objects have equal fields.
        /// Otherwise, return false.</returns>
        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   First == person.First &&
                   Last == person.Last &&
                   EqualityComparer<Status>.Default.Equals(Status, person.Status);
        }
    }
}