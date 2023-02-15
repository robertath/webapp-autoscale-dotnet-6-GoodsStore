using AutoMapper;
using GoodsStore.App.Models.AccessManagement;
using GoodsStore.App.Models.AccessManagement.ViewModels;
using GoodsStore.App.Models.AcessManagement.ViewModels;

namespace GoodsStore.App.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRegistrationViewModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<MenuRegistrationViewModel, Menu>()
                .ForMember(u => u.Enabled, opt => opt.MapFrom(x => x.Status == "Active" ? true : false));

            CreateMap<Menu, MenuRegistrationViewModel>()
                .ForMember(u => u.Status, opt => opt.MapFrom(x => x.Enabled == true ? "Active" : "Disactive"));
        }

        public class StatusConverter : ITypeConverter<string, Boolean>
        {
            public Boolean Convert(string source, Boolean destination, ResolutionContext context)
            {
                if (source == "Enabled")
                    return true;
                else 
                    return false;
            }
        }
    }
}

