using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public class UtteranceMessage
    {
        public enum MessageType { connection, speech, action, knowledge };
        public UtteranceMessage()
        {
        }
        private string sender;
        public string Sender { get { return sender; } set { sender = value; } }
        private string receiver;
        public string Receiver { get { return receiver; } set { receiver = value; } }
        private MessageType type;
        public MessageType Type { get { return type; } set { type = value; } }
        private string content;
        public string Content { get { return content; } set { content = value; } }
        private string utteranceSemanticForm;
        public string UtteranceSemanticForm { get { return utteranceSemanticForm; } set { utteranceSemanticForm = value; } }
        private object genericContent;
        public object GenericContent { get { return genericContent; } set { genericContent = value; } }

    }
}

