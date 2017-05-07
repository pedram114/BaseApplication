using Pedram.Core.Domain.Language;
using Pedram.Core.Domain.Users;
using Pedram.Services.Services.Language.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Pedram.Framework.Helpers
    {
    public class LanguageHelper : ILanguageHelper
        {
        ILocalResourceService _ILocalResourceService;
        IUserContext _IUserContext;
        ILanguageService _ILanguageService;
        public static bool UseCachToLoadLanguage = true;
        public LanguageHelper( ILocalResourceService ILocalResourceService , IUserContext IUserContext, ILanguageService ILanguageService) {
            _ILocalResourceService = ILocalResourceService;
            _IUserContext = IUserContext;
            _ILanguageService = ILanguageService;
            }

        public string GetResource(int LangId, string ResourceName)
        {
            try
            {
                if (UseCachToLoadLanguage)
                    return Cache.LocalResourcesCache.LocalResource.Where(s => s.Language.LanguageId == LangId).Where(d => d.LocalName == ResourceName).FirstOrDefault().LocalValue;
                else
                    return _ILocalResourceService.GetResource(LangId, ResourceName);

            }
            catch
            {

                return "";
            }

        }

    
        public string GetResource(string ResourceName)
        {
            try
            {
                if (UseCachToLoadLanguage)
                    return Cache.LocalResourcesCache.LocalResource.Where(s => s.Language.LanguageId == ((IUserContext)HttpContext.Current.Session[HttpContext.Current.Session.SessionID]).MyLanguage.LanguageId).Where(d => d.LocalName == ResourceName).FirstOrDefault().LocalValue;
                else
                    return _ILocalResourceService.GetResource(((IUserContext)HttpContext.Current.Session[HttpContext.Current.Session.SessionID]).MyLanguage.LanguageId, ResourceName);
            }
            catch
            {

                return "";
            }
        }

    
        public void ChangeLanguage( Language NewLanguage )
            {
            ((IUserContext)HttpContext.Current.Session[HttpContext.Current.Session.SessionID]).MyLanguage = NewLanguage;
            OtherHelpers.StoreModelToCookies("DefaultLan", NewLanguage);
      
        }

        public Language GetCurrentLanguage()
        {
            var serializer = new JavaScriptSerializer();
            var savedModel = serializer.Deserialize<Language>(HttpContext.Current.Request.Cookies["DefaultLan"].Value);
            if (savedModel == null)
                savedModel = ((IUserContext)HttpContext.Current.Session[HttpContext.Current.Session.SessionID]).MyLanguage;
            if (savedModel == null)
                savedModel = _ILanguageService.GetDefaultLanguage();
            return savedModel;
        }
    }
    }
