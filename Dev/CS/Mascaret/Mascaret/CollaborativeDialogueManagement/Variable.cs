using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{


    class Variable
    {
        public string Name { get; set; }
        private object value;
        public object Value { get { return value; } set { this.value = value; } }

        Variable(string name)
        {
            this.Name = name;
        }
        bool equals(object obj)
        {
            try {
                Variable v = (Variable)obj;
                if (this.Name.CompareTo(v.Name) == 0)
                {
                    return true; 
                }

            } catch (InvalidCastException) { }

            return false;
        }

    }

}
