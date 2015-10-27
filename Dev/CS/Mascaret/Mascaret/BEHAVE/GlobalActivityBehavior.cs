using System;
using System.Collections.Generic;
using System.IO;

namespace Mascaret
{
	public class GlobalActivityBehavior : CyclicBehaviorExecution
	{ 
		
		public bool ispause = false;
		public List<ProcedureExecution> runningProcedures = new List<ProcedureExecution>();
		public List<ActionNode> actionsToDo = new List<ActionNode>();
		public Dictionary<BehaviorExecution, ActionNode> behaviorToNode = new Dictionary<BehaviorExecution,ActionNode>();
		
		Agent agt;		
		
		
		private List<ActivityNode> actions = new List<ActivityNode>();
		public List<ActivityNode> Actions
		{
			get { return actions; }
			set { actions = value; }
		}
		
		
		private List<ActionNode> execs = new List<ActionNode>();
		public List<ActionNode> Execs
		{
			get { return execs; }
			set { execs = value; }
		}
		
		
		private Dictionary<string, InstanceSpecification> affectations;
		public Dictionary<string, InstanceSpecification> Affectations
		{
			get { return affectations; }
			set { affectations = value; }
		}
		
		
		private int stateActivity;
		public int StateActivity
		{
			get { return stateActivity; }
			set { stateActivity = value; }
		}
		
		
		private Activity activity;
		public Activity Activity
		{
			get { return activity; }
			set { activity = value; }
		}
		
	
		List<ActionNode> _scenarioActionsRunning = new List<ActionNode> ();
		List<ActionNode> _scenarioActionsDone = new List<ActionNode>();
		
		
		//default parameters sync=false
		public GlobalActivityBehavior(GlobalActivity gActivity, InstanceSpecification host, Dictionary<string, ValueSpecification> p) :
			base(gActivity.globalActivity, host, p)
		{
			stateActivity = 0;
			this.activity = gActivity.globalActivity;
			affectations = new Dictionary<string, InstanceSpecification>();
			
			
			/* Affectation des parametres */
			MascaretApplication.Instance.VRComponentFactory.Log("BUILD AFFECTATIONS ........ "+ activity.name + " : " + activity.Partitions.Count);
			
			foreach (ActivityPartition currentPartition in activity.Partitions)
			{
				if (currentPartition.name == "this")
				{
					affectations.Add("this", host);
					// MascaretApplication.Instance.VRComponentFactory.Log("Affectation de this a : " + host.name);
				}
				else
				{
					if (p.ContainsKey(currentPartition.name))
					{
						InstanceValue val = (InstanceValue)p[currentPartition.name];
						affectations.Add(currentPartition.name, val.SpecValue);
					}
					else
					{
						if (host.Slots.ContainsKey(currentPartition.name))
							affectations.Add(currentPartition.name, host.getProperty(currentPartition.name).getValue().valueSpecificationToInstanceSpecification());
						else
							MascaretApplication.Instance.VRComponentFactory.Log("[ActivityBehaviorExecution.cpp] Affectation Partition de " + currentPartition.name + " impossible ...");
					}
				}
				
			}

		}
		
		public Activity getActivity()
		{
			return (Activity)this.Specification;
		}
		
		public void fire(ActivityNode activityNode)
		{
			//nothing to do
		}
		
