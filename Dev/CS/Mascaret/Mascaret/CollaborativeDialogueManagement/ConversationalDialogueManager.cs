using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NLP;
using DM;
namespace Mascaret
{
public class ConversationalDialogueManager
{
		PlanLibrary planLib;

		Agent host { get; set; }
		public ConversationalDialogueManager(Agent agt){
			host = agt;
			_incomingUtteranceMessageBox = new UtteranceMessageBox();
			_outgoingUtteranceMessageBox = new UtteranceMessageBox();
			planLib = new PlanLibrary ();
		}

		private	UtteranceMessageBox _incomingUtteranceMessageBox;	
		private	UtteranceMessageBox  _outgoingUtteranceMessageBox;	
		public ConversationalDialogueManager(){
			_incomingUtteranceMessageBox = new UtteranceMessageBox();
			_outgoingUtteranceMessageBox = new UtteranceMessageBox();
			planLib = new PlanLibrary ();
		}


		private InitialiseComponents components;

		private TemplateExtractor extractor;
		public TemplateExtractor Extractor
		{
			get{ return extractor;}
			set{ extractor = value;}

		}
		private ComponentConfig config;
		public ComponentConfig Config
		{
			get{ return config;}
			set{ config = value;}
			
		}

		private HMMComponent hmmComponent;
		GenericTextAnnotation textAnnotation;
		DialogueInterpreter dialogueInterpreter;

        NaturalLanguageDialogueGenerator nlgDialogueGenerator;


		public	UtteranceMessageBox	IncomingUtteranceMessageBox 
		{ get{return _incomingUtteranceMessageBox;}
			set{_incomingUtteranceMessageBox =  value;}
		}
		private EasyDialogueManager edm = null;
		public EasyDialogueManager easyDM{ get { return edm; } set { edm = value; } }

        public InformationState IS = null;
		public void ComponentInitialization(string path)
		{

            ParseInformationState isParser = new ParseInformationState();
            IS = new InformationState();
            IS = isParser.parseIS(path+ "res/InformationState.xml");
            MascaretApplication.Instance.VRComponentFactory.Log(IS.toString());
            edm = new EasyDialogueManager ();

            string[] fileEntries = Directory.GetFiles(path+"res/dialogue");
            foreach (string fileName in fileEntries)
            {
                MascaretApplication.Instance.VRComponentFactory.Log("................Easy dialogue files ...........::" + fileName);
                if (fileName.ToString().EndsWith(".xml"))
                {
                    EasyDialogueFile easyDialPlan = EasyDialogueManager.LoadDialogueFileFromPath1(fileName);
                    //	DialogueFile dialoguePlan 	= EasyDialogueManager.LoadDialogueFileFromPath (Application.dataPath + "/StreamingAssets/res/tutorial.xml");
                    planLib.AddPlan(easyDialPlan);
                    MascaretApplication.Instance.VRComponentFactory.Log("................Easy dialogue files ....file loaded successfully");


                }

            }
            //edm =  EasyDialogueManager.LoadDialogueFile(Application.dataPath + "/StreamingAssets/res/tutorial.xml");


            components = new InitialiseComponents (path);
			config = new ComponentConfig();
			config = components.Config;



			extractor = new TemplateExtractor();
			extractor.setupComponent(config);
	//		TextComponent tc = new TextComponent();
			hmmComponent = new HMMComponent();
			hmmComponent.setupComponent(config);



			dialogueInterpreter = new DialogueInterpreter();
			dialogueInterpreter.setupComponent(config);
            nlgDialogueGenerator = new NaturalLanguageDialogueGenerator();
            nlgDialogueGenerator.setupComponent(config);

		}
		// Use this for initialization
		void Start () {
		}
	
	// Update is called once per frame
		void Update () {
	
		}

