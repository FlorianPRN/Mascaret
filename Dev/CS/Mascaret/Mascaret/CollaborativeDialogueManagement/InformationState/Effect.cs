using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{

    public abstract class Effect
    {
        public Effect()
        {
        }
        public Effect(string path, object value)
        {
            this.path = path;
            this.value = value;
        }
        public void Dispose()
        {
        }
        public abstract void apply(ref InformationState IS);

        protected string path;
        protected object value = null;
    }

}