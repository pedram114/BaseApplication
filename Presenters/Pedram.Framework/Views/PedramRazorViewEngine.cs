using Pedram.Core.Domain.NotDbClasses.BreadCrumbs;
using Pedram.Framework.Helpers;
using Pedram.Framework.StartUpClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Pedram.Framework.Views
    {
    public class PedramRazorViewEngine : RazorViewEngine
        {

        public static ILanguageHelper _ILanguageHelper;
        private IContextHelper _IContextHelper;
        public PedramRazorViewEngine() {
            _IContextHelper = SmObjectFactory.Container.GetInstance<IContextHelper>();
            _ILanguageHelper = SmObjectFactory.Container.GetInstance<ILanguageHelper>();
        }
        protected override IView CreatePartialView( ControllerContext controllerContext, string partialPath )
            {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView( controllerContext, partialPath, null, false, fileExtensions );
            //return new RazorView(controllerContext, partialPath, layoutPath, runViewStartPages, fileExtensions, base.ViewPageActivator);
            }

        protected override IView CreateView( ControllerContext controllerContext, string viewPath, string masterPath )
            {
            
            var cn = controllerContext.RequestContext.RouteData.Values["controller"].ToString();
            var view = controllerContext.RequestContext.RouteData.Values["action"].ToString();
            string ActionDescription = "";
            try {
                ActionDescription = ControllerHelper.GetActionList(controllerContext.Controller.GetType()).Where(d => d.Name == view).FirstOrDefault().Description;
            }
            catch { }

            _IContextHelper.GetCurrentContext().Breadcrumbs = new List<BreadCrumbModel>();
         
            if (cn.ToLower().Equals("home") && view.ToLower().Equals("index")) {
                _IContextHelper.GetCurrentContext().Breadcrumbs.Clear();
                _IContextHelper.GetCurrentContext().PageTitle = "";
            }
            else
            {
                if (_IContextHelper.GetCurrentContext() != null)
                {
                    _IContextHelper.GetCurrentContext().Breadcrumbs.Add(new BreadCrumbModel()
                    {
                        Address = new BreadCrumbAddress()
                        {
                            ActionName = "index",
                            ControllerName = "home",
                        },
                        ShowText = ""
                    });
                    _IContextHelper.GetCurrentContext().Breadcrumbs.Add(new BreadCrumbModel()
                    {
                        Address = new BreadCrumbAddress()
                        {
                            ActionName = view,
                            ControllerName = cn,
                        },
                        ShowText = ActionDescription
                    });
                    _IContextHelper.GetCurrentContext().PageTitle = ActionDescription;
                }
            }
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView( controllerContext, viewPath, masterPath, true, fileExtensions );
            }

        }
   
    }
