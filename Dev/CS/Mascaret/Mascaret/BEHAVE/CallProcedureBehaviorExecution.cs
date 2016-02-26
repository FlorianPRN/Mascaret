using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mascaret
{
    public class CallProcedureBehaviorExecution : BehaviorExecution
    {
        public CallProcedureAction action;
        //public BehaviorExecution behaviorExecution;


        public CallProcedureBehaviorExecution(CallProcedureAction paction, InstanceSpecification host, Dictionary<string, ValueSpecification> p)
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
            List<OrganisationalEntity> orgs = appli.AgentPlateform.Organisations;
            appli.VRComponentFactory.Log("CallProcedure");
            
            foreach (OrganisationalEntity orgEntity in orgs)
            {
                appli.VRComponentFactory.Log(" Org " + orgEntity.name + " ?");
                OrganisationalStructure os = orgEntity.Structure;
                List<Procedure> procs = os.Procedures;

                Procedure p = procs.Where(proc => proc.name.Equals(action.Procedure)).First();
                if (p != null)
                {
                    appli.VRComponentFactory.Log("Procedure " + p.name + " found");
                    List<RoleAssignement> goodAssigns = orgEntity.RoleAssignement.Where(
                        rAssign => appli.AgentPlateform.Agents[rAssign.Agent.toString()].getBehaviorExecutingByName("ProceduralBehavior") != null
                        ).ToList();
                    foreach (RoleAssignement gAssign in goodAssigns)
                    {
                        AgentBehaviorExecution pbehavior = appli.AgentPlateform.Agents[gAssign.Agent.toString()].getBehaviorExecutingByName("ProceduralBehavior");
                        ProceduralBehavior procBehave = (ProceduralBehavior)(pbehavior);
                        procBehave.pushProcedureToDo(p, orgEntity, gAssign.Role, new Dictionary<string, ValueSpecification>());
                    }

                    /*
                    List<RoleAssignement> assigns = orgEntity.RoleAssignement;

                    appli.VRComponentFactory.Log("Assigns : " + assigns.Count);
                    foreach (RoleAssignement rAssign in assigns)
                    {
                        Agent agt = appli.AgentPlateform.Agents[rAssign.Agent.toString()];

                        appli.VRComponentFactory.Log("Role : " + rAssign.Role.name + " == " + agt.name);
                        AgentBehaviorExecution pbehavior = agt.getBehaviorExecutingByName("ProceduralBehavior");

                        if (pbehavior != null)
                        {
                            appli.VRComponentFactory.Log("Procedure launched for " + agt.name);
                            ProceduralBehavior procBehave = (ProceduralBehavior)(pbehavior);
                            procBehave.pushProcedureToDo(p, orgEntity, rAssign.Role, new Dictionary<string, ValueSpecification>());
                        }
                    }
                    */
                }
            }
            /*
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
                            List<RoleAssignement> assigns = orgs[iOrg].RoleAssignement;

                            appli.VRComponentFactory.Log("Assigns : " + assigns.Count);
                            for (int iAss = 0; iAss < assigns.Count; iAss++)
                            {
                                Agent agt = appli.AgentPlateform.Agents[assigns[iAss].Agent.toString()];
                                askedRole = assigns[iAss].Role;

                                appli.VRComponentFactory.Log("Role : " + assigns[iAss].Role.name + " == " + agt.name);

                                AgentBehaviorExecution pbehavior = agt.getBehaviorExecutingByName("ProceduralBehavior");

                                if (pbehavior != null)
                                {
                                    appli.VRComponentFactory.Log("Procedure launched for " + agt.name);
                                    ProceduralBehavior procBehave = (ProceduralBehavior)(pbehavior);

                                    Dictionary<string, ValueSpecification> procParams = new Dictionary<string, ValueSpecification>();

                                    procBehave.pushProcedureToDo(askedProc, askedOrg, askedRole, procParams);
                                }
                            }
                        }
                    }
                }
                
            }
         */
            return 0;
        }
    }
}