		public void addToUtteranceMessageBox(UtteranceMessage  message) {
			_incomingUtteranceMessageBox.postMessage(message);
		}
		public void addToOutgoingUtteranceMessageBox(UtteranceMessage  message) {
			_outgoingUtteranceMessageBox.postMessage(message);
		}
		public UtteranceMessage nextIncomingMessage(){
			UtteranceMessage msg= _incomingUtteranceMessageBox.check ();
			return msg;
		}
		public UtteranceMessage nextOutgoingMessage(){
			UtteranceMessage msg= _outgoingUtteranceMessageBox.check ();
			return msg;
		}
		public string  processNextUtterance(){
			string result = null;
			UtteranceMessage msg = nextIncomingMessage ();

			if(msg!=null){

             //   string utternace = Services.toLowerCase(msg.Content);
                string utternace = msg.Content;
                StringData inputUtterance = new StringData(1, utternace, LanguageUtils.getLanguageCodeByLocale("es-US"));
				textAnnotation = hmmComponent.handleData(inputUtterance);
				TemplateData tempData = extractor.handleData(textAnnotation);
                MascaretApplication.Instance.VRComponentFactory.Log("Input::" + utternace+ " " + tempData.Id+ " "+ tempData.ToString()
 );
                result =   	dialogueInterpreter.handleUtternace(tempData);

                MascaretApplication.Instance.VRComponentFactory.Log("NLU::" + result);
                }

			if (result != null) {
				UtteranceMessage reply = new UtteranceMessage();
                reply.Content = result;
                reply.Sender = "Technicien";
				reply.Receiver = "Technicien2";
                msg.UtteranceSemanticForm = result;

                List<string> outputStrings = handleDialogueUtterance(msg);
                foreach(string outputString in outputStrings)
                {
                    MascaretApplication.Instance.VRComponentFactory.Log("DM::" + outputString);
                    reply.Content = outputString;

                   // StringData outputUtterance = new StringData(1, Services.toLowerCase(outputString), LanguageUtils.getLanguageCodeByLocale("es-US"));

                    StringData outputUtterance = new StringData(1, outputString, LanguageUtils.getLanguageCodeByLocale("es-US"));

                    textAnnotation = hmmComponent.handleData(outputUtterance);
                    TemplateData tempData = extractor.handleData(textAnnotation);

                    result = nlgDialogueGenerator.handleUtternace(tempData);
                    MascaretApplication.Instance.VRComponentFactory.Log("NLG::" + result);
                    reply.Content = result;

                    _outgoingUtteranceMessageBox.postMessage(reply);

                }
 
			}
		   
			return result;
		}
       
