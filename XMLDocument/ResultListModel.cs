using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLDocument
{
    class ResultListModel
    {
        public ResultListModel(string locaName, string innerText)
        {
            this.localName = locaName;
            this.innerText = innerText;
        }

        public string localName = string.Empty;
        public string innerText = string.Empty;

        public List<ResultListModel> resultDataChild = new List<ResultListModel>();
    }
}
