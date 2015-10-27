using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public class IsNotIntegratedMove : Condition
    {
        private Predicate move = new Predicate();
        public IsNotIntegratedMove()
        {
        }
        public void Dispose()
        {
            base.Dispose();
        }
        public IsNotIntegratedMove(Predicate p)
        {
           move = p ;
        }

        public override bool isValid(InformationState IS)
        {
            string path = DefineConstants.integratedMoves;
            Property p = IS.getPropertyValueOfPath(path);
            if (p.contains(move))
            {
                return false;
            }

            return true;

        }
       
    }

}
