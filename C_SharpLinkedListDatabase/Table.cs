using System;
namespace C_SharpLinkedListDatabase
{
    /// <summary>
    /// Represents a single table in the database.
    /// </summary>
    /// <typeparam name="T">A generic piece of data for the table.</typeparam>
    /// 
    /// Author: Kevin Filanowski
    /// Version: 05/13/2020
    public class Table<T> where T : IAttribute
    {
        /// <summary>
        /// First record in the table.
        /// </summary>
        private Node Head { get; set; }

        /// <summary>
        /// Last record in the table.
        /// </summary>
        private Node Tail { get; set; }

        /// <summary>
        /// Label for the table.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The counter to keep the size of the table.
        /// </summary>
        internal int Size { get; set; }

        /// <summary>
        /// This temp node is only used to iterate through the table.
        /// It is declared here so that we don't constantly declare it
        /// throughout the code.
        /// </summary>
        private Node temp;

        /// <summary>
        /// Initializes a table with the first record and a label.
        /// </summary>
        /// <param name="head">The first record in the table.</param>
        /// <param name="title">The label for the table.</param>
        public Table(string title, Node head = null)
        {
            Title = title;
            Head = head;
            Tail = head;
            Size = 0;
        }

        /// <summary>
        /// Checks the table to see if a row with the data specified exists.
        /// NOTE: The data in the table should have a valid and appropriate
        /// equals method to compare data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean Contains(T data)
        {
            // Reset the temporary node back to the head to iterate the table.
            temp = Head;

            for (int i = 0; i < Size; i++)
            {
                // Compare the data
                if (temp.data.Equals(data))
                {
                    return true;
                }
                temp = temp.next;
            }
            return false;
        }

        /// <summary>
        /// Creates a new table comprised of nodes in this table, but not in the
        /// table being passed in.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Table<T> Difference(Table<T> table)
        {
            // The new table comprised of differences.
            Table<T> differenceTable = new Table<T>(Title + ", " + table.Title);

            // Reset the temporary node back to the head to iterate the table.
            temp = Head;

            // Iterate through the first table, only adding rows unique
            // to the first table.
            for (int i = 0; i < Size; i++)
            {
                if (!table.Contains(temp.data))
                {
                    differenceTable.Insert(temp.data);
                }
                temp = temp.next;
            }
            return differenceTable;
        }

        /// <summary>
        /// Adds a new record to the end of the table.
        /// </summary>
        /// <param name="data">The information that would be contained in a row of the table.</param>
        public void Insert(T data)
        {
            // If this is the first element, set it to head. Otherwise,
            // add it to the end of the tail.
            if (Head == null)
            {
                Head = new Node(data);
                Tail = Head;
            }
            else
            {
                Tail.next = new Node(data);
                Tail = Tail.next;
            }
            Size++;
        }

        /// <summary>
        /// Creates a new table comprised of nodes having a value for a specific
        /// attribute, created from two tables.
        /// </summary>
        /// <param name="attribute">The attribute to restrict to.</param>
        /// <param name="value">The value of that attribute to restrict to.</param>
        /// <param name="table">The other table to compare with.</param>
        /// <returns>A new table composed of the same specified attributes and values
        /// in both tables.</returns>
        public Table<T> Intersect(string attribute, string value, Table<T> table)
        {
            // The new table comprised of intersecting attributes and values.
            Table<T> intersectTable = new Table<T>(Title + ", " + table.Title);

            // First we select the attributes and values consisted in both tables
            Table<T> selectedOne = Select(attribute, value);
            Table<T> selectedTwo = table.Select(attribute, value);

            // Reset the temporary node back to the head to iterate the table.
            temp = selectedOne.Head;

            // Then we add the first table into intersectTable.
            for (int i = 0; i < selectedOne.Size; i++)
            {
                if (selectedTwo.Contains(temp.data))
                {
                    // Create a copy to modify department without affecting
                    // the original record.
                    // We know the copy is of type T since we copied it from type T.    
                    T copy = (T)temp.data.MakeCopy();
                    copy.Change("department", Title + ", " + table.Title);
                    intersectTable.Insert(copy);
                }
                else
                {
                    intersectTable.Insert(temp.data);
                }
                temp = temp.next;
            }

            // Then we ensure there are no duplicates by checking the second
            // table before we insert the row into intersectTable.
            temp = selectedTwo.Head;
            for (int i = 0; i < selectedTwo.Size; i++)
            {
                if (!intersectTable.Contains(temp.data))
                {
                    intersectTable.Insert(temp.data);
                }
                temp = temp.next;
            }
            return intersectTable;
        }

