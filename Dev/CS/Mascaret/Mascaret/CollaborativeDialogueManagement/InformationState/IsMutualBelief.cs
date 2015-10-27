using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace DM
{

    public class IsMutualBelief : Condition
    {
        private MutualBelief mutualBel = new MutualBelief();

        public IsMutualBelief()
        {
        }
        public IsMutualBelief(MutualBelief mb)
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
                return true;
            }
            return false;
        }
     }


}

