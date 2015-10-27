using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{

    public class UpdateRule
    {
        protected List<Precondition> preconditions = new List<Precondition>();
        protected List<Precondition> applicabilityConditions = new List<Precondition>();
        protected List<Effect> effects = new List<Effect>();
        public UpdateRule()
        {
        }
        public void Dispose()
        {
        }
        public bool verifyConditions(InformationState IS)
        {
            foreach (Precondition pcondition in preconditions)
            {
                if (!pcondition.isValid(IS))
                {
                    return false;
                }
            }

            return true;
        }
        public void applyEffects(ref InformationState IS)
        {
           foreach (Effect effect in  effects)
            {
                effect.apply( ref IS);
            }

        }
        public void checkAndApply(ref InformationState IS)
        {
            if (verifyConditions(IS))
            {
                Console.Write(" inside effect ");
                Console.Write("\n");
                applyEffects(ref IS);
            }
        }
        public void addPrecondition(Precondition condition)
        {
            preconditions.Add(condition);
        }
        public void addApplicabilityConditions(Precondition condition)
        {
            applicabilityConditions.Add(condition);
        }
        public bool isApplicable(InformationState IS)
        {
           foreach (Precondition condition in applicabilityConditions)
            {
                if (!condition.isValid(IS))
                {
                    return false;
                }
            }
            return true;

        }
        public void addEffect(Effect effect)
        {
            effects.Add(effect);

        }



    }

} //namespace


