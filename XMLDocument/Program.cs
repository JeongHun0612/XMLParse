using System;
using System.Collections.Generic;
using System.Linq;

namespace XMLDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlParser xmlParser = new XmlParser();
            string[] tagNameArray = new string[] { "Object", "versionInfo", "name", "number" };
            string filePath = "../../AFile.xml";

            // XmlAllParser 데이터 불러오기
            List<ParserResultModel> resultList = xmlParser.XmlAllParser(filePath);

            // XmlSelectParser 데이터 불러오기
            Dictionary<string, List<ParserResultModel>> resultDic = xmlParser.XmlSelectParser(filePath, tagNameArray);

            // XmlAllParser 출력
            //Print(resultList);

            // XmlSelectParser 출력
            for (int i = 0; i < resultDic.Count; i++)
            {
                Console.WriteLine("===========================================================");
                Print(resultDic[tagNameArray[i]]);
            }

            Console.ReadLine();
        }

        public static void Print(List<ParserResultModel> resultList)
        {
            for (int index = 0; index < resultList.Count; index++)
            {
                Console.WriteLine("[{0}] {1} : {2}", resultList[index].ParentNodeIndex, resultList[index].LocalName, resultList[index].InnerText);

                if (resultList[index].Attributes != null)
                {
                    for (int attrIndex = 0; attrIndex < resultList[index].Attributes.Count; attrIndex++)
                    {
                        Console.WriteLine("{0} : {1}", resultList[index].Attributes.Keys.ToList()[attrIndex], 
                                                                    resultList[index].Attributes.Values.ToList()[attrIndex]);
                    }
                }

                if (resultList[index].ChildNodeList != null)
                {
                    Print(resultList[index].ChildNodeList);
                    Console.WriteLine();
                }
            }
        }
    }
}
