﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLP
{

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// This class stores the actual dialogues. This is the actual asset you see in the project
    /// </summary>
    public class DialogueFile 
	{

      
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
