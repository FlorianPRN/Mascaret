using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 


namespace DM
{

    public class Cell
    {
        private string name;
        public string Name { get { return name;  } set { name = value; } }
        private List<Component> components;
        public  List<Component> Components { get { return components; } set { components = value; } }
        public Cell()
        {
            components = new List<Component>();
        }
        public Cell(string name)
        {
            this.name = name;
            components = new List<Component>();
        }
        public void Dispose()
        {
        }
        public Component getComponent(string cName)
        {
            Component res = null;
            foreach (Component component in components)
            {
                if (component.Name.CompareTo(cName) == 0)
                {  return component; }
            }
            return res;
        }

        public void addComponent(Component comp)
        {
            components.Add(comp);
        }
        public string toString()
        {
            string str = null;

            foreach (Component component in components)
            {
                if (string.IsNullOrEmpty(str))
                {
                    str = "		";
                }
                else
                {
                    str = str + (",	");
                    str = str + ("\n				");
                }

                str = str + component.toString();

            }

            if (!string.IsNullOrEmpty(str))
            {
                str = str + (" ");
            }

            string result = "		";
            result = result + (name);
            result = result+("{\n		");
            result = result + (str);
            result = result + ("\n				}");
            return result;
        }

    }

}

