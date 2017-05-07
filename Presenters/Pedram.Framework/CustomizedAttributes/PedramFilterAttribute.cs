using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace Pedram.Framework.CustomizedAttributes
{
    public class PedramFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            modifyUrlAndRedirectPermanent(filterContext);
            base.OnActionExecuting(filterContext);
        }

        private static void modifyUrlAndRedirectPermanent(ActionExecutingContext filterContext)
        {
            if (canIgnoreRequest(filterContext))
                return;

            var absoluteUrl = HttpUtility.UrlDecode(filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri.ToString(CultureInfo.InvariantCulture));
            var absoluteUrlToLower = absoluteUrl.ToLowerInvariant();

            absoluteUrlToLower = forceWwwAndLowercase(filterContext, absoluteUrlToLower);
            absoluteUrlToLower = avoidTrailingSlashes(filterContext, absoluteUrlToLower);

            if (!absoluteUrl.Equals(absoluteUrlToLower))
            {
                filterContext.Result = new RedirectResult(absoluteUrlToLower, permanent: true);
            }
        }

        private static string avoidTrailingSlashes(ActionExecutingContext filterContext, string absoluteUrlToLower)
        {
            if (!isRootRequest(filterContext) && absoluteUrlToLower.EndsWith("/"))
                return absoluteUrlToLower.TrimEnd(new[] { '/' });

            return absoluteUrlToLower;
        }

        private static bool isRootRequest(ActionExecutingContext filterContext)
        {
            return filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath == "/";
        }

        private static bool canIgnoreRequest(ActionExecutingContext filterContext)
        {
            return filterContext.IsChildAction ||
                   filterContext.HttpContext.Request.IsAjaxRequest() ||
                   filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri.Contains("?");
        }

        private static string forceWwwAndLowercase(ActionExecutingContext filterContext, string absoluteUrlToLower)
        {
            if (isLocalRequet(filterContext))
                return absoluteUrlToLower;

            if (absoluteUrlToLower.Contains("www"))
                return absoluteUrlToLower;

            return absoluteUrlToLower.Replace("http://", "http://www.")
                                     .Replace("https://", "https://www.");
        }

        private static bool isLocalRequet(ActionExecutingContext filterContext)
        {
            return filterContext.RequestContext.HttpContext.Request.IsLocal;
        }
    }
}
