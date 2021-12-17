using System;
using System.Collections.Generic;
using System.Xml;

namespace XMLDocument
{
    class XmlParser
    {
        // XmlDocument 클래스 생성
        XmlDocument xmlDoc = new XmlDocument();

        // 입력받은 Xml파일의 모든 데이터 추출
        public List<ParserResultModel> XmlAllParser(string filePath)
        {
            // 결과 데이터 리스트 생성
            List<ParserResultModel> resultList = new List<ParserResultModel>();

            try
            {
                // Xml 파일 로드
                xmlDoc.Load(filePath);

                // 최상위 노드 검색
                XmlNodeList xmlList = xmlDoc.GetElementsByTagName(xmlDoc.DocumentElement.LocalName);

                // xmlList의 값을 결과 리스트에 추가
                AddResultList(xmlList, resultList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return resultList;
        }

        // 입력받은 Xml파일의 선택한 데이터 추출
        public Dictionary<string, List<ParserResultModel>> XmlSelectParser(string filePath, string[] tagNameArray)
        {
            // 결과 Dictinoary 생성
            Dictionary<string, List<ParserResultModel>> resultDic = new Dictionary<string, List<ParserResultModel>>();

            try
            {
                // Xml 파일 로드
                xmlDoc.Load(filePath);

                // 입력 받은 string으로 xml 파일 검색
                for (int index = 0; index < tagNameArray.Length; index++)
                {
                    // 결과 List 생성
                    List<ParserResultModel> resultList = new List<ParserResultModel>();

                    // 입력받은 tagName과 일치하는 모든 하위 요소의 목록이 포함된 XmlNodeList 
                    XmlNodeList xmlList = xmlDoc.GetElementsByTagName(tagNameArray[index]);

                    // xmlList의 값을 결과 리스트에 추가
                    AddResultList(xmlList, resultList);

                    // tagName과 해당 tagName의 결과 리스트 추가
                    resultDic.Add(tagNameArray[index], resultList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return resultDic;
        }

        private void AddResultList(XmlNodeList xmlList, List<ParserResultModel> resultList)
        {
            // xmlList 수 만큼 반복
            for (int nodeIndex = 0; nodeIndex < xmlList.Count; nodeIndex++)
            {
                // currentNode 생성
                ParserResultModel currentNode = new ParserResultModel(nodeIndex, xmlList[nodeIndex].LocalName);

                // xmlNode의 Attributes가 1개 이상이면
                if (xmlList[nodeIndex].Attributes.Count >= 1)
                {
                    // currentNode의 Attributes추가
                    AddAttributes(xmlList[nodeIndex].Attributes, currentNode);
                }
                else { }

                // xmlNode의 자식노드가 있으면 true
                if (xmlList[nodeIndex].HasChildNodes)
                {
                    switch (xmlList[nodeIndex].FirstChild.NodeType)
                    {
                        // 자식노드의 타입이 Text이면 currentNode의 InnerText 추가
                        case XmlNodeType.Text:
                            currentNode.InnerText = xmlList[nodeIndex].InnerText;
                            break;
                        // 자식노드의 타입이 Element이면 currentNode의 자식노드리스트 생성 후 함수 실행
                        case XmlNodeType.Element:
                            {
                                currentNode.ChildNodeList = new List<ParserResultModel>();

                                AddResultList(xmlList[nodeIndex].ChildNodes, currentNode.ChildNodeList);
                            }
                            break;
                        default:
                            break;
                    }
                }
                else { }

                resultList.Add(currentNode);
            }
        }

        private void AddAttributes(XmlAttributeCollection xmlAttribute, ParserResultModel currentNode)
        {
            // Attributes 생성
            currentNode.Attributes = new Dictionary<string, string>();

            // Attributes 수 만큼 반복
            for (int index = 0; index < xmlAttribute.Count; index++)
            {
                // currentNode의 Attributes 추가
                currentNode.Attributes.Add(xmlAttribute[index].Name, xmlAttribute[index].Value);
            }
        }
    }
}
