using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace DM
{

    public class InformationState
    {

        private Dictionary<string, Cell> cellMap; 
        private List<Cell> cells;
        private string name;
        public string Name { get { return name; } set { name = value; } }

        public InformationState()
        {
            cellMap = new Dictionary<string, Cell>();
            cells = new List<Cell>();
        }
        public void Dispose()
        {
        }
        public void addCell(string name, Cell cell)
        {
            cellMap.Add(name, cell);
        }
        public void addCell(Cell cell)
        {
            cells.Add(cell);
        }



        /*
        Cell InformationState::getCell(string cellName)
        { 
            map<string, Cell>::const_iterator it;
            it = cellMap.find(cellName);
            if(it!=cellMap.end())
                return it->second;

            return Cell();
        }
        */
        public Cell getCell(string cellName)
        {
            Cell result = null;
            foreach (Cell cell in cells)
            {
                if (cell.Name.CompareTo(cellName) == 0)
                {   return cell;
                }
            }
            return result; 
        }
        public bool hasCell(string cellName)
        {
          
            foreach (KeyValuePair<string, Cell > cell in  cellMap)
            {
                if (cell.Key.CompareTo(cellName) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void set(string path, object value)
        {

            string temp = path;
            Property p = getPropertyValueOfPath(path);

            if(p!= null)
                p.setValue(value);
        }
        public Property getPropertyValueOfPath(string keyPath)
        {
            Property p = null ;
            string path = keyPath;
            try
            {
               // path = InformationStateVariables.ISVariables[keyPath];
                string temp = keyPath;
                if (path.ElementAt(0).CompareTo('$')==0)
                {
                    temp = path.Substring(1, path.Length-1);

                    string[] strs = temp.Split(new string[] { "." }, StringSplitOptions.None);
                    if (strs.Length == 4)
                    {
                        p = getCell(strs[1]).getComponent(strs[2]).getProperty(strs[3]);
                    }
                }               
            }
            catch (KeyNotFoundException) {
            }
            return p;
        }
        public object getValueOfPath(string keyPath)
        {
            object value = null;

            Property p = getPropertyValueOfPath(keyPath);
            if(p!= null)
            {
                value = p.data();
                return value;
            }
            return value;
        }
        public string getTypeOfPath(string path)
        {
            string typeName = null;

            Property p = getPropertyValueOfPath(path);
            if (p != null)
                typeName = p.AttTyep; 
            return typeName;
        }
        /*
        string InformationState::toString()
        {  string result,str;
            map<string, Cell>::iterator it;
            str = str.append(name);
            str = str.append("\n");
            for(it = cellMap.begin(); it!=cellMap.end(); it++)
            {	
                str = str.append("	");
                str = str.append(it->first);
                str = str.append("\n	");
                str = str.append(it->second.toString());
                str = str.append("\n\n" );
            }


            return str;
        }
        */

        public string toString()
        {
            string result = "";
            string str ="";
           
            //str = str.append(name);
            str = str+("\n");

            List<Cell>.Enumerator it1;

            foreach (Cell cell in cells)
            {

                str = str+("\n	");
                str = str+(cell.toString());
                str = str+("\n\n");

            }
            /*for(it = cellMap.begin(); it!=cellMap.end(); it++)
            {
                str = str.append("	");
                str = str.append(it->first);
                str = str.append("\n	");
                str = str.append(it->second.toString());
                str = str.append("\n\n" );
            }

            */
            result = result +(name);
            result = result +(str);
            return result;
        }

  
    }

}






