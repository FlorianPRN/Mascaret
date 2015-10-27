using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM
{
    public static partial class DefineConstants
    {
        public const string nextMoves = "$IS.dialogueContext.lu.nextMoves";
        public const string addresseeDialogueActs = "$IS.dialogueContext.addresseeDialogueAct.dialogueActs";
        public const string agentDialogueActs = "$IS.dialogueContext.agentDialogueAct.dialogueActs";
        public const string integratedMoves = "$IS.dialogueContext.dialogueHistory.integratedMoves";
        public const string qud = "$IS.semanticContext.shared.qud";
        public const string agenda = "$IS.semanticContext.shared.agenda";
        public const string semanticBeliefs = "$IS.semanticContext.private.beliefs";
        public const string expectedPredicate = "$IS.semanticContext.private.expected";
        public const string cognitiveBeliefs = "$IS.cognitiveContext.private.beliefs";
        public const string commonGround = "$IS.cognitiveContext.shared.commonGround";
        public const string commPressure = "$IS.SocialContext.socialRule.commPressure";
    }
}
