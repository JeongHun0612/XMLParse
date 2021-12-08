using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XMLDocument
{
    class TestClass
    {
        public static void XMLDocumentParser(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            string[] stringArray = new string[] { "number", "name", "versionId", "iterationId" };

            XmlElement root = xmlDoc.DocumentElement;

            //XmlNodeList xmlList2 = xmlDoc.SelectNodes("/cdm/VendorPart/versionInfo");

            for (int i = 0; i < stringArray.Length; i++)
            {
                XmlNodeList xmlList = xmlDoc.GetElementsByTagName(stringArray[i]);
                Console.WriteLine(xmlList.Count);

                for (int j = 0; j < xmlList.Count; j++)
                {
                    Console.WriteLine(xmlList[i].Value);

                    //Console.WriteLine("({0}){1} : {2}", xmlList[i].ChildNodes[j].NodeType, xmlList[i].ChildNodes[j].LocalName,
                    //    xmlList[i].ChildNodes[j].Value);
                }
            }

            //XmlNodeList xmlList3 = xmlDoc.GetElementsByTagName("versionId");
            //foreach (XmlNode node in xmlList3)
            //{
            //    foreach (XmlNode item in node.Attributes)
            //    {
            //        Console.Write(item.LocalName + " : " + item.Value);
            //    }
                
            //    Console.WriteLine("({0}){1} : {2}", node.NodeType, node.LocalName, node.Value);
            //}
        }

        public static List<ParserResultModel> XMLAllParser(string fileName)
        {
            List<ParserResultModel> resultList = new List<ParserResultModel>();

            ParserResultModel actualNode = null;

            try
            {
                XmlReaderSettings setting = new XmlReaderSettings();
                setting.IgnoreComments = true;    // <!--주석--> 무시
                setting.IgnoreWhitespace = true;  // enter 키 공백 무시

                using (XmlReader xmlReader = XmlReader.Create(fileName, setting)) // 리더 생성
                {
                    while (xmlReader.Read())   // 읽을 노드가 없으면 false
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                actualNode = new ParserResultModel() { LocalName = xmlReader.Name, Depth = xmlReader.Depth };
                                resultList.Add(actualNode);

                                while (xmlReader.MoveToNextAttribute())
                                {
                                    actualNode.Attributes.Add(xmlReader.Name, xmlReader.Value);
                                }
                                break;
                            case XmlNodeType.Text:
                                actualNode.InnerText = xmlReader.Value;
                                break;
                            default:
                                break;
                        }
                    }

                    xmlReader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return resultList;
        }

        public static void PrintXmlNode(List<ParserResultModel> resultList)
        {
            for (int i = 0; i < resultList.Count; i++)
            {
                Console.WriteLine("[{0}] <{1}> : {2}", resultList[i].Depth, resultList[i].LocalName, resultList[i].InnerText);

                if (resultList[i].Attributes != null)
                {
                    for (int j = 0; j < resultList[i].Attributes.Count; j++)
                    {
                        var resultAttributes = resultList[i].Attributes;

                        Console.WriteLine("{0} : {1}", resultAttributes.Keys.ToList()[j], resultAttributes.Values.ToList()[j]);
                    }
                }
            }
        }
    }
}
