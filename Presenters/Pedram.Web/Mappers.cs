using AutoMapper;
using Pedram.Core.Domain.Language;
using Pedram.Core.Domain.Users;
using Pedram.Web.Areas.Admin.Models;
using Pedram.Web.Models.CommonModel;

namespace Pedram.Web
{
    public static class Mappers
    {
        public static void CreateMapper()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Language, SelectLanguageModel>();
                cfg.CreateMap<CustomRole, CustomRoleModel>();
                cfg.CreateMap<RoleAccess, RoleAccessModel>();
                cfg.CreateMap<CustomRole, EditRoleViewModel>();

                cfg.CreateMap<SelectLanguageModel, Language>();
                cfg.CreateMap<CustomRoleModel, CustomRole>();
                cfg.CreateMap<RoleAccessModel, RoleAccess>();
                cfg.CreateMap<EditRoleViewModel, CustomRole>();
                
          
            });

        }
    }
}