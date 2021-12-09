using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace XMLDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. List<ParserResultModel> 반환
            //string[] tagNameArray = new string[] { "cdm", "name", "versionId", "iterationId" };
            //XmlParser.PrintXmlNode(XmlParser.XmlSelectParser("../../AFile.xml", tagNameArray));
            //XmlParser.PrintXmlNode(XmlParser.XmlAllParser("../../AFile.xml"));

            XmlParser xmlParser = new XmlParser();
            string[] tagNameArray = new string[] { "name", "versionInfo", "lifecycleInfo" };

            //xmlParser.PrintResultList(xmlParser.XmlAllParser("../../AFile.xml"));
            xmlParser.PrintResultList(xmlParser.XmlSelectParser("../../AFile.xml", tagNameArray));

            Console.ReadLine();
        }
    }
}
