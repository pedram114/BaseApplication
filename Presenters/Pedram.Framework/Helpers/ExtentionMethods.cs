using Pedram.Framework.StartUpClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Pedram.Framework.Helpers
    {
    public static class ExtentionMethods
        {
        public static ILanguageHelper _ILanguageHelper;
        
        public static string GetValue( this HtmlHelper helper, string ResourceName )
            {

            if (_ILanguageHelper == null)
                _ILanguageHelper = SmObjectFactory.Container.GetInstance<ILanguageHelper>();

            if (ResourceName != null) {
                try
                    {
                    return _ILanguageHelper.GetResource( ResourceName );
                    }
                catch (Exception e) {
                    return "NotFound";
                    }
                }
            return "NotFound";
            }
       
    }
    }
