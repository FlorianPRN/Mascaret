using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{

    public class AgentDialogueActRuleFactory
    {
        protected static AgentDialogueActRuleFactory _instance = null;

        protected DialogueAct dialogueAct = null;
        protected InformationState infoState = null;
        public static AgentDialogueActRuleFactory getInstance()
        {
            if (_instance == null)
            {
                _instance = new AgentDialogueActRuleFactory();
            }
            return _instance;
        }
        public List<UpdateRule> createRules(InformationState IS)
        {
            List<UpdateRule> factoryRules = new List<UpdateRule>();
            Property p;
            p = IS.getPropertyValueOfPath(DefineConstants.agentDialogueActs);

            List<object> systemDialogueActs = p.DataVector;
            //	cerr << " ADARF : number of sda " << systemDialogueActs->size()<< std::endl;
            foreach (object sda in systemDialogueActs)
            {
                DialogueAct da = (DialogueAct)(sda);
                //rule to accomodate system greet in response to user greet
                /*		try
                       {
                           std::vector<boost::any> args;
                           args.push_back((da->getSender()) );
                           args.push_back(da->getAddressee());


                       //predicate greet( system, user)
                           Predicate systemGreet("Greet-open", args);
                      //preconditions
                           //boost::shared_ptr<Precondition>  notGreeted = boost::shared_ptr<Precondition>(new Precondition("isNotIntegratedMove", systemGreet));
                           boost::shared_ptr<Precondition>  firstOnAgenda = boost::shared_ptr<Precondition>(new Precondition("firstOnAgenda", systemGreet));

                       //effects
                           boost::shared_ptr<Update> addNextMove =  boost::shared_ptr<Update>(new Update("addNextMove", systemGreet));

                           //  downdate agenda
                           boost::shared_ptr<Update> popAgenda =  boost::shared_ptr<Update>(new Update("popAgenda"));

                           boost::shared_ptr<Update> addInIntegraedMoves =  boost::shared_ptr<Update>(new Update("addInIntegraedMoves",systemGreet));

                           std::string grt = "Greet-open";
                           boost::shared_ptr<Update> socialContext =  boost::shared_ptr<Update>(new Update("commPressure",grt));


                           boost::shared_ptr<UpdateRule> rule  = boost::shared_ptr<UpdateRule>(new UpdateRule() );

                       //	rule->addPrecondition(notGreeted);
                           rule->addPrecondition(firstOnAgenda);

                           rule->addEffect(addNextMove);
                           rule->addEffect(popAgenda);
                           rule->addEffect(addInIntegraedMoves);
                           rule->addEffect(socialContext);

                           factoryRules.push_back(rule);

                               std::vector<boost::any> args2;
                               Predicate exp("Greet-open",args2);
                               boost::shared_ptr<Update> expected =  boost::shared_ptr<Update>(new Update("addExpected",exp));
                               rule->addEffect(expected);

                       }
                       catch(boost::exception &e)
                       {
                       }

                */


                try
                {
                    //preconditions
                    Precondition firstOnAgenda = new Precondition("firstOnAgenda", da.logicalForm);
                    //effects
                    Update addNextMove = new Update("addNextMove", da.logicalForm);
                    Update popAgenda = new Update("popAgenda");
                    Update addInIntegraedMoves = new Update("addInIntegraedMoves", da.logicalForm);
                    //update rule
                    UpdateRule rule = new UpdateRule();

                    //	rule->addPrecondition(notAsked);
                    rule.addPrecondition(firstOnAgenda);
                    rule.addEffect(addNextMove);
                    rule.addEffect(popAgenda);
                    rule.addEffect(addInIntegraedMoves);

                    if (da.getID() == "Self-Introduction")
                    {
                        List<object> args2 = new List<object>();
                        string addressee = da.getAddressee();
                        args2.Add(addressee);
                        Predicate exp = new Predicate("Self-Introduction", args2);
                        Update expected = new Update("addExpected", exp);
                        rule.addEffect(expected);
                    }
                    if (da.getID() == "Commissive-Offer")
                    {
                        List<object> args2 = new List<object>();
                        string addressee = da.getAddressee();
                        args2.Add(addressee);
                        Predicate exp = new Predicate("YES-NO-Answer", args2);
                        Update expected = new Update("addExpected",   exp);
                        rule.addEffect(expected);
                    }
                    else if (da.getID() == "Information-Seeking-Function")
                    {
                        List<object> args2 = new List<object>();
                        string addressee = da.getAddressee();
                        args2.Add(addressee);
                        string predicateName = null;
                        if (da.logicalForm.Functor == "WHQ-WHICH-Resource-Choice")
                        {
                            predicateName = "Resource-Choice";
                            object resourceClass = da.logicalForm.Arguments[1];
                            //	args2.push_back(resourceClass);
                        }



                        if(predicateName != null)
                        {
                            Predicate exp = new Predicate(predicateName, args2);
                            Update expected = new Update("addExpected", exp);
                            rule.addEffect(expected);
                        }
                       
                    }
                    else if (da.getID() == "Request-Action-Choice")
                    {
                        List<object> args = new List<object>();
                        string predicateName = "Collective-Obligation";

                         Predicate exp = new Predicate(predicateName, args);
                        Update expected = new Update("addExpected",   exp);
                        //rule->addEffect(expected);
                    }

                    else if (da.getID() == "Ask")
                    {
                        List<object> args2 = new List<object>();
                        Predicate exp = new Predicate("Inform", args2);
                        Update expected = new Update("addExpected",   exp);
                        rule.addEffect(expected);
                    }

                    else if (da.getID() == "Initial-Greet")
                    {
                        List<object> args = new List<object>();
                        args.Add(da.getAddressee());
                        string self = "Self";
                        args.Add(self);
                        List<object> args2 = new List<object>();
                        Predicate exp = new Predicate("Greet-open", args);
                        Update expected = new Update("addExpected", exp);
                        rule.addEffect(expected);
                    }

                    factoryRules.Add(rule);
                }
                catch (Exception)
                {
                }

            }



            return factoryRules;

        }
        public List<UpdateRule> createRules(DialogueAct da, InformationState IS)
        {
            List<UpdateRule> factoryRules = new List<UpdateRule>();


            try
            {
                //preconditions
                Precondition firstOnAgenda = new Precondition("firstOnAgenda", da.logicalForm);
                //effects
                Update addNextMove = new Update("addNextMove", da.logicalForm);
                Update popAgenda = new Update("popAgenda");
                Update addInIntegraedMoves = new Update("addInIntegraedMoves", da.logicalForm);
                //update rule
                UpdateRule rule = new UpdateRule();

                //	rule->addPrecondition(notAsked);
                rule.addPrecondition(firstOnAgenda);
                rule.addEffect(addNextMove);
                rule.addEffect(popAgenda);
                rule.addEffect(addInIntegraedMoves);

                if (da.getID() == "Self-Introduction")
                {
                    List<object> args2 = new List<object>();
                    string addressee = da.getAddressee();
                    args2.Add(addressee);
                   Predicate exp = new Predicate("Self-Introduction", args2);
                    Update expected = new Update("addExpected", exp);
                    rule.addEffect(expected);
                }
                if (da.getID() == "Commissive-Offer")
                {
                    List<object> args2 = new List<object>();
                    string addressee = da.getAddressee();
                    args2.Add(addressee);
                    Predicate exp = new Predicate("YES-NO-Answer", args2);
                   Update expected = new Update("addExpected", exp);
                    rule.addEffect(expected);
                }
                else if (da.getID() == "Information-Seeking-Function")
                {
                    Update expected = null;
                    string predicateName;
                    if (da.logicalForm.Functor== "WHQ-WHICH-Resource-Choice")
                    {
                        List<object> args2 = new List<object>();
                        string addressee = da.getAddressee();
                        args2.Add(addressee);
                        predicateName = "Resource-Choice";
                        Predicate exp = new Predicate(predicateName, args2);
                        expected = new Update("addExpected", exp);
                    }
                    else if (da.getCommunicativeFunction() == "Request-Action-Choice")
                    {
                        List<object> args = new List<object>();
                        predicateName = "Collective-Obligation";
                        Predicate exp = new Predicate(predicateName, args);
                        expected = new Update("addExpected", exp);
                    }

                    // adding the effect if it is not null
                    if(expected!= null)
                    {
                        rule.addEffect(expected);
                    }
                    
                }

                else if (da.getID() == "Ask")
                {
                    List<object> args2 = new List<object>();
                    Predicate exp = new Predicate("Inform", args2);
                    Update expected = new Update("addExpected", exp);
                    rule.addEffect(expected);
                }

                else if (da.getID() == "Initial-Greet")
                {
                    List<object> args = new List<object>();
                    args.Add(da.getAddressee());
                    string self = "Self";
                    args.Add(self);
                    List<object> args2 = new List<object>();
                    Predicate exp = new Predicate("Greet-open", args);
                    Update expected = new Update("addExpected", exp);
                    rule.addEffect(expected);
                }

                factoryRules.Add(rule);
            }
            catch (Exception)
            {
            }





            return factoryRules;
        }


        public virtual void Dispose()
        {
        }


    }


}


