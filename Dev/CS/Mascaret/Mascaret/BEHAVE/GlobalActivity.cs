using System;
using System.Collections.Generic;

namespace Mascaret
{
   
     public class GlobalActivity : Activity
    {
	    private	Activity activity;
		public Activity globalActivity{
			get{return activity;}
			set{activity = value;}
		}

         public GlobalActivity(string name)
            : base(name)
        {
        }
		public GlobalActivity(Activity activity)
			: base(activity.name)
		{
			this.activity = activity;
		}
        //default parameter sync = false
        public override BehaviorExecution createBehaviorExecution(InstanceSpecification host, Dictionary<string, ValueSpecification> p, bool sync)
        {
            GlobalActivityBehavior be = new GlobalActivityBehavior(this, host, p);
			return be;
        }

    }
}
