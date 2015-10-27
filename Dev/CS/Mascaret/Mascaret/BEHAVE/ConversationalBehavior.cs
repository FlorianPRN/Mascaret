using System;
using System.Collections.Generic;
using NLP;
using DM;
namespace Mascaret
{
  public  class ConversationalBehavior : CyclicBehaviorExecution
    {
       
      //  public ConversationalBehavior() { }
        public ConversationalBehaviourExecution runningCommBehaveExe;
        public List<ConversationalBehaviourExecution> listCommBehaveExe = new List<ConversationalBehaviourExecution>();
		private EasyDialogueManager edm;

        public ConversationalBehavior(Behavior specif, InstanceSpecification host, Dictionary<String, ValueSpecification> p)
           : base(specif, host, p)
        {

        }
        public void pushCommunicationPlanToDo(string dialoguePlan)
        {

        }

        public override void action()
        {
		/*	Console.WriteLine(" executing converational behavior");
			string msg = " I am being called by abstract class";
			dialogueInterface.Speak (Host.name, msg);
			*/
			VirtualHuman agt = Host as VirtualHuman;
			if (agt!=null) {
			/*	if(!agt.DialogueManager.easyDM.isFinished )
				{ 
					 Dialog  d = agt.DialogueManager.easyDM.process();
					if(d!=null)
						dialogueInterface.Speak(d);
				}
				//reactive behaviour 
			*/	agt.DialogueManager.processNextUtterance();
				UtteranceMessage reply =  agt.DialogueManager.nextOutgoingMessage();
				if(reply!=null)
				{

					if(reply.Content!= null)
						dialogueInterface.Speak (Host.name, reply.Content);
					else{
						try{
							if(reply.GenericContent!= null){
								Dialogue d =  (Dialogue) reply.GenericContent;
								if(d!=null )
								{ 
									if(!d.GetCurrentLine().Equals(""))
									{
										if(agt.DialogueManager.easyDM!= null)
											if(!agt.DialogueManager.easyDM.EasyBExecution.isFinished)
												dialogueInterface.Speak(d,agt.DialogueManager.easyDM.EasyBExecution.GetChoiceSelection);

									}
								}
							}

						}catch(InvalidCastException e){
							MascaretApplication.Instance.VRComponentFactory.Log (Host.name + ":..Exception: " + e.ToString());
						}
					}
				}
			}
        }

        private DialogueInterface dialogueInterface;
        public DialogueInterface DialInterface
        {
            get { return dialogueInterface; }
            set { dialogueInterface = value; }
        }
    }
}

