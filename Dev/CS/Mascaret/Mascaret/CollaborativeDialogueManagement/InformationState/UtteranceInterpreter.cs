using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mascaret;
namespace DM
{

    //C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
    //class UtteranceMessage;
    //C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
    //class dialgueContext;
    //C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
    //class ConversationalAgent;
    public class UtteranceInterpreter
    {
        private string message;
        private UtteranceMessage utterance;
        private VirtualHuman hostAgent;

        public UtteranceInterpreter()
        {
        }
        public UtteranceInterpreter(VirtualHuman host)
        {
            hostAgent = host;
        }
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	UtteranceInterpreter(string message);
        public UtteranceInterpreter(UtteranceMessage utterance)
        {
            this.utterance = utterance;
        }
        public UtteranceInterpreter(VirtualHuman host, UtteranceMessage utterance)
        {
            this.utterance = utterance;
            hostAgent = host;
        }
        public void Dispose()
        {
        }
        //	boost::shared_ptr<DialogueAct> constructDialogueAct();
        public DialogueAct constructDialogueAct(UtteranceMessage utterance)
        {
            DialogueAct da = new DialogueAct();



            //std::string utternaceFrame = "WHQ-WHAT-Concept car";
            string utternaceFrame = utterance.UtteranceSemanticForm;
            //	boost::algorithm::to_lower(utternaceFrame);


          //  List<string> vectorString = new List<string>();
            // converting string into lowercase
            string[] vectorString = utternaceFrame.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
          //  boost.algorithm.split_regex(vectorString, utternaceFrame, boost.regex("[ ]+"));
            //	copy( vectorString.begin(), vectorString.end(),  std::ostream_iterator<std::string>( std::cout, "\n" ) ) ;

            /*
            WHQuestion-WHAT
            */
            //	std::cout << " \n***********WHQ-QUESTION-WHAT*****************************\n"<< std::endl;
            string cueWord = "WHAT";
            if (vectorString[0].Contains(cueWord) && utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                Console.Write("input is a WHQ-what ");
                Console.Write("\n");



                /*
                *   WHQ-What-Concept
                *   what is a  car?
                */
                if (vectorString[0] == "WHQ-WHAT-Concept" && vectorString.Length == 2)
                {
                    Console.Write(" asking about description of concept :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Concept");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string conceptName = vectorString[1];
                    args.Add(conceptName);
                   Predicate p = new Predicate(da.id,args);
                   da.logicalForm  =p;
                }

                /*
                *   WHQ-What-Value
                *   what is the (description of) speed of car?
                * or what does calculateMilage of car do?
                * "WHQ-WHAT-Concept-Feature"  Bateau aller
                */
               else if (vectorString[0] == "WHQ-WHAT-Concept-Feature" && vectorString.Length == 3)
                {
                    Console.Write(" asking about description of the feature (attribute/operation) of concept :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Concept-Feature");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string conceptName = vectorString[1];
                    args.Add(conceptName);

                    //feature name
                    string featureName = vectorString[2];
                    args.Add(featureName);
                     Predicate p = new Predicate(da.getID(), args);
                
                    da.logicalForm = p;
                }

                /* value of the attribute of a concept/instance
                     simplest case:  when instance/object/class name and attribute name is given
                */
                else if (vectorString[0] == "WHQ-WHAT-Concept-Attribute-Value" && vectorString.Length == 3)
                {
                    Console.Write(" asking about the value  an attribute of concept :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Concept-Attribute-Value");
                    da.communicativeFunction = "WHQ-What";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string conceptName = vectorString[1];
                    args.Add(conceptName);
                    //attribute name
                    string attributeName = vectorString[2];
                    args.Add(attributeName);

                    Predicate p = new Predicate(da.id, args);
                    da.logicalForm = p;
                }

                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Value
                    eg. what is my age?  (1st person)
                    eg. what is your age?  (2nd person)
                    eg. what is the speed of it?  ( object in focus)  (third person / object)
                    eg. what is the speed of that/this car? ( referenced instance of the type car)(third person / object).

                */


                /*   WHQ-What-All-Attributes
                *   what are the attributes of a  car?
                */
                else if (vectorString[0] == "WHQ-WHAT-All-Attributes" && vectorString.Length == 2)
                {
                    Console.Write(" asking about all attributes of concept :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-All-Attributes");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string conceptName = vectorString[1];
                    args.Add(conceptName);
                    Predicate p = new Predicate(da.getID(), args);
                    da.logicalForm = p;
                }


                /*  "WHQ-WHAT-All-Operations"
                *   what are the operations of a  car?
                */
                else if (vectorString[0] == "WHQ-WHAT-All-Operations" && vectorString.Length == 2)
                {
                    Console.Write(" asking about all attributes of concept :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-All-Operations");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string conceptName = vectorString[1];
                    args.Add(conceptName);

                    Predicate p = new Predicate(da.getID(), args);
                    da.logicalForm = p;
                }

                //....................................................................................
                /*  "WHQ-WHAT-Instance-state"
                *   what are the state of door?
                */
                else if (vectorString[0] == "WHQ-WHAT-Entity-State" && vectorString.Length == 3)
                {
                    Console.Write(" asking about the state of an instance :");
                    Console.Write(vectorString[2]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Entity-State");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //concept name
                    string instanceName = vectorString[2];
                    args.Add(instanceName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //...................................................................

                /*  "WHQ-WHAT-Role"
                *   what is/are the role of an  agent?
                * An agent can have more than one roles in different activities
                */
                else if (vectorString[0] == "WHQ-WHAT-Agent-Role" && vectorString.Length == 2)
                {
                    Console.Write(" asking about role  of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Agent-Role");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string conceptName = vectorString[1];
                    args.Add(conceptName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

                /*  "WHQ-WHAT-Role"
                *   what is/are the role of an  agent in the given actity?
                * An agent can have more than one roles in different activities
                */
                else if (vectorString[0] == "WHQ-WHAT-Agent-Role" && vectorString.Length == 3)
                {
                    Console.Write(" asking about role  of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Role");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //activity name
                    string activityName = vectorString[2];
                    args.Add(activityName);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Role
                */

                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Role  in a current activity
                */

                /*  "WHQ-WHAT-Current-Action"
                *   what is the current action of an  agent?
                * (by default in a current ongoing activity )
                */
                else if (vectorString[0] == "WHQ-WHAT-Current-Action" && vectorString.Length == 2)
                {
                    Console.Write(" asking about current action of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Current-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string agentName = vectorString[1];
                    args.Add(agentName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                /*  "WHQ-WHAT-Current-Action hostAgent $performer"
                *   what is the current action of a performer perceived by $hostAgent?
                *   current action in a given activity )
                */
                else if (vectorString[0] == "WHQ-WHAT-Current-Action" && vectorString.Length == 3)
                {
                    Console.Write(" asking about current action of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("  in a given activity ");
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Current-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //performer name
                    string performerName = vectorString[2];
                    args.Add(performerName);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

                /*  "WHQ-WHAT-Current-Action"
                *   what is the current action of an  agent in the activity1?
                *   current action in a given activity )
                */
                else if (vectorString[0] == "WHQ-WHAT-Current-Action" && vectorString.Length == 4)
                {
                    Console.Write(" asking about current action of an performer agent :");
                    Console.Write(vectorString[2]);
                    Console.Write("  in a given activity ");
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Current-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    string performerName = vectorString[2];
                    args.Add(performerName);
                    //activity name
                    string activityName = vectorString[3];
                    args.Add(activityName);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Current-action
                */


                /*  "WHQ-WHAT-Last-Action"
                *   what is the Last action of an  agent?
                * (by default in a current ongoing activity )
                */
                else if (vectorString[0] == "WHQ-WHAT-Last-Action" && vectorString.Length == 3)
                {
                    Console.Write(" asking about Last action of an agent :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Last-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //performer name
                    string performer = vectorString[2];
                    args.Add(performer);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

                /*  "WHQ-WHAT-Last-Action"
                *   what is the Last action of an  agent in the activity1?
                *   current action in a given activity )
                */
                else if (vectorString[0] == "WHQ-WHAT-Last-Action" && vectorString.Length == 4)
                {
                    Console.Write(" asking about Last action of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("  in a given activity ");
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Last-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //activity name
                    string activityName = vectorString[2];
                    args.Add(activityName);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Last-action
                */

                /*  "WHQ-WHAT-Next-Action"
                *   what is the Next action of an  agent?
                * (by default in a current ongoing activity )
                */
                else if (vectorString[0] == "WHQ-WHAT-Next-Action" && vectorString.Length == 2)
                {
                    Console.Write(" asking about Next action of an agent :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Next-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

                /*  "WHQ-WHAT-Next-Action"
                *   what is the Next action of an  agent in the activity1?
                *   current action in a given activity )
                */
                else if (vectorString[0] == "WHQ-WHAT-Next-Action" && vectorString.Length == 3)
                {
                    Console.Write(" asking about Next action of an agent :");
                    Console.Write(vectorString[1]);
                    Console.Write("  in a given activity ");
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Next-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //activity name
                    string activityName = vectorString[2];
                    args.Add(activityName);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Next-action
                */

                /*  "WHQ-WHAT-Role-Next-Action"
                *   what is the Next action of the role in the activity1?
                )
                */
                else if (vectorString[0] == "WHQ-WHAT-Role-Next-Action" && vectorString.Length == 2)
                {
                    Console.Write(" asking about Next action of the Role :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Role-Next-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string roleName = vectorString[1];
                    args.Add(roleName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //...................................................................................................
                /*  "WHQ-WHAT-Team-Next-Action"
                *   what is the Next action of the team in the activity1?
                )
                */
                else if (vectorString[0] == "WHQ-WHAT-Team-Next-Action" && vectorString.Length == 1)
                {
                    Console.Write(" asking about Next action of the Team :");
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Team-Next-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name

                    Predicate p = new Predicate(da.getID());
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }



                //...................................................................................................


                /*  "WHQ-WHAT-Role-current-Action"
                *   what is the current action of the role in the activity1?
                )
                */
                else if (vectorString[0] == "WHQ-WHAT-Role-Current-Action" && vectorString.Length == 2)
                {
                    Console.Write(" asking about curent action of the Role :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Role-Current-Action");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string roleName = vectorString[1];
                    args.Add(roleName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //...........................................................................

                /*  "WHQ-WHAT-Action-Decision"
                *   what is the Next decision of an  agent in the activity1?
                *   current action in a given activity )
                */
                else if (vectorString[0] == "WHQ-WHAT-Action-Decision" && vectorString.Length == 2)
                {
                    Console.Write(" asking about Next action of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("  in a given activity ");
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Action-Decision");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //concept name
                    string agentName = vectorString[1];
                    args.Add(agentName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }


                /*  "WHQ-WHAT-Said"
                  eg.  what did he/she/you/$agent say?
                *  request for the repeatition of the last utterance ( hearer didnt understand tha last utterance)
                */
                else if (vectorString[0] == "WHQ-WHAT-Said" && vectorString.Length == 2)
                {
                    Console.Write(" asking about repetation of the last utterance  of an agent :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Said");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-said
                */

                /*  "WHQ-WHAT-Current-Activity"
                  eg.  what is your current activity
                *
                */
                else if (vectorString[0] == "WHQ-WHAT-Current-Activity" && vectorString.Length == 3)
                {
                    Console.Write(" asking about Current-Activity of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Current-Activity");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //activity name
                    string activityName = vectorString[2];
                    args.Add(activityName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }


                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-said
                */
                /*  "WHQ-WHAT-Previous-Activity"
                  eg.  what was your Previous activity
                *
                */
                else if (vectorString[0] == "WHQ-WHAT-Previous-Activity" && vectorString.Length == 3)
                {
                    Console.Write(" asking about Previous-Activity of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Previous-Activity");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //activity name
                    string activityName = vectorString[2];
                    args.Add(activityName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }


                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Previous
                */

                /*  "WHQ-WHAT-Next-Activity"
                  eg.  what is Next current activity
                *
                */
                else if (vectorString[0] == "WHQ-WHAT-Next-Activity" && vectorString.Length == 3)
                {
                    Console.Write(" asking about Next-Activity of an agetn :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Next-Activity");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);
                    //activity name
                    string activityName = vectorString[2];
                    args.Add(activityName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }


                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Next
                */
                //........................................................

                /*  "WHQ-WHAT-Role-Commitment"

                *
                */
                else if (vectorString[0] == "WHQ-WHAT-Role-Commitment" && vectorString.Length == 2)
                {
                    Console.Write(" asking about Role-Commitment of an agent :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHAT-Role-Commitment");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    //agent name
                    string agentName = vectorString[1];
                    args.Add(agentName);


                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }


                /*
                TODO :: treatement for the reference of 1st, second and third person pronoun  for : WHQ-WHAT-Role-Commitment
                */

                //...................................................................................................


                /*  "WHQ-WHAT-Action-Resource <?hostAgent> <?performer> <?action> <?resourceUsage> <?tense>"
                *	resourceUsage can be one of the {object, mean, humanBodyPart, target}
                )
                */
                else if (vectorString[0] == "WHQ-WHAT-Action-Resource" && vectorString.Length == 6)
                {
                    da.setID("WHQ-WHAT-Action-Resource");
                    da.communicativeFunction = "WHQ-WHAT";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    for (int i = 1; i < vectorString.Length; i++)
                    {
                        args.Add(vectorString[i]);
                    }

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //...........................................................................






            } //end of if (cuWord ==  what)

            //********************************************************************************
            //					WHQ-WHO   (present , past , future ) action
            //********************************************************************************

            
            else if (vectorString[0].Contains("WHO") && utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "WHO";
                Console.Write("input is a WHQ-WHO ");
                Console.Write("\n");

                //==========================================================================================
                /*  "WHQ-WHO-Will-Do-Action" @actionName
                *   Qui vas guider le moule?
                */


                //********************************************************************************
                //********************************************************************************
                //********************************************************************************
                //					WHQ-WHO   (present , past , future ) action
                //********************************************************************************



                //==========================================================================================
                /*  "WHQ-WHO-Will-Do-Action" @actionName
                *   Qui vas guider le moule?
                */


                //	std::cout << "v1  : " << vectorString[0]<<  " v2 "<< "WHQ-WHO-Will-Do-Action" << vectorString.size()<< std::endl;
                if (vectorString[0] == "WHQ-WHO-Will-Do-Action" && vectorString.Length == 2)
                {
                    Console.Write(" asking about who will do action :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHO-Will-Do-Action");
                    da.communicativeFunction = "WHQ-WHO";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    for (int i = 1; i < vectorString.Length; i++)
                    {
                        args.Add(vectorString[i]);
                    }

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.id, args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //==========================================================================================
                //==========================================================================================
                /*  "WHQ-WHO-Do-Action" @actionName
                *   Qui vas guider le moule?
                */


                //	std::cout << "v1  : " << vectorString[0]<<  " v2 "<< "WHQ-WHO-Will-Do-Action" << vectorString.size()<< std::endl;
                else if (vectorString[0] == "WHQ-WHO" && vectorString.Length == 5)
                {
                    Console.Write(" asking about who has done (is doing) action :");
                    Console.Write(vectorString[1]);
                    Console.Write("\n");
                    da.setID("WHQ-WHO");
                    da.communicativeFunction = "WHQ-WHO";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    for (int i = 1; i < vectorString.Length; i++)
                    {
                        args.Add(vectorString[i]);
                    }

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //********************************************************************************

            } //end of who


            //********************************************************************************

            //********************************************************************************
            //					WHQ-WHY   (Action-Execution-by-Role , $role , $action , $tense
            //********************************************************************************

            
           else if (vectorString[0].Contains("WHY") && utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "WHY";
                Console.Write("input is a WHQ-WHY ");
                Console.Write("\n");

                //==========================================================================================
                /*  "WHQ-WHy Action-Execution-By-Agent" $hostAgent $role $agent $tense
                *   Pourquoi <agentRole> va guider le moule?
                */


                //	std::cout << "v1  : " << vectorString[0]<<  " v2 "<< "WHQ-WHO-Will-Do-Action" << vectorString.size()<< std::endl;
                if (vectorString[1] == "Action-Execution-By-Role" && vectorString.Length == 6)
                {
                    Console.Write(" asking about reason  why for action :");
                    Console.Write(vectorString[3]);
                    Console.Write("\n");
                    da.setID("WHQ-WHY");
                    da.communicativeFunction = "WHQ-WHY";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();
                    for (int i = 1; i < vectorString.Length; i++)
                    {
                        args.Add(vectorString[i]);
                    }

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //==========================================================================================

            } //end of who

            //********************************************************************************

            // end of  WHQuestion-WHAT
            //********************************************************************************
            //********************************************************************************

            else if (vectorString[0].Contains("Greet-open") && utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "Greet-open";


                Console.Write(" input is a greet-open dialogue act ");
                Console.Write("\n");
                Console.Write("  agent greet system :");
                Console.Write("\n");
                da.setID("Greet-open");
                da.communicativeFunction = "Greet-open";
                da.dimension = Dimensions.socialObligation;
                da.sender = utterance.Sender;
                da.addressee = utterance.Receiver;
                da.utterance = utterance.Content;
                List<object> args = new List<object>();
                //agent name
                //std::string agentName = vectorString[1] ;
                args.Add(da.sender);
                args.Add(da.addressee);
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: Predicate p(da.id,args);
                Predicate p = new Predicate(da.getID(), args);
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                //ORIGINAL LINE: da->logicalForm = p;
                da.logicalForm = p;

            }


            else if (vectorString[0].Contains("SOM-Presented-by-other") && utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "SOM-Presented-by-other";
                Console.Write(" input is a SOM dialogue act ");
                Console.Write("\n");
                da.setID("SOM-Presented-by-other");
                da.communicativeFunction = "SOM-Presented-by-other";
                da.dimension = Dimensions.socialObligation;
                da.sender = utterance.Sender;
                da.addressee = utterance.Receiver;
                da.utterance = utterance.Content;
                List<object> args = new List<object>();
                //agent name
                //std::string agentName = vectorString[1] ;
                args.Add(da.sender);
                args.Add(da.addressee);
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: Predicate p(da.id,args);
                Predicate p = new Predicate(da.getID(), args);
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                //ORIGINAL LINE: da->logicalForm = p;
                da.logicalForm = p;

            }
            //********************************************************************************

            //********************************************************************************
            //********************************************************************************

            else if (vectorString[0].Contains("Greet-close") && utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "Greet-close";
                Console.Write(" input is a greet-close dialogue act ");
                Console.Write("\n");
                Console.Write("  agent greet close to system :");
                Console.Write("\n");
                da.setID("Greet-close");
                da.communicativeFunction = "Greet-close";
                da.dimension = Dimensions.socialObligation;
                da.sender = utterance.Sender;
                da.addressee = utterance.Receiver;
                da.utterance = utterance.Content;
                List<object> args = new List<object>();
                //agent name
                //std::string agentName = vectorString[1] ;
                args.Add(da.sender);
                args.Add(da.addressee);
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                //ORIGINAL LINE: Predicate p(da.id,args);
                Predicate p = new Predicate(da.getID(), args);
                //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                //ORIGINAL LINE: da->logicalForm = p;
                da.logicalForm = p;

            }

            //********************************************************************************



            //********************************************************************************
            // YES-NO-Question

            //	std::cout << " \n***********YES-NO-Question*****************************\n"<< std::endl;
            
            else if (vectorString[0].Contains("YES-NO-Question") && utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "YES-NO-Question";
                Console.Write("input is a YES-NO-Question ");
                Console.Write("\n");
                //YES-NO-Question PropositionType Arguments//
                if (vectorString[1] == "Agent-Role" && vectorString.Length == 4)
                {
                    Console.Write("asking abou whether agent plays the role :");
                    Console.Write(vectorString[3]);
                    Console.Write("\n");
                    da.setID("YES-NO-Question");
                    da.communicativeFunction = "YES-NO-Question";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();

                    //predicate name Agent-Role
                    string functor = vectorString[1];
                    args.Add(functor);
                    //agent name
                    string agentName = vectorString[2];
                    args.Add(agentName);
                    //role name
                    string roleName = vectorString[3];
                    args.Add(roleName);

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }

            } //end of YES-NO-Question
              //...............................................................................
              //	std::cout << " \n***********Check-Question*****************************\n"<< std::endl;
              //frame structure :  Check-Question QuestionType @parameters


            
            if (vectorString[0].Contains("Check-Question") &&  utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "Check-Question";
                // "case 1 :  Check-Question Action-Execution-By-Role roleName actionName Tense @parameters

                Console.Write("input is a Propositional Check-Question ");
                Console.Write("\n");
                //.........................................................................................

                {
                    //	if(vectorString[1] == "Action-Execution-By-Role" && vectorString.size()==5)
                    //		std::cout<< "asking whether role executes the  action "<< vectorString[4] << " in :" << vectorString[4]<< std::endl;
                    da.setID("Check-Question");
                    da.communicativeFunction = "Check-Question";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();

                    for (int i = 1; i < vectorString.Length; i++)
                    {
                        args.Add(vectorString[i]);
                    }

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //.........................................................................................
            } //end of Check-Question
              //...............................................................................

            //********************************************************************************
            //********************************************************************************

            //...............................................................................
            //	std::cout << " \n***********Unknown-concept*****************************\n"<< std::endl;
            //frame structure :


            
            else if (vectorString[0].Contains("Unknown-concep") &&  utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "Unknown-concept";
                // "case 1 :  Check-Question Action-Execution-By-Role roleName actionName Tense @parameters

                Console.Write("input is a Unknown-concept ");
                Console.Write("\n");
                //.........................................................................................

                {
                    //	if(vectorString[1] == "Action-Execution-By-Role" && vectorString.size()==5)
                    //		std::cout<< "asking whether role executes the  action "<< vectorString[4] << " in :" << vectorString[4]<< std::endl;
                    da.setID("Unknown-concept");
                    da.communicativeFunction = "Unknown-concept";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();

                    for (int i = 1; i < vectorString.Length; i++)
                    {
                        args.Add(vectorString[i]);
                    }

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //.........................................................................................
            } //end of Check-Question
              //...............................................................................

            //********************************************************************************

            //********************************************************************************

            //...............................................................................
            //	std::cout << " \n***********not-understood*****************************\n"<< std::endl;
            //frame structure :

            //agent doesn't understand what the interlocutor has said
            
            else if (vectorString[0].Contains("Not-Understood") &&  utternaceFrame.IndexOf(cueWord) != utternaceFrame.Length)
            {
                cueWord = "Not-Understood";
                Console.Write("input is a not-understood by agent ");
                Console.Write("\n");
                //.........................................................................................

                {
                    //	if(vectorString[1] == "Action-Execution-By-Role" && vectorString.size()==5)
                    //		std::cout<< "asking whether role executes the  action "<< vectorString[4] << " in :" << vectorString[4]<< std::endl;
                    da.setID("Not-Understood");
                    da.communicativeFunction = "Not-Understood";
                    da.dimension = Dimensions.task; //"task";
                    da.sender = utterance.Sender;
                    da.addressee = utterance.Receiver;
                    da.utterance = utterance.Content;
                    List<object> args = new List<object>();

                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: Predicate p(da.id,args);
                    Predicate p = new Predicate(da.getID(), args);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
                    //ORIGINAL LINE: da->logicalForm = p;
                    da.logicalForm = p;
                }
                //.........................................................................................
            } //end of Check-Question
              //






            return da;
        }


    }



} //namespace

