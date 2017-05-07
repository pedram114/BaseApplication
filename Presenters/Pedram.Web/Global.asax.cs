using Pedram.Core;
using Pedram.Core.Domain.Language;
using Pedram.Data;
using Pedram.Data.Migrations;
using Pedram.Framework.Controller;
using Pedram.Framework.Helpers;
using Pedram.Framework.MyCodes;
using Pedram.Framework.StartUpClasses;
using Pedram.Framework.Theme;
using Pedram.Framework.Views;
using Pedram.Services.Services.Configuration.Interfaces;
using Pedram.Web.App_Start;
using Pedram.Web.Models.Management;
using StructureMap.Web.Pipeline;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Pedram.Web
    {
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        private IContextHelper _IContextHelper;
        private ILanguageHelper _ILanguageHelper;
        protected void Application_Start()
        {
            //    WebApiConfig.Register( GlobalConfiguration.Configuration );
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
            RouteConfig.RegisterRoutes( RouteTable.Routes );
            //    BundleConfig.RegisterBundles( BundleTable.Bundles );

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add( new PedramRazorViewEngine() );


            setDbInitializer();
            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory( new StructureMapControllerFactory() );
            // Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = SmObjectFactory.Container.GetInstance<Microsoft.AspNet.SignalR.IDependencyResolver>();

            MvcHandler.DisableMvcResponseHeader = true;
            ReadStartUpData.ReadAllLanguages();
            ReadStartUpData.ReadAllLocalResources();
            ReadAllConfigs();
            Mappers.CreateMapper();
            ViewEngines.Engines.OfType<RazorViewEngine>().Single().Themeable();

            Pedram.Web.Models.Management.Configurations.WebApplicationDescription = "Iran Dejac Desc";
            Pedram.Web.Models.Management.Configurations.WebApplicationName = "Iran Dejac";



        }
        private void ReadAllConfigs() {
            InlineUsefullMethods.ReadConfigs();
        }
        protected void Application_EndRequest( object sender, EventArgs e )
            {
            HttpContextLifecycle.DisposeAndClearAll();
            }
        protected void Session_Start() {
            _IContextHelper = SmObjectFactory.Container.GetInstance<IContextHelper>();
            _ILanguageHelper = SmObjectFactory.Container.GetInstance<ILanguageHelper>();
            SmObjectFactory.Container.GetInstance<IContextHelper>().CreateContext();
            ReadStartUpData.SetDefaultLanguage();
            var culture = SmObjectFactory.Container.GetInstance<ILanguageHelper>().GetCurrentLanguage().LangCalture;
            var ci = CultureInfo.GetCultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var cookie = new HttpCookie("culture", ci.Name);
            Response.Cookies.Add(cookie);
            var cx = _IContextHelper.GetCurrentContext();
            if (cx == null)
                cx = _IContextHelper.CreateContext();
            cx.MyLanguage = _ILanguageHelper.GetCurrentLanguage();
            // new InitialCodes().InitialFirstData();
        }
        protected void Application_BeginRequest()
            {

            
            }

        public class StructureMapControllerFactory : DefaultControllerFactory
            {
            protected override IController GetControllerInstance( RequestContext requestContext, Type controllerType )
                {

                if (controllerType == null)
                {
                    
                    throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));
                }
                return SmObjectFactory.Container.GetInstance( controllerType ) as Controller;
                }
            }

        private static void setDbInitializer()
            {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<PedramDbContext, Configuration>() );
            SmObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
            }
        }
}
