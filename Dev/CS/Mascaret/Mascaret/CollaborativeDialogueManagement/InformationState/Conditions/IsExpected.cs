using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public class IsExpected : Condition
    {
        private Predicate belief = new Predicate();
        public IsExpected()
        {
        }
        public IsExpected(Predicate bel)
        {
            belief  = bel;
        }
        public  void Dispose()
        {
            base.Dispose();
        }
        public override bool isValid(InformationState IS)
        {

            string path = DefineConstants.expectedPredicate;
            Property p = IS.getPropertyValueOfPath(path);
            if (p.contains(belief))
            {
                return true;
            }
            return false;
        }
        
    }


} //namespace



