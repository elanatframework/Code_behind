using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Xml;

namespace CodeBehind
{
    public class PersonalCache
    {
        private readonly IMemoryCache _Cache;

        public PersonalCache(HttpContext context)
        {
            _Cache = context.RequestServices.GetService<IMemoryCache>();
        }

        public void SetPersonalCache(string PersonalName, string ResponseResult, int Duration)
        {
            _Cache.Set("code_behind_cache_personal_" + PersonalName, ResponseResult, TimeSpan.FromSeconds(Duration));
        }

        public bool PersonalHasCache { get; private set; }
        public string GetPersonalCache(string PersonalName)
        {
            if (_Cache.TryGetValue("code_behind_cache_personal_" + PersonalName, out string ResponseResult))
            {
                PersonalHasCache = true;
                return ResponseResult;
            }

            PersonalHasCache = false;
            return null;
        }
    }

    public class ControllerCache
    {
        private readonly IMemoryCache _Cache;

        public ControllerCache(HttpContext context)
        {
            _Cache = context.RequestServices.GetService<IMemoryCache>();
        }

        public void SetControllerCache(string ControllerName, string ResponseResult, int Duration)
        {
            _Cache.Set("code_behind_cache_controller_" + ControllerName, ResponseResult, TimeSpan.FromSeconds(Duration));
        }

        public bool ControllerHasCache { get; private set; }
        public string GetControllerCache(string ControllerName)
        {
            if (_Cache.TryGetValue("code_behind_cache_controller_" + ControllerName, out string ResponseResult))
            {
                ControllerHasCache = true;
                return ResponseResult;
            }

            ControllerHasCache = false;
            return null;
        }
    }

    public class ViewCache
    {
        private readonly IMemoryCache _Cache;

        public ViewCache(HttpContext context)
        {
            _Cache = context.RequestServices.GetService<IMemoryCache>();
        }

        public void SetViewCache(string ViewPath, string ResponseResult, int Duration)
        {
            _Cache.Set("code_behind_cache_view_" + ViewPath, ResponseResult, TimeSpan.FromSeconds(Duration));
        }

        public bool ViewHasCache { get; private set; }
        public string GetViewCache(string ViewPath)
        {
            if (_Cache.TryGetValue("code_behind_cache_view_" + ViewPath, out string ResponseResult))
            {
                ViewHasCache = true;
                return ResponseResult;
            }

            ViewHasCache = false;
            return null;
        }
    }

    public class CodeBehindControllerCache
    {
        public int Duration { get; set; }
        public string CacheFilter { get; set; }

        public bool ControllerHasCache(string ControllerName)
        {
            foreach (CacheProperties cache in CachePropertiesList.Caches)
                if (cache.ControllerName == ControllerName)
                {
                    Duration = cache.Duration;
                    return true;
                }

            return false;
        }

        public bool HasMatchingController(HttpRequest request, string ControllerName)
        {
            string Path = request.Path;
            string QueryString = request.QueryString.ToString();
            string FormData = "";

            try
            {
                FormData = request.Form.ToString();
            }
            catch (Exception)
            {
            }

            foreach (CacheProperties cache in CachePropertiesList.Caches)
            {
                if (cache.ControllerName == ControllerName)
                {
                    if (!string.IsNullOrEmpty(cache.Path))
                        if (!Path.HasMatching(cache.PathMatchType, cache.Path))
                            continue;
                        else
                            CacheFilter += "-path-" + cache.PathMatchType + "-" + cache.Path;

                    if (!string.IsNullOrEmpty(cache.Query))
                        if (!QueryString.HasMatching(cache.QueryMatchType, cache.Query))
                            continue;
                        else
                            CacheFilter += "-query-" + cache.QueryMatchType + "-" + cache.Query;

                    if (!string.IsNullOrEmpty(cache.FormData))
                        if (!FormData.HasMatching(cache.FormDataMatchType, cache.FormData))
                            continue;
                        else
                            CacheFilter += "-form_data-" + cache.FormDataMatchType + "-" + cache.FormData;

                    CacheFilter += "-index-" + cache.Id;

                    return true;
                }
            }

            return false;
        }
    }

