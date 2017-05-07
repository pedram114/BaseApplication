using Pedram.Core.Domain.Users;
using Pedram.Framework.Controller;
using Pedram.Framework.Helpers;
using Pedram.Framework.StartUpClasses;
using Pedram.Services.Services.Language.Interfaces;
using Pedram.Web.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pedram.Web.Areas.Admin.Controllers
{
    public class CommonController : BaseController
        {
        private ILanguageService _ILanguageService;
        private readonly ILanguageHelper _ILanguageHelper;
        public CommonController( ILanguageService ILanguageService , ILanguageHelper ILanguageHelper ) {
            _ILanguageService = ILanguageService;
            _ILanguageHelper = ILanguageHelper;
            }
        // GET: Common
        [ChildActionOnly]
        public ActionResult SelectLanguage()
        {
            var Languages = _ILanguageService.GetAllLanguages();
            var model = new List<SelectLanguageModel>();
            AutoMapper.Mapper.Map( Languages, model );
            var currentLang = _ILanguageHelper.GetCurrentLanguage();
            int count = 0;
            foreach (var item in model)
                {
                item.Index = count;
                if (currentLang.LangName == item.LangName)
                    item.Selected = true;
                count++;
                }
            return PartialView(model);
            
        }

        public ActionResult ChangedLanguage(string SelectedId,string Url ) {
          //  string []temp= SelectedId.Split( ',' );
            _ILanguageHelper.ChangeLanguage( _ILanguageService.GetLanguage( int.Parse(SelectedId ) ) );
            return Redirect( Url );
            }



        }
}