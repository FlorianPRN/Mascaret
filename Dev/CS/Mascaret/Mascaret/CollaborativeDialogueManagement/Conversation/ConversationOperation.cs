//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
using System;
using Mascaret;
namespace DM
{
	public class ConversationOperation
	{   InstanceSpecification host { get; set; }
		public ConversationOperation (string cname, InstanceSpecification agt)
		{   host = agt;
			name = cname;
		}
		string name { get; set;}
		public void execute(){
			MascaretApplication.Instance.VRComponentFactory.Log("............................Executing conversation operation " + name);
			UtteranceMessage reply = new UtteranceMessage();
			reply.Content = "..Executing conversation operation " + name;

			reply.Receiver = "Technicien2";
			VirtualHuman vh = host as VirtualHuman;
			reply.Sender = vh.name;
			//vh.DialogueManager.addToOutgoingUtteranceMessageBox (reply);
		}
	}
}

