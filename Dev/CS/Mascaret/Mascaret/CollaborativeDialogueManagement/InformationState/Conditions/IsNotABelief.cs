using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System;

namespace DM
{
    public class IsNotABelief : Condition
    {
        private SemanticPredicate belief = new SemanticPredicate();
        private Predicate predicate = new Predicate();
        public IsNotABelief()
        {
        }
        public IsNotABelief(SemanticPredicate bel)
        {
            belief = bel;
        }
        public IsNotABelief(Predicate bel)
        {
             predicate = bel;
        }

        public  void Dispose()
        {
            base.Dispose();
        }

        public override bool isValid(InformationState IS)
        {
            bool result = true;
            string path = DefineConstants.semanticBeliefs;
            Property p = IS.getPropertyValueOfPath(path);
            if (p != null)
            {
                if (p.contains(belief))
                {
                    result = false;
                }
                else
                {
                    object temp = p.evaluate(belief);
                    if (temp != null)
                    {
                        result = false;
                    }
                }

            }

            return result;


        }


    }

} //namespace
