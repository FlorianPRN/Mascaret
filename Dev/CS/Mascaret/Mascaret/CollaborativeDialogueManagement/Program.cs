using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Xml.Linq;
using DM;

 public   class Program
    {

    static void Main(string[] args)
    {
        ParseInformationState isParser = new ParseInformationState();
        InformationState IS = new InformationState();
        IS =  isParser.parseIS(@"D:\Mukesh\Work\INSA\DialogueManagement\NLP\PredicateCalculus\bin\Debug\InformationState.xml");
        Console.WriteLine(IS.toString());
    }
  }