        /// <summary>
        /// Removes the node/row with a matching ID from the table.
        /// </summary>
        /// <param name="id">The id of the node/row.</param>
        public void Remove(string id)
        {
            // Checking the first record, then every other one afterwards.
            if (Head != null)
            {
                if (Head.data.Check("id", id))
                {
                    Head = Head.next;
                    Size--;
                }
                else
                {
                    Node prev = Head;
                    temp = Head.next;

                    // Walk the list and check for the record to remove.
                    while (!temp.data.Check("id", id) && temp.next != null)
                    {
                        temp = temp.next;
                        prev = prev.next;
                    }
                    // Move the node previous to temp past temp.
                    if (temp.data.Check("id", id))
                    {
                        prev.next = temp.next;
                        Size--;
                        // Boundary checking. Update tail if tail is removed.
                        if (prev.next == null)
                        {
                            Tail = prev;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new table comprised of nodes having a value for a
        /// specific attribute.
        /// </summary>
        /// <param name="attribute">The attribute to select and restrict to.</param>
        /// <param name="value">The value to select and restrict to.</param>
        /// <returns>A new table consisting of only the attributes and values selected.</returns>
        public Table<T> Select(String attribute, String value)
        {
            // Create the new table
            Table<T> selectTable = new Table<T>(Title);

            // Reset the temporary node back to the head to iterate the table.
            temp = Head;

            // Iterate through the table to select the attribute value.
            for (int i = 0; i < Size; i++)
            {
                if (temp.data.Check(attribute, value))
                {
                    selectTable.Insert(temp.data);
                }
                temp = temp.next;
            }
            return selectTable;
        }

        /// <summary>
        /// Creates a new table comprised of nodes that occur in both tables.
        /// </summary>
        /// <param name="table">The other table to compare with.</param>
        /// <returns>A new table containing nodes that are contained in both tables.</returns>
        Table<T> Union(Table<T> table)
        {
            // The new table consisting of elements of both tables.
            Table<T> unionTable = new Table<T>(Title + ", " + table.Title);

            // Reset the temporary node back to the head to iterate the table.
            temp = Head;

            // Copy the original table into the unionTable.
            for (int i = 0; i < Size; i++)
            {
                if (table.Contains(temp.data))
                {
                    // Create a copy to modify department without affecting
                    // the original record.
                    // We know the copy is of type T since we copied it from type T.    
                    T copy = (T)temp.data.MakeCopy();
                    copy.Change("department", Title + ", " + table.Title);
                    unionTable.Insert(copy);
                }
                else
                {
                    unionTable.Insert(temp.data);
                }
                temp = temp.next;
            }

            // Switch control to the table parameter.
            temp = table.Head;

            // Copy the parameter table into unionTable and check for duplicates.
            for (int i = 0; i < table.Size; i++)
            {
                if (!unionTable.Contains(temp.data))
                {
                    unionTable.Insert(temp.data);
                }
                temp = temp.next;
            }
            return unionTable;
        }

        /// <summary>
        /// Prints the data in this entire table.
        /// </summary>
        /// <returns>A string representation of the data in the table.</returns>
        public override string ToString()
        {
            // Creating a string buffer to concatenate strings to.
            StringBuffer text = new StringBuffer();

            // Reset the temporary node back to the head to iterate the table.
            temp = Head;

            text.Append("\n=========================" + Title
                    + "=========================\n");

            // Iterate through the entire table and append to the string.
            for (int i = 0; i < Size; i++)
            {
                text.Append(temp.ToString());
                text.Append("\n-----------------------------"
                        + "---------------------------------\n");
                temp = temp.next;
            }

            text.Append("=========================" + Title
                    + "=========================\n");
            return text.ToString();
        }

        /// <summary>
        /// Inner class node, each node is a row in the table. Each node also
        /// contains the data and a pointer to the next row in the table.
        /// </summary>
        ///
        public class Node
        {
            /// <summary>
            /// The data contained in this node. This contains the information
            /// of one row in the table.
            /// </summary>
            internal T data;

            /// <summary>
            /// The next row in the table.
            /// </summary>
            internal Node next;

            /// <summary>
            /// Constructor initializes a new row in a table;
            /// </summary>
            /// <param name="data">The data that will be contained in the node.</param>
            internal Node(T data)
            {
                this.data = data;
                next = null;
            }

            /// <summary>
            /// Prints the data in this node.
            /// </summary>
            /// <returns>A string representation of the data in a row of the table.</returns>
            public override string ToString()
            {
                return data.ToString();
            }
        }
    }
}
