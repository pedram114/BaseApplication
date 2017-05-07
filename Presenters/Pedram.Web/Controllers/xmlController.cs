using Pedram.Framework.Controller;
using Pedram.Framework.CustomizedAttributes;
using Pedram.Framework.Helpers;
using Pedram.Framework.SEO.SiteMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pedram.Web.Controllers
{
    public class xmlController : BaseController
    {
        ILanguageHelper _ILanguageHelper;
        public xmlController(ILanguageHelper ILanguageHelper) {
            _ILanguageHelper = ILanguageHelper;
        }
        // GET: xml
        
        public XmlSitemapResult sitemap()
        {
            List<SitemapItem> items = new List<SitemapItem>();
            DateTime lastModified = DateTime.Now;

            var Controllers = new ControllerHelper().GetWebUIControllersNameAnDescription();
          
            foreach (var cns in Controllers)
            {
                foreach (var actions in cns.Actions)
                {
                   
                        items.Add(new SitemapItem(Url.Action(actions.Name, cns.Name), 
                                  lastModified, 
                                  ChangeFrequency.Weekly, 
                                  1f,
                                  actions.Name));
                  
                }
            }

           
          
          //    items.Add(new SitemapItem("http://psi-co.net/support", lastModified, ChangeFrequency.Weekly, 1f));

            //foreach (Post item in posts)
            //{
            //    items.Add(new SitemapItem("http://psi-co.net/blog/posts/" + item.slug, item.publishedDate, ChangeFrequency.Yearly, .3f));
            //}

            //foreach (PortfolioItem item in portfolio)
            //{
            //    if (item.caseStudyUrl != null && item.caseStudyUrl != "")
            //        items.Add(new SitemapItem("http://psi-co.net/work/" + item.caseStudyUrl, lastModified, ChangeFrequency.Yearly, .3f));
            //}

            return new XmlSitemapResult(items);
        }

        [PedramDescription("Pedram.Controller.xmlController.ShowSitemap")]
        public ActionResult ShowSitemap() {
            List<SitemapItem> items = new List<SitemapItem>();
            DateTime lastModified = DateTime.Now;

            var Controllers = new ControllerHelper().GetWebUIControllersNameAnDescription();
          
            foreach (var cns in Controllers)
            {
                foreach (var actions in cns.Actions)
                {
                        if (!string.IsNullOrEmpty(actions.Description))
                        {
                   
                                items.Add(new SitemapItem(Url.Action(actions.Name, cns.Name),
                                      lastModified,
                                      ChangeFrequency.Weekly,
                                      1f,
                                      actions.Description));
                        }
                }
            }


            

            return View(items);
        }
    }
}

