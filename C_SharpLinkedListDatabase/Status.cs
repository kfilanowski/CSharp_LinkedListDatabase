using System;
namespace C_SharpLinkedListDatabase
{
    /// <summary>
    /// This class models the marital status of a person. Marital statuses
    /// include: Married, Widowed, Divorced, and Single
    /// </summary>
    /// 
    /// Author: Kevin Filanowski
    /// Version: 05/13/2020
    public class Status
    {
        /// <summary>
        /// Martial Status of a person.
        /// </summary>
        private string _maritalStatus;

        /// <summary>
        /// Property defining set and getting methods for the martial status.
        /// </summary>
        public string MaritalStatus
        {
            get => _maritalStatus;
            set
            {
                // Assign the full marital status word.
                switch (value)
                {
                    case "Married":
                    case "married":
                    case "m":
                    case "M": _maritalStatus = "Married"; break;
                    case "widowed":
                    case "Widowed":
                    case "w":
                    case "W": _maritalStatus = "Widowed"; break;
                    case "divorced":
                    case "Divorced":
                    case "d":
                    case "D": _maritalStatus = "Divorced"; break;
                    case "Single":
                    case "single":
                    case "s":
                    case "S": _maritalStatus = "Single"; break;
                    default:
                        {
                            throw new ArgumentException("Invalid Marital Status, "
                                    + "Must be Married, Widowed, Divorced, or Single.");
                        }
                }
            }
        }

        /// <summary>
        /// Constructor to set the marital status of the person.
        /// </summary>
        /// <param name="maritalStatus"></param>
        public Status(string maritalStatus) => _maritalStatus = maritalStatus;

        /// <summary>
        /// Compares two Status objects by their marital status. If the marital
        /// status is the same, then they are considered equal.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the two objects are equal, false if they are not.</returns>
        public override bool Equals(object obj)
        {
            return obj is Status status &&
                string.Equals(MaritalStatus, status.MaritalStatus, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns the marital status of the associated Status object.
        /// </summary>
        /// <returns>The marital status of the person.</returns>
        public override string ToString() => MaritalStatus;
    }
}
