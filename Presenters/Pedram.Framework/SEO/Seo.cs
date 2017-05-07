﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pedram.Framework.SEO
{
    public static class Seo
    {
        public enum CacheControlType
        {
            [Description("public")]
            _public,
            [Description("private")]
            _private,
            [Description("no-cache")]
            _nocache,
            [Description("no-store")]
            _nostore
        }

        private const string SeparatorTitle = " - ";
        private const int MaxLenghtTitle = 60;
        public static string GeneratePageTitle(params string[] crumbs)
        {
            var title = "";

            for (int i = 0; i < crumbs.Length; i++)
            {
                title += string.Format
                            (
                                "{0}{1}",
                                crumbs[i],
                                (i < crumbs.Length - 1) ? SeparatorTitle : string.Empty
                            );
            }

            title = title.Substring(0, title.Length <= MaxLenghtTitle ? title.Length : MaxLenghtTitle).Trim();

            return title;
        }
        private const int MaxLenghtDescription = 170;
        private const string FaviconPath = "~\favicon.ico";
        public static string GenerateMetaTag(
            string title="Title", 
            string description="Desc", 
            bool allowIndexPage=true, 
            bool allowFollowLinks=true, 
            string author = "", 
            string lastmodified = "", 
            string expires = "never", 
            string language = "fa", 
            CacheControlType cacheControlType = CacheControlType._private,
            string copywrite = "Dejac",
            string keywords =""
            )
        {
            title = title.Substring(0, title.Length <= MaxLenghtTitle ? title.Length : MaxLenghtTitle).Trim();
            try
            {
                description = description.Substring(0, description.Length <= MaxLenghtDescription ? description.Length : MaxLenghtDescription).Trim();
            }
            catch {
                description = "";
            }
            var meta = "";
            meta += string.Format("<title>{0}</title>\n", title);
            //meta += string.Format("<link rel=\"shortcut icon\" href=\"{0}\"/>\n", FaviconPath);
            //meta += string.Format("<meta http-equiv=\"content-language\" content=\"{0}\"/>\n", language);
            meta += string.Format("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\"/>\n");
            //meta += string.Format("<meta charset=\"utf-8\"/>\n");
            meta += string.Format("<meta name=\"description\" content=\"{0}\"/>\n", description);
            //meta += string.Format("<meta http-equiv=\"cache-control\" content=\"{0}\"/>\n", EnumExtensions.EnumHelper<CacheControlType>.GetEnumDescription(cacheControlType.ToString()));
            meta += string.Format("<meta name=\"robots\" content=\"{0}, {1}\" />\n", allowIndexPage ? "index" : "noindex", allowFollowLinks ? "follow" : "nofollow");
            meta += string.Format("<meta name=\"expires\" content=\"{0}\"/>\n", expires);
            meta += string.Format("<meta name=\"copyright\" content=\"{0}\"/>\n", copywrite);
            if (!string.IsNullOrEmpty(lastmodified))
                meta += string.Format("<meta name=\"last-modified\" content=\"{0}\"/>\n", lastmodified);

            if (!string.IsNullOrEmpty(author))
                meta += string.Format("<meta name=\"author\" content=\"{0}\"/>\n", author);

            //------------------------------------Google & Bing Doesn't Use Meta Keywords ...
            if (!string.IsNullOrEmpty(keywords))
                meta += string.Format("<meta name=\"keywords\" content=\"{0}\"/>\n", keywords);

            return meta;
        }



        private const int MaxLenghtSlug = 45;
        public static string GenerateSlug(string title)
        {
            var slug = RemoveAccent(title).ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
            slug = Regex.Replace(slug, @"\s+", "-").Trim();
            slug = Regex.Replace(slug, @"-+", "-");
            slug = slug.Substring(0, slug.Length <= MaxLenghtSlug ? slug.Length : MaxLenghtSlug).Trim();

            return slug;
        }

        private static string RemoveAccent(string text)
        {
            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
