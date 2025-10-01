using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
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

        public void SetPersonalCache(string CacheParameter, string ResponseResult, int Duration)
        {
            _Cache.Set("code_behind_cache_personal_" + CacheParameter, ResponseResult, TimeSpan.FromSeconds(Duration));
        }

        public bool PersonalHasCache { get; private set; }
        public string GetPersonalCache(string CacheParameter)
        {
            if (_Cache.TryGetValue("code_behind_cache_personal_" + CacheParameter, out string ResponseResult))
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

        public void SetControllerCache(string CacheParameter, string ResponseResult, int Duration)
        {
            _Cache.Set("code_behind_cache_controller_" + CacheParameter, ResponseResult, TimeSpan.FromSeconds(Duration));
        }

        public bool ControllerHasCache { get; private set; }
        public string GetControllerCache(string CacheParameter)
        {
            if (_Cache.TryGetValue("code_behind_cache_controller_" + CacheParameter, out string ResponseResult))
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

        public void SetViewCache(string CacheParameter, string ResponseResult, int Duration)
        {
            _Cache.Set("code_behind_cache_view_" + CacheParameter, ResponseResult, TimeSpan.FromSeconds(Duration));
        }

        public bool ViewHasCache { get; private set; }
        public string GetViewCache(string CacheParameter)
        {
            if (_Cache.TryGetValue("code_behind_cache_view_" + CacheParameter, out string ResponseResult))
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
            catch (Exception) {}

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
            catch (Exception) { }

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

    public class ClientCache
    {
        private readonly IHeaderDictionary _headers;
        private readonly HttpContext _httpContext;

        public ClientCache(IHeaderDictionary headers)
        {
            _headers = headers ?? throw new ArgumentNullException(nameof(headers));
        }

        public ClientCache(HttpContext httpContext)
        {
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _headers = httpContext.Response.Headers;
        }

        /// <summary>
        /// Sets basic caching with private cache control
        /// </summary>
        /// <param name="duration">Duration in seconds</param>
        public void Set(int duration)
        {
            SetPrivate(duration);
        }

        /// <summary>
        /// Sets private caching (browser-only)
        /// </summary>
        /// <param name="duration">Duration in seconds</param>
        public void SetPrivate(int duration)
        {
            _headers["Cache-Control"] = $"private, max-age={duration}";
            _headers["Expires"] = DateTime.UtcNow.AddSeconds(duration).ToString("R");
            SetDefaultVaryHeaders();
        }

        /// <summary>
        /// Sets public caching (shared caches)
        /// </summary>
        /// <param name="duration">Duration in seconds</param>
        public void SetPublic(int duration)
        {
            _headers["Cache-Control"] = $"public, max-age={duration}";
            _headers["Expires"] = DateTime.UtcNow.AddSeconds(duration).ToString("R");
            SetDefaultVaryHeaders();
        }

        /// <summary>
        /// Sets no-cache headers (revalidate with server)
        /// </summary>
        public void SetNoCache()
        {
            _headers["Cache-Control"] = "no-cache, must-revalidate";
            _headers["Pragma"] = "no-cache";
            _headers["Expires"] = "0";
        }

        /// <summary>
        /// Sets no-store headers (don't cache at all)
        /// </summary>
        public void SetNoStore()
        {
            _headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            _headers["Pragma"] = "no-cache";
            _headers["Expires"] = "0";
        }

        /// <summary>
        /// Sets immutable content that never changes
        /// </summary>
        /// <param name="duration">Duration in seconds</param>
        public void SetImmutable(int duration)
        {
            _headers["Cache-Control"] = $"public, max-age={duration}, immutable";
            _headers["Expires"] = DateTime.UtcNow.AddSeconds(duration).ToString("R");
        }

        /// <summary>
        /// Sets stale-while-revalidate pattern for background revalidation
        /// </summary>
        /// <param name="maxAge">Maximum age in seconds</param>
        /// <param name="staleWhileRevalidate">Stale while revalidate period in seconds</param>
        public void SetStaleWhileRevalidate(int maxAge, int staleWhileRevalidate)
        {
            _headers["Cache-Control"] = $"public, max-age={maxAge}, stale-while-revalidate={staleWhileRevalidate}";
            _headers["Expires"] = DateTime.UtcNow.AddSeconds(maxAge).ToString("R");
        }

        /// <summary>
        /// Sets stale-if-error pattern for serving stale content on errors
        /// </summary>
        /// <param name="maxAge">Maximum age in seconds</param>
        /// <param name="staleIfError">Stale if error period in seconds</param>
        public void SetStaleIfError(int maxAge, int staleIfError)
        {
            _headers["Cache-Control"] = $"public, max-age={maxAge}, stale-if-error={staleIfError}";
            _headers["Expires"] = DateTime.UtcNow.AddSeconds(maxAge).ToString("R");
        }

        /// <summary>
        /// Sets ETag for conditional requests
        /// </summary>
        /// <param name="etag">ETag value (without quotes)</param>
        public void SetETag(string etag)
        {
            if (!string.IsNullOrEmpty(etag))
            {
                // Ensure ETag is properly formatted with quotes
                var formattedEtag = etag.StartsWith("\"") ? etag : $"\"{etag}\"";
                _headers["ETag"] = formattedEtag;
            }
        }

        /// <summary>
        /// Generates and sets ETag based on content hash
        /// </summary>
        /// <param name="content">Content bytes to hash</param>
        public void SetETagFromContent(byte[] content)
        {
            if (content != null && content.Length > 0)
            {
                using var sha256 = System.Security.Cryptography.SHA256.Create();
                var hash = sha256.ComputeHash(content);
                var etag = Convert.ToBase64String(hash)
                    .Replace("+", "-")
                    .Replace("/", "_")
                    .Replace("=", "");
                SetETag(etag);
            }
        }

        /// <summary>
        /// Generates and sets ETag based on string content
        /// </summary>
        /// <param name="content">String content to hash</param>
        public void SetETagFromString(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(content);
                SetETagFromContent(bytes);
            }
        }

        /// <summary>
        /// Generates ETag hash from content (without setting the header)
        /// </summary>
        /// <param name="content">Content to hash</param>
        /// <returns>ETag value without quotes</returns>
        public string GenerateETag(byte[] content)
        {
            if (content == null || content.Length == 0)
                return string.Empty;

            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hash = sha256.ComputeHash(content);
            return Convert.ToBase64String(hash)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }

        /// <summary>
        /// Generates ETag hash from string content (without setting the header)
        /// </summary>
        /// <param name="content">String content to hash</param>
        /// <returns>ETag value without quotes</returns>
        public string GenerateETag(string content)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;

            var bytes = System.Text.Encoding.UTF8.GetBytes(content);
            return GenerateETag(bytes);
        }

        /// <summary>
        /// Sets Last-Modified header
        /// </summary>
        /// <param name="lastModified">Last modified date</param>
        public void SetLastModified(DateTime lastModified)
        {
            _headers["Last-Modified"] = lastModified.ToUniversalTime().ToString("R");
        }

        /// <summary>
        /// Sets custom vary headers
        /// </summary>
        /// <param name="headers">Headers to vary by</param>
        public void SetVaryHeaders(params string[] headers)
        {
            if (headers != null && headers.Length > 0)
            {
                _headers["Vary"] = new StringValues(headers);
            }
        }

        /// <summary>
        /// Sets default vary headers for conditional requests
        /// </summary>
        public void SetDefaultVaryHeaders()
        {
            _headers["Vary"] = "Accept-Encoding, Accept";
        }

        /// <summary>
        /// Checks if client has fresh cached version using If-None-Match
        /// </summary>
        /// <param name="currentEtag">Current ETag of the resource (without quotes)</param>
        /// <returns>True if client has fresh version</returns>
        public bool IsFresh(string currentEtag)
        {
            if (_httpContext == null) return false;

            var ifNoneMatch = _httpContext.Request.Headers["If-None-Match"];
            if (string.IsNullOrEmpty(ifNoneMatch) || string.IsNullOrEmpty(currentEtag))
                return false;

            var formattedEtag = currentEtag.StartsWith("\"") ? currentEtag : $"\"{currentEtag}\"";
            return ifNoneMatch == formattedEtag;
        }

        /// <summary>
        /// Checks if client has fresh cached version using If-Modified-Since
        /// </summary>
        /// <param name="lastModified">Last modified date of the resource</param>
        /// <returns>True if client has fresh version</returns>
        public bool IsFresh(DateTime lastModified)
        {
            if (_httpContext == null) return false;

            var ifModifiedSince = _httpContext.Request.Headers["If-Modified-Since"];
            if (DateTime.TryParse(ifModifiedSince, out var clientModifiedSince))
            {
                return lastModified <= clientModifiedSince.ToUniversalTime();
            }
            return false;
        }

        /// <summary>
        /// Sends 304 Not Modified response if client has fresh cache
        /// </summary>
        /// <param name="currentEtag">Current ETag</param>
        /// <returns>True if 304 was sent</returns>
        public bool TrySendNotModified(string currentEtag)
        {
            if (_httpContext != null && IsFresh(currentEtag))
            {
                _httpContext.Response.StatusCode = 304;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sends 304 Not Modified response if client has fresh cache
        /// </summary>
        /// <param name="lastModified">Last modified date</param>
        /// <returns>True if 304 was sent</returns>
        public bool TrySendNotModified(DateTime lastModified)
        {
            if (_httpContext != null && IsFresh(lastModified))
            {
                _httpContext.Response.StatusCode = 304;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets cache headers for API responses with recommended defaults
        /// </summary>
        /// <param name="duration">Duration in seconds</param>
        public void SetApiCache(int duration)
        {
            SetPrivate(duration);
            _headers["Vary"] = "Accept, Accept-Encoding, Authorization";
        }

        /// <summary>
        /// Sets cache headers for static assets with long expiration
        /// </summary>
        /// <param name="duration">Duration in seconds (default: 1 year)</param>
        public void SetStaticCache(int duration = 31536000) // 1 year
        {
            SetPublic(duration);
            SetImmutable(duration);
        }

        /// <summary>
        /// Clears all cache headers
        /// </summary>
        public void Clear()
        {
            _headers.Remove("Cache-Control");
            _headers.Remove("Expires");
            _headers.Remove("Pragma");
            _headers.Remove("ETag");
            _headers.Remove("Last-Modified");
            _headers.Remove("Vary");
        }
    }
}
