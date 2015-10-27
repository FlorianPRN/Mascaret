using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{

    public class IsNotMutualBelief : Condition
    {
        private MutualBelief mutualBel = new MutualBelief();

        public IsNotMutualBelief()
        {
        }
        public IsNotMutualBelief(MutualBelief mb)
        {
            this.mutualBel = mb;
        }
        public  void Dispose()
        {
            base.Dispose();
        }
        public override bool isValid(InformationState IS)
        {

            string path = DefineConstants.commonGround;
            Property p = IS.getPropertyValueOfPath(path);
            if (p.contains(mutualBel))
            {
                return false;
            }
            return true;
        }
        
    }


}
