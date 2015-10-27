using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mascaret;

namespace NLP
{
	
	using System.Collections;
	using System.Collections.Generic;
	
	/// <summary>
	/// This class stores the actual dialogues. This is the actual asset you see in the project
	/// </summary>
	public class EasyDialogueFile : Plan
	{
		public EasyDialogueFile (string goal):base(goal){
		}

		class position
		{
			int x { get; set; }
			int y { get; set; }
			
		}
		/// <summary>
		/// A node in the graph; line in the conversation
		/// </summary>

		/// <summary>
		/// A list of all the entries
		/// </summary>
		
		public List<DialogueEntry> entries = new List<DialogueEntry>();
		/// <summary>
		/// A list of all the lines
		/// </summary>
		
		public List<DialogueLine> lines = new List<DialogueLine>();


		public override BehaviorExecution createBehaviorExecution(InstanceSpecification host, Dictionary<String, ValueSpecification> p, bool sync)
		{
			
			EasyDM cpe = new EasyDM (this, host, p);
            MascaretApplication.Instance.VRComponentFactory.Log("............................easy behavior has been added to schedular");
            return cpe ;
		}

	}
}
