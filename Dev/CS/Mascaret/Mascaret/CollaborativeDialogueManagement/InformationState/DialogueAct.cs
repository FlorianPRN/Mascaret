using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{
    public enum Dimensions
    {
        task,
        turnManagement,
        discourseStructur,
        socialObligation,
        ownCommunicationManagement,
        partnerCommunicationManagement,
        coordination
    }
    public enum QualificationAspect
    {
        certainity,
        conditionality,
        partiality,
        sentiment
    }
    public enum DialogueActs
    {
        greet
    }
    public class DialogueAct
    {
        public string id;
        public string sender;
        public string addressee;
        public string communicativeFunction;
        public Dimensions dimension;
        public QualificationAspect qualification;
        public string context;
        public string utterance;

        public bool aboutOtherPerson;
        public bool aboutOtherObject;
        public bool aboutSelf;
        public bool aboutAddressee;

        public Predicate logicalForm = new Predicate();
        public DialogueAct()
        {
        }
        public void Dispose()
        {
        }
        public string getSender()
        {
            return sender;
        }
        
        public string getAddressee()
        {
            return addressee;
        }
        public string getCommunicativeFunction()
        {
            return communicativeFunction;
        }
        public Dimensions getDimension()
        {
            return dimension;
        }
       
        public string getContext()
        {
            return context;
        }
        public string getUtterance()
        {
            return utterance;
        }
        public string getID()
        {
            return id;
        }
        public void setSender(string sender)
        {
            this.sender = sender;
        }
        
        public void setAddressee(string addressee)
        {
            this.addressee = addressee;
        }
        public void setCommunicativeFunction(string function)
        {
            this.communicativeFunction = function;
        }
        
        public void setContext(string context)
        {
            this.context = context;
        }
        public void setUtterance(string utterance)
        {
            this.utterance = utterance;
        }
        public void setID(string id)
        {
            this.id = id;
        }
        public Predicate getLogicalForm()
        {
            return logicalForm;
        }
        public void setLogicalForm(Predicate p)
        {
            //C++ TO C# CONVERTER WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
            //ORIGINAL LINE: logicalForm = p;
           // logicalForm.CopyFrom(p);
            logicalForm = (p);
        }



    }

}