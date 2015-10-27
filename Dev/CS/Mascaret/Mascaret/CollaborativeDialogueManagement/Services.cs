using System;
using System.Collections.Generic;
using System.Reflection;

using System.Globalization;
using DM;
namespace Mascaret
{
    public static class Services
    {
        public static string toInitCap(string inputString)
        {
            string outputString = null;

            outputString = new CultureInfo("en-US").TextInfo.ToTitleCase(inputString);

            return outputString;
        }
        public static string toLowerCase(string inputString)
        {

            string outputString = new CultureInfo("en-US").TextInfo.ToLower(inputString);
            return outputString;
        }

        /////////////////////////////////////
        //          Model related          //
        /////////////////////////////////////

        /////////////////////////////////////
        //          Model related          //
        /////////////////////////////////////
        public static List<string> listClasses()
        {
            List<string> result = new List<string>();
            try
            {
                Model model = MascaretApplication.Instance.Model;
                Dictionary<string, Class> classes = model.AllClassesFullName;

                Dictionary<string, Class>.Enumerator it = classes.GetEnumerator();
                while (it.MoveNext())
                {
                    result.Add((it.Current.Value).name);
                }
            }
            catch (IndexOutOfRangeException)
            {

            }
            return result;
        }
        public static bool isClass(string className)
        {
            List<string> classes = listClasses();
            try
            {
                for (int i = 0; i < classes.Count; i++)
                {
                    string temp1 = toLowerCase(classes[i]);
                    string temp2 = toLowerCase(className);
                    if (temp1.Equals(temp2))
                    {
                        return true;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            return false;


        }

        //vector<string> listClassAttributes(string className fullName=false)
        public static Class getClassByFullname(string classFullName)
        {
            Class c = null;
            Model model = MascaretApplication.Instance.Model;
            Dictionary<string, Class> classes = model.AllClassesFullName;
            try
            {
                c = classes[classFullName];
            }
            catch (KeyNotFoundException)
            {
            }
            return c;
        }

        public static List<string> listClassAttributes(string className)
        {
            List<string> result = new List<string>();
            Model model = MascaretApplication.Instance.Model;
            try
            {
                List<string> classFullNames = listClassFromSimpleName(className);
                for (int i = 0; i < classFullNames.Count; i++)
                {
                    Class c = getClassByFullname(classFullNames[i]);
                    if (c != null)
                    {
                        Dictionary<string, Property> attributes = c.Attributes;
                        Dictionary<string, Property>.Enumerator it = attributes.GetEnumerator();
                        while (it.MoveNext())
                        {
                            result.Add(it.Current.Key);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            return result;
        }
        public static bool isClassAttribute(string className, string attributeName)
        {
            Model model = MascaretApplication.Instance.Model;
            try
            {
                List<string> attributes = listClassAttributes(className);
                Console.Write("list of attributes:");
                Console.Write("\n");
                for (int i = 0; i < attributes.Count; i++)
                {
                    Console.Write("attribute: ");
                    Console.Write(attributes[i]);
                    Console.Write("\n");
                    string tempAttribute = toLowerCase(attributes[i]);
                    string attributeToLower = toLowerCase(attributeName);
                    if (tempAttribute.Equals(attributeToLower))
                    {
                        return true;
                    }
                }

            }
            catch (KeyNotFoundException)
            {
            }
            return false;
        }

        //vector<string> listClassOperations(string className fullName=false)


        public static List<string> listClassOperations(string className)
        {
            List<string> result = new List<string>();
            Model model = MascaretApplication.Instance.Model;
            try
            {
                List<string> classFullNames = listClassFromSimpleName(className);
                for (int i = 0; i < classFullNames.Count; i++)
                {
                    Class c = getClassByFullname(classFullNames[i]);
                    if (c != null)
                    {
                        Dictionary<string, Operation> operations = c.Operations;
                        Dictionary<string, Operation>.Enumerator it = operations.GetEnumerator();
                        while (it.MoveNext())
                        {
                            result.Add(it.Current.Key);
                        }
                    }
                }
            }
            catch (KeyNotFoundException)
            {
            }
            return result;
        }
        public static bool isClassOperation(string className, string operationName)
        {
            Model model = MascaretApplication.Instance.Model;
            try
            {
                List<string> operations = listClassOperations(className);
                Console.Write("list of attributes:");
                Console.Write("\n");
                for (int i = 0; i < operations.Count; i++)
                {
                    //std::cout<< "operation: " << operations[i]<<std::endl;
                    if (operations[i] == operationName)
                    {
                        return true;
                    }
                }

            }
            catch (KeyNotFoundException)
            {
            }
            return false;
        }

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getClassSimpleName(string className);





        public static List<string> listClassFromSimpleName(string simpleClassName)
        {
            Model model = MascaretApplication.Instance.Model;
            List<string> result = new List<string>();

            Dictionary<string, Class> classes = model.AllClassesFullName;
            foreach (KeyValuePair<string, Class> cl in classes)
            {
                if (cl.Value.name.Equals(simpleClassName))
                {
                    result.Add(cl.Key);
                }
            }
            return result;
        }

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getClassMetaData(string className, string data);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getClassAttributeClass(string className, string attributeName);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getClassAttributeMetaData(string className, string attributeName, string data);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getClassOperationMetaData(string className, string operationName, string data);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listClassOperationParameters(string className, string operationName);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getClassOperationParameterClass(string className, string operationName, string parameterName);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getClassOperationParameterMetaData(string className, string operationName, string parameterName, string data);


        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listClassAllParents(string className); // current ancestors  only
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listClassAllChildrens(string className); // current descendants  level
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getClassPackage(string className);




        /////////////////////////////////////
        //        Instances related        //
        /////////////////////////////////////
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listInstances();
        public static bool isInstance(string instanceName)
        {
            InstanceSpecification instance = getInstanceByName(instanceName);
            if (instance!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<string> listInstancesByClass(string className)
        {
            List<InstanceSpecification> instances = getAllInstance();
            List<string> result = new List<string>();
            Model model = MascaretApplication.Instance.Model;
            try
            {
                List<string> classFullNames = listClassFromSimpleName(className);
                for (int i = 0; i < instances.Count; i++)
                    {
                        if (instances[i].Classifier!=null)
                        {
                                string name = instances[i].Classifier.name;
                                if (name == className)
                                {
                                    result.Add(instances[i].name);
                                }
                        }
                    }

             }
            catch (Exception)
            {
            }
            return result;
        }


        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listAgents();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listAllAgentsFirstName();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listAgentsByClass(string className);

        //for the moment the agent name is verified from the conversational agent name 
        //not from the environment which should be the case
        public static bool isAgent(string name)
        {
            try
            {
                Dictionary<string, Agent> agents = MascaretApplication.Instance.AgentPlateform.Agents;
                foreach (KeyValuePair<string, Agent> agt in agents)
                {
                       if (agt.Value.name == name)
                        return true;
                }

            }
            catch (KeyNotFoundException)
            {
            }

            return false;
        }
        public static Agent getAgentByName(string name)
        {
            Agent res = null;
            try
            {
                Dictionary<string, Agent> agents = MascaretApplication.Instance.AgentPlateform.Agents;
                foreach (KeyValuePair<string, Agent> agt in agents)
                {
                    if (agt.Value.name == name)
                        res =  agt.Value;
                }

            }
            catch (KeyNotFoundException)
            {
            }

            return res;
        }

        public static List<Entity> getAllEntities()
        {
            List<Entity> results = new List<Entity>();
            List<InstanceSpecification> instances = getAllInstance();

            Environment env = MascaretApplication.Instance.getEnvironment();
            Dictionary<string, InstanceSpecification>.Enumerator it = env.InstanceSpecifications.GetEnumerator();
            while (it.MoveNext())
            {
                try
                {
                    Entity entity = (Entity)(it.Current.Value);
                    if (entity != null)
                    {
                        results.Add(entity);
                    }
                }
                catch (InvalidCastException)
                {

                }
            }
            return results;

      }

        public static List<string> getAllEntitiesName()
        {
            List<string > results = new List<string>();
            List<InstanceSpecification> instances = getAllInstance();
            Environment env = MascaretApplication.Instance.getEnvironment();
            Dictionary<string, InstanceSpecification>.Enumerator it = env.InstanceSpecifications.GetEnumerator();
            while (it.MoveNext())
            {
                try
                {
                    Entity entity = (Entity)(it.Current.Value);
                    if (entity != null)
                    {
                        results.Add(entity.name);
                    }
                }
                catch (InvalidCastException)
                {

                }
            }
            return results;
        }


        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listEntities();
        /*      public static string getEntyStatePerceivedByAgent(string agentName, string entityName)
              {
                  string result;
                  try
                  {
                      VirtualHuman agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          //return result;
                      }
                      else
                      {
                          List<perceivedEntity> entities = agent.getMemory().getPerceptionKnowledgeBase().getPerceivedEntities();
                          Console.Write(" number of perceived entities =  ");
                          Console.Write(entities.Count);
                          Console.Write("\n");
                          List<perceivedEntity>.Enumerator it;
                          it = entities.GetEnumerator();
                          while (it.MoveNext())
                          {
                              Console.Write(" db entity :  ");
                              Console.Write(toLowerCase(it.get().getName()));
                              Console.Write(" to ");
                              Console.Write(toLowerCase(entityName));
                              Console.Write("\n");
                              if (toLowerCase(it.get().getName()) == toLowerCase(entityName))
                              {
                                  result = (it.Current).getState();
                                  Console.Write(" inside service : state ");
                                  Console.Write(result);
                                  return result;
                              }
                          }
                      }
                  }
                  catch (KeyNotFoundException)
                  {
                  }
                  return result;
              }



                  */




        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getAgentByAttributeValue(string attributeName, string attributeValue);
        public static string getInstanceClass(string instanceName)
        {
            InstanceSpecification instance = getInstanceByName(instanceName);
            return instance.Classifier.name;
        }
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getInstanceFullClass(string instanceName);
        public static string getInstanceAttributeValue(string instanceName, string attributeName)
        {
            string result = null;
            InstanceSpecification instance = getInstanceByName(instanceName);
            try
            {
                Slot slot = instance.Slots[attributeName];
                result = slot.getValue().getStringFromValue();

                // return MAP_AT(instance.getSlots(), (string)attributeName).getValue().getStringFromValue();
            }
            catch (KeyNotFoundException)
            {
            }
            return result;
        }



        //string getInstanceAttribute(string instanceName, string attributeName);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //bool isInstanceAnAgent(string instanceName);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listInstanceExecutedOperations(string instanceName);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getInstanceExecutedOperationParamValue(string instanceName, string operationName, string parameterName);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void startInstanceOperation(string instanceName, string operationName, ClassicVector<string> paramValues);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void stopInstanceOperation(string instanceName, string operationName);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string> listInstanceStateMachines(string instanceName);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getInstanceStateMachineState(string instanceName, string stateMachineName);


        /*      public static string getCurrentAction(string agentName)
              {

                  string result;
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          //return result;
                      }
                      else
                      {
                          //  perceivedAction action = 
                          agent.getMemory().getPerceptionKnowledgeBase().getPerceivedAction(agentName);
                          if (action.getActionStatus() == "InProgress")
                          {
                              result = action.getActionName();
                          }
                      }
                  }
                  catch (KeyNotFoundException)
                  {

                  }
                  return result;

              }


              public static string getCurrentAction(string agentName, string performer)
              {
                  Console.Write(" self :");
                  Console.Write(agentName);
                  Console.Write(" performer : ");
                  Console.Write(performer);
                  Console.Write("\n");
                  string result;
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          //return result;
                      }
                      else
                      {
                          perceivedAction action = agent.getMemory().getPerceptionKnowledgeBase().getPerceivedAction(performer);
                          if (action.getActionStatus() == "InProgress")
                          {
                              result = action.getActionName();
                          }
                      }
                  }
                  catch (KeyNotFoundException)
                  {

                  }
                  return result;

              }

              public static string getLastAction(string agentName, string performer)
              {
                  string result;
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          //return result;
                      }
                      else
                      {
                          List<perceivedAction> actions = agent.getMemory().getPerceptionKnowledgeBase().getPerceivedActions(performer);
                          if (actions)
                          {
                              if (actions.Count > 0)
                              {
                                  for (int i = actions.Count - 1; i >= 0; i--)
                                  {
                                      if (actions[i].getActionStatus() == "Done")
                                      {
                                          result = actions[i].getActionName();
                                          return result;
                                      }

                                  }

                              }
                          }
                      }
                  }
                  catch (KeyNotFoundException)
                  {

                  }
                  return result;
              }
              public static bool isLastAction(string agent, string performer, string action)
              {
                  string lastAction = getLastAction(agent, performer);
                  if (toLowerCase(lastAction) == toLowerCase(action))
                  {
                      return true;
                  }
                  else
                  {
                      return false;
                  }
              }
              public static bool isPastAction(string agentName, string performer, string pastAction)
              {
                  Console.Write("inside isPastAction");
                  Console.Write("\n");
                  Console.Write(" agent : ");
                  Console.Write(agentName);
                  Console.Write(" performer ");
                  Console.Write(performer);
                  Console.Write(" action ");
                  Console.Write(pastAction);
                  Console.Write("\n");
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          return false;
                      }
                      else
                      {
                          List<perceivedAction> actions = agent.getMemory().getPerceptionKnowledgeBase().getPerceivedActions(performer);
                          if (actions)
                          {

                              if (actions.Count > 0)
                              {
                                  for (int i = actions.Count - 1; i >= 0; i--)
                                  {
                                      Console.Write(" comaparing");
                                      Console.Write(actions[i].getActionName());
                                      Console.Write(" == ");
                                      Console.Write(pastAction);
                                      Console.Write("\n");
                                      Console.Write(" ");
                                      Console.Write(actions[i].getActionStatus());
                                      Console.Write("\n");
                                      if ((actions[i].getActionStatus() == "Done") && (toLowerCase(actions[i].getActionName()) == toLowerCase(pastAction)))
                                      {
                                          return true;
                                      }

                                  }

                              }
                          }
                      }
                  }
                  catch (KeyNotFoundException)
                  {

                  }
                  return false;
              }
              public static string getLastAction(string agentName)
              {
                  string result;
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          //return result;
                      }
                      else
                      {
                          List<perceivedAction> actions = agent.getMemory().getPerceptionKnowledgeBase().getPerceivedActions(agentName);
                          if (actions)
                          {
                              if (actions.Count > 0)
                              {
                                  for (int i = actions.Count - 1; i >= 0; i--)
                                  {
                                      if (actions[i].getActionStatus() == "Done")
                                      {
                                          result = actions[i].getActionName();
                                          return result;
                                      }

                                  }

                              }
                          }
                      }
                  }
                  catch (KeyNotFoundException)
                  {

                  }
                  return result;
              }
              public static string getActionStatus(string performer, string action)
              {
                  string result;
                  return result;
              }

              public static string getAgentRole(string agentName)
              {
                  string role;
                  ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                  role = CollaborativeDialogueManager.getInstance().getOrganisationModel().getRoleAssignmentFrame().getRole(agent.getName());
                  //	std::cout<< " agent :"<< agentName << " role :"<< role<< std::endl;
                  return role;
              }
              public static bool isAgentRole(string agent, string role)
              {
                  string agentRole = getAgentRole(agent);
                  //std::cout<< " agentRole" << agentRole << " given role "<< role << std::endl;

                  boost.algorithm.to_lower(agentRole);
                  boost.algorithm.to_lower(role);
                  if (agentRole == role)
                  {
                      return true;
                  }
                  else
                  {
                      return false;
                  }
              }
              */



        public static string getAgentByRole(string roleName)
        {
            string agentName = null;
            OrganisationalEntity organisation = MascaretApplication.Instance.AgentPlateform.ActiveOrgansation;
            List<RoleAssignement> roleAssignments = organisation.RoleAssignement;
            foreach (RoleAssignement roleAssignment in roleAssignments)
            {
                if (roleAssignment.Role.name.Equals(roleName))
                {
                    agentName = roleAssignment.Agent.name;
                    break;
                }

            }
            return agentName;
        }

        /*    public static string getAgentActionDecision(string agentName)
            {

                string result;
                try
                {
                    ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                    if (agent == null)
                    {
                        std.cerr << " agent name not found  " << std.endl;
                        //return result;
                    }
                    else
                    {
                        Stack<ActionDecision> decisions = agent.getMemory().getActionDecisions();
                        if (decisions.Count > 0)
                        {
                            result = decisions.Peek().getChosenStepId();
                        }
                    }
                }
                catch (KeyNotFoundException)
                {

                }
                return result;

            }
            */
        //Activity execution
        //specific service functions
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<ActionNode> listRoleCurrentActions(string role);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<ActionNode> listRoleNextActions(string role);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector< ActionNode> getOutgoingScenarioActionNodeByPartition(ActionNode node, string role);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //boost::shared_ptr<TeamAction> getTeamNextAction();

        //string functions
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string > listRoleCurrentActionsNames(string role);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string > listRoleNextActionsNames(string role);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //ClassicVector<string > getTeamNextActionName();

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getRoleByFutureActionName(ActionNode node, string actionName);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string getRoleByFutureActionName(string actionName);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //bool isActionExecutionByRole(string role, string action, string tense);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //bool willActionBeExecutedByRole(string role, string action);


        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //bool isActionExecutionByAgent(string hostAgent, string performer, string action, string tense);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //bool isLastAction(string agent, string performer, string action);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //bool isPastAction(string agent, string performer, string action);

        /*      public static bool isEntityPerceivedByAgent(string agentName, string entity)
              {
                  string result;
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          //return result;
                      }
                      else
                      {
                          List<perceivedEntity> entities = agent.getMemory().getPerceptionKnowledgeBase().getPerceivedEntities();
                          Console.Write(" number of perceived entities =  ");
                          Console.Write(entities.Count);
                          Console.Write("\n");
                          List<perceivedEntity>.Enumerator it;
                          it = entities.GetEnumerator();
                          while (it.MoveNext())
                          {
                              //	std::cout<< " db entity :  " << toLowerCase(it->get()->getName())<< " to " <<  toLowerCase(entityName)<< std::endl;
                              if (toLowerCase(it.get().getName()) == toLowerCase(entity))
                              {
                                  return true;
                              }
                          }
                      }

                  }
                  catch (KeyNotFoundException)
                  {
                  }
                  return false;
              }

              public static bool isCurrentActionOfAgent(string agent, string currentAction)
              {

                  string action = getCurrentAction(agent);
                  if (toLowerCase(currentAction) == toLowerCase(action))
                  {
                      return true;
                  }
                  else
                  {
                      return false;
                  }
              }

              public static bool isCurrentActionOfPerformerPerceivedByAgent(string agent, string performer, string currentAction)
              {
                  string action = getCurrentAction(agent, performer);
                  if (toLowerCase(currentAction) == toLowerCase(action))
                  {
                      return true;
                  }
                  else
                  {
                      return false;
                  }

              }


              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //bool isTeamAction(string action, string tense);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //bool isTeamFutureAction(string action);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //bool isTeamNextAction(string action);


              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //string getActionActivityName(string actionName);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //string ActionNodeParentActivity(Activity activity, string actionName);

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //string getReasonOfActionExecution(string hostAgent, string role, string action, string tense);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<string> listAllActionsDoneByRole(string role);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<ActionNode> listAllActionsForRole(Activity activity, string role);


              public static string getActionPerformer(string hostAgent, string action, string tense)
              {
                  string result;
                  if (tense == "past")
                  {
                      result = getPastActionPerformer(hostAgent, action);
                      Console.Write("DEBUG >>> returing from getActionPerformer");
                      Console.Write("\n");
                  }
                  if (tense == "pastnear")
                  {
                      result = getLastActionPerformer(hostAgent, action);
                      Console.Write("DEBUG >>> returing from getLastActionPerformer");
                      Console.Write("\n");
                  }
                  else if (tense == "present")
                  {
                      result = getCurrentActionPerformer(hostAgent, action);
                      Console.Write("DEBUG >>> returing from getCurrentActionPerformer");
                      Console.Write("\n");
                  }
                  return result;
              }
              public static string getCurrentActionPerformer(string agentName, string action)
              {
                  Console.Write("inside getCurrentActionPerformer");
                  Console.Write("\n");
                  string performer;
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          return false;
                      }
                      else
                      {
                          Dictionary<string, List<perceivedAction>> actionMap = agent.getMemory().getPerceptionKnowledgeBase().getAllPerceivedActions();
                          Dictionary<string, List<perceivedAction>>.Enumerator it = actionMap.GetEnumerator();
                          while (it.MoveNext())
                          {
                              List<perceivedAction> actions = it.Current.Value;

                              perceivedAction tempAction = actions[actions.Count - 1];
                              if (toLowerCase(tempAction.getActionName()) == toLowerCase(action) && tempAction.getActionStatus() == "inProgress")
                              {
                                  performer = tempAction.getPerformerName();
                                  return performer;
                              }

                          }
                      }



                  }
                  catch (KeyNotFoundException)
                  {

                  }
                  return performer;
              }
              public static string getPastActionPerformer(string agentName, string action)
              {
                  Console.Write("inside getPastActionPerformer");
                  Console.Write("\n");
                  Console.Write("DEBUG:: agent ");
                  Console.Write(agentName);
                  Console.Write(" action:  ");
                  Console.Write(action);
                  Console.Write("\n");
                  string performer;
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          return false;
                      }
                      else
                      {
                          Dictionary<string, List<perceivedAction>> actionMap = agent.getMemory().getPerceptionKnowledgeBase().getAllPerceivedActions();
                          Dictionary<string, List<perceivedAction>>.Enumerator it = actionMap.GetEnumerator();
                          while (it.MoveNext())
                          {
                              Console.Write("DEBUG  checking actions performed by >>> ");
                              Console.Write(it.Current.Key);
                              Console.Write("\n");
                              List<perceivedAction> actions = it.Current.Value;
                              List<perceivedAction>.Enumerator itr = actions.GetEnumerator();
                              //C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
                              while (itr != actions.end())
                              {
                                  Console.Write("DEBUG comparing>>> ");
                                  Console.Write(action);
                                  Console.Write(" <> ");
                                  Console.Write(itr.getActionName());
                                  Console.Write("\n");
                                  if (toLowerCase(itr.getActionName()) == toLowerCase(action) && itr.getActionStatus() == "Done")
                                  {
                                      Console.Write("DEBUG >>> match found done");
                                      Console.Write("\n");
                                      performer = it.Current.Key;
                                      return performer;
                                  }

                                  //C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
                                  itr++;
                              }

                          }
                      }



                  }
                  catch (KeyNotFoundException)
                  {

                  }
                  return performer;
              }
              public static string getLastActionPerformer(string agentName, string action)
              {
                  Console.Write("inside getLastActionPerformer");
                  Console.Write("\n");
                  Console.Write("DEBUG:: agent ");
                  Console.Write(agentName);
                  Console.Write(" action:  ");
                  Console.Write(action);
                  Console.Write("\n");
                  string performer;
                  try
                  {
                      ConversationalAgent agent = CollaborativeDialogueManager.getInstance().getAgent(agentName);
                      if (agent == null)
                      {
                          std.cerr << " agent name not found  " << std.endl;
                          return false;
                      }
                      else
                      {
                          Dictionary<string, List<perceivedAction>> actionMap = agent.getMemory().getPerceptionKnowledgeBase().getAllPerceivedActions();
                          Dictionary<string, List<perceivedAction>>.Enumerator it = actionMap.GetEnumerator();
                          while (it.MoveNext())
                          {
                              Console.Write("DEBUG  checking actions performed by >>> ");
                              Console.Write(it.Current.Key);
                              Console.Write("\n");
                              List<perceivedAction> actions = it.Current.Value;
                              for (int i = actions.Count - 1; i >= 0; i--)
                              {
                                  if (actions[i].getActionStatus() == "Done")
                                  {
                                      if (toLowerCase(actions[i].getActionName()) == toLowerCase(action))
                                      {
                                          Console.Write("DEBUG comparing>>> ");
                                          Console.Write(action);
                                          Console.Write(" <> ");
                                          Console.Write(actions[i].getActionName());
                                          Console.Write("\n");
                                          performer = performer = it.Current.Key;
                                          return performer;
                                      }
                                      break;
                                  }

                              }

                          }
                      }



                  }
                  catch (KeyNotFoundException)
                  {

                  }
                  return performer;
              }

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              // boost::shared_ptr<ActionNode> getActionNodeOfActivityByActionName(Activity activity, string actionName);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              // boost::shared_ptr<ActionNode> getActionNodeByName(string actionName);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              // ClassicVector<ObjectNode> listMeanResourcesOfActionByName(string actionName);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<string> listMeanResourcesOfAction(string action);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<string> listObjectResourcesOfAction(string action);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<string> listTargetResourcesOfAction(string action);

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<string> listHumanBodyResourcesOfAction(string action);

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //bool guardConditionEvaluation(string hostAgent, Expression e);

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<ActionNode> listCurrentMacroActivity(string hostAgent);

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<ObjectNode> listResourcesOfAction(ActionNode actionNode, string resourceType);

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //string getAgentName(string host, string tartgetAgent);
              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //ClassicVector<string> listCurrentMacroActionNames(string hostAgent);

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //boost::any randomChoice(ClassicVector<boost::any> NamelessParameter);

              //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
              //bool pluginCall(InstanceSpecification host, string behaviorName);


          */



        /////////////////////////////////////
        //        Instances related        //
        /////////////////////////////////////






        public static InstanceSpecification getInstanceByName(string entityName)
        {
            Environment env = MascaretApplication.Instance.getEnvironment();
            InstanceSpecification entity = null;
            try
            {

                entity = (InstanceSpecification)env.getEntity(entityName);

            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Can't find : " + entityName);
                return entity;
            }
            return entity;
        }


        public static List<InstanceSpecification> getAllInstance()
        {
            List<InstanceSpecification> results = new List<InstanceSpecification>();
            Environment env = MascaretApplication.Instance.getEnvironment();
            Dictionary<string, InstanceSpecification>.Enumerator it = env.InstanceSpecifications.GetEnumerator();
            while (it.MoveNext())
            {
                results.Add(it.Current.Value);
            }
            return results;
        }



        public static string getAgentName(string host, string tartgetAgent)
        {
            string agentName = null;
            Agent agt = getAgentByName(host);
            VirtualHuman vh = (VirtualHuman)agt;
            if (vh!= null)
            {
                List<object> args2 = new List<object>();
                string name = tartgetAgent;
                string var = "$";
                args2.Add(tartgetAgent);
                args2.Add(var);
                string functor = "Agent-Name";
                Predicate exp = new Predicate(functor, args2);
                DM.Property beliefs = vh.DialogueManager.IS.getPropertyValueOfPath( DefineConstants.semanticBeliefs);
                object result = beliefs.evaluate(exp);
                if (result!= null)
                {
                    try {
                        agentName = (string)(result);
                    }
                    catch (InvalidCastException) { }
                    
                }
            }
            return agentName;
        }


    public static bool evaluateCondition(string predicate)
        {
            bool result = false;

            string[] functionCallElements = predicate.Split(new string[] { " " }, StringSplitOptions.None);

            string serviceMethod = functionCallElements[0];


            Type t = MethodBase.GetCurrentMethod().DeclaringType;
            MethodInfo method = t.GetMethod(serviceMethod);
            object[] parametersArray = null; //
            List<object> args = new List<object>();
            if (functionCallElements.Length > 1)
            {
                parametersArray = new object [functionCallElements.Length-1];
                for (int i = 1; i < functionCallElements.Length; i++)
                {
                    parametersArray[i-1] = functionCallElements[i];
                }

            }
            
           object evalResult =  method.Invoke(t, parametersArray);
            try
            {
                result = (bool)evalResult;
            }
            catch (InvalidCastException e)
            {
            }
            return result;
        }










    }





    public class TeamAction
    {
        public bool insideCurrentNodeOnly;
        public List<string> roles = new List<string>();
        public List<ActionNode> nodes = new List<ActionNode>();
        public Dictionary<List<string>, ActionNode> teamNextActions = new Dictionary<List<string>, ActionNode>();
        public Dictionary<string, List<ActionNode>> individualRoleActions = new Dictionary<string, List<ActionNode>>();
    }

}
