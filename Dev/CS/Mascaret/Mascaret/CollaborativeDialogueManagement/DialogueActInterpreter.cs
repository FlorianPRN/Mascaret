using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Mascaret;
namespace DM
{

    public class DialogueActInterpreter
    {

        private DialogueAct da =  new DialogueAct();
        private InformationState infoState = new InformationState();
        //std::vector<boost::shared_ptr<UpdateRule> >  updateRules;
        private VirtualHuman hostAgent = null;

        public  DialogueActInterpreter()
        {
            
        }
        public DialogueActInterpreter(VirtualHuman host)
        {
            hostAgent = host;

        }
        public DialogueActInterpreter(DialogueAct da, InformationState IS)
        {
            infoState = IS;
            this.da = da;
        }

        public void Dispose()
        {
        }
        public List<UpdateRule> interprete(DialogueAct da, InformationState infoState)
        {
            List<UpdateRule> updateRules = new List<UpdateRule>();

            Console.Write(" step 3 : Dialogue act interpretation : ");
            Console.Write("\n");
            if (da.getID() == "Greet-open")
            {
                List<object> args = new List<object>();
                args.Add(da.getSender());
                string addressee = "Self";
                //args.push_back(da->getAddressee());
                args.Add(addressee);
                //predicate greet(user, system)
                Predicate userGreet = new Predicate("Greet-open", args);

                List<object> args1 = new List<object>();

                //	args1.push_back(da->getAddressee());

                args1.Add(addressee);
                //	args1.push_back(addressee);

                args1.Add(da.getSender());
                //predicate greet( system, user)
                 Predicate systemGreet = new Predicate("reply-Greet-open", args1);

                //  boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", userGreet));
                Update pushAgenda = new Update("pushAgenda",systemGreet);
                Update addInIntegraedMoves = new Update("addInIntegraedMoves", userGreet);
                string grt = "Greet-open";
                //	boost::any anygrt = grt;
                Update socialContext = new Update("commPressure", grt);

                UpdateRule rule = new UpdateRule();
                //rule->addPrecondition(condition);
                rule.addEffect(pushAgenda);
                rule.addEffect(addInIntegraedMoves);

                updateRules.Add(rule);

            }


            if (da.getID() == "SOM-Presented-by_other")
            {
                List<object> args = new List<object>();
                args.Add(da.getSender());
                string addressee = "Self";
                //args.push_back(da->getAddressee());
                args.Add(addressee);
                //predicate greet(user, system)
                Predicate userGreet = new Predicate("Greet-open", args);

                List<object> args1 = new List<object>();

                //	args1.push_back(da->getAddressee());

                args1.Add(addressee);
                //	args1.push_back(addressee);

                args1.Add(da.getSender());
                Predicate systemGreet = new Predicate("reply-Greet-open", args1);

                Update pushAgenda = new Update("pushAgenda", systemGreet);
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  userGreet);
                string grt = "Greet-open";
                //	boost::any anygrt = grt;
                Update socialContext = new Update("commPressure", grt);

                UpdateRule rule = new UpdateRule();
                //rule->addPrecondition(condition);
                rule.addEffect(pushAgenda);
                rule.addEffect(addInIntegraedMoves);

                updateRules.Add(rule);

            }

            if (da.getID() == "Greet-close")
            {
                List<object> args = new List<object>();
                args.Add(da.getSender());
                args.Add(da.getAddressee());
                //predicate greet(user, system)
                Predicate userGreet = new Predicate("Greet-close", args);

                List<object> args1 = new List<object>();

                //	args1.push_back(da->getAddressee());
                //	args1.push_back(da->getSender());
                //predicate greet( system, user)
                Predicate systemGreet = new Predicate("Greet-close", args1);

                //  boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", userGreet));

                Update pushAgenda = new Update("pushAgenda",  systemGreet);
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  userGreet);
                string grt = "Greet-close";
                //	boost::any anygrt = grt;
                Update socialContext = new Update("commPressure", grt);

                UpdateRule rule = new UpdateRule();
                //rule->addPrecondition(condition);
                rule.addEffect(pushAgenda);
                rule.addEffect(addInIntegraedMoves);

                updateRules.Add(rule);

            }

