using System;
using System.Collections.Generic;

namespace Mascaret
{
	public class PlanLibrary
	{
		public PlanLibrary ()
		{
			plans	= new List<Plan> ();
		}

		public List<Plan> plans { get; set; }

		public void AddPlan( Plan p){
			plans.Add (p);
		}
		Plan FindPlanByGoal(string goal){
			Plan result = null;
			foreach (Plan p in plans) {
				if(p.Goal == goal)
					return result= p;
			}
			return result;
		}

	}
}

