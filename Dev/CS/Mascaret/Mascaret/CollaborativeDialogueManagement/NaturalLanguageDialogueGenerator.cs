using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLP;
using Mascaret;
namespace DM
{
    class NaturalLanguageDialogueGenerator : DialogueInterpreter
    {

        public virtual bool evaluateCondition(string predicateString)
        {
            bool evalResult = Services.evaluateCondition(predicateString);
            //  Debug.LogWarning("..................Evaluation result::" + evalResult);
            return evalResult;






        }
        /*   public virtual string replacePredicateByResult(string input)
           {
               string output = null;
               return output;
           }
           public virtual object evaluatePredicateQuery(string predicate)
           {
               object result = null;
               return result;
           }
           */
    }
}