             //user ask what-concept to system
            if (da.getID() == "WHQ-WHAT-Concept")
            {
                string conceptName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isClass(conceptName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatConcept = new Predicate("ans-WHQ-WHAT-Concept", args);
                    Update pushAgenda = new Update("pushAgenda",  ansWhatConcept);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //	boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //	//boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);
                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }
            } //whq-what-concept

            //user asks what are the features of a car?
            if (da.getID() == "WHQ-WHAT-All-Operations")
            {
                string conceptName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isClass(conceptName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatConcept = new Predicate("ans-WHQ-WHAT-All-Operations", args);

                   Update pushAgenda = new Update("pushAgenda",  ansWhatConcept);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-All-Operations
              //.............................................................................................

            //user asks what are the features of a car?
            if (da.getID() == "WHQ-WHAT-Entity-State")
            {
                string instanceName = (string)(da.logicalForm.Arguments[1]);
                //TODO verify whether object is a valid instance or not
                //	if (isInstance(instanceName) )
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansEntityState = new Predicate("ans-WHQ-WHAT-Entity-State", args);
                    Update pushAgenda = new Update("pushAgenda",  ansEntityState);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                /*	else
                    {
                        std::vector<boost::any> args;
                    //	args.push_back(conceptName);
                        Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                        boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                        boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                        rule->addEffect(pushAgenda);
                        updateRules.push_back(rule);
                    }
                    */
            } //WHQ-WHAT-instance state



            //.............................................................................................
            //user asks what are the attributes of a car?
            if (da.getID() == "WHQ-WHAT-All-Attributes")
            {
                string conceptName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isClass(conceptName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatConcept = new Predicate("ans-WHQ-WHAT-All-Attributes", args);
                    Update pushAgenda = new Update("pushAgenda",  ansWhatConcept);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                    
                }
                else
                {

                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }



            } //WHQ-WHAT-All-Operations

            //user asks  for the speed of a car?
            if (da.getID() == "WHQ-WHAT-Concept-Attribute-Value")
            {

                string conceptName = (string)(da.logicalForm.Arguments[0]);
                string attributeName = (string)(da.logicalForm.Arguments[1]);
                if (Services.isClass(conceptName) && Services.isClassAttribute(conceptName, attributeName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatConceptAttribute = new Predicate("ans-WHQ-WHAT-Concept-Attribute-Value", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatConceptAttribute);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));

                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-concept-attribute

            //user asks  for the description of the operation of the concept?
            if (da.getID() == "WHQ-WHAT-Concept-Feature")
            {

                string conceptName = (string)(da.logicalForm.Arguments[0]);
                string operationName = (string)(da.logicalForm.Arguments[1]);
                if (Services.isClass(conceptName) && Services.isClassOperation(conceptName, operationName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatConceptFeature = new Predicate("ans-WHQ-WHAT-Concept-Feature", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatConceptFeature);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-concept-feature

            //user asks  for the description of the current action of the agent
            if (da.getID() == "WHQ-WHAT-Current-Action")
            {

                string agentName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isAgent(agentName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatCurrent = new Predicate("ans-WHQ-WHAT-Current-Action", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatCurrent);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-All-Operations


            //user asks  for the description of the action decision of the agent
            if (da.getID() == "WHQ-WHAT-Action-Decision")
            {

                string agentName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isAgent(agentName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansActionDecision = new Predicate("ans-WHQ-WHAT-Action-Decision", args);

                    Update pushAgenda = new Update("pushAgenda",  ansActionDecision);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));
                    
                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);
                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-action-decision


            //user asks  for the description of the last action of the agent
            if (da.getID() == "WHQ-WHAT-Last-Action")
            {

                string agentName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isAgent(agentName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatLastAction = new Predicate("ans-WHQ-WHAT-Last-Action", args);
                    Update pushAgenda = new Update("pushAgenda",  ansWhatLastAction);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-All-Operations

            //user asks  for the description of the next action of the agent
            if (da.getID() == "WHQ-WHAT-Next-Action")
            {

                string agentName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isAgent(agentName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatConceptFeature = new Predicate("ans-WHQ-WHAT-Next-Action", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatConceptFeature);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-next action

            //user asks  for the description of the next action of the agent
            if (da.getID() == "WHQ-WHAT-Current-Activity")
            {

                string agentName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isAgent(agentName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatAgentActivity = new Predicate("ans-WHQ-WHAT-Current-Activity", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatAgentActivity);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));
                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-next action

            //user asks  for the description of the role of the agent
            if (da.getID() == "WHQ-WHAT-Agent-Role")
            {

                string agentName = (string)(da.logicalForm.Arguments[0]);

                if (Services.isAgent(agentName))
                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatAgentRole = new Predicate("ans-WHQ-WHAT-Agent-Role", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatAgentRole);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));

                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                else
                {
                    List<object> args = new List<object>();
                    //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                    Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                    UpdateRule rule = new UpdateRule();
                    rule.addEffect(pushAgenda);
                    updateRules.Add(rule);
                }

            } //WHQ-WHAT-AgentT-Role

            //........................................................................................................

            //user asks  for the description of the next action of the role
            if (da.getID() == "WHQ-WHAT-Role-Next-Action")
            {
                string roleName = (string)(da.logicalForm.Arguments[0]);
                {
                    //if (isRole(roleName))
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatRoleNext = new Predicate("ans-WHQ-WHAT-Role-Next-Action", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatRoleNext);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));
                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                //else
                /*	{
                        std::vector<boost::any> args;
                    //	args.push_back(conceptName);
                        Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                        boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                        boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                        rule->addEffect(pushAgenda);
                        updateRules.push_back(rule);
                    }
            */
            } //WHQ-WHAT-Role-Next-Action
              //........................................................................................................

            //........................................................................................................

            //user asks  for the description of the next action of the team
            if (da.getID() == "WHQ-WHAT-Team-Next-Action")
            {

                {
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatTeamNext = new Predicate("ans-WHQ-WHAT-Team-Next-Action", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatTeamNext);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                //else
                /*	{
                        std::vector<boost::any> args;
                    //	args.push_back(conceptName);
                        Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                        boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                        boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                        rule->addEffect(pushAgenda);
                        updateRules.push_back(rule);
                    }
            */
            } //WHQ-WHAT-Role-Next-Action
              //........................................................................................................
              //........................................................................................................

            //user asks  for the description of the current action of the role
            if (da.getID() == "WHQ-WHAT-Role-Current-Action")
            {

                string roleName = (string)(da.logicalForm.Arguments[0]);

                {
                    //if (isRole(roleName))
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatRoleCurrentAction = new Predicate("ans-WHQ-WHAT-Role-Current-Action", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatRoleCurrentAction);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                //else
                /*	{
                        std::vector<boost::any> args;
                    //	args.push_back(conceptName);
                        Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                        boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                        boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                        rule->addEffect(pushAgenda);
                        updateRules.push_back(rule);
                    }
            */
            } //WHQ-WHAT-Role-Next-Action
              //.............................................................................................
              //........................................................................................................

            //user asks  for the description of the means of action performed by performer
            if (da.getID() == "WHQ-WHAT-Action-Resource")
            {

                {
                    //if (isRole(roleName))
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhatMeanResource = new Predicate("ans-WHQ-WHAT-Action-Resource", args);

                    Update pushAgenda = new Update("pushAgenda",  ansWhatMeanResource);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                //else
                /*	{
                        std::vector<boost::any> args;
                    //	args.push_back(conceptName);
                        Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                        boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                        boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                        rule->addEffect(pushAgenda);
                        updateRules.push_back(rule);
                    }
            */
            } //"WHQ-WHAT-Action-Resource"
              //.............................................................................................



            //********************************************************************************
            //					WHQ-WHO   (present , past , future ) action
            //********************************************************************************

            /*  "WHQ-WHO-Will-Do-Action" @actionName
            */

            //user asks  for the role name who will do action in  future
            if (da.getID() == "WHQ-WHO-Will-Do-Action")
            {

                string actionName = (string)(da.logicalForm.Arguments[0]);

                {
                    //if (isAction(actionName))
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansWhoWillDo = new Predicate("ans-WHQ-WHO-Will-Do-Action", args);
                    Update pushAgenda = new Update("pushAgenda",  ansWhoWillDo);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));
                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                //else
                /*	{
                    std::vector<boost::any> args;
                //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                    boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                    boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                    rule->addEffect(pushAgenda);
                    updateRules.push_back(rule);
                }
            */
            } //WHQ-Who-Will-Do


            //********************************************************************************

            /*  "WHQ-WHO-Will-Do-Action" @actionName
            */

            //user asks  for the role name who will do action in  future
            if (da.getID() == "WHQ-WHO")
            {

                //std::string actionName = boost::any_cast<std::string>(da->logicalForm.Arguments.front());

                {
                    //if (isAction(actionName))
                    //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                    List<object> args = new List<object>();
                    Predicate ansDo = new Predicate("ans-WHQ-WHO", args);

                    Update pushAgenda = new Update("pushAgenda",  ansDo);
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                    //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                    //std::string comGround = "mutual-belief";
                    //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                    UpdateRule rule = new UpdateRule();

                    //rule->addPrecondition(condition);
                    rule.addEffect(pushAgenda);
                    //rule->addEffect(pushQUD);
                    rule.addEffect(addInIntegraedMoves);

                    updateRules.Add(rule);
                }
                //else
                /*	{
                    std::vector<boost::any> args;
                //	args.push_back(conceptName);
                    Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                    boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                    boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                    rule->addEffect(pushAgenda);
                    updateRules.push_back(rule);
                }
            */
            } //WHQ-Who-Will-Do

            //********************************************************************************

            //********************************************************************************
            //				end of 	WHQ-WHO   (present , past , future ) action
            //********************************************************************************


            //Yes-No-Questions

            if (da.getID() == "YES-NO-Question")
            {
                string funtor = (string)(da.logicalForm.Arguments[0]);

                //preprocessing of yes no questions

                if (funtor == "Agent-Role")
                {
                    //verify whether isAgent()  && isRole()
                    string agentName = (string)(da.logicalForm.Arguments[1]);
                    if (Services.isAgent(agentName))
                    {
                        //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                        List<object> args = new List<object>();
                        Predicate ansYesNoQtn = new Predicate("ans-YES-NO-Question", args);
                        Update pushAgenda = new Update("pushAgenda",  ansYesNoQtn);
                        Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                        //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                        //std::string comGround = "mutual-belief";
                        //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                        UpdateRule rule = new UpdateRule();

                        //rule->addPrecondition(condition);
                        rule.addEffect(pushAgenda);
                        //rule->addEffect(pushQUD);
                        rule.addEffect(addInIntegraedMoves);

                        updateRules.Add(rule);
                    }
                    else
                    {
                        List<object> args = new List<object>();
                        //	args.push_back(conceptName);
                        //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                        //ORIGINAL LINE: Predicate ansConceptNotKnown("respond-Unknown-concept", args);
                        Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);

                        //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                        //ORIGINAL LINE: Update *pushAgenda = new Update("pushAgenda",ansConceptNotKnown);
                        Update pushAgenda = new Update("pushAgenda",  ansConceptNotKnown);
                        UpdateRule rule = new UpdateRule();
                        rule.addEffect(pushAgenda);
                        updateRules.Add(rule);

                    }
                } //funcot == agent role

            } //WHQ-WHAT-All-Operations


            //********************************************************************************
            //					Check-Question
            //********************************************************************************
            if (da.getID() == "Check-Question")
            {
                string funtor = (string)(da.logicalForm.Arguments[0]);

                //preprocessing of yes no questions

                if (funtor == "Action-Execution-By-Role")
                {
                    //verify whether isRole() && sufficient parameters are available
                    string roleName = (string)(da.logicalForm.Arguments[1]);
                    //	if (isRole(roleName))
                    {
                        //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                        List<object> args = new List<object>();
                        Predicate ansCheckQtn = new Predicate("ans-Check-Question", args);

                        Update pushAgenda = new Update("pushAgenda",  ansCheckQtn);
                        Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                        //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));
                        
                        //std::string comGround = "mutual-belief";
                        //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                        UpdateRule rule = new UpdateRule();

                        //rule->addPrecondition(condition);
                        rule.addEffect(pushAgenda);
                        //rule->addEffect(pushQUD);
                        rule.addEffect(addInIntegraedMoves);

                        updateRules.Add(rule);
                    }
                    /*	//else
                        {
                            std::vector<boost::any> args;
                        //	args.push_back(conceptName);
                            Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                            boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                            boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                            rule->addEffect(pushAgenda);
                            updateRules.push_back(rule);

                        }
                        */
                } //funcot == Action-Execution-By-Role

                //..............................................................................
                if (funtor == "Entity-Perceived-by-Agent")
                {

                    string agent = (string)(da.logicalForm.Arguments[1]);
                    string entity = (string)(da.logicalForm.Arguments[2]);
                    //verify whether isEntity(entity)
                    //	if (isEntity(entity))
                    {
                        //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                        List<object> args = new List<object>();
                        Predicate ansCheckQtn = new Predicate("ans-Check-Question", args);
                         Update pushAgenda = new Update("pushAgenda",  ansCheckQtn);
                        Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                        //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                        //std::string comGround = "mutual-belief";
                        //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                        UpdateRule rule = new UpdateRule();

                        //rule->addPrecondition(condition);
                        rule.addEffect(pushAgenda);
                        //rule->addEffect(pushQUD);
                        rule.addEffect(addInIntegraedMoves);

                        updateRules.Add(rule);
                    }
                    /*	//else
                        {
                            std::vector<boost::any> args;
                        //	args.push_back(conceptName);
                            Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                            boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                            boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                            rule->addEffect(pushAgenda);
                            updateRules.push_back(rule);

                        }
                        */
                } //funcot == Action-Execution-By-Role

                //..............................................................................
                //frame:		Action-Execution-Perceived-By-Agent $agent $performer $action
                //..............................................................................
                if (funtor == "Action-Execution-Perceived-By-Agent")
                {

                    string agent = (string)(da.logicalForm.Arguments[1]);
                    string performer = (string)(da.logicalForm.Arguments[2]);
                    //verify whether isAgent(performer)
                    //	if (isAgent(performer))
                    {
                        //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                        List<object> args = new List<object>();
                        Predicate ansCheckQtn = new Predicate("ans-Check-Question", args);
                        Update pushAgenda = new Update("pushAgenda",  ansCheckQtn);
                        Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                        //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                        //std::string comGround = "mutual-belief";
                        //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                        UpdateRule rule = new UpdateRule();

                        //rule->addPrecondition(condition);
                        rule.addEffect(pushAgenda);
                        //rule->addEffect(pushQUD);
                        rule.addEffect(addInIntegraedMoves);

                        updateRules.Add(rule);
                    }
                    /*	//else
                        {
                            std::vector<boost::any> args;
                        //	args.push_back(conceptName);
                            Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                            boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                            boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                            rule->addEffect(pushAgenda);
                            updateRules.push_back(rule);

                        }
                        */
                } //funcot == Action-Execution-By-Role

                //..............................................................................

                //..............................................................................
                //frame:		Action-Execution-By-Team  $action $tense
                //..............................................................................
                if (funtor == "Action-Execution-By-Team")
                {

                    string action = (string)(da.logicalForm.Arguments[1]);

                    //verify whether isAction(action)
                    //	if (isAction(action))
                    {
                        //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                        List<object> args = new List<object>();
                        Predicate ansCheckQtn = new Predicate("ans-Check-Question", args);

                        Update pushAgenda = new Update("pushAgenda",  ansCheckQtn);
                        Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                        //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                        //std::string comGround = "mutual-belief";
                        //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                        UpdateRule rule = new UpdateRule();

                        //rule->addPrecondition(condition);
                        rule.addEffect(pushAgenda);
                        //rule->addEffect(pushQUD);
                        rule.addEffect(addInIntegraedMoves);

                        updateRules.Add(rule);
                    }
                    /*	//else
                        {
                            std::vector<boost::any> args;
                        //	args.push_back(conceptName);
                            Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                            boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                            boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                            rule->addEffect(pushAgenda);
                            updateRules.push_back(rule);

                        }
                        */
                } //funcot == Action-Execution-By-Team
                  //...............................................................................................

            }

            //********************************************************************************
            //				end of	Check-Question
            //********************************************************************************


            //********************************************************************************
            //					WHQ-WHY
            //********************************************************************************
            if (da.getID() == "WHQ-WHY")
            {
                string funtor = (string)(da.logicalForm.Arguments[0]);

                //preprocessing of yes no questions

                if (funtor == "Action-Execution-By-Role")
                {
                    //verify whether isRole() && sufficient parameters are available
                    string roleName = (string)(da.logicalForm.Arguments[1]);
                    //	if (isRole(roleName))
                    {
                        //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                        List<object> args = new List<object>();
                        Predicate ansCheckQtn = new Predicate("ans-WHQ-WHY", args);

                        Update pushAgenda = new Update("pushAgenda",  ansCheckQtn);
                        Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                        //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                        //std::string comGround = "mutual-belief";
                        //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                        UpdateRule rule = new UpdateRule();

                        //rule->addPrecondition(condition);
                        rule.addEffect(pushAgenda);
                        //rule->addEffect(pushQUD);
                        rule.addEffect(addInIntegraedMoves);

                        updateRules.Add(rule);
                    }
                    /*	//else
                        {
                            std::vector<boost::any> args;
                        //	args.push_back(conceptName);
                            Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                            boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                            boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                            rule->addEffect(pushAgenda);
                            updateRules.push_back(rule);

                        }
                        */
                } //funcot == Action-Execution-By-Role
            }


            //********************************************************************************
            //				end of	WHQ-WHY
            //********************************************************************************


            //********************************************************************************
            //					Unknown-concept
            //********************************************************************************
            if (da.getID() == "Unknown-concept")
            {
                string funtor = (string)(da.logicalForm.Arguments[0]);

                //preprocessing of yes no questions

                {
                    //if(funtor == "Action-Execution-By-Role")
                    //verify whether isRole() && sufficient parameters are available
                    //	std::string roleName = boost::any_cast<std::string>(da->logicalForm.Arguments[1]);
                    //	if (isRole(roleName))
                    {
                        //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                        List<object> args = new List<object>();
                        //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                        //ORIGINAL LINE: Predicate ansNotUnderstood("respond-Unknown-concept", args);
                        Predicate ansNotUnderstood = new Predicate("respond-Unknown-concept", args);

                        Update pushAgenda = new Update("pushAgenda",  ansNotUnderstood);
                        Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                        //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                        //std::string comGround = "mutual-belief";
                        //boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                        UpdateRule rule = new UpdateRule();

                        //rule->addPrecondition(condition);
                        rule.addEffect(pushAgenda);
                        //rule->addEffect(pushQUD);
                        rule.addEffect(addInIntegraedMoves);

                        updateRules.Add(rule);
                    }
                    /*	//else
                        {
                            std::vector<boost::any> args;
                        //	args.push_back(conceptName);
                            Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                            boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                            boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                            rule->addEffect(pushAgenda);
                            updateRules.push_back(rule);

                        }
                        */
                } //funcot == Action-Execution-By-Role
            }


            //********************************************************************************
            //				ansNotUnderstood
            //********************************************************************************


            //********************************************************************************
            //					not-understood
            //********************************************************************************
            if (da.getID() == "Not-Understood")
            {

                //preprocessing of yes no questions

                {
                    //if(funtor == "Action-Execution-By-Role")
                    //verify whether isRole() && sufficient parameters are available
                    //	std::string roleName = boost::any_cast<std::string>(da->logicalForm.Arguments[1]);
                    //	if (isRole(roleName))
                    {
                        //	boost::shared_ptr<Condition>  condition = boost::shared_ptr<Condition>(new Condition("isNotIntegratedMove", da->logicalForm));

                        List<object> args = new List<object>();
                        Predicate ansNotUnderstood = new Predicate("respond-Not-Understood", args);

                        Update pushAgenda = new Update("pushAgenda",  ansNotUnderstood);
                        Update addInIntegraedMoves = new Update("addInIntegraedMoves",  da.logicalForm);
                        //boost::shared_ptr<Update> pushQUD =  boost::shared_ptr<Update>(new Update("pushQUD",da->logicalForm));


                        //	//std::string comGround = "mutual-belief";
                        //	//boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("addCommonGround",comGround));

                        UpdateRule rule = new UpdateRule();

                        //rule->addPrecondition(condition);
                        rule.addEffect(pushAgenda);
                        //rule->addEffect(pushQUD);
                        rule.addEffect(addInIntegraedMoves);

                        updateRules.Add(rule);
                    }
                    /*	//else
                        {
                            std::vector<boost::any> args;
                        //	args.push_back(conceptName);
                            Predicate ansConceptNotKnown("respond-Unknown-concept", args);

                            boost::shared_ptr<Update> pushAgenda =  boost::shared_ptr<Update>(new Update("pushAgenda",ansConceptNotKnown));
                            boost::shared_ptr<UpdateRule> rule =boost::shared_ptr<UpdateRule> (new UpdateRule()) ;
                            rule->addEffect(pushAgenda);
                            updateRules.push_back(rule);

                        }
                        */
                } //funcot == Action-Execution-By-Role
            }


            //********************************************************************************



            return updateRules;
        }

    }

}

