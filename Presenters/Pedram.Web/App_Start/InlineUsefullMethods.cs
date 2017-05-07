using Pedram.Framework.Helpers;
using Pedram.Framework.StartUpClasses;
using Pedram.Services.Services.Configuration.Interfaces;
using Pedram.Web.Models.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.App_Start
{
    public static class InlineUsefullMethods
    {
        public static void ReadConfigs() {
            var data = SmObjectFactory.Container.GetInstance<IConfigurationService>().GetConfiguration();

            if (data != null)
            {
                Pedram.Framework.Helpers.LanguageHelper.UseCachToLoadLanguage = data.UseCachToLoadLanguage;

                Configurations.UseCachToLoadLanguage = data.UseCachToLoadLanguage;
                Configurations.UseEmailInsteadUserName = data.UseEmailInsteadUserName;
                Configurations.LogoAddress = data.LogoAddress;
                Configurations.WebsiteDomainName = data.WebsiteDomainName;
                Configurations.WebApplicationDescription = SmObjectFactory.Container.GetInstance<ILanguageHelper>().GetResource(data.WebApplicationDescription);
                Configurations.WebApplicationName = SmObjectFactory.Container.GetInstance<ILanguageHelper>().GetResource(data.WebApplicationName);
                Configurations.copywrite = SmObjectFactory.Container.GetInstance<ILanguageHelper>().GetResource(data.copywrite);
            }
        }
    }
}