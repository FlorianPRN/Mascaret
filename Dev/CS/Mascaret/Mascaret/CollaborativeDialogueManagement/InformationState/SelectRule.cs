using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Collections.Generic;


namespace DM
{
    /*
	*	SelectRule class defines the strategy to select the next move
	* based on the current information state and on the context of the 
	* current ongoing activity task under the control of the 
	* conversation policy ( dialogue protocols) 
	* It then apply these moves in order to generate 
	* output dialogue acts
	*/

    public class SelectRule
    {
        private DialogueAct dialogueAct;
        private InformationState infoState;
        private List<UpdateRule> selectedRules = new List<UpdateRule>();

        public SelectRule()
        {
        }
        public SelectRule(DialogueAct da, InformationState IS)
        {
            dialogueAct = da;
            infoState = IS;
        }
        public SelectRule(InformationState IS)
        {
            infoState = IS;
            selectedRules.Clear();
        }

        public void Dispose()
        {
        }
        public List<UpdateRule> select()
        {
            List<UpdateRule> rules = RuleFactory.getInstance().createRules(dialogueAct, infoState);
            List<UpdateRule>.Enumerator it;
            foreach (UpdateRule rule in rules)
            {
                if (rule.verifyConditions(infoState))
                {
                    selectedRules.Add(rule);
                    //return selectedRules;
                }
            }

            return selectedRules;
        }
        public List<UpdateRule> selectAgentGeneratedRules()
        {
            List<UpdateRule> rules = AgentDialogueActRuleFactory.getInstance().createRules(dialogueAct, infoState);
            //cerr<< " select rule : number of created rules  = " << rules.size() << endl;
            List<UpdateRule>.Enumerator it;
            foreach (UpdateRule rule in rules )
            {
                if (rule.verifyConditions(infoState))
                {
                    selectedRules.Add(rule);
                    //return selectedRules;
                }
            }
            return selectedRules;
        }



    }

} //namespace

