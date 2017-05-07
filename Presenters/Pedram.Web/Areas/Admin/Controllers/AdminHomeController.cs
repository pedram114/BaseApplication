using Pedram.Core.Domain.Language;
using Pedram.Framework.Controller;
using Pedram.Framework.CustomizedAttributes;
using Pedram.Framework.Helpers;
using Pedram.Services.Services.Configuration.Interfaces;
using Pedram.Services.Services.Language.Interfaces;
using Pedram.Services.Services.Users;
using Pedram.Web.App_Start;
using Pedram.Web.Areas.Admin.Models.Management;
using Pedram.Web.Models.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pedram.Web.Areas.Admin.Controllers
{
    //[PedramAuthorize()]
    [PedramDescription("Pedram.Controller.AdminPanel.AdminHome")]
    public class AdminHomeController : BaseController
    {
        private ApplicationRoleManager _roleManager;
        private readonly ILanguageService _ILanguageService;
        private IConfigurationService _IConfigurationService;
        private ILanguageHelper _ILanguageHelper;
        private ILocalResourceService _ILocalResourceService;
        private IContextHelper _IContextHelper;

        public AdminHomeController (ApplicationRoleManager roleManager ,
                                    ILanguageService ILanguageService,
                                    IConfigurationService IConfigurationService,
                                    ILanguageHelper ILanguageHelper,
                                    ILocalResourceService ILocalResourceService,
                                    IContextHelper IContextHelper
            )
            {
            _roleManager = roleManager;
            _ILanguageService = ILanguageService;
            _IConfigurationService = IConfigurationService;
            _ILanguageHelper = ILanguageHelper;
            _ILocalResourceService = ILocalResourceService;
            _IContextHelper = IContextHelper;
            }


        [PedramDescription("Pedram.Controller.AdminPanel.AdminHome.Index")]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        [PedramDescription("Pedram.Controller.AdminPanel.AdminHome.ChangeSiteSetting")]
        [HttpGet]
        public ActionResult ChangeSiteSetting() {
            var lans = _ILanguageService.GetAllLanguages();
            var readeddata = _IConfigurationService.GetConfiguration();
            var model = new EditSettings();
            model.Settings = new List<Settings>();
            if (readeddata != null)
            {
                try
                {
                    model.LogoAddress = readeddata.LogoAddress;
                    model.UseCachToLoadLanguage = readeddata.UseCachToLoadLanguage;
                    model.UseEmailInsteadUserName = readeddata.UseEmailInsteadUserName;
                    model.WebsiteDomainName = readeddata.WebsiteDomainName;
                    int i = 0;
                    foreach (var item in lans)
                    {
                        model.Settings.Add(new Settings() { LanguageName = item.LangName,
                                                            LanguageId = item.LanguageId,
                            WebApplicationDescription = _ILanguageHelper.GetResource(item.LanguageId,readeddata.WebApplicationDescription),
                            WebApplicationName = _ILanguageHelper.GetResource(item.LanguageId, readeddata.WebApplicationName),
                            copywrite = _ILanguageHelper.GetResource(item.LanguageId, readeddata.copywrite),
                        });
                        i++;
                    }
                       
                    
                }
                catch (Exception e11)
                {

                }
            }
            else {
                model.LogoAddress = "";
                model.UseCachToLoadLanguage = true;
                model.UseEmailInsteadUserName = true;
                model.WebsiteDomainName = "http://www.IranDejak.com";

                int i = 0;
                foreach (var item in lans)
                {
                    model.Settings.Add(new Settings() { LanguageName = item.LangName, LanguageId = item.LanguageId });
                    i++;
                }

            }

              
            return View(model);
          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeSiteSetting(EditSettings model) {
            if (!ModelState.IsValid)
                return View(model);
            string path = "", path1 = "";
            var file = model.LogoImageUpload;
            if (file != null && file.ContentLength > 0)
                try
                {
                    if (!Directory.Exists(Path.Combine(Server.MapPath("~/LogoImage/"))))
                        Directory.CreateDirectory(Path.Combine(Server.MapPath("~/LogoImage/")));
                    path = Path.Combine(Path.Combine(Server.MapPath("~/LogoImage/")));
                    string[] temp22 = GetFileNameSlides(file.FileName);
                    string filename = OtherHelpers.GenerateNewId() + temp22[temp22.Length - 1];
                    path += filename;
                    path1 = "LogoImage\\" + filename;
                    file.SaveAs(path);
                    Pedram.Core.Domain.Management.Configuration InsertModel = new Pedram.Core.Domain.Management.Configuration();

                    string WebApplicationName, WebApplicationDescription, Copywrite;

                    WebApplicationName = "Pedram.WebApplicationName";
                    WebApplicationDescription = "Pedram.WebApplicationDescription";
                    Copywrite = "Pedram.Copywrite";
                    _ILocalResourceService.DeleteLocalResurce(WebApplicationName);
                    _ILocalResourceService.DeleteLocalResurce(WebApplicationDescription);
                    _ILocalResourceService.DeleteLocalResurce(Copywrite);


                    foreach (var item in model.Settings)
                    {
                        LocalResource temp = new LocalResource();
                        temp.Language = _ILanguageService.GetLanguage(item.LanguageId);
                        temp.LocalName = WebApplicationName;
                        temp.LocalValue = item.WebApplicationName;
                        var res1 = _ILocalResourceService.AddLocalResurce(temp);

                        temp = new LocalResource();
                        temp.Language = _ILanguageService.GetLanguage(item.LanguageId);
                        temp.LocalName = WebApplicationDescription;
                        temp.LocalValue = item.WebApplicationDescription;
                        res1 = _ILocalResourceService.AddLocalResurce(temp);

                        temp = new LocalResource();
                        temp.Language = _ILanguageService.GetLanguage(item.LanguageId);
                        temp.LocalName = Copywrite;
                        temp.LocalValue = item.copywrite;
                        res1 = _ILocalResourceService.AddLocalResurce(temp);

                    
                    }

                    InsertModel.WebApplicationName = WebApplicationName;
                    InsertModel.WebApplicationDescription = WebApplicationDescription;
                    InsertModel.copywrite = Copywrite;
                    InsertModel.LogoAddress = path1;
                    InsertModel.UseCachToLoadLanguage = model.UseCachToLoadLanguage;
                    InsertModel.UseEmailInsteadUserName = model.UseEmailInsteadUserName;
                    InsertModel.WebsiteDomainName = model.WebsiteDomainName;

                    var res = _IConfigurationService.SetConfiguration(InsertModel);
                    if (res == 1)
                    {
                        ViewBag.Message = "File uploaded successfully";
                        _IContextHelper.PostTransaction();
                        InlineUsefullMethods.ReadConfigs();
                        return RedirectToAction("Index");
                    }
                    else {
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }

            return View();

        }
        private string[] GetFileNameSlides(string FileName)
        {
            string[] temp22 = FileName.Split('/');
            if (temp22.Length == 1)
                temp22 = FileName.Split('\\');
            return temp22;

        }

    }
}