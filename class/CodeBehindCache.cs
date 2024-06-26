using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Xml;

namespace CodeBehind
{
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

        public bool ControllerHasCache(string ControllerName)
        {
            foreach (Cache cache in CacheList.Caches)
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

            foreach (Cache cache in CacheList.Caches)
            {
                if (cache.ControllerName == ControllerName)
                {
                    if (!string.IsNullOrEmpty(cache.Path))
                        if (!Path.HasMatching(cache.PathMatchType, cache.Path))
                            return false;

                    if (!string.IsNullOrEmpty(cache.Query))
                        if (!QueryString.HasMatching(cache.QueryMatchType, cache.Query))
                            return false;

                    if (!string.IsNullOrEmpty(cache.FormData))
                        if (!FormData.HasMatching(cache.FormDataMatchType, cache.FormData))
                            return false;

                    return true;
                }
            }

            return false;
        }
    }

    public class CodeBehindViewCache
    {
        public int Duration { get; set; }

        public bool ViewHasCache(string ViewPath)
        {
            foreach (Cache cache in CacheList.Caches)
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

            foreach (Cache cache in CacheList.Caches)
            {
                if (cache.ViewPath == ViewPath)
                {
                    if (!string.IsNullOrEmpty(cache.Path))
                        if (!Path.HasMatching(cache.PathMatchType, cache.Path))
                            return false;

                    if (!string.IsNullOrEmpty(cache.Query))
                        if (!QueryString.HasMatching(cache.QueryMatchType, cache.Query))
                            return false;

                    if (!string.IsNullOrEmpty(cache.FormData))
                        if (!FormData.HasMatching(cache.FormDataMatchType, cache.FormData))
                            return false;

                    return true;
                }
            }

            return false;
        }

    }

    public class FillCacheList
    {
        public void Set()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("code_behind/cache.xml");

            XmlNodeList NodeList = doc.SelectSingleNode("cache_list").ChildNodes;

            foreach (XmlNode node in NodeList)
            {
                bool CacheIsActive = node.Attributes["active"] == null;

                if (!CacheIsActive)
                    CacheIsActive = node.Attributes["active"].Value == "true";

                if (CacheIsActive)
                {
                    Cache cache = new Cache();
                    cache.Duration = node.Attributes["duration"].Value.ToNumber();

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

                    CacheList.Caches.Add(cache);
                }
            }
        }
    }

    public static class CacheList
    {
        public static List<Cache> Caches = new List<Cache>();
    }

    public class Cache
    {
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