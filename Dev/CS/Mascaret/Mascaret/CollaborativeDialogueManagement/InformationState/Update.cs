using System;
using System.Threading;

using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public class Update : Effect
    {
        private object syncLock;
        private string modifier= null;

        public Update() : base()
        {
            syncLock = new object();
        }
        public Update(string path, object value) : base(path, value)
        {
            syncLock = new object();
            if (path == "pop")
            {
                try
                {
                    modifier = "pop";
                    this.path = (string)(value);
                }
                catch (InvalidCastException)
                {

                }
            }
            else if (path == "pushAgenda")
            {
                try
                {
                    modifier = "pushAgenda";
                    this.path = DefineConstants.agenda;
                    this.value =(Predicate)(value);
                }
                catch (InvalidCastException)
                {
                   
                }
            }
            else if (path == "pushQUD")
            {
                try
                {
                    modifier = "pushQUD";
                    this.path = DefineConstants.qud;
                    this.value = (Predicate)(value);
                }
                catch (InvalidCastException)
                {
                  
                }
            }
            else if (path == "addInIntegraedMoves")
            {
                try
                {
                    modifier = "addInIntegraedMoves";
                    this.path = DefineConstants.integratedMoves;
                    this.value = (Predicate)(value);
                }
                catch (InvalidCastException)
                {
                    
                }
            }
            else if (path == "commPressure")
            {
                try
                {
                    modifier = "commPressure";
                    this.path = DefineConstants.commPressure;
                    this.value = (string)(value);
                }
                catch (InvalidCastException)
                {
                   
                }
            }
            else if (path == "addNextMove")
            {
                try
                {
                    modifier = "addNextMove";
                    this.path = DefineConstants.nextMoves;
                    this.value = (Predicate)(value);
                }
                catch (InvalidCastException)
                {
                  
                }
            }
            else if (path == "addBelief")
            {
                modifier = "addBelief";
                path = DefineConstants.semanticBeliefs;
                bool flag = true;
                try
                {
                    this.value = (SemanticPredicate)(value);
                    flag = false;

                }
                catch (InvalidCastException)
                {
                   
                    flag = true;
                }
                if (flag)
                {
                    try
                    {
                        Predicate p = (Predicate)(value);
                        this.value = p;
                      
                    }
                    catch (InvalidCastException)
                    {
                      
                    }
                }
            }
            else if (path == "addCommonGround")
            {
                try
                {
                    modifier = "addCommonGround";
                    
                    path = DefineConstants.commonGround;
                    this.value = (MutualBelief)(value);
                }
                catch (InvalidCastException)
                {
                    
                }
            }
            else if (path == "addExpected")
            {
                try
                {
                    modifier = "addExpected";
                    path = DefineConstants.expectedPredicate;
                    this.value = (Predicate)(value);
                }
                catch (InvalidCastException)
                {
                   
                }
            }
            else if (path == "addAgentDialogueAct")
            {
                try
                {
                    modifier = "addAgentDialogueAct";
                    path = DefineConstants.agentDialogueActs;
                    this.value = (DialogueAct)(value);
                }
                catch (InvalidCastException)
                {
                   
                }
            }
        }
        //	Update(boost::shared_ptr<Predicate) p);
        public Update(string tag, string path, object value) : base(path, value)
        {
            syncLock = new object();
            modifier = tag;
        }
        /*
        Update::Update(boost::shared_ptr<Predicate) p) : Effect()
        {
            predicate = p;
        }*/
        public Update(string tag) : base()
        {
            syncLock = new object();
            modifier = tag;
        }
        //	Update::Update(string tag, int index);
        public new void Dispose()
        {
            base.Dispose();
        }
        /*bool Update::isPredicate()
        {
            return isAPredicate;
        }
        void Update::setIsPredicate(bool flag)
        {
            isAPredicate =  flag;
        }*/
        public override void apply(ref InformationState IS)
        {
            //steps::
            // get the property of current path to be updated
            //	check the type of the path property
            // give treatment for assert based on the type of the property element
            // if list then pushback
            //if vector then pushback
            //if queue then push
            //if stack then push
            //if(path.ElementAt(0) == '$')
            //{
            //	boost::shared_ptr<Property) p =  is-)getPropertyValueOfPath(path);
            //std::string type  = p-)type();
            //queue
            if (modifier == "push")
            {
                try
                {
                    Property p = IS.getPropertyValueOfPath(path);
                    p.push(value);
                }
                catch (InvalidCastException)
                {
                    Console.Write(" push:  update operation out of range: ");
                    Console.Write("\n");
                }
            }

            else if (modifier == "pop")
            {
                try
                {
                    Property p = IS.getPropertyValueOfPath(path);

                    p.pop();
                }
                catch (InvalidCastException)
                {
                    Console.Write(" pop:  update operation out of range: ");
                    Console.Write("\n");
                }

            }
            else if (modifier == "set")
            {
                try
                {
                    Property p = IS.getPropertyValueOfPath(path);
                    p.setValue(value);
                }
                catch (InvalidCastException)
                {
                    Console.Write(" set:  update operation out of range: ");
                    Console.Write("\n");
                }

            }

            else if (string.IsNullOrEmpty(modifier))
            {
                 try
                {
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.setValue(value);
                    }
                   
                }
                catch (InvalidCastException)
                {
                    Console.Write(" set:  update operation out of range: ");
                    Console.Write("\n");
                }

            }
            else if (modifier == "pushAgenda")
            {
                try
                {
                    this.path = DefineConstants.agenda;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property agenda ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.push(this.value);
                    }
                }
                catch (InvalidCastException)
                {
                    Console.Write(" pushAgenda:  update operation out of range: ");
                    Console.Write("\n");
                }

            }
            else if (modifier == "pushQUD")
            {
                try
                {
                    this.path = DefineConstants.qud;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property pushQUD ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.push(this.value);
                    }
                   
                }
                catch (InvalidCastException)
                {
                    Console.Write(" pushQUD:  update operation out of range: ");
                    Console.Write("\n");
                }

            }
            else if (modifier == "addInIntegraedMoves")
            {
                try
                {
                    this.path = DefineConstants.integratedMoves;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property addIntegrated Move ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.push(this.value);
                    }
                    
                }
                catch (InvalidCastException)
                {
                    Console.Write(" addInIntegraedMoves:  update operation out of range: ");
                    Console.Write("\n");
                }
            }
            else if (modifier == "addNextMove")
            {
                try
                {
                    this.path = DefineConstants.nextMoves;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property addIntegrated Move ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.push(this.value);
                    }
                   
                }
                catch (InvalidCastException)
                {
                    Console.Write(" addNextMove:  update operation out of range: ");
                    Console.Write("\n");
                }

            }

            else if (modifier == "addBelief")
            {
                try
                {
                    this.path = DefineConstants.semanticBeliefs;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property Belief ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.push(this.value);
                    }
                   
                }
                catch (InvalidCastException)
                {
                    Console.Write(" addBelief:  update operation out of range: ");
                    Console.Write("\n");
                }
            }


            else if (modifier == "addExpected")
            {
                try
                {
                    this.path = DefineConstants.expectedPredicate;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property addExpected ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.push(this.value);
                    }
                  
                }
                catch (InvalidCastException)
                {
                    Console.Write(" addExpected:  update operation out of range: ");
                    Console.Write("\n");
                }
            }

            else if (modifier == "addAgentDialogueAct")
            {
                try
                {
                    this.path = DefineConstants.agentDialogueActs;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property addAgentDialogueAct ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.push(this.value);
                    }
                   
                }
                catch (InvalidCastException)
                {
                    Console.Write(" addAgentDialogueAct:  update operation out of range: ");
                    Console.Write("\n");
                }

            }



            else if (modifier == "popAgenda")
            {
                try
                {
                    path = DefineConstants.agenda;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property popAgenda ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.pop();
                    }
                   
                }
                catch (InvalidCastException)
                {
                    Console.Write(" popAgenda:  update operation out of range: ");
                    Console.Write("\n");
                }
            }
            else if (modifier == "popExpected")
            {
                try
                {
                    path = DefineConstants.expectedPredicate;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property popExpected ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.pop();
                    }
                  
                    
                }
                catch (InvalidCastException)
                {
                    Console.Write(" popExpected:  update operation out of range: ");
                    Console.Write("\n");
                }
            }
            else if (modifier == "clearAgenda")
            {
                path = DefineConstants.agenda; // "$IS.semanticContext.shared.agenda";
                Property p = IS.getPropertyValueOfPath(path);
                if (p == null)
                {
                    Console.Write("unable to access property popAgenda ");
                    Console.Write("\n");
                }
                else
                {
                    p.clear();
                }

            }
            else if (modifier == "popQUD")
            {
                try
                {
                    path = DefineConstants.qud;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property popQUD ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.pop();
                    }
                }
                catch (InvalidCastException)
                {
                    Console.Write(" popQUD:  update operation out of range: ");
                    Console.Write("\n");
                }
            }

            else if (modifier == "addCommonGround")
            {
                try
                {
                    path = DefineConstants.commonGround;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property addCommonGround ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.push(this.value);
                    }
                    
                }
                catch (InvalidCastException)
                {
                    Console.Write(" addCommonGround:  update operation out of range: ");
                    Console.Write("\n");
                }

            }

            else if (modifier == "clearCommonGround")
            {
                try
                {
                    path = DefineConstants.commonGround;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property addCommonGround ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.clear();
                    }
                    
                }
                catch (InvalidCastException)
                {
                    Console.Write(" clearCommonGround:  update operation out of range: ");
                    Console.Write("\n");
                }
            }
            else if (modifier == "removeCommonGround")
            {

            }
            else if (modifier == "clearSocialContext")
            {
                try
                {
                    this.path = DefineConstants.commPressure;
                    Property p = IS.getPropertyValueOfPath(path);
                    if (p == null)
                    {
                        Console.Write("unable to access property clearSocialContext ");
                        Console.Write("\n");
                    }
                    else
                    {
                        p.clear();
                    }
                }
                catch (InvalidCastException)
                {
                    Console.Write(" clearSocialContext:  update operation out of range: ");
                    Console.Write("\n");
                }

            }

        }

       


      
    }

   


} //namespace

