using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Newtonsoft.Json;

namespace Excel
{
    public class ImportData
    {
        public Function[] ImportTXT(string path)
        {
            string[] funcs = File.ReadAllLines(path);
            Function[] func = new Function[funcs.Length];
            
            for (int i = 0; i < funcs.Length; i++)
                func[i] = new Function(funcs[i]);

            return func;
        }
        public Function[] ImportJSON(string path)
        {
            string allfuncs = File.ReadAllText(path);
            Function[] func = JsonConvert.DeserializeObject<Function[]>(allfuncs);
            return func;
        }
        public Function[] ImportXML(string path)
        {
            XmlDocument xmlD = new XmlDocument();
            xmlD.Load(path);
            XmlElement xmlB = xmlD.DocumentElement;

            Function[] func = new Function[xmlB.ChildNodes.Count];

            for (int i = 0; i < func.Length; i++)
                func[i] = new Function(xmlB.ChildNodes[i].InnerText);

            return func;
        }
    }
}