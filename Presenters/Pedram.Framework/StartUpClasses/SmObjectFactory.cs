using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Pedram.Core;
using Pedram.Core.Domain.Users;
using Pedram.Data;
using Pedram.Services.Services.Users;
using Pedram.Services.Services.Users.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


using StructureMap.Web;
using Microsoft.Owin.Security.DataProtection;
using System.Security.Principal;
using System.Security.Claims;
using Pedram.Services.Services.Language.Interfaces;
using Pedram.Services.Services.Language;
using Pedram.Framework.Helpers;
using Pedram.Framework.MyCodes;
using Pedram.Services.Services.Configuration;
using Pedram.Services.Services.Configuration.Interfaces;

namespace Pedram.Framework.StartUpClasses
    {
    public static class SmObjectFactory
        {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>( defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication );

        public static IContainer Container
            {
            get { return _containerBuilder.Value; }
            }

        private static Container defaultContainer()
            {
            return new Container( ioc =>
            {
             //   ioc.For<Microsoft.AspNet.SignalR.IDependencyResolver>().Singleton().Add<StructureMapSignalRDependencyResolver>();

                ioc.For<IIdentity>().Use( () => getIdentity() );

                ioc.For<IUnitOfWork>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<PedramDbContext>();
                // Remove these 2 lines if you want to use a connection string named connectionString1, defined in the web.config file.
                //.Ctor<string>("connectionString")
                //.Is("Data Source=(local);Initial Catalog=TestDbIdentity;Integrated Security = true");

                ioc.For<PedramDbContext>().HybridHttpOrThreadLocalScoped()
                   .Use( context => (PedramDbContext)context.GetInstance<IUnitOfWork>() );
                ioc.For<DbContext>().HybridHttpOrThreadLocalScoped()
                   .Use( context => (PedramDbContext)context.GetInstance<IUnitOfWork>() );

                ioc.For<IUserStore<ApplicationUser, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<CustomUserStore>();

                ioc.For<IRoleStore<CustomRole, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<RoleStore<CustomRole, int, CustomUserRole>>();

                ioc.For<IAuthenticationManager>()
                      .Use( () => HttpContext.Current.GetOwinContext().Authentication );

                ioc.For<IApplicationRoleManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationRoleManager>();

               
                ioc.For<RoleManager<CustomRole, int>>().HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationRoleManager> ();
               

                ioc.For<IApplicationUserManager>().HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationUserManager>()
                   .Ctor<IIdentityMessageService>( "smsService" ).Is<SmsService>()
                   .Ctor<IIdentityMessageService>( "emailService" ).Is<EmailService>()
                   .Setter( userManager => userManager.SmsService ).Is<SmsService>()
                   .Setter( userManager => userManager.EmailService ).Is<EmailService>();

                ioc.For<ApplicationUserManager>().HybridHttpOrThreadLocalScoped()
                   .Use( context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>() );

                ioc.For<ICustomRoleStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomRoleStore>();

           
                ioc.For<IIdentity>().Use( () => getIdentity() );
                
            
                ioc.For<IApplicationSignInManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationSignInManager>();

                

                ioc.For<IIdentityMessageService>().Use<SmsService>();
                ioc.For<IIdentityMessageService>().Use<EmailService>();

           
                ioc.For<ICustomUserStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomUserStore>();

                ioc.For<ILanguageService>().HybridHttpOrThreadLocalScoped().Use<LanguageService>();
                ioc.For<ILocalResourceService>().HybridHttpOrThreadLocalScoped().Use<LocalResourceService>();
                ioc.For<IConfigurationService>().HybridHttpOrThreadLocalScoped().Use<ConfigurationService>();


                ioc.For<ILanguageHelper>().HybridHttpOrThreadLocalScoped().Use<LanguageHelper>();
                ioc.For<IContextHelper>().HybridHttpOrThreadLocalScoped().Use<ContextHelper>();
                ioc.For<IUserContext>().HybridHttpOrThreadLocalScoped().Use<UserContext>();
                ioc.For<IGlobalUsersContext>().HybridHttpOrThreadLocalScoped().Use<GlobalUsersContext>();
              

                // ioc.For<ICategoryService>().Use<EfCategoryService>();
                //        ioc.For<IProductService>().Use<EfProductService>();
            });
            }
        private static IIdentity getIdentity()
            {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
                {
                return HttpContext.Current.User.Identity;
                }

            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
            }
        }
    }
