using Pedram.Core.Domain.Language;
using Pedram.Core.Domain.Users;
using Pedram.Framework.StartUpClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Pedram.Framework.Helpers
    {
    public class OtherHelpers
        {
        public static void StoreModelToCookies<T>( string cookieName, T newModel ) where T : class
            {
            var serializer = new JavaScriptSerializer();
            HttpCookie cookie;
            T savedModel;
            if (HttpContext.Current.Request.Cookies[cookieName] == null)
                {
                cookie = new HttpCookie( cookieName );
                savedModel = newModel;
                }
            else
                {
                cookie = HttpContext.Current.Request.Cookies[cookieName];
                savedModel = serializer.Deserialize<T>( cookie.Value );
                Type type = typeof( T );
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties( System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance ))
                    {
                    object currentValue = type.GetProperty( pi.Name ).GetValue( savedModel, null );
                    object newValue = type.GetProperty( pi.Name ).GetValue( newModel, null );

                    if (currentValue != newValue && (currentValue == null || !currentValue.Equals( newValue )))
                        {
                        type.GetProperty( pi.Name ).SetValue( savedModel, newValue, null );
                        }
                    }
                }
            cookie.Value = serializer.Serialize( savedModel );
            HttpContext.Current.Response.Cookies.Add( cookie );
            }
        public static string GenerateNewId()
        {
            Thread.Sleep(1);
            var Rand1 = new Random((int)DateTime.Now.Ticks).Next(999999999);
            return (Rand1.ToString());

        }
    }
    }
