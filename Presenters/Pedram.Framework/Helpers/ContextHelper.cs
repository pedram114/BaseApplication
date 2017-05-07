
using Pedram.Core.Domain.Users;
using Pedram.Framework.StartUpClasses;
using Pedram.Services.Services.Configuration.Interfaces;
using Pedram.Services.Services.Language.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Pedram.Framework.Helpers
{
    public class ContextHelper : IContextHelper
    {
        ILanguageService _ILanguageService;
        ILanguageHelper _ILanguageHelper;
        IGlobalUsersContext _IGlobalUsersContext;
        IConfigurationService _IConfigurationService;
        public ContextHelper(IGlobalUsersContext IGlobalUsersContext,
                             ILanguageHelper ILanguageHelper, 
                             ILanguageService ILanguageService,
                             IConfigurationService IConfigurationService)
        {
            _ILanguageService = ILanguageService;
            _IGlobalUsersContext = IGlobalUsersContext;
            _ILanguageHelper = ILanguageHelper;
            _IConfigurationService = IConfigurationService;
        }
        public IUserContext GetCurrentContext()
        {
            var model = ((IUserContext)HttpContext.Current.Session[HttpContext.Current.Session.SessionID]);

            if (model == null)
                model = SmObjectFactory.Container.GetInstance<IUserContext>();
            if (model.MyLanguage == null)
                model.MyLanguage = _ILanguageHelper.GetCurrentLanguage();
            if (model.MyLanguage == null)
                model.MyLanguage = _ILanguageService.GetDefaultLanguage();


            return ((IUserContext)HttpContext.Current.Session[HttpContext.Current.Session.SessionID]);


        }
        public IUserContext CreateContext()
        {
            var mm = SmObjectFactory.Container.GetInstance<IUserContext>();
            mm.SessionId = HttpContext.Current.Session.SessionID;
            HttpContext.Current.Session[mm.SessionId] = mm;
            return GetCurrentContext();

        }

        public IGlobalUsersContext GetGlobalUsersContext()
        {

            return _IGlobalUsersContext;
        }

        

        public void PostTransaction() {
            th();
            //Thread nthread = new Thread(new ThreadStart(th));
         //   nthread.Start();
          
        }
        private void th() {
            ReadStartUpData.ReadAllLocalResources();            
        }

       




    }
}
