using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLP;
namespace Mascaret
{
    public abstract class DialogueInterface
    { 
        public abstract void Speak(string utterance);
		public abstract void Speak(string host, string utterance);
		public abstract void Speak(Dialogue dialogue);
		public abstract void Speak(Dialogue dialogue, Action<int> callback );
    }
}
