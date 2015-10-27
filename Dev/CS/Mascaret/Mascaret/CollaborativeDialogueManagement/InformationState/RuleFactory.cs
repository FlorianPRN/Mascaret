using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 


namespace DM
{

    public class RuleFactory
    {
        public static RuleFactory getInstance()
        {
            if (_instance == null)
            {
                _instance = new RuleFactory();
            }
            return _instance;
        }
        //	RuleFactory(void);
        //	RuleFactory(boost::shared_ptr<DialogueAct> da, boost::shared_ptr<InformationState> is );

        /*
        RuleFactory::RuleFactory(void)
        {

        }
        RuleFactory::RuleFactory(boost::shared_ptr<DialogueAct> da, boost::shared_ptr<InformationState> is )
        {
            dialogueAct = da;
            infoState =  is;

        }
        */

        public virtual void Dispose()
        {
        }
        /*
         std::vector<boost::shared_ptr<UpdateRule> > RuleFactory::getFactoryRules()
        {
           return factoryRules;
        }
        */
        public List<UpdateRule> createRules(DialogueAct da, InformationState IS)
        {
            List<UpdateRule> factoryRules = new List<UpdateRule>();
            //rule to accomodate system greet in response to user greet
            try
            {
                List<object> args = new List<object>();
                string addressee = "Self";
                //			args.push_back(da->getAddressee());
                args.Add(addressee);
                args.Add(da.getSender());
                Predicate systemGreet = new Predicate("Greet-open", args);
                //preconditions
                Precondition notGreeted = new Precondition("isNotIntegratedMove",  systemGreet);
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  systemGreet);
                //effects
                Update addNextMove = new Update("addNextMove",  systemGreet);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  systemGreet);
                Update clearSocialContext = new Update("clearSocialContext");
                //SemanticPredicate mutualBel("MutualBelief","","",systemGreet);
                Update belief = new Update("addBelief",  systemGreet);
                UpdateRule rule = new UpdateRule();
                rule.addPrecondition(notGreeted);
                rule.addPrecondition(firstOnAgenda);
                //	rule->addEffect(belief);
                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);
                rule.addEffect(clearSocialContext);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }

            try
            {
                List<object> args = new List<object>();
                //			args.push_back(da->getAddressee());
                //			args.push_back(da->getSender());


                //predicate greet( system, user)
                Predicate systemGreet = new Predicate("Greet-close", args);
                //preconditions
                //boost::shared_ptr<Precondition>  notGreeted = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", systemGreet));
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  systemGreet);

                //effects
                Update addNextMove = new Update("addNextMove",  systemGreet);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  systemGreet);
                Update clearSocialContext = new Update("clearSocialContext");

                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notGreeted);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);
                rule.addEffect(clearSocialContext);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }

            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatConcept = new Predicate("ans-WHQ-WHAT-Concept", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatConcept);


                //effects
                Predicate awc = new Predicate("ans-WHQ-WHAT-Concept", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  awc);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  awc);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //accomodate system response to the user what-concept  question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
               Predicate ansActionChoice = new Predicate("Reply-Action-Choice", args);
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansActionChoice);

                //effects
                Update addNextMove = new Update("addNextMove",  ansActionChoice);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansActionChoice);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);
                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }

            //accomodate system response to the user what-instance-state  question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansEntityState = new Predicate("ans-WHQ-WHAT-Entity-State", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansEntityState);


                //effects
                Predicate ansInstanceState = new Predicate("ans-WHQ-WHAT-Entity-State", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansInstanceState);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansInstanceState);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //........................................................................................
            //accomodate system response to the user what-concept-feature  question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatAllOperations = new Predicate("ans-WHQ-WHAT-All-Operations", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatAllOperations);


                //effects
                Predicate ansAllOperations = new Predicate("ans-WHQ-WHAT-All-Operations", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansAllOperations);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansAllOperations);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //accomodate system response to the user what-all-attribute  question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatAllAttributes = new Predicate("ans-WHQ-WHAT-All-Attributes", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatAllAttributes);


                //effects
                Predicate ansAllOperations = new Predicate("ans-WHQ-WHAT-All-Attributes", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansAllOperations);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansAllOperations);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //accomodate system response to the user what-concept-attribute  question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatConceptAttributes = new Predicate("ans-WHQ-WHAT-Concept-Attribute-Value", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatConceptAttributes);


                //effects
                Predicate ansConceptAttribute = new Predicate("ans-WHQ-WHAT-Concept-Attribute-Value", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansConceptAttribute);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansConceptAttribute);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }

            //accomodate system response to the user what-concept-feature  question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatConceptFeature = new Predicate("ans-WHQ-WHAT-Concept-Feature", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatConceptFeature);


                //effects
                Predicate ansConceptFeature = new Predicate("ans-ans-WHQ-WHAT-Concept-Feature", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansConceptFeature);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansConceptFeature);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }

            //accomodate system response to the user what-next action
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatAgentNextAction = new Predicate("ans-WHQ-WHAT-Next-Action", args);
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatAgentNextAction);


                //effects
                Predicate ansNextAction = new Predicate("ans-WHQ-WHAT-Next-Action", da.logicalForm.Arguments);
               Update addNextMove = new Update("addNextMove",  ansNextAction);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansNextAction);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //accomodate system response to the user what-action decision
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatActionDecision = new Predicate("ans-WHQ-WHAT-Action-Decision", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatActionDecision);


                //effects
                Predicate ansActionDecision = new Predicate("ans-WHQ-WHAT-Action-Decision", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansActionDecision);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansActionDecision);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //accomodate system response to the user what-next action
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatAgentCurrentAction = new Predicate("ans-WHQ-WHAT-Current-Action", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatAgentCurrentAction);


                //effects
                Predicate ansCurrentAction = new Predicate("ans-WHQ-WHAT-Current-Action", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansCurrentAction);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansCurrentAction);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }


            //accomodate system response to the user what-last action
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatAgentLastAction = new Predicate("ans-WHQ-WHAT-Last-Action", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatAgentLastAction);


                //effects
                Predicate ansLastAction = new Predicate("ans-WHQ-WHAT-Last-Action", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansLastAction);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansLastAction);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //accomodate system response to the user what-last action
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatActionRes = new Predicate("ans-WHQ-WHAT-Action-Resource", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatActionRes);


                //effects
                Predicate ansActionMeanRes = new Predicate("ans-WHQ-WHAT-Action-Resource", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansActionMeanRes);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansActionMeanRes);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //accomodate system response to the user what-current activity
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatAgentActivity = new Predicate("ans-WHQ-WHAT-Current-Activity", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatAgentActivity);


                //effects
                Predicate ansAgentActivity = new Predicate("ans-WHQ-WHAT-Current-Activity", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansAgentActivity);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansAgentActivity);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }

            //accomodate system response to the user what-agent role
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatAgentRole = new Predicate("ans-WHQ-WHAT-Agent-Role", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatAgentRole);


                //effects
                Predicate ansAgentRole = new Predicate("ans-WHQ-WHAT-Agent-Role", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansAgentRole);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansAgentRole);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //...........................................................
            //accomodate system response to the user what is role next action
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatRoleNextAction = new Predicate("ans-WHQ-WHAT-Role-Next-Action", args);
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatRoleNextAction);


                //effects
                Predicate ansRoleNextAction = new Predicate("ans-WHQ-WHAT-Role-Next-Action", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansRoleNextAction);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansRoleNextAction);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }


            //...................................................................................................................
            //accomodate system response to the user: what is team next action
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatTeamNextAction = new Predicate("ans-WHQ-WHAT-Team-Next-Action", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatTeamNextAction);


                //effects
                Predicate ansTeamNextAction = new Predicate("ans-WHQ-WHAT-Team-Next-Action", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansTeamNextAction);
                Update popAgenda = new Update("popAgenda");
               Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansTeamNextAction);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }


            //...................................................................................................................
            //accomodate system response to the user what is role current action
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhatRoleCurrentAction = new Predicate("ans-WHQ-WHAT-Role-current-Action", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhatRoleCurrentAction);


                //effects
                Predicate ansRoleCurrentAction = new Predicate("ans-WHQ-WHAT-Role-Current-Action", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansRoleCurrentAction);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansRoleCurrentAction);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }


            //================================================================================

            //********************************************************************************
            //					WHQ-WHO   (present , past , future ) action
            //********************************************************************************
            //================================================================================
            //accomodate system response to the user who will do  action
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhoWillDoAction = new Predicate("ans-WHQ-WHO-Will-Do-Action", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhoWillDoAction);


                //effects
                Predicate ansWhoWillAction = new Predicate("ans-WHQ-WHO-Will-Do-Action", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansWhoWillAction);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansWhoWillAction);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }
            //================================================================================

            //********************************************************************************
            //accomodate system response to the ans-WHQ-WHO question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansWhoQuestion = new Predicate("ans-WHQ-WHO", args);
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhoQuestion);


                //effects
                Predicate ansWhoQtn = new Predicate("ans-WHQ-WHO", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansWhoQtn);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansWhoQtn);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }



            //********************************************************************************

            //********************************************************************************
            //			end of 		WHQ-WHO   (present , past , future ) action
            //********************************************************************************




            //yes no question
            //accomodate system response to the user yes no question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansYesNoQtn = new Predicate("ans-YES-NO-Question", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansYesNoQtn);


                //effects
                Predicate ansYesNo = new Predicate("ans-YES-NO-Question", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansYesNo);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansYesNo);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }

            //********************************************************************************
            //			Check Questions
            //********************************************************************************
            //accomodate system response to the check question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate ansCheckQuestion = new Predicate("ans-Check-Question", args);
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansCheckQuestion);


                //effects
                Predicate ansYesNo = new Predicate("ans-Check-Question", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansYesNo);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansYesNo);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }



            //********************************************************************************
            //			end of 		Check Questions
            //********************************************************************************

            //********************************************************************************
            //			WHQ-WHY Questions
            //********************************************************************************
            //accomodate system response to the whq-why question
            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
               Predicate ansWhyQuestion = new Predicate("ans-WHQ-WHY", args);
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansWhyQuestion);


                //effects
                Predicate ansWHYQtn = new Predicate("ans-WHQ-WHY", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansWHYQtn);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  ansWHYQtn);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }



            //********************************************************************************
            //			end of 		WHQ-WHY Questions
            //********************************************************************************




            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate inform = new Predicate("Inform", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  inform);

                //effects
                Predicate pre = new Predicate("Inform", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  pre);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  pre);
                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }


            /*
                   try {
                  //preconditions
               //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                       std::vector<boost::any> args;
                       Predicate ask("Ask", args);

                       boost::shared_ptr<Precondition>  firstOnAgenda = boost::shared_ptr<Precondition>(new Precondition("firstOnAgenda", ask));

                   //effects
                       Predicate pre("Ask", da->logicalForm.Arguments);
                       boost::shared_ptr<Update> addNextMove =  boost::shared_ptr<Update>(new Update("addNextMove",pre));
                       boost::shared_ptr<Update> popAgenda =  boost::shared_ptr<Update>(new Update("popAgenda"));
                       boost::shared_ptr<Update> addInIntegraedMoves =  boost::shared_ptr<Update>(new Update("addInIntegraedMoves",pre));
                       std::vector<boost::any> args2;
                       Predicate exp("Inform",args2);
                       boost::shared_ptr<Update> expected =  boost::shared_ptr<Update>(new Update("addExpected",exp));
                   //effects
                       boost::shared_ptr<UpdateRule> rule  = boost::shared_ptr<UpdateRule>(new UpdateRule() );

                   //	rule->addPrecondition(notAsked);
                       rule->addPrecondition(firstOnAgenda);
                       rule->addEffect(addNextMove);
                       rule->addEffect(expected);
                       rule->addEffect(popAgenda);
                       rule->addEffect(addInIntegraedMoves);

                       factoryRules.push_back(rule);
                   }
                   catch(boost::exception & e)
                   {

                   }


               try{
                       std::vector<boost::any> args;
                       args.push_back(da->logicalForm.Arguments[0]);
                   //predicate greet( system, user)
                       Predicate iGreet("Initial-Greet", args);
                  //preconditions
                       //boost::shared_ptr<Precondition>  notGreeted = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", systemGreet));
                       boost::shared_ptr<Precondition>  firstOnAgenda = boost::shared_ptr<Precondition>(new Precondition("firstOnAgenda", iGreet));
                   //effects
                       boost::shared_ptr<Update> addNextMove =  boost::shared_ptr<Update>(new Update("addNextMove", iGreet));
                       boost::shared_ptr<Update> popAgenda =  boost::shared_ptr<Update>(new Update("popAgenda"));
                       boost::shared_ptr<Update> addInIntegraedMoves =  boost::shared_ptr<Update>(new Update("addInIntegraedMoves",iGreet));
                       boost::shared_ptr<Update> clearSocialContext =  boost::shared_ptr<Update>(new Update("clearSocialContext"));

                       SemanticPredicate bel("Bel","Self",iGreet);
                       boost::shared_ptr<Update> belief =  boost::shared_ptr<Update>(new Update("addBelief", bel));
                       boost::shared_ptr<UpdateRule> rule  = boost::shared_ptr<UpdateRule>(new UpdateRule() );

                   //	rule->addPrecondition(notGreeted);
                       rule->addPrecondition(firstOnAgenda);

                       rule->addEffect(belief);
                       rule->addEffect(addNextMove);
                       rule->addEffect(popAgenda);
                       rule->addEffect(addInIntegraedMoves);
                       rule->addEffect(clearSocialContext);
                       factoryRules.push_back(rule);
                   }
               catch(boost::exception &e)
                   {
                   }
            */
            try
            {
                List<object> args = new List<object>();
                string addressee = "Self";
                //args.push_back(da->getAddressee());
                args.Add(addressee);
                args.Add(da.getSender());
                Predicate greet = new Predicate("Greet-open", args);
                //predicate greet( system, user)
                Predicate systemGreet = new Predicate("reply-Greet-open", args);
                //preconditions
                Precondition notGreeted = new Precondition("isNotIntegratedMove",  greet);
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  systemGreet);
                //effects
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves",  greet);
                Update clearSocialContext = new Update("clearSocialContext");
                //	SemanticPredicate mutualBel("MutualBelief","","",systemGreet);
                //			boost::shared_ptr<Update> belief =  boost::shared_ptr<Update>(new Update("addBelief", systemGreet));

                UpdateRule rule = new UpdateRule();
                rule.addPrecondition(notGreeted);
                rule.addPrecondition(firstOnAgenda);
                //		rule->addEffect(belief);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);
                rule.addEffect(clearSocialContext);


                Update addNextMove = new Update("addNextMove",  systemGreet);
                rule.addEffect(addNextMove);



                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }



            try
            {
                //preconditions
                //		boost::shared_ptr<Precondition>  notAsked = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", da->logicalForm));
                List<object> args = new List<object>();
                Predicate init_intro = new Predicate("Initial-Introduction", args);

                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  init_intro);

                //effects
                Predicate pre = new Predicate("Initial-Introduction", da.logicalForm.Arguments);
                Update popAgenda = new Update("popAgenda");
                //boost::shared_ptr<Update> addInIntegraedMoves =  boost::shared_ptr<Update>(new Update("addInIntegraedMoves",pre));

                List<object> args2 = new List<object>();

                //std::string addressee = "Self";
                //args2.push_back(addressee);
                args.Add(da.getAddressee());
                args2.Add(da.getSender());
                //predicate greet( system, user)
                Predicate initIntro = new Predicate("Initial-Introduction", args);
                Update belief = new Update("addBelief",  initIntro);
                Update addNextMove = new Update("addNextMove",  initIntro);
                Update addInInteg = new Update("addInIntegraedMoves",  initIntro);

                //effects
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);
                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(belief);
                rule.addEffect(addInInteg);

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }



            //===========================================================
            //accomodate system reply for concept not known
            try
            {
                List<object> args = new List<object>();
                //	args.push_back(conceptName);
                Predicate ansConceptNotKnown = new Predicate("respond-Unknown-concept", args);
                //preconditions
               Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansConceptNotKnown);

                //effects
                Predicate ansCNKnown = new Predicate("respond-Unknown-concept", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansCNKnown);
                Update popAgenda = new Update("popAgenda");

                //make rule
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                //add rule to rule factory
                factoryRules.Add(rule);
            }
            catch (Exception)
            {

            }


            //===========================================================
            //accomodate system reply for utterance not understood
            try
            {
                List<object> args = new List<object>();
                //	args.push_back(conceptName);
                Predicate ansNotUnderstood = new Predicate("respond-Not-Understood", args);
                //preconditions
                Precondition firstOnAgenda = new Precondition("firstOnAgenda",  ansNotUnderstood);

                //effects
                Predicate ansNotUnderstanding = new Predicate("respond-Not-Understood", da.logicalForm.Arguments);
                Update addNextMove = new Update("addNextMove",  ansNotUnderstanding);
                Update popAgenda = new Update("popAgenda");

                //make rule
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);

                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                //add rule to rule factory
                factoryRules.Add(rule);
            }
            catch (Exception)
            {

            }



            //factoryRules.push_back(rule);

            return factoryRules;
        }
        //	 std::vector<boost::shared_ptr<UpdateRule> > getFactoryRules();
        //	 std::vector<boost::shared_ptr<UpdateRule> >  factoryRules;

        protected static RuleFactory _instance = null;

        protected DialogueAct dialogueAct;
        protected InformationState infoState;

    }


} //namespace




