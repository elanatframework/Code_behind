using CodeBehind.HtmlData;
using Microsoft.AspNetCore.Http;

namespace CodeBehind
{
    internal class RequestQuery
    {
        internal void AddQueryString(HttpContext context, string QueryString)
        {
            if (string.IsNullOrEmpty(QueryString))
                return;

            NameCollection QueryValues = new NameCollection();
            QueryString TmpQueryString = new QueryString();
            string[] QueryElements = QueryString.Split('&');
            foreach (string element in QueryElements)
            {
                string[] NameValue = element.Split('=');

                if (NameValue.Length > 1)
                    TmpQueryString = TmpQueryString.Add(NameValue[0], NameValue[1]);
                else
                    TmpQueryString = TmpQueryString.Add(NameValue[0], "");

                QueryValues.Add(NameValue[0]);
            }

            string RequestQueryString = context.Request.QueryString.Value;

            if (!string.IsNullOrEmpty(RequestQueryString))
            {
                RequestQueryString = RequestQueryString.GetTextAfterValue("?");
                string[] TmpQueryElements = RequestQueryString.Split('&');
                foreach (string element in TmpQueryElements)
                {
                    string[] NameValue = element.Split('=');

                    if (!QueryValues.Exist(NameValue[0]))
                        if (NameValue.Length > 1)
                            TmpQueryString = TmpQueryString.Add(NameValue[0], NameValue[1]);
                        else
                            TmpQueryString = TmpQueryString.Add(NameValue[0], "");
                }
            }

            context.Request.QueryString = TmpQueryString;
        }
    }
}
