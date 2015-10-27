using System;
using System.Collections.Generic;
namespace Mascaret
{
	public abstract class Plan : Behavior
	{
		private string goal;
		public string Goal{get{return goal;} set{goal = value;}}
	

		public Plan(string planGoal):base(planGoal)
		{
			goal = planGoal;
		}
	//	public abstract BehaviorExecution createBehaviorExecution(InstanceSpecification host, Dictionary<String, ValueSpecification> p, bool sync);

	}
}

