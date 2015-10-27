using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 


namespace DM
{
    public class Component
    {
        private string name;
        public string Name { get { return name;  } set { name = value; } }
        private List<Property> properties;
        public List<Property> Properties
        {
            get
            {
                return properties;
            }
            set
            {
                properties = value;
            }
        }
        private List<Property> properties1 = new List<Property>();
        private Dictionary<string, Property> propertyMap = new Dictionary<string, Property>();

        public Component()
        {
            properties = new List<Property>();

        }
        public Component(string name)
        {
            this.name = name;
            properties = new List<Property>();
        }
        public void Dispose()
        {
        }
        public void addProperty(Property p)
        {
            properties.Add(p);
            //  return myAttributes.add(a);
        }

        public Property getProperty(string pname)
        {
            Property result = null;
            foreach (Property property in properties)
            {
                if (property.Name.CompareTo(pname) == 0)
                {
                   return property;
                }
            }
            return result;
        }
  
        public string toString()
        {
            string str = null; 
            foreach (Property property in properties)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    str = "[";
                }
                else
                {
                    str = str+ ",	";
                    str = str + "\n				 ";
                }
                str = str + property.toString();

            }

            if (!string.IsNullOrEmpty(str))
            {
                str = str+ "]";
            }

            string result = "";
            result = result + name;
            result = result + "{";
            result = result+(str);
            result = result+("}");
            return result;
        }
      //Property testPro;
    }
}