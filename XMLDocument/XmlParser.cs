using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XMLDocument
{
    class XmlParser
    {
        List<ParserResultModel> resultList = new List<ParserResultModel>(); // 결과 데이터 리스트 생성
        XmlDocument xmlDoc = new XmlDocument();                             // XmlDocument 클래스 생성

        public List<ParserResultModel> XmlSelectParser(string filePath, string tagNameArray)
        {
            // 결과 데이터 리스트 제거
            resultList.Clear();

            // Xml 파일 로드
            xmlDoc.Load(filePath);

            // 입력 받은 string으로 xml 파일 검색
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(tagNameArray);

            for (int i = 0; i < xmlList.Count; i++)
            {
                if (xmlList[i].HasChildNodes)
                {
                    if (xmlList[i].ChildNodes[0].NodeType == XmlNodeType.Text)
                    {
                        resultList.Add(new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName, xmlList[i].InnerText));
                    }
                    else if (xmlList[i].ChildNodes[0].NodeType == XmlNodeType.Element)
                    {
                        ParserResultModel rootNode = new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName);

                        resultList.Add(rootNode);

                        AddResultList(xmlList[i].ChildNodes, rootNode);
                    }
                    else { }
                }
            }

            return resultList;
        }

        public List<ParserResultModel> XmlAllParser(string filePath)
        {
            // 결과 데이터 리스트 제거
            resultList.Clear();

            // Xml 파일 로드
            xmlDoc.Load(filePath);

            // 최상위 노드 검색
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(xmlDoc.DocumentElement.LocalName);

            ParserResultModel rootNode = new ParserResultModel(0, xmlList[0].ParentNode, xmlList[0].LocalName);
            resultList.Add(rootNode);

            AddResultList(xmlList[0].ChildNodes, rootNode);

            return resultList;
        }

        private void AddResultList(XmlNodeList xmlList, ParserResultModel currentNode)
        {
            for (int i = 0; i < xmlList.Count; i++)
            {
                if (xmlList[i].HasChildNodes)
                {
                    // 자식이 없는 노드
                    if (xmlList[i].ChildNodes[0].NodeType == XmlNodeType.Text)
                    {
                        currentNode.ChildNodeList.Add(new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName, xmlList[i].InnerText));
                    }
                    // 자식이 있는 노드
                    else if (xmlList[i].ChildNodes[0].NodeType == XmlNodeType.Element)
                    {
                        ParserResultModel childNode = new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName);
                        currentNode.ChildNodeList.Add(childNode);

                        AddResultList(xmlList[i].ChildNodes, childNode);
                    }
                    else { }
                }
            }
        }


        private void AddResultList(XmlNodeList xmlList, ParserResultModel currentNode, int parentNodeIndex)
        {
            // xmlList 수 만큼 반복
            for (int i = 0; i < xmlList.Count; i++)
            {
                // nodeType이 Text이면 현재 클래스에 InnerText값 추가
                if (xmlList[i].NodeType == XmlNodeType.Text)
                {
                    currentNode.InnerText = xmlList[i].InnerText;
                }
                // nodeType이 Element이면 클래스 생성 후 리스트 추가
                else if (xmlList[i].NodeType == XmlNodeType.Element)
                {
                    //currentNode = new ParserResultModel(xmlList[i].ParentNode, ParentNodeIndex, xmlList[i].LocalName);
                    //currentNode.ChildNodeList.Add(currentNode);

                    // Attributes가 0개 이상이면 현재 클래스에 Attributes값 추가
                    if (xmlList[i].Attributes.Count > 0)
                    {
                        //AddAttributes(xmlList[i].Attributes);
                    }
                    else { }
                }
                else { }

                // 자식노드가 없을때까지 반복
                AddResultList(xmlList[i].ChildNodes, currentNode, parentNodeIndex);
            }
        }

        private void AddAttributes(XmlAttributeCollection xmlAttribute, ParserResultModel currentNode)
        {
            currentNode.Attributes = new Dictionary<string, string>();

            for (int i = 0; i < xmlAttribute.Count; i++)
            {
                currentNode.Attributes.Add(xmlAttribute[i].Name, xmlAttribute[i].Value);
            }
        }
    }
}
