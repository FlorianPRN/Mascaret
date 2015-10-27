using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public class IsBelief : Condition
    {
        private Predicate predicate = new Predicate();

        public IsBelief()
        {
        }
  /*      public IsBelief(SemanticPredicate bel)
        {
            belief = (bel);
        }
        */
        public IsBelief(Predicate bel)
        {
            predicate = bel;
        }

        public void Dispose()
        {
            base.Dispose();
        }

        public override bool isValid(InformationState IS)
        {

            string path = DefineConstants.semanticBeliefs;
            Property p = IS.getPropertyValueOfPath(path);

            //	std::cout<< p->getName() << "   " <<std::endl;
            /*   if (p.contains(belief))
               {
                   return true;
               }

               else */
            if (p.contains(predicate))
            {
                return true;
            }
            return false;
        }
     //   private SemanticPredicate belief = new SemanticPredicate();
       

    }

} //namespace

