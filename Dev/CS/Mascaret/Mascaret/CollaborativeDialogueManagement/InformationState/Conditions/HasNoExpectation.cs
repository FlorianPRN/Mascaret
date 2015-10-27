using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public class HasNoExpectation : Condition
    {
        public HasNoExpectation()
        {
        }
        public void Dispose()
        {
            base.Dispose();
        }
        public override bool isValid(InformationState IS)
        {
            string path = DefineConstants.expectedPredicate;
            Property p = IS.getPropertyValueOfPath(path);
            if (p != null) {
                try
                {
                    object pFront = p.front();
                    Predicate predicate = (Predicate)(pFront);
                }
                catch (InvalidCastException)
                {
                    return true;
                }
            }
            return false;
        }

    }

}