using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Mascaret.CollaborativeDialogueManagement.InformationState
{
    public partial class NaturalLanguageDialogueGenerator : Component
    {
        public NaturalLanguageDialogueGenerator()
        {
            InitializeComponent();
        }

        public NaturalLanguageDialogueGenerator(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
