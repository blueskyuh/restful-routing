using System.Web.Mvc;
using RestfulRouting.Exceptions;

namespace RestfulRouting.Format.ActionResultExposal
{
    public class ActionResultExposer
    {
        readonly FormatCollection formatCollection;

        public ActionResultExposer(FormatCollection formatCollection)
        {
            this.formatCollection = formatCollection;
        }

        public ActionResult Html()
        {
            return ExecuteAction(FormatCollection.HtmlKey);
        }

        public ActionResult Json()
        {
            return ExecuteAction(FormatCollection.JsonKey);
        }

        public ActionResult Js()
        {
            return ExecuteAction(FormatCollection.JsKey);
        }

        private ActionResult ExecuteAction(string key)
        {
            if (!this.formatCollection.ContainsKey(key))
            {
                string message = string.Format("Format you are trying to get is not registered. Requested format is {0}. Registered formats are {1}",
                        key, string.Join(", ", this.formatCollection.Keys));
                throw new NotRegisteredFormatException(message);
            }

            return this.formatCollection[key]();
        }

        public ActionResult Xml()
        {
            return ExecuteAction(FormatCollection.XmlKey);
        }

        public ActionResult Csv()
        {
            return ExecuteAction(FormatCollection.CsvKey);
        }
    }
}