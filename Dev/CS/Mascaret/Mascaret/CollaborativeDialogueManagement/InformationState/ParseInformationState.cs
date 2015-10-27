using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Xml.Linq;
namespace DM
{
    class ParseInformationState
    {


        public InformationState parseIS(string path)
        {
            InformationState IS = new InformationState();
            XDocument doc = new XDocument();

            try
            {
                doc = XDocument.Load(path);
            }
            catch (Exception e)
            {

                Console.Write("exception Occured: " + e.Data.ToString());
            }
            XElement _root = doc.Root;
            Console.WriteLine("$$$$$$$  : " + _root.Name);

            IS.Name = _root.Attribute("name").Value.ToString();
            foreach (XElement currentNode in _root.Elements())
            {
                Console.WriteLine("processing cell*********  : " + currentNode.Name);

                if (currentNode.Name.LocalName.Trim() == "Cell")
                {
                    Cell cell = _parseCell(currentNode);
                    IS.addCell(cell);

                }
            }

            return IS;
        }

       public  Cell _parseCell(XElement cellNode)
        {
            Cell cell = new Cell();
            cell.Name = cellNode.Attribute("name").Value.ToString();
            foreach (XElement currentNode in cellNode.Elements())
            {
                Console.WriteLine("processing Component ..............  : " + currentNode.Name);
                if (currentNode.Name.LocalName.Trim() == "Component")
                {
                    Component component = _parseComponent(currentNode);
                    cell.addComponent(component);
                }
            }

                return  cell;
        }

     public   Component _parseComponent(XElement ComponentNode)
        {
            Component component = new Component();
            component.Name = ComponentNode.Attribute("name").Value.ToString();
            foreach (XElement currentNode in ComponentNode.Elements())
            {
            //    Console.WriteLine("processing Property  : " + currentNode.Name);
                if (currentNode.Name.LocalName.Trim() == "Property")
                {
                    string name = currentNode.Attribute("name").Value;
                    string type = currentNode.Attribute("type").Value;

                    Property property = new Property(name,type);
                    Console.WriteLine(".processing attribute  : " + name + "   " + type);
                    Property  p = new Property(name, type);
                    component.addProperty(p);
                }
            }

            return component;
        }


    }
}
