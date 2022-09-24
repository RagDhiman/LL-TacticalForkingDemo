using AutoMapper;
using ShopDomain.Model;
using ShopWebAPI.Model;

namespace ShopWebAPI.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<AccountModel, Account>()
                //.ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
