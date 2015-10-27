using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{

    public class Equals : Condition
    {
        private object value1 = null;
        private object value2 = null;
        

        public Equals()
        {
        }
        public Equals(object value1, object value2)
        {
           // Condition(value1, value2, Comparator.equals);
            this.value1 = value1;
            this.value2 = value2;


        }
  /*      public Equals(Value value1, Value value2)
        {
        }
    */    public new void Dispose()
        {
            base.Dispose();
        }
        public override bool isValid(InformationState IS)
        {
            try
            {

                if (value1.GetType() == TypeCode.Boolean.GetType() && value2.GetType() == TypeCode.Boolean.GetType())
                {
                    return ((bool)(value1) == (bool)(value2));
                }
                //C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
                if (value1.GetType() == TypeCode.Int32.GetType() && value2.GetType() == TypeCode.Int32.GetType())
                {
                    return ((int)(value1) == (int)(value2));
                }
                if (value1.GetType() == TypeCode.Int32.GetType() && value2.GetType() == TypeCode.Double.GetType())
                {
                    return ((int)(value1) == (double)(value2));
                }
                if (value1.GetType() == TypeCode.Double.GetType() && value2.GetType() == TypeCode.Int32.GetType())
                {
                    return ((double)(value1) == (int)(value2));
                }
                if (value1.GetType() == TypeCode.Double.GetType() && value2.GetType() == TypeCode.Double.GetType())
                {
                    return ((double)(value1) == (double)(value2));
                }
                //C++ TO C# CONVERTER TODO TASK: There is no C# equivalent to the classic C++ 'typeid' operator:
                if (value1.GetType() == TypeCode.String.GetType() && value2.GetType() == TypeCode.String.GetType())
                {
                    if (((string)(value1)).CompareTo((string)(value2)) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (InvalidCastException)
            {
            }


            return false;
        }


    }

}