		public BehaviorExecution  addConversationPlanToExecutue(string  planToExe)
		{
			BehaviorExecution be = null;
		/*	ConversationPlan plan = new ConversationPlan ("planToExe");
			plan.addOperation(new ConversationOperation("Dialogue1", host));
			plan.addOperation(new ConversationOperation("Dialogue2",host));
			be = BehaviorScheduler.Instance.executeBehavior (plan, host, new Dictionary<string, ValueSpecification> (), false);

		*/
			foreach (Plan plan in planLib.plans) {
				if(plan.Goal == planToExe)
				{   
					ConversationPlan cp = null;
					try{ 
						cp= (ConversationPlan) plan; 
					}
					catch(System.InvalidCastException e )
					{

					}
					if(cp!= null)
					{
						be = BehaviorScheduler.Instance.executeBehavior (plan, host, new Dictionary<string, ValueSpecification> (), false);
					break;
					}

					EasyDialogueFile edf = null;
					try{ 
						edf= (EasyDialogueFile) plan; 
					}
					catch(System.InvalidCastException e )
					{
						
					}
					if(edf!= null)
					{
						be = BehaviorScheduler.Instance.executeBehavior (plan, host, new Dictionary<string, ValueSpecification> (), false);
						edm.EasyBExecution = (EasyDM) be;
						break;
					}
				}
			}


			return be;
		}

	
        List<string> handleDialogueUtterance(UtteranceMessage utteranceMsg)
        {
           UtteranceMessage utterance = new UtteranceMessage();
            utterance.UtteranceSemanticForm = "WHQ-WHAT-Agent-Role agent1";
            utterance.Sender = "agent2";
            utterance.Receiver = "agent1";
            utterance.Content = "quel est ta role";
            VirtualHuman agent = (VirtualHuman)host;

            DM.Property dialActs = agent.DialogueManager.IS.getPropertyValueOfPath(DefineConstants.addresseeDialogueActs);
            UtteranceInterpreter utteranceInterpreter = new UtteranceInterpreter(agent);
            DialogueAct dialogueAct = utteranceInterpreter.constructDialogueAct(utteranceMsg);
            if(dialogueAct!= null)
            {
                dialActs.push(dialogueAct);
            }
            
            MascaretApplication.Instance.VRComponentFactory.Log("utternace " + utterance.Content + "    generated dialogue act : " + dialogueAct.id);

            DialogueActInterpreter di = new DialogueActInterpreter(agent);

            List<UpdateRule> rules = di.interprete(dialogueAct, agent.DialogueManager.IS);
            MascaretApplication.Instance.VRComponentFactory.Log("\n numbers of applying rules to integrate Effects : " + rules.Count); ;

            foreach (UpdateRule rule in rules)
             {
                 rule.checkAndApply(ref agent.DialogueManager.IS);

              }

            MascaretApplication.Instance.VRComponentFactory.Log(agent.DialogueManager.IS.toString());



            dialActs = agent.DialogueManager.IS.getPropertyValueOfPath(DefineConstants.addresseeDialogueActs);
            List<object> dataVector = dialActs.DataVector;
            List<object> processedDAs = new List<object>(); ;
            MascaretApplication.Instance.VRComponentFactory.Log("number of dialogue acts in addressee DA " + dataVector.Count);

            foreach (object da in dataVector)
            {
                  DialogueAct dialAct = null;
                   try
                   {
                       dialAct = (DialogueAct)da;
                       MascaretApplication.Instance.VRComponentFactory.Log("dialogue acts for select rules: "+dialAct.getLogicalForm().Functor);
                   }
                    catch (KeyNotFoundException)
                    {
                    }

                if (dialAct != null)
                {
                    SelectRule ruleSelector = new SelectRule(dialAct, agent.DialogueManager.IS);
                    List<UpdateRule> selectedRules = ruleSelector.select();
                    if (selectedRules.Count > 0)
                    { 
                    MascaretApplication.Instance.VRComponentFactory.Log("dialogue acts for select rules: " + dialAct.getLogicalForm().Functor);

                    MascaretApplication.Instance.VRComponentFactory.Log("number of Selected rules :" + selectedRules.Count);
                    }
                   foreach (UpdateRule rule in selectedRules)
                    {
                         rule.checkAndApply(ref agent.DialogueManager.IS);
                     }
                    processedDAs.Add(da);
                }
            }
            foreach(object da in processedDAs)
            {

                dialActs.DataVector.Remove(da);
            }
           // dialActs.clear();
            MascaretApplication.Instance.VRComponentFactory.Log("processing next moves for the generation phase ");
            string movePath = DefineConstants.nextMoves;
            DM.Property tmoves = agent.DialogueManager.IS.getPropertyValueOfPath(movePath);
            List<object> moveVec = tmoves.DataVector;
            List<string> resultSet = new List<string>();
            foreach (object move in moveVec)
            {
                Predicate nextMove = null;
                try
                {
                    nextMove = (Predicate)move;
                    if (nextMove != null)
                    {
                        string nlgFrame = Services.toLowerCase(nextMove.Functor);
                        List<object> args = nextMove.Arguments;
                        for (int i = 0; i < args.Count; i++)
                        {
                            string temp = (string) (args[i]);
                            nlgFrame = nlgFrame + " " + temp;
                        }
                        resultSet.Add(nlgFrame);
                    }

                }
                catch (InvalidCastException) { }
            }
            tmoves.clear();
            return resultSet;
         }








    }
}//namespace
