using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string[]> tagNameDic = new Dictionary<string, string[]>();
            tagNameDic.Add("Point", null);

            //new Program().XMLParsing("AFile.xml", "VendorPart", tagNameDic);
            new Program().XMLAllParsing("AFile.xml", "VendorPart");

            Console.ReadLine();
        }

        public void XMLParsing(string xmlName, string firstTag, Dictionary<string, string[]> tagNameDic)
        {
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load(xmlName);

            XmlNodeList xmlList = xmlFile.GetElementsByTagName(firstTag);

            foreach (XmlNode item in xmlList[0].ChildNodes)
            {
                Console.WriteLine("{0} : {1}", item.LocalName, xmlList[0][item.LocalName].InnerText);

                foreach (XmlNode item2 in item)
                {
                    if (item2.LocalName != "#text")
                    {
                        Console.WriteLine("{0} : {1}", item2.LocalName, xmlList[0][item.LocalName][item2.LocalName].InnerText);
                    }
                }
                Console.WriteLine("===================================");
            }
            Console.ReadLine();
        }

        public void XMLAllParsing(string xmlName, string topLevelTag)
        {
            // 저장할 List 선언
            List<ResultListModel> resultList = new List<ResultListModel>();

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load(xmlName);
            XmlNodeList xmlList = xmlFile.GetElementsByTagName(topLevelTag);

            foreach (XmlNode xnList in xmlList)
            {
                AddResultList(xnList, resultList);
            }

            //PrintResultList(resultList);
        }

        private void AddResultList(XmlNode xnList, List<ResultListModel> resultList)
        {
            foreach (XmlNode item in xnList.ChildNodes)
            {
                Console.WriteLine(xnList[item.LocalName].InnerText);

                ResultListModel resultListModel = new ResultListModel(item.LocalName, xnList[item.LocalName].InnerText);
                resultList.Add(resultListModel);

                if (item.ChildNodes.Count > 1)
                {
                    this.AddResultList(item, resultListModel.resultDataChild);
                }

                //Console.WriteLine(item.LocalName, xnList[item.LocalName].InnerText);
                //Console.WriteLine(item.ChildNodes.Count);

                //if (item.ChildNodes.Item(1) != null)
                //{
                //    this.AddResultList(item, resultListModel.resultDataChild);
                //}
            }
        }

        private void PrintResultList(List<ResultListModel> resultList)
        {
            foreach (ResultListModel item in resultList)
            {
                Console.WriteLine("1Depth <{0}> : {1}", item.localName, item.innerText);

                foreach (ResultListModel item2 in item.resultDataChild)
                {
                    Console.WriteLine("2Depth <{0}> : {1}", item2.localName, item2.innerText);
                }
            }
        }
    }
}
