using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace DM
{
	public class UtteranceMessageBox
	{
		public UtteranceMessageBox ()
		{
		}
		private List<UtteranceMessage> messagesQueue = new List<UtteranceMessage>();
		public List<UtteranceMessage> MessagesQueue
		{
			get { return messagesQueue; }
			set { messagesQueue = value; }
		}
		
		
		private List<UtteranceMessage> messagesChecked = new List<UtteranceMessage>();
		public List<UtteranceMessage> MessagesChecked
		{
			get { return messagesChecked; }
			set { messagesChecked = value; }
		}
		
		
		private List<UtteranceMessage> messagesSent = new List<UtteranceMessage>();
		public List<UtteranceMessage> MessagesSent
		{
			get { return messagesSent; }
			set { messagesSent = value; }
		}
		
		
		public void postMessage(UtteranceMessage message)
		{
			messagesQueue.Add(message);
		}
		
		public UtteranceMessage check()
		{
			UtteranceMessage msg = null;
			
			if (messagesQueue.Count != 0)
			{
				msg = messagesQueue[0];
				messagesQueue.RemoveRange(0, 1);
				messagesChecked.Add(msg);
			}
			
			return msg;
		}
		
		public void send(UtteranceMessage message)
		{
			messagesSent.Add(message);
		}
	}
}

