using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{

    public enum Comparator
    {
        equals,
        not_equals,
        lesser_than,
        greater_than,
        lesser_equals,
        greater_equals,
        exists,
        not_exists,
        contains,
        not_contains
    }

    public class Precondition : Condition
    {
        protected object value1 = new object();
        protected string stringValue1;

        protected object value2 = new object();
        protected string stringValue2;
        protected Comparator comparator;
        protected Predicate predicate;
        protected int numberOfarguemnts;

        public Precondition()
        {

        }
        public Precondition(string value1)
        {
            this.stringValue1 = value1;
            numberOfarguemnts = 1;
        }
        public Precondition(string value1, Comparator op)
        {
            this.stringValue1 = value1;
            this.comparator = op;
            numberOfarguemnts = 1;
        }
        public Precondition(object value1, Comparator op)
        {
            this.value1 = value1;
            this.comparator = op;
            numberOfarguemnts = 1;
        }
        public Precondition(string conditionName, object predicateValue)
        {
            stringValue1 = conditionName;
            value2 = predicateValue;

        }
        /*Condition(std::string value1, std::string value2 , Comparator op);
        Condition(std::string value1, boost::any value2 , Comparator op);
        */

        /*
        Condition::Condition(std::string value1, std::string value2 , Comparator op)
        {
            this->stringValue1 = value1;
            this->stringValue2 = value2;
            this->comparator = op;
            numberOfarguemnts = 2;
        }

        Condition::Condition(std::string value1, boost::any value2 , Comparator op)
        {

            this->stringValue1 = value1;
            this->value2 = value2;
            this->comparator = op;
            numberOfarguemnts = 2;
        }*/

        public Precondition(object value1, object value2, Comparator op)
        {
            string temp = null;
            try
            {
                temp = (string)(value1);
            }
            catch (InvalidCastException )
            {
                Console.Write(" cought an exception");
                Console.Write("\n");
            }
            if (!string.IsNullOrEmpty(temp))
            {
                if (temp.ElementAt(0) == '$')
                {
                    stringValue1 = temp;
                }
                else
                {
                    this.value1 = value1;
                }
                temp = null;
            }
            else
            {
                this.value1 = value1;
            }

            try
            {
                temp = (string)(value2);
            }
            catch (InvalidCastException)
            {
                Console.Write(" cought an exception");
                Console.Write("\n");
            }
            if (!string.IsNullOrEmpty(temp))
            {
                if (temp.ElementAt(0) == '$')
                {
                    Console.Write(" argument value1 is a path string  ");
                    Console.Write("\n");
                    stringValue2 = temp;
                }
                else
                {
                    this.value2 = value2;
                }
                temp = null;
            }
            else
            {
                this.value2 = value2;
            }
            /*


            try
            {
                string temp  =  boost::any_cast<string>(value1);
                if(temp.ElementAt(0) == '$')
                 {
                    cout<< " argument value1 is a path string  " << endl;
                    stringValue1 = temp;
                }
                else
                {
                    cout<< "string artument value2 is a value type "<< endl;
                    this->value1  =  value1;
                }

                 temp  =  boost::any_cast<string>(value2);
                if(temp.ElementAt(0) == '$')
                 {
                    cout<< " argument value2 is a path string  " << endl;
                    stringValue2 = temp;
                }
                else
                {
                    cout<< "string artument value2 is a value type "<< endl;
                    this->value2  =  value2;
                }

            }
            catch(boost::exception const&  ex)
            {
                cout<< " cought an exception" <<endl;
            }

            /*string temp = value1;
                 if(value1.ElementAt(0) == '$')
                 {
                 */
            //	this->value1 = value1;
            //	this->value2 = value2;

            this.comparator = op;
            numberOfarguemnts = 2;
        }



        public new void Dispose()
        {
            base.Dispose();
        }
        public override bool isValid(InformationState IS)
        {
            if (stringValue1 == "firstOnAgenda")
            {
                Predicate move = (Predicate)(value2);
                FirstOnAgenda condition = new FirstOnAgenda(move);
                return condition.isValid(IS);
            }

            else if (stringValue1 == "IsNotIntegratedMove")
            {
                Predicate move = (Predicate)(value2);
              IsNotIntegratedMove condition = new IsNotIntegratedMove(move);
                return condition.isValid(IS);
            }
            else if (stringValue1 == "isBelief")
            {
                try
                {
                    SemanticPredicate belief = (SemanticPredicate)(value2);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: isBelief *condition = new isBelief(belief);
                    IsBelief condition = new IsBelief(belief);
                    return condition.isValid(IS);
                }
                catch (InvalidCastException)
                {
                }

                try
                {
                    Predicate belief = (Predicate)(value2);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: isBelief *condition = new isBelief(belief);
                    IsBelief condition = new IsBelief(belief);
                    return condition.isValid(IS);
                }
                catch (InvalidCastException)
                {
                }
            }
            else if (stringValue1 == "isNotABelief")
            {
                /*	try
                    {
                        SemanticPredicate belief = boost::any_cast<SemanticPredicate>( value2);
                        boost::shared_ptr<isNotABelief> condition = boost::shared_ptr<isNotABelief>(new isNotABelief(belief));
                        return condition->isValid(is);
                    }
                    catch(boost::exception const&  ex)
                    {
                    }
                    */
                try
                {
                    Predicate belief = (Predicate)(value2);
                    Predicate p = new Predicate(belief.Functor, belief.Arguments);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: isNotABelief *condition = new isNotABelief(p);
                    IsNotABelief condition = new IsNotABelief(p);
                    return condition.isValid(IS);
                }
                catch (InvalidCastException)
                {
                }
            }


            else if (stringValue1 == "isMutualBelief")
            {
                try
                {
                    MutualBelief belief =(MutualBelief)(value2);
                     IsMutualBelief condition = new IsMutualBelief(belief);
                    return condition.isValid(IS);

                }
                catch (InvalidCastException)
                {
                }
            }
            else if (stringValue1 == "isNotMutualBelief")
            {
                try
                {
                    MutualBelief belief = (MutualBelief)(value2);
                     IsNotMutualBelief condition = new IsNotMutualBelief(belief);
                    return condition.isValid(IS);

                }
                catch (InvalidCastException)
                {
                }
            }

            else if (stringValue1 == "isExpected")
            {
                try
                {
                    Predicate belief = (Predicate)(value2);
                    Predicate p = new Predicate(belief.Functor, belief.Arguments);
                    //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
                    //ORIGINAL LINE: isExpected expected(p);
                    IsExpected expected = new IsExpected(p);
                    return expected.isValid(IS);

                }
                catch (InvalidCastException)
                {
                }
            }

            else if (stringValue1 == "isTopOfAgendaEmpty")
            {
                IsTopOfAgendaEmpty condition = new IsTopOfAgendaEmpty();
                return condition.isValid(IS);
            }
            else if (stringValue1 == "hasNoExpectation")
            {
                HasNoExpectation condition = new HasNoExpectation();
                return condition.isValid(IS);
            }
            else
            {

                if (!string.IsNullOrEmpty(stringValue1))
                {
                    this.value1 = IS.getValueOfPath(stringValue1);
                }
                if (!string.IsNullOrEmpty(stringValue2))
                {
                    this.value2 = IS.getValueOfPath(stringValue2);
                }
                if (value1 != null   && value2 != null)
                {
                    Equals condition = new Equals(value1, value2);
                    return condition.isValid(IS);
                }

            }
            return true;
        }
    }

}

