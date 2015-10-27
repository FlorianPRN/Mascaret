using System;
using System.Collections.Generic;

namespace Mascaret
{
	public class CallGlobalActivityAction : Action, IEquatable<CallGlobalActivityAction>
    {

        private String procedure;
        public String Procedure
        {
            get { return procedure; }
            set { procedure = value; }
        }


        private String organisationalEntity;
        public String OrganisationalEntity
        {
            get { return organisationalEntity; }
            set { organisationalEntity = value; }
        }


        private string stringArguments;
        public string StringArguments
        {
            get { return stringArguments; }
            set { stringArguments = value; }
        }


        private Dictionary<String, String> arguments;
        public Dictionary<String, String> Arguments
        {
            get { return arguments; }
            set { arguments = value; }
        }

        public CallGlobalActivityAction()
        {
            Kind = "CallProcedure";
        }

        public bool Equals(CallGlobalActivityAction other)
        {
            if (other == this)
                return true;
            return false;
        }
        public bool Equals(CallProcedureAction other)
        {
           
            return false;
        }
        public override BehaviorExecution createBehaviorExecution(InstanceSpecification host, Dictionary<String, ValueSpecification> p, bool sync)
        {
            CallGlobalActivityExecution behavior = new CallGlobalActivityExecution(this, host, p);
            return behavior;
        }

        public void addArgument(string name, string val)
        {
            if (!arguments.ContainsKey(name))
                arguments.Add(name, val);
        }

        public string getArgument(string name)
        {
            return arguments[name];
        }

        public Dictionary<String, ValueSpecification> getParameters()
        {
            //MapStringString parameters = (MapStringString) ScriptableObject.CreateInstance("MapStringString");
            return null;
        }

        public bool isDynamic()
        {
            if (stringArguments.Length > 0)
                return true;
            else return false;
        }
    }
}
