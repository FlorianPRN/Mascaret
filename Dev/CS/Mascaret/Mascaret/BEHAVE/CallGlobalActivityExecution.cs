
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Mascaret
{
    public class CallGlobalActivityExecution : BehaviorExecution
    {
        public CallGlobalActivityAction action;
		private BehaviorExecution geb;
        private CallGlobalActivityAction callGlobalActivityAction;
        private Dictionary<string, ValueSpecification> p;
		GlobalActivity scenarioActivity;


        //public BehaviorExecution behaviorExecution;

        
        public CallGlobalActivityExecution(CallGlobalActivityAction paction, InstanceSpecification host, Dictionary<string, ValueSpecification> p)
            : base(paction, host, p, false)
        {
            this.action = paction;
            System.Console.WriteLine("CallProcedureAction : " + action.Procedure);
            //behaviorExecution=action.Operation.Method.createBehaviorExecution(this.Host, p,false);




        }

      

        public override void stop()
        {
            base.stop();
            //behaviorExecution.stop();
        }

        public override void restart()
        {
            base.restart();
            //behaviorExecution.restart();
        }

        public override void pause()
        {
            base.pause();
            //behaviorExecution.pause();
        }

        public override double execute(double dt)
        {
			MascaretApplication appli = MascaretApplication.Instance;
			
			//bool found = false;
			OrganisationalEntity askedOrg = null;
			Procedure askedProc = null;
			Role askedRole = null;
			
			List<OrganisationalEntity> orgs = appli.AgentPlateform.Organisations;
			
			appli.VRComponentFactory.Log("CallGlobalActivity"); 
			
			for (int iOrg = 0; iOrg < orgs.Count; iOrg++)
			{
				appli.VRComponentFactory.Log(" Org " + orgs[iOrg].name + " ?");
				if (orgs[iOrg].name == action.OrganisationalEntity)
				{
					appli.VRComponentFactory.Log("Org : " + orgs[iOrg].name + " found");
					
					OrganisationalStructure os = orgs[iOrg].Structure;
					List<Procedure> procs = os.Procedures;
					askedOrg = orgs[iOrg];
					
					for (int iP = 0; iP < procs.Count; iP++)
					{
						if (procs[iP].name == action.Procedure)
						{
							appli.VRComponentFactory.Log("Procedure " + procs[iP].name + " found");
							askedProc = procs[iP];
							//  appli.AgentPlateform.Agents
							//  Agent agt = appli.AgentPlateform.Agents[assigns[iAss].Agent.toString()];
							foreach (KeyValuePair<string, Agent> nameAgentDict in appli.AgentPlateform.Agents)
							{
								
								Agent agt = nameAgentDict.Value;
								VirtualHuman vh = null;
								string type = agt.GetType().ToString();
								appli.VRComponentFactory.Log(agt.name + " ---Type :  "+ type);
								if(type.Contains("VirtualHuman"))
								{
									appli.VRComponentFactory.Log(agt.name + " ---------CallGlobalBehaviourExecution");
									
									AgentBehaviorExecution pbehavior = agt.getBehaviorExecutingByName("ProceduralBehavior");
									
									if (pbehavior != null)
									{
										appli.VRComponentFactory.Log("Procedure launched for " + agt.name);
										scenarioActivity = new GlobalActivity(askedProc.Activity.name);
										scenarioActivity.globalActivity = askedProc.Activity;

										
										Dictionary<string, ValueSpecification> param = new Dictionary<string, ValueSpecification>();

										LiteralString orgEntity = new LiteralString ((action.OrganisationalEntity));
										param.Add("orgEntity", (ValueSpecification)(orgEntity));
										LiteralString orgStructure = new LiteralString (os.name);
										param.Add("orgStructure", (ValueSpecification)(orgStructure));
										
										//geb = scenarioActivity.createBehaviorExecution(agt,param,false);
										appli.VRComponentFactory.Log("===================================================Behaviour scedular launched for " + agt.name);
										BehaviorScheduler.Instance.executeBehavior(scenarioActivity, agt, param, false);

										
									}
								}
								
								
								
							}
						}
					}
				}
			}
            return 0;
        }
    }
}
