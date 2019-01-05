using AutoMapper;
using Czar.Cms.Models;
using Czar.Cms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czar.Cms.Admin.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            #region ManagerRole
            CreateMap<ManagerRoleAddOrModifyModel, ManagerRole>();
            #endregion
            #region Manager
            CreateMap<Manager, ManagerListModel>();

            CreateMap<ManagerAddOrModifyModel, Manager>();

            #endregion
            #region Menu
            CreateMap<MenuAddOrModifyModel, Menu>();

            #endregion
        }
    }
}