		public override void stop()
		{
			base.stop();
			foreach (ActionNode currentNode in execs)
			{
				currentNode.stop();
			}
		}    
		

		
		public override double execute(double dt)
		{
			int i = 0;

			//	MascaretApplication.Instance.VRComponentFactory.Log (" ####### Running GlobalActivityBehavior " + stateActivity);
				
				if (stateActivity == 0) {
					MascaretApplication.Instance.VRComponentFactory.Log (Host.name +" ####### Running GlobalActivityBehavior " + Activity.name);
					if (Activity.Initial != null) {
						//					Debug.Log(Activity.Initial.getOutgoingActionNode().Count);
						
						foreach (ActionNode currentNode in Activity.Initial.getOutgoingActionNode()) {
							if (currentNode.Kind == "action") {
								MascaretApplication.Instance.VRComponentFactory.Log (" Starting Init Action : " + currentNode.getFullName ());
								
								// currentNode.start(affectations[currentNode.Partitions[0].name], affectations, false);
								
								_scenarioActionsRunning.Add (currentNode);

								CallProceduralBehaviourExecution (currentNode);
								
								
							}
							execs.Add (currentNode);
							actions.Add (currentNode);

						}
						stateActivity++;
					}
				}
				if (stateActivity == 1) {
 
			 //else
				{
					if (_scenarioActionsDone.Count > 0) {

						List<ActionNode> next = new List<ActionNode> ();
						foreach (ActionNode currentNode in _scenarioActionsDone) {
							MascaretApplication.Instance.VRComponentFactory.Log ("$$$$$$$$$$$$$$$$$$$$_scenarioActionsDone------------------------"+ currentNode.name);
							foreach (ActionNode currentChildNode in currentNode.getOutgoingActionNode()) {
								next.Add (currentChildNode);
							}
							if(_scenarioActionsRunning.Contains(currentNode))
								_scenarioActionsRunning.Remove(currentNode);
						}
						_scenarioActionsDone.Clear ();
						if (next.Count > 0) {
							foreach (ActionNode currentNode in next) {
								MascaretApplication.Instance.VRComponentFactory.Log (".......NEXT NODE........." + currentNode.getFullName ());
								if (currentNode.Kind == "action") {
									
									_scenarioActionsRunning.Add (currentNode);	
									CallProceduralBehaviourExecution (currentNode);
									
									//    currentNode.start(affectations[currentNode.Partitions[0].name], affectations, false);
								}
							}
						}
					}

					
					if (_scenarioActionsRunning.Count > 0) {
						
						foreach (ActionNode node in _scenarioActionsRunning) {
							if (node.Stereotype =="GlobalActivity")
							{  List<BehaviorExecution> doneBehave = new List<BehaviorExecution>();
								foreach(KeyValuePair<BehaviorExecution,ActionNode> nodeBehave in behaviorToNode){
									if(nodeBehave.Value == node){
										BehaviorExecution be = nodeBehave.Key;
										if(be.IsFinished){
											_scenarioActionsDone.Add (node);
											doneBehave.Add(nodeBehave.Key);
										}
									}
								}
								foreach(BehaviorExecution be in doneBehave)
								{								
									behaviorToNode.Remove(be);
								}
							}

							else{
								bool flag =  IsBehaviourFinished(node) ;
								if(flag)
								{
									_scenarioActionsDone.Add (node);
								}
							}
						}
						
					}
					else {
						MascaretApplication.Instance.VRComponentFactory.Log ("$$$$$$$$$$$$$$$$$$$$$$$$$$$YUPPY no more global actions to do");
						return 0.0f;
					}
				}
			}
			return 0.1f;
		}
		bool IsBehaviourFinished(ActionNode node){
			bool result = true;
			AgentBehaviorExecution pbehavior = agt.getBehaviorExecutingByName ("ProceduralBehavior");
		//	MascaretApplication.Instance.VRComponentFactory.Log ("++++++++++++++++++++++++checking ++++++++++" +node.name);
			if (pbehavior != null) {
				ProceduralBehavior procBehave = (ProceduralBehavior)(pbehavior);
				foreach(ProcedureExecution pe in  procBehave.runningProcedures)
				{
					//MascaretApplication.Instance.VRComponentFactory.Log ("+++++++++++++++++++++++++Running++++++++++" +pe.procedure.name + "  : " +  pe.isFinished());
					if(pe.procedure.name == node.Action.name)
						result =pe.isFinished();
				}
			}
						
			return result;
			
	
		}
		void CallGlobalActivityBehaviourExecution(ActionNode action)
			{
			MascaretApplication.Instance.VRComponentFactory.Log (Host.name+"+++++++++++++++++++++++++++########################################### CallGlobalActivityBehaviourExecution  " + action.name);
			
			string orgEntity = null;
			
			
			List<OrganisationalStructure> structs = VRApplication.Instance.AgentPlateform.Structures;
			foreach (OrganisationalStructure s in structs)
			{
				List<Procedure> procs = s.Procedures;
				foreach (Procedure p in procs)
				{
					if (p.name == action.Action.name)
					{
						orgEntity = s.Entities[0].name;
					}
				}
			}

			List<Entity> entities = MascaretApplication.Instance.getEnvironment().getEntities();
			Entity entity = entities[0];
			Action action2 = null;
			/*     action2 = new CallProcedureAction();
        ((CallProcedureAction)(action2)).Procedure = procedure;
        ((CallProcedureAction)(action2)).OrganisationalEntity = orgEntity;
        BehaviorScheduler.Instance.executeBehavior(action2, entity, new Dictionary<string, ValueSpecification>(), false);

*/
			action2 = new CallGlobalActivityAction();
			((CallGlobalActivityAction)(action2)).Procedure = action.Action.name;
			((CallGlobalActivityAction)(action2)).OrganisationalEntity = orgEntity;
			BehaviorExecution be =  BehaviorScheduler.Instance.executeBehavior(action2, entity, new Dictionary<string, ValueSpecification>(), false);
			behaviorToNode.Add (be, action);

		}


