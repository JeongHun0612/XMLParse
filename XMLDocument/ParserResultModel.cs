using System.Collections.Generic;
using System.Xml;

namespace XMLDocument
{
    class ParserResultModel
    {
        public ParserResultModel(XmlNode parentNoed, int parentNodeIndex, string localName, string innerText)
        {
            this.ParentNode = parentNoed;
            this.ParentNodeIndex = parentNodeIndex;
            this.LocalName = localName;
            this.InnerText = innerText;

            this.Attributes = new Dictionary<string, string>();
        }
    
        public int ParentNodeIndex = 0;
        public string LocalName = string.Empty;
        public string InnerText = string.Empty;

        public Dictionary<string, string> Attributes;
        public List<ParserResultModel> Item;
        public XmlNode ParentNode;
    }
}
