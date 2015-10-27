using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{

    public class IsTopOfAgendaEmpty : Condition
    {
        public IsTopOfAgendaEmpty()
        {
        }
        //	isTopOfAgendaEmpty(Predicate p);

        public void Dispose()
        {
            base.Dispose();
        }
        public override bool isValid(InformationState IS)
        {
            Property p = IS.getPropertyValueOfPath(DefineConstants.agenda);
            try
            {
                object pFront = p.front();
                Predicate predicate = (Predicate)(pFront);
            }
            catch (InvalidCastException)
            {
                return true;
            }
            return false;
        }
        private Predicate move = new Predicate();

    }

}
