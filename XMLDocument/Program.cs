using System;
using System.Collections.Generic;

namespace XMLDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlParser xmlParser = new XmlParser();
            string[] tagNameArray = new string[] { "versionInfo" };

            //List<ParserResultModel> resultList = xmlParser.XmlAllParser("../../AFile.xml");
            List<ParserResultModel> resultList = xmlParser.XmlSelectParser("../../AFile.xml", "versionInfo");

            new Program().PrintRsultData(resultList);

            Console.ReadLine();
        }

        private void PrintRsultData(List<ParserResultModel> resultList)
        {
            for (int i = 0; i < resultList.Count; i++)
            {
                Console.WriteLine("[{0}]{1} : {2}", resultList[i].ParentNodeIndex, resultList[i].LocalName, resultList[i].InnerText);

                if (resultList[i].ChildNodeList != null)
                {
                    PrintRsultData(resultList[i].ChildNodeList);
                }
            }

            Console.WriteLine();
        }
    }
}
