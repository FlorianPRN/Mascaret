using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public class FirstOnAgenda : Condition
    {
        private Predicate move = new Predicate();

        public FirstOnAgenda()
        {
        }
        public FirstOnAgenda(Predicate p)
        {
           move = p;

        }
        public  void Dispose()
        {
            base.Dispose();
        }
        public override bool isValid(InformationState IS)
        {
            string path = "$IS.semanticContext.shared.agenda";
            Property p = IS.getPropertyValueOfPath(path);
           object pFront = p.front();
            if (pFront== null)
            {
                return false;
            }
            else if (Unify.matchTerms(pFront, move))
            {
                return true;
            }
            return false;

        }
    }

} //namespace