using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public enum semanticPredicateType
    {
        Bel,
        Knows,
        Wants,
        currentSpeaker,
        nextSpeaker
    }

    public class SemanticPredicate : Predicate
    {

        private string type = null;
        private string owner = null;
        private string partner = null;


        public string Type { get { return type; } set { type = value; } }
        public string Owner { get { return owner; } set { owner = value; } }
        public string Partner { get { return partner; } set { partner = value; } }
        public SemanticPredicate() : base()
        {
        }
        public SemanticPredicate(SemanticPredicate sp) : base(sp.Functor, sp.Arguments)
        {
            this.type = sp.Type;
            this.owner = sp.Owner;
        }

        public void Dispose()
        {
           
        }
        //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
        //ORIGINAL LINE: base(p);
        public SemanticPredicate(string type, string owner, Predicate p) : base(p)
        {
            this.type = type;
            this.owner = owner;
        }
        //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
        //ORIGINAL LINE: base(p);
        public SemanticPredicate(string type, string owner, string partner, Predicate p) : base(p)
        {
            this.type = type;
            this.owner = owner;
            this.partner = partner;
        }
       
     }

}