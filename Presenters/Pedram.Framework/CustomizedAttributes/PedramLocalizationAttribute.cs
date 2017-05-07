using Newtonsoft.Json;
using Pedram.Core.Domain.Language;
using Pedram.Core.Domain.Users;
using Pedram.Framework.StartUpClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Pedram.Framework.CustomizedAttributes
    {
    public class PedramLocalizationAttribute : ActionFilterAttribute
        {
        public override void OnActionExecuting( ActionExecutingContext filterContext )
            {
            var serializer = new JavaScriptSerializer();
            var savedModel = serializer.Deserialize<Language>( HttpContext.Current.Request.Cookies["DefaultLan"].Value );

            filterContext.RouteData.Values["lang"] = savedModel.LangCalture.Split( '-' )[0];
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture( savedModel.LangCalture );
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture( savedModel.LangCalture );
            base.OnActionExecuting( filterContext );
            }
        }
    }
