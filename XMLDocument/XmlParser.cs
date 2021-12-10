﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XMLDocument
{
    class XmlParser
    {
        List<ParserResultModel> resultList = new List<ParserResultModel>(); // 결과 데이터 리스트 생성
        XmlDocument xmlDoc = new XmlDocument();                             // XmlDocument 클래스 생성
        ParserResultModel currentNode = null;                               // 결과 데이터 클래스 초기화

        public List<ParserResultModel> XmlSelectParser(string filePath, string tagNameArray)
        {
            // 결과 데이터 리스트 제거
            resultList.Clear();

            // Xml 파일 로드
            xmlDoc.Load(filePath);

            // 입력 받은 string으로 xml 파일 검색
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(tagNameArray);

            ParserResultModel currentNode = new ParserResultModel();

            AddResultListTest(xmlList, currentNode);

            //resultList.Add(currentNode);


            // 검색된 태그 수 만큼 반복
            //for (int i = 0; i < xmlList.Count; i++)
            //{
            //    ParserResultModel currentNode = new ParserResultModel();

            //    if (xmlList[i].HasChildNodes)
            //    {
            //        // 자식이 없는 노드
            //        if (xmlList[i].ChildNodes[0].NodeType == XmlNodeType.Text)
            //        {
            //            currentNode = new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName, xmlList[i].InnerText);
            //        }
            //        // 자식이 있는 노드
            //        else if (xmlList[i].ChildNodes[0].NodeType == XmlNodeType.Element)
            //        {
            //            currentNode = new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName);
            //            AddResultListTest(xmlList[i].ChildNodes, currentNode);
            //        }
            //        else { }

            //        resultList.Add(currentNode);
            //    }
            //    else { }
            //}

            return resultList;
        }

        private void AddResultListTest(XmlNodeList xmlList, ParserResultModel currentNode)
        {
            for (int i = 0; i < xmlList.Count; i++)
            {
                if (xmlList[i].HasChildNodes)
                {
                    // 자식이 없는 노드
                    if (xmlList[i].ChildNodes[0].NodeType == XmlNodeType.Text)
                    {
                        if (currentNode.ChildNodeList == null)
                        {
                            currentNode = new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName, xmlList[i].InnerText);
                        }
                        else
                        {
                            currentNode.ChildNodeList.Add(new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName, xmlList[i].InnerText));
                        }
                    }
                    // 자식이 있는 노드
                    else if (xmlList[i].ChildNodes[0].NodeType == XmlNodeType.Element)
                    {
                        if (currentNode.ChildNodeList == null)
                        {
                            currentNode = new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName);
                        }
                        else
                        {
                            currentNode.ChildNodeList.Add(new ParserResultModel(i, xmlList[i].ParentNode, xmlList[i].LocalName));
                        }

                        AddResultListTest(xmlList[i].ChildNodes, currentNode);
                    }
                    else { }
                }
            }
        }

        public List<ParserResultModel> XmlAllParser(string filePath)
        {
            // 결과 데이터 리스트 제거
            resultList.Clear();

            // Xml 파일 로드
            xmlDoc.Load(filePath);

            // 최상위 노드 검색 (xml)
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(xmlDoc.DocumentElement.LocalName);

            //AddResultList(xmlList, 0);

            return resultList;
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
                        AddAttributes(xmlList[i].Attributes);
                    }
                    else { }
                }
                else { }

                // 자식노드가 없을때까지 반복
                AddResultList(xmlList[i].ChildNodes, currentNode, parentNodeIndex);
            }
        }

        private void AddAttributes(XmlAttributeCollection xmlAttribute)
        {
            for (int i = 0; i < xmlAttribute.Count; i++)
            {
                currentNode.Attributes.Add(xmlAttribute[i].Name, xmlAttribute[i].Value);
            }
        }

        // 사용 클래스 : XmlReader
        // 반환값 : List<ParserResultModel>
        //public static List<ParserResultModel> XmlAllParser(string filePath)
        //{
        //    // 결과 데이터 저장 리스트 생성
        //    List<ParserResultModel> resultList = new List<ParserResultModel>();

        //    // 현재 노드 초기화
        //    ParserResultModel currentNode = null;

        //    try
        //    {
        //        XmlReaderSettings setting = new XmlReaderSettings();

        //        setting.IgnoreComments = true;    // <!--주석--> 무시
        //        setting.IgnoreWhitespace = true;  // enter 키 공백 무시

        //        using (XmlReader xmlReader = XmlReader.Create(filePath, setting)) // XmlReader 생성
        //        {
        //            // 읽을 노드가 없으면 false
        //            while (xmlReader.Read())
        //            {
        //                switch (xmlReader.NodeType)
        //                {
        //                    // Element : 요소  ex) <item>
        //                    case XmlNodeType.Element:

        //                        // 현재 노드 LocalName 저장
        //                        currentNode = new ParserResultModel(currentNode) { LocalName = xmlReader.Name };
        //                        resultList.Add(currentNode);

        //                        // 다음 Attribute가 없으면 false
        //                        while (xmlReader.MoveToNextAttribute())
        //                        {
        //                            // 현재 노드 Attributes Dictionory에 Name, Value 추가
        //                            currentNode.Attributes.Add(xmlReader.Name, xmlReader.Value);
        //                        }
        //                        break;

        //                    // EndElement : 끝 요소 태그  ex) </item>
        //                    case XmlNodeType.EndElement:
        //                        if (currentNode.Parent != null)
        //                        {
        //                            // 부모 노드 유지
        //                            currentNode = currentNode.Parent;
        //                        }
        //                        break;

        //                    // Text : 노드의 텍스트 내용
        //                    case XmlNodeType.Text:

        //                        // 현재 노드 InnerText 추가
        //                        currentNode.InnerText = xmlReader.Value;
        //                        break;
        //                    default:
        //                        break;
        //                }
        //            }

        //            // XmlReader 닫기
        //            xmlReader.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return null;
        //    }

        //    return resultList;
        //}
    }
}
