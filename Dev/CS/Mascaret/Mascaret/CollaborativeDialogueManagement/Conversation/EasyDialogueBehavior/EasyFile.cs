using System.Collections.Generic;

namespace NLP
{
	
	/// <summary>
	/// This class stores the actual dialogues. This is the actual asset you see in the project
	/// </summary>
	public class EasyFile 
	{
		public string name { get; set; }
		public EasyFile(){        
			
		}
		public EasyFile(string planName){ 
		name = planName;    
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
		

		
	}
}
