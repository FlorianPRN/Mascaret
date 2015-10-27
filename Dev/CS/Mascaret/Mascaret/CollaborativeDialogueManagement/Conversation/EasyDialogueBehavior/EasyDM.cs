using System;
using System.Collections.Generic;
using Mascaret;
using DM;
namespace NLP 
{
	public class EasyDM: CyclicBehaviorExecution
	{
		public string mDialogueName;
		private Dialogue mDialogue;
		static  Dialogue currentDialogue;
		Dialogue.Choice  currentChoice;
		float speedsmooth = 0.5f; // your speed smooth, but not time
		private float myAlpha = 0f;
		bool pauseFlag = false;
		bool speechUpdated = true;
        int nextLineId;

		public bool isFinished { get; set;}
		public bool isPlanStarted { get; set; }
			
		int counter { get; set;}

		private EasyDialogueFile plan = null;
			int x ;
		public EasyDialogueFile runningPlan {
				get{return plan;} 
				set{plan = value;}
			}
		VirtualHuman vh = null;
		public EasyDM(EasyDialogueFile planToExe, InstanceSpecification host, Dictionary<String, ValueSpecification> p)
				: base(planToExe, host, p)
			{
			vh = host as VirtualHuman;
				plan = planToExe;
				isFinished = false;
				isPlanStarted = false;
				x = 1;
			}
		public override double execute(double dt)
		{
				//	MascaretApplication.Instance.VRComponentFactory.Log (Host.name + ":.............................Executing Conversational Behavior");
				double res = Process ();
				if (res == 0)
					return 0;

				
				return 5f;
			}
			
		int  test(int x){
				int t = 2; 
				int result;
				result = t * 2 - t / 2 + 2;
				return result;
		}
			
			
		public double Process(){
			if (isFinished) {

                MascaretApplication.Instance.VRComponentFactory.Log("EasyDM finished......................................");
                return 0;
            }
				

            MascaretApplication.Instance.VRComponentFactory.Log("calling process next ac......................................");

            Dialogue dialogue = processNextDialogue ();
			if (dialogue != null) {
				if (dialogue.GetCurrentLine ().dialogue != "")
				{
					MascaretApplication.Instance.VRComponentFactory.Log (Host.name + ":.......................Current Dialogue : " + dialogue.GetCurrentLine ().dialogue);
	
					UtteranceMessage reply = new UtteranceMessage ();

							reply.GenericContent = dialogue;

					vh.DialogueManager.addToOutgoingUtteranceMessageBox (reply);
				}

			}

			MascaretApplication.Instance.VRComponentFactory.Log (Host.name + ":.............................Executing Easy Behavior");
			return 3f;
		}

		public virtual void speek(Dialogue currentDialogue) {
			
			
		}
		public virtual void receive(Dialogue.Choice choice)
		{
			currentDialogue.PickChoice(choice);
			currentChoice = choice;
		}
		public virtual void receive(string utternace)
		{
		}
		public virtual void  init(string dialogueFile)
		{
			currentDialogue = GetDialogue("TutorialStart");
			currentChoice = currentDialogue.GetChoices()[0];
			
			currentDialogue.PickChoice(currentChoice);
			Console.WriteLine("dialogue initialized");
		}
		public Dialogue GetDialogue(string dialogueName)
		{
			// create the dialogue and return it
			Dialogue result = new Dialogue();
			
			// get the lines
			foreach (DialogueLine line in plan.lines)
			{
				if (line.dialogueEntry == dialogueName)
					result.AddLine(line);
			}
			
			return result;
		}	






		public virtual Dialogue processNextDialogue()
        {
            MascaretApplication.Instance.VRComponentFactory.Log("EasyDM processNextDialogue......................................");
            if (isFinished)
                return null;
			if (!isPlanStarted)
			{
                pauseFlag = false;

                mDialogue = GetDialogue(plan.Goal);
				if (mDialogue == null )
				{
					isFinished = true;

					return mDialogue;
				}
				mDialogue.Start();

				/*
				currentDialogue = GetDialogue("TutorialStart");
				currentChoice = currentDialogue.GetChoices()[0];
				
				currentDialogue.PickChoice(currentChoice);
				Console.WriteLine("dialogue initialized");
				if (currentChoice.dialogue != null)
					Console.WriteLine(currentChoice.dialogue);
					*/
				isPlanStarted = true;
				speechUpdated = true;
                nextLineId = -1;
                currentDialogue = mDialogue;
				return currentDialogue;

			}
			else
			{
                if (currentDialogue.GetCurrentLine()== null || currentDialogue.GetCurrentLine().output.Count== 0)
                {
                    isFinished = true;
                    currentDialogue = null;
                    return currentDialogue;
                }
                NextLine(nextLineId);
				/*
				// pauseFlag = false;
	
					if (currentDialogue.GetChoices().Length > 1)
					{
						pauseFlag = true;
						speechUpdated = true;
						
					}
					else
					{
						if (currentDialogue.GetChoices().Length == 1)
							{
								//	if (GUILayout.Button("Next"))
								currentChoice = null;
								currentChoice = currentDialogue.GetChoices()[0];
								if (currentChoice != null)
								{
									currentDialogue.PickChoice(currentChoice);
									if (currentChoice.dialogue != null){
										Console.WriteLine(currentChoice.dialogue);
										speechUpdated = true;
									}
									
								}
							}
						}
				*/
			}

			
			return currentDialogue;
		}
		
		public static Dialogue getCurrentDialogue()
		{
			return currentDialogue;
		}
		public void NextLine (int nextId = -1)
		{
            pauseFlag = false;
            currentDialogue = null;
			bool continueDialogue;
            if (nextId == -1)
            {
                continueDialogue = mDialogue.GoNextLine();
            }
            else
                continueDialogue = mDialogue.GoToLine(nextId);

            if (continueDialogue)
                currentDialogue = mDialogue;
            else if (mDialogue.GetCurrentLine().output.Count == 0) {
                MascaretApplication.Instance.VRComponentFactory.Log("EasyDM is terminated ....");
                isFinished = true;
            }
               
            //mSpeechRenderer.Speak(mDialogue.GetCurrentLine(), NextLine);

            pauseFlag = true;
        }
        public void GetChoiceSelection(int Id = -1)
        {
            MascaretApplication.Instance.VRComponentFactory.Log("Current choice!!!!!!!!!!!!!!!!!!!!!!!!!!!!." + Id);
            if(Id == -1)
                isFinished = true;

            nextLineId = Id;
        }
    }
}
	
