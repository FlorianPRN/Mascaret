using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mascaret
{
	public class ConversationalBehaviourExecution
    {

        private double interval;
        public double Interval
        {
            get { return interval; }
            set { interval = value; }
        }
		public ConversationalBehaviourExecution()
        {
        }

		public ConversationalBehaviourExecution(Behavior specif, InstanceSpecification host, Dictionary<String, ValueSpecification> p)
            
        {

        }

        public  double execute(double dt)
        {
            action();
            if (!done())
                return interval;
            else return 0;
        }

        public  void action()
        {
            throw new NotImplementedException();
        }

        public  bool done()
        {
            throw new NotImplementedException();
        }
    }
}
