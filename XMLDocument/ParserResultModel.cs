using System.Collections.Generic;
using System.Xml;

namespace XMLDocument
{
    class ParserResultModel
    {
        public ParserResultModel()
        {

        }

        public ParserResultModel(int parentNodeIndex, string localName)
        {
            this.parentNodeIndex = parentNodeIndex;
            this.localName = localName;
        }

        public ParserResultModel(int parentNodeIndex, string localName, string innerText)
        {
            this.parentNodeIndex = parentNodeIndex;
            this.localName = localName;
            this.innerText = innerText;
        }

        private int parentNodeIndex = 0;
        public int ParentNodeIndex { get => parentNodeIndex; set => parentNodeIndex = value; }

        private string localName = string.Empty;
        public string LocalName { get => localName; set => localName = value; }

        private string innerText = string.Empty;
        public string InnerText { get => innerText; set => innerText = value; }

        private Dictionary<string, string> attributes = null;
        public Dictionary<string, string> Attributes { get => attributes; set => attributes = value; }

        private List<ParserResultModel> childNodeList = null;
        public List<ParserResultModel> ChildNodeList { get => childNodeList; set => childNodeList = value; }
    }
}
