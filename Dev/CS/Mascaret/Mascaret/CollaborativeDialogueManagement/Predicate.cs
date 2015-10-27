using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
   public class Predicate
    {
        private string functor = null;
        public string Functor
        {
            get { return functor; }
            set{ functor = value; }
        }
        public List<object> Arguments
        {
            get { return arguments; }
            set { arguments = value; }
        }

        private List<object> arguments = new List<object>();
        public Predicate()
        {
            
        }
        public  Predicate(string name)
        {
            functor = name;
        }
        public  Predicate(string name, List<object> args)
        {
            functor =  name;
	        arguments =  args;
        }
        public Predicate(Predicate p)
        {
            this.functor = p.Functor;
            this.arguments = p.Arguments;
        }
        public  Predicate (string name, string args)
        {
            functor = name;
            string[] stringSeparators = new string[] { "[ ]" };
            // converting string into lowercase
            string[]  listString = args.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string arg in listString)
            {
                arguments.Add(arg);
            }
        }

        int Arity()
        {
            return arguments.Count;
        }

    }
}