    public class CodeBehindViewCache
    {
        public int Duration { get; set; }
        public string CacheFilter { get; set; }

        public bool ViewHasCache(string ViewPath)
        {
            foreach (CacheProperties cache in CachePropertiesList.Caches)
                if (cache.ViewPath == ViewPath)
                {
                    Duration = cache.Duration;
                    return true;
                }

            return false;
        }

        public bool HasMatchingView(HttpRequest request, string ViewPath)
        {
            string Path = request.Path;
            string QueryString = request.QueryString.ToString();
            string FormData = "";

            try
            {
                FormData = request.Form.ToString();
            }
            catch (Exception)
            {
            }

            foreach (CacheProperties cache in CachePropertiesList.Caches)
            {
                if (cache.ViewPath == ViewPath)
                {
                    if (!string.IsNullOrEmpty(cache.Path))
                        if (!Path.HasMatching(cache.PathMatchType, cache.Path))
                            continue;
                        else
                            CacheFilter += "-path-" + cache.PathMatchType + "-" + cache.Path;

                    if (!string.IsNullOrEmpty(cache.Query))
                        if (!QueryString.HasMatching(cache.QueryMatchType, cache.Query))
                            continue;
                        else
                            CacheFilter += "-query-" + cache.QueryMatchType + "-" + cache.Query;

                    if (!string.IsNullOrEmpty(cache.FormData))
                        if (!FormData.HasMatching(cache.FormDataMatchType, cache.FormData))
                            continue;
                        else
                            CacheFilter += "-form_data-" + cache.FormDataMatchType + "-" + cache.FormData;

                    CacheFilter += "-index-" + cache.Id;

                    return true;
                }
            }

            return false;
        }

    }

    internal class FillCacheList
    {
        internal void Set()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("code_behind/cache.xml");

            XmlNodeList NodeList = doc.SelectSingleNode("cache_list").ChildNodes;

            int Id = 0;

            foreach (XmlNode node in NodeList)
            {
                bool CacheIsActive = node.Attributes["active"] == null;

                if (!CacheIsActive)
                    CacheIsActive = node.Attributes["active"].Value == "true";

                if (CacheIsActive)
                {
                    CacheProperties cache = new CacheProperties();
                    cache.Duration = node.Attributes["duration"].Value.ToNumber();
                    cache.Id = Id++;

                    foreach (XmlNode CacheChild in node.ChildNodes)
                    {
                        if (CacheChild.Name == "controller")
                            cache.ControllerName = CacheChild.InnerText;

                        if (CacheChild.Name == "view")
                            cache.ViewPath = CacheChild.InnerText;

                        if (CacheChild.Name == "path")
                        {
                            cache.Path = CacheChild.InnerText;
                            cache.PathMatchType = CacheChild.Attributes["match_type"].Value;
                        }

                        if (CacheChild.Name == "query")
                        {
                            cache.Query = CacheChild.InnerText;
                            cache.QueryMatchType = CacheChild.Attributes["match_type"].Value;
                        }

                        if (CacheChild.Name == "form")
                        {
                            cache.FormData = CacheChild.InnerText;
                            cache.FormDataMatchType = CacheChild.Attributes["match_type"].Value;
                        }
                    }

                    CachePropertiesList.Caches.Add(cache);
                }
            }
        }
    }

    public static class CachePropertiesList
    {
        public static List<CacheProperties> Caches = new List<CacheProperties>();
    }

    public class CacheProperties
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string ControllerName { get; set; }
        public string ViewPath { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string FormData { get; set; }

        // Accept Values: regex, exist, start, end, full_match
        public string PathMatchType { get; set; }
        public string QueryMatchType { get; set; }
        public string FormDataMatchType { get; set; }
    }
}
