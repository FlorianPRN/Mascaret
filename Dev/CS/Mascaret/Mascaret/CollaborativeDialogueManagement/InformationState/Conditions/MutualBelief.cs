using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public class MutualBelief : SemanticPredicate
    {
        private List<string> partners = new List<string>();
        public List<string> Partners
        {
            get { return partners; }
            set
            {
                partners = value;
            }
        } 




        public MutualBelief() : base()
        {
        }
        public MutualBelief(SemanticPredicate sp, List<string> partners)
        {
            this.partners = partners;
            Owner = sp.Owner;
            Type = sp.Type;
            Functor = sp.Functor;
            Arguments = sp.Arguments;

        }

        public MutualBelief(string type, string owner, List<string> partner, Predicate p) : base(type, owner, p)
        {
            this.partners = partner;

        }


        public  void Dispose()
        {
            base.Dispose();
        }

       
    }

}