		void CallProceduralBehaviourExecution(ActionNode action){
			MascaretApplication.Instance.VRComponentFactory.Log (Host.name+"+++++++++++++++++++++++++++########################################### CallProceduralBehaviourExecution  " + action.name);

			MascaretApplication appli = MascaretApplication.Instance;
			//bool found = false;
			OrganisationalEntity askedOrg = null;
			Procedure askedProc = null;
			Role askedRole = null;
			
			List<OrganisationalEntity> orgs = appli.AgentPlateform.Organisations;
			
			appli.VRComponentFactory.Log ("ProceduralBehaviourExecution"); 
			
			for (int iOrg = 0; iOrg < orgs.Count; iOrg++) {
				appli.VRComponentFactory.Log ("------ Org " + orgs [iOrg].name + " ?");
				if (orgs [iOrg].name == parameters ["orgEntity"].getStringFromValue()) {
					appli.VRComponentFactory.Log ("----------Org : " + orgs [iOrg].name + " found");
					OrganisationalStructure os = orgs [iOrg].Structure;
					List<Procedure> procs = os.Procedures;
					askedOrg = orgs [iOrg];
					
					for (int iP = 0; iP < procs.Count; iP++) {
						if (procs [iP].name == action.Action.name) {
							appli.VRComponentFactory.Log ("Procedure " + procs [iP].name + " found");
							askedProc = procs [iP];
							List<RoleAssignement> assigns = orgs [iOrg].RoleAssignement;
							
							appli.VRComponentFactory.Log ("Assigns : " + assigns.Count);
							for (int iAss = 0; iAss < assigns.Count; iAss++) {
								agt = appli.AgentPlateform.Agents [assigns [iAss].Agent.toString ()];
								askedRole = assigns [iAss].Role;
								if(agt.name == Host.name)
								{
									appli.VRComponentFactory.Log ("Role : " + assigns [iAss].Role.name + " == " + agt.name);
								
									AgentBehaviorExecution pbehavior = agt.getBehaviorExecutingByName ("ProceduralBehavior");
								
									if (pbehavior != null) {
										appli.VRComponentFactory.Log ("Procedure "+ askedProc.name + " launched for " + agt.name);
										ProceduralBehavior procBehave = (ProceduralBehavior)(pbehavior);
									
										Dictionary<string, ValueSpecification> procParams = new Dictionary<string, ValueSpecification> ();
									
										procBehave.pushProcedureToDo (askedProc, askedOrg, askedRole, procParams);

									}
								}
							}
						}
						
					}
				}
			}
		}
		
		
		public void sendActionRealisationMessage(ActionNode action, ProcedureExecution procInfo)
		{
			Agent agt = (Agent)(this.Host);
			
			ACLMessage procMsg = new ACLMessage(ACLPerformative.INFORM);
			
			//we inform at wich time the action start
			TimeExpression timestamp = action.CurrentExecution.Start;
			
			procMsg.Timestamp = timestamp;
			
			//set ACLMessage content
			string content = "((action ";
			content += agt.name;
			content += " ";
			content += "(" + clean(action.name) + ")";
			content += "))";
			procMsg.Content = content;
			
			//send message to other agents
			List<AID> agents = procInfo.getOtherAgents();
			
			for (int iA = 0; iA < agents.Count; iA++)
			{
				procMsg.Receivers.Add(agents[iA]);
			}
			agt.send(procMsg);
		}
		
		public void sendActionDoneMessage(ActionNode action, ProcedureExecution procInfo)
		{
			Agent agt = (Agent)(this.Host);
			
			ACLMessage procMsg = new ACLMessage(ACLPerformative.INFORM);
			
			//we inform at wich time the action finished
			TimeExpression timestamp = action.CurrentExecution.Finish;
			
			procMsg.Timestamp = timestamp;
			
			//set ACLMessage content
			string content = "((done (action ";
			content += agt.name;
			content += " ";
			content += "(" + clean(action.name) + ")";
			content += ")))";
			procMsg.Content = content;
			//MascaretApplication.Instance.VRComponentFactory.Log(content);
			
			//send message to other agents
			List<AID> agents = procInfo.getOtherAgents();
			
			for (int iA = 0; iA < agents.Count; iA++)
			{
				procMsg.Receivers.Add(agents[iA]);
			}
			agt.send(procMsg);
		}
		
		
		public void sendProcedureDoneMessage(ProcedureExecution procInfo)
		{
			Agent agt = (Agent)(this.Host);
			
			ACLMessage procMsg = new ACLMessage(ACLPerformative.INFORM);
			
			//we inform at wich time the procedure finish
			TimeExpression timestamp = BehaviorScheduler.Instance.getCurrentVirtualTime();
			procMsg.Timestamp = timestamp;
			
			string content = "((done (action ";
			content += agt.name;
			content += " ";
			content += "(" + clean(procInfo.procedure.name) + ")";
			content += ")))";
			procMsg.Content = content;
			
			procMsg.Receivers.Add(MascaretApplication.Instance.Agent.Aid);
			
			agt.send(procMsg);
		}
		
		
		
		
		string clean(string toClean)
		{
			string returned = toClean;
			returned.Replace("/", "");
			returned.Replace("(", "");
			returned.Replace(")", "");
			returned.Replace("^", "");
			
			return returned;
		}
		
		
		
		public void onActionDone(AID agent, ActionNode action)
		{
			for (int iP = 0; iP < runningProcedures.Count;iP++ )
			{
				ProcedureExecution procInfo = runningProcedures[iP];
				
				procInfo.informActionDone(agent,action);
			}
			ispause = false;
		}
		
		public bool IsFinished(){
			if ( _scenarioActionsDone .Count==0 && _scenarioActionsRunning.Count== 0) {
				return true;
			}
			return false;
		}
	}
}

