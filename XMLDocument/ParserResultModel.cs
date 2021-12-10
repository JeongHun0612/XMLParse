using System.Collections.Generic;
using System.Xml;

namespace XMLDocument
{
    class ParserResultModel
    {
        public ParserResultModel()
        {

        }

        public ParserResultModel(int parentNodeIndex, XmlNode parentNoed, string localName)
        {
            this.parentNodeIndex = parentNodeIndex;
            this.parentNode = parentNoed;
            this.localName = localName;

            this.childNodeList = new List<ParserResultModel>();
        }

        public ParserResultModel(int parentNodeIndex, XmlNode parentNoed, string localName, string innerText)
        {
            this.parentNodeIndex = parentNodeIndex;
            this.parentNode = parentNoed;
            this.localName = localName;
            this.innerText = innerText;
        }

        private int parentNodeIndex = 0;
        public int ParentNodeIndex { get => parentNodeIndex; set => parentNodeIndex = value; }

        private XmlNode parentNode;
        public XmlNode ParentNode { get => parentNode; set => parentNode = value; }

        private string localName = string.Empty;
        public string LocalName { get => localName; set => localName = value; }

        private string innerText = string.Empty;
        public string InnerText { get => innerText; set => innerText = value; }

        private Dictionary<string, string> attributes = null;
        public Dictionary<string, string> Attributes { get => attributes; set => attributes = value; }

        private List<ParserResultModel> childNodeList;
        public List<ParserResultModel> ChildNodeList { get => childNodeList; set => childNodeList = value; }
    }
}
