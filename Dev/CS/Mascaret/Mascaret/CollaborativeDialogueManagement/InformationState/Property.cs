using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
 
    public class Property
    {
        private string attName = null;
        public string Name { get { return attName; } set { attName = value; } }
        private string attType = null;
        public string AttTyep { get { return attType; } set { attType = value; } }
        private string attValue = null;
        public string AttValue { get { return attValue; } set { attValue = value; } }
        private object dataValue = null;
        public object DataValue { get { return dataValue; } set { dataValue = value; } }



        private List<object> dataVector = new List<object>();
        public  List<object> DataVector { get { return dataVector; } set { dataVector = value; } } 

        private List<object> dataList = new List<object>();
        public List<object> DataList { get { return dataList; } set { dataList = value; } }

        private Queue<object> dataQueue = new Queue<object>();
        public Queue<object> DataQueue { get { return dataQueue; } set { dataQueue = value; } }

        private Stack<object> dataStack = new Stack<object>();
        public Stack<object> DataStack { get { return dataStack; } set { dataStack = value; } }

        private List<object> vectorValues = new List<object>();
        public List<object> VectorValues { get { return vectorValues; } set { vectorValues = value; } }



        public Property()
        {
        }
        public Property(string name , string type)
        {
            attName = name;
            attType = type;
        }
        public Property(string name, string type, object value)
        {

            attName = name;
            attType = type;
            if (type == "int")
            {
                try {
                    dataValue = (int)(value);
                }catch (InvalidCastException) { }
            }
            if (type == "double")
            {
                try
                {
                    dataValue = (double)(value);
                }
                catch (InvalidCastException) { }
                
            }
            if (type == "string")
            {
                try
                {
                    dataValue = (string)(value);
                }
                catch (InvalidCastException) { }
            }
        }
        public void setStringTypeValue(string value)
        {
            if (attType == "int")
            {
                try
                {
                    dataValue = (Convert.ToInt32(value));
                }
                catch (InvalidCastException) { }
                
            }
            if (attType == "double")
            {
                try
                {
                    dataValue = (Convert.ToDouble(value));
                }
                catch (InvalidCastException) { }
               
            }
            if (attType == "string")
            {
                 dataValue = value;
            }
        }


        public void setValue(object value)
        {
            if (attType == "int")
            {
                try
                {
                    dataValue = (int)(value);
                }
                catch (InvalidCastException) { }
            }
            if (attType == "double")
            {
                try
                {
                    dataValue = (double)(value);
                }
                catch (InvalidCastException) { }

            }
            if (attType == "string")
            {
                try
                {
                    dataValue = (string)(value);
                }
                catch (InvalidCastException) { }
            }
            else if (attType == "Property")
            {
                try
                {
                    dataValue = (Property)(value);
                }
                catch (InvalidCastException) { }
            }
            if (attType == "Component")
            {
                try
                {
                    dataValue = (Component)(value);
                }
                catch (InvalidCastException) { }
            }
            if (attType == "Cell")
            {
                try
                {
                     dataValue = (Cell)(value);
                }
                catch (InvalidCastException) { }
            }
            else
            {
                Console.Write("invialid type data ");
                Console.Write("\n");
            }
        }
        public void setValue(string path, object value)
        {
            //attValue = value;
            if (path == null)
            {
                if (attType == "int")
                {
                    try
                    {
                        dataValue = (int)(value);
                    }
                    catch (InvalidCastException) { }
                }
                if (attType == "double")
                {
                    try
                    {
                        dataValue = (double)(value);
                    }
                    catch (InvalidCastException) { }

                }
                if (attType == "string")
                {
                    try
                    {
                        dataValue = (string)(value);
                    }
                    catch (InvalidCastException) { }
                }
            }
            else
            {
                if (attType == "Property")
                {
                    try
                    {
                        dataValue = (Property)(value);
                    }
                    catch (InvalidCastException) { }
                }
                if (attType == "Component")
                {
                    try
                    {
                          dataValue = (Component)(value);
                    }
                    catch (InvalidCastException) { }
                }
                if (attType == "Cell")
                {
                    try
                    {
                           dataValue = (Cell)(value);
                    }
                    catch (InvalidCastException) { }
                }
                else
                {
                    Console.Write("invialid type data ");
                    Console.Write("\n");
                }
            }
        }

        public void push(object value)
        {

            if (attType.CompareTo("vector") == 0 || attType.CompareTo("list") == 0)
            {
                object result = null;
                try
                { //if property vector already contains  value then do nothing
                    if (contains(value))
                    {
                        return;
                    }
                    //else
                    //      evaluate property and if it is not null then delete old entry
                    //      push new value
                    else
                    {
                        int i = 0;
                        foreach (object data in dataVector)
                        {
                            try
                            {
                                Predicate p = (Predicate)(data);
                                Predicate pValue = (Predicate)(value);
                                Predicate partialPrediate = new Predicate();

                                List<object> args = pValue.Arguments;
                                List<object> partialArgs = new List<object>();
                                foreach (object arg in args)
                                {
                                    partialArgs.Add(arg);
                                }
                                string var = "$";
                                partialArgs.Add(var);
                                partialPrediate.Functor = pValue.Functor;
                                partialPrediate.Arguments = partialArgs;

                                result = Unify.unify(partialPrediate, p);
                                if (result != null)
                                {
                                    break;
                                }
                                i++;
                            }
                            catch (InvalidCastException)
                            {
                            }
                        }
                        if (result != null)
                        {
                            dataVector.RemoveAt(i);
                           
                        }
                        dataVector.Add(value);
                    }
                }
                catch (Exception )
                {
                    Console.WriteLine( "property :cought an exception while push operation" );
                }
            }

            else if (attType.CompareTo("queue") == 0)
            {
                //TODO ::  handle push for other data types
                try
                {
                    dataQueue.Enqueue(value);
                }
                catch (Exception )
                {
                    Console.WriteLine("property :cought an exception while push operation queue");
                }
            }
         else if (attType.CompareTo("stack") == 0)
            {
                try
                {
                    dataStack.Push(value);
                }
                catch (Exception )
                {
                    Console.WriteLine("property :cought an exception while push operation queue");
                }
            }

        }

        public object front()
        {
            object result = null;
            if (attType.CompareTo("vector") == 0 || attType.CompareTo("list") == 0)
            {
                if (dataVector.Count == 0)
                {
                    return null;
                }
                try
                {
                    result = dataVector[0];
                }
                catch (Exception )
                {
                    Console.WriteLine(" data vector is empty ");
                   
                }
            }
            if (attType.CompareTo("queue") == 0)
            {
                if (dataQueue.Count == 0)
                {
                    return null;
                }
                try
                {
                    result = dataQueue.Peek();
                }
                catch (Exception )
                {
                    Console.WriteLine(" data queue is empty ");
                    
                }
            }
            if (attType.CompareTo("stack") == 0)
            {
                if (dataStack.Count == 0)
                {
                    return null;
                }
                try
                {
                    result = dataStack.Peek();
                }
                catch (Exception )
                {
                    Console.WriteLine(" data stack is empty ");
                }
            }
            return result;
        }

        public bool contains(object predicate)
        {
            if (attType == "vector" || attType == "list")
            {
               foreach (object data in  dataVector)
                {
                    try
                    {
                        Predicate p = (Predicate)(data);
                        if (Unify.matchTerms(p, predicate))
                        {
                            return true;
                        }
                    }
                    catch (InvalidCastException)
                    {
                    }
                    try
                    {
                        //TODO : handle for Semantic predicata
                    /*   SemanticPredicate p = (SemanticPredicate)(data);
                        if (Unify.matchTerms(p, predicate))
                        {
                            return true;
                        }
                        */
                    }
                    catch (Exception )
                    {
                    }
                }

            }
            else if (attType == "stack")
            {
                try
                {
                    Predicate p = new Predicate();
                    if (dataStack.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        p = (Predicate)(dataStack.Peek());
                        if (Unify.matchTerms(p, predicate))
                        {
                            return true;
                        }
                    }
                }
                catch (InvalidCastException)
                {
                }
            }
            return false;
        }
        public void clear()
        {
            attValue = null;
            //	dataValue.;
            dataVector.Clear();
            dataList.Clear();
            dataQueue.Clear();
            dataStack.Clear();
            vectorValues.Clear();
        }

        public object evaluate(object predicate)
        {
            object result = null;
            if (attType == "vector" || attType == "list")
            {
               foreach (object data in  dataVector)
                {
                    try
                    {
                        Predicate p = (Predicate)(data);
                        result = Unify.unify(predicate, p);
                        if (result != null)
                        {
                            return result;
                        }

                    }
                    catch (InvalidCastException)
                    {
                    }
                }

            }
            return result;
        }
        public void erase(object predicate)
        {
            object result = null;
            if (attType == "vector")
            {
                int i = 0;
                foreach (object data in dataVector)
                {
                    try
                    {
                        //C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
                        Predicate p = (Predicate)(data);
                        result = Unify.unify(predicate, p);
                        if (result != null)
                        {
                            break;
                        }
                    }
                    catch (InvalidCastException)
                    {
                    }
                    i++;
                }
                if(result !=null )
                    dataVector.RemoveAt(i);
            }
        }

        public void pop()
        {
            if (attType.CompareTo("queue") == 0)
            {
                try
                {
                    dataQueue.Dequeue();
                }
                catch (Exception)
                {
                }
            }
            if (attType.CompareTo("stack") == 0)
            {
                try
                {
                    dataStack.Pop();
                }
                catch (Exception)
                {
                }
            }

        }
        public bool hasCompatibleType(string type)
        {
            if (attType == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object data()
        {
            return dataValue;
        }

        public string toString()
        {
            string result = attName;
            return result;
        }


    }






}

