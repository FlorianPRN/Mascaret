
using System;
using System.Collections.Generic;
using DM;
namespace Mascaret
{
	public class ConversationPlanExecution: CyclicBehaviorExecution
	{
		public bool isFinished { get; set;}
		public bool isPlanStarted { get; set; }
	    private	List<ConversationOperation> operations;
		public List<ConversationOperation> Operations{
			get {return operations;}
			set{ operations = value; }
		}
		int counter { get; set;}
		ProcedureExecution procInfo = null;
		private ConversationPlan plan = null;
		int x ;
		public ConversationPlan runningPlan {
			get{return plan;} 
			set{plan = value;}
		}
		public ConversationPlanExecution(ConversationPlan planToExe, InstanceSpecification host, Dictionary<String, ValueSpecification> p)
			: base(planToExe, host, p)
		{
			plan = planToExe;
			isFinished = false;
			isPlanStarted = false;
			x = 1;
		}
		public override double execute(double dt)
		{
		//	MascaretApplication.Instance.VRComponentFactory.Log (Host.name + ":.............................Executing Conversational Behavior");
			double res = Process ();
			if (res == 0)
				return 0;

		/*	isFinished = true;
			int y=  2;

			x = x + y;
			int result = test (x);
			while(x<40){
				x= x+ result;
				
				
				MascaretApplication.Instance.VRComponentFactory.Log (Host.name + ":.............................Executing Conversational Behavior");
				result = test(x);
				return 0.5f;
			}
    */
			
			return 3f;
		}
		
		int  test(int x){
			int t = 2; 
			int result;
			result = t * 2 - t / 2 + 2;
			return result;
		}
	
		public double Process(){
			/*
			 * If Agent has an expectation of information it must wait for the expectation to be satisfied.
			 * Once the expectation is satisfied, agent can process the next action or can initiate new conversation plan
			 * 
			 * */
			if(!isPlanStarted){
				operations = plan.Operations;
				counter = 0;
				isPlanStarted = true;
			
			} else {
				if(counter < Operations.Count){
					Operations[counter].execute();


					counter++;
				}
				if(counter== Operations.Count)
				{ 
					isFinished = true;
					return 0;
				}
			}
			return 0.5;
		}
	}
}

