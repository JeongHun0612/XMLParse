using System.Collections.Generic;

namespace XMLDocument
{
    class ParserResultModel
    {
        public ParserResultModel()
        {
            this.Attributes = new Dictionary<string, string>();
        }

        public ParserResultModel(int depth, string localName, string innerText)
        {
            this.Depth = depth;
            this.LocalName = localName;
            this.InnerText = innerText;
            this.Attributes = new Dictionary<string, string>();
        }

        public int Depth;
        public string LocalName = string.Empty;
        public string InnerText = string.Empty;
        public Dictionary<string, string> Attributes { get; set; }
    }
}
