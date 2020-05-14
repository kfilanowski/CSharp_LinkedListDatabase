using System;
namespace C_SharpLinkedListDatabase
{
    /// <summary>
    /// Interface class defines attribute related operations.
    /// </summary>
    /// 
    /// Author: Kevin Filanowski
    /// Version: 05/13/2020
    public interface IAttribute
    {
        /// <summary>
        /// Check to see if a record has an attribute containing a specific value.
        /// </summary>
        /// <param name="attribute">The attribute to check that value for.</param>
        /// <param name="value">The value to check with the attribute.</param>
        /// <returns>True if there exists an attribute with the value specified,
        /// false otherwise.</returns>
        public bool Check(string attribute, string value);

        /// <summary>
        /// Change the value of a specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute to change.</param>
        /// <param name="value">The new value to assign to the specified
        /// attribute.</param>
        /// <returns>True if a change was made, false otherwise.</returns>
        public bool Change(string attribute, string value);

        /// <summary>
        /// Makes a deep copy of the employee object without using clone() or
        /// a copy constructor.
        /// </summary>
        /// <returns>A deep copy of the current employee object.</returns>
        public IAttribute MakeCopy();
    }
}
