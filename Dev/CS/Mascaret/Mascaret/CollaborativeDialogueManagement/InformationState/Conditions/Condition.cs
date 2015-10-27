using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public abstract class Condition
    {
        public Condition()
        {
        }
        public void Dispose()
        {
        }
        public abstract bool isValid(InformationState IS);
    }

}

