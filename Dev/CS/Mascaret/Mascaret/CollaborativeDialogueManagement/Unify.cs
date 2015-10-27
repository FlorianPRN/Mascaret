using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    class Unify
    {
      public static bool matchTerms(object term1, object term2)
        {
            if (term1 == null)
            {
                if (term2 == null)
                    return true;
                return false;
            }
            if (term2 == null)
            {
                if (term2 == null)
                    return true;

                else return false;
            }
            try
            {
                string s1 = (string)term1;
                if (s1.CompareTo("_") == 0)
                    return true;
            }
            catch (InvalidCastException)
            {
            }
            try
            {
                string s1 = (string) term1;
                string s2 = (string)term2;
                if (s1.CompareTo(s2) == 0)
                    return true;
                return false;
            }
            catch (InvalidCastException )
            {
            }
            try
            {
                bool b1 = (bool)term1;
                bool b2 = (bool)term2;
                if(b1 == b2)
                    return true;
                return false;
            }
            catch (InvalidCastException )
            {
            }
            try
            {
                int  i1 = (int)term1;
                int i2 = (int)term2;
                if (i1 == i2)
                    return true;
                return false;
            }
            catch (InvalidCastException )
            {
            }
            try
            {
                double d1 = (double)term1;
                double d2 = (double)term2;
                if (d1 == d2)
                    return true;
                return false;
            }
            catch (InvalidCastException )
            {
            }
            try
            {
                float d1 = (float)term1;
                float d2 = (float)term2;
                if (d1 == d2)
                    return true;
                return false;
            }
            catch (InvalidCastException )
            {
            }
            try
            {
                Variable v1 =(Variable)term1;
                Variable v2 = (Variable)term2;
               
                if (v1.Name.CompareTo(v2.Name) == 0)
                    return true;
                else return false;

            }
            catch (InvalidCastException)
            {
            }
            try
            {
                Predicate p1, p2;
                p1 = (Predicate)term1;
                p2 = (Predicate)term2;

                if (p1.Functor.CompareTo(p2.Functor) != 0)
                    return false;

                List<object> args1 = p1.Arguments;
                List<object> args2 = p2.Arguments;
                //		std::cout<< " " <<args1.size() << "   " << args2.size() << std::endl;		
                if (args1.Count != args2.Count)
                    return false;

                bool unified = true;
                int count = 0;
                while (count < args1.Count && unified)
                {
                    unified = unified && matchTerms(args1[count], args2[count]);
                    count++;
                }

                return unified;
            }
            catch (InvalidCastException)
            {
            }

            return false;
        }
            
        public static object unify(object term1, object term2)
        {
            object result = null;
            bool unified;
            try
            {
                Predicate p1 = (Predicate)term1;
                Predicate p2 = (Predicate)term2;
                if (p1.Functor.CompareTo(p2.Functor) != 0)
                    return result;

                List<object> args1 = p1.Arguments;
                List<object> args2 = p2.Arguments;
                	
                if (args1.Count != args2.Count)
                    return result;

                unified = true;
                int i = 0;
                while (i < args1.Count() && unified)
                {
                    string s1 = null;
                    try
                    {
                        s1  = (string) args1.ElementAt(i);
                    }
                    catch( InvalidCastException )
			        {
                    }
                    if(s1!= null)
                    {
                        if (s1.CompareTo("$") == 0)
                            result = args2.ElementAt(i);
                        else
                        {
                            unified = unified && matchTerms(args1.ElementAt(i), args1.ElementAt(i));
                        }
                    }
                    ++i;
                 }
                if (unified)
                {
                    return result;
                }
            }
            catch (Exception )
            {
            }
                return result;
        }



    }
}
