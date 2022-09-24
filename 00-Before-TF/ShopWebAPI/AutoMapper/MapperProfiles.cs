using AutoMapper;
using ShopDomain.Model;

namespace ShopWebAPI.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<Account, Account>();
                //.ForMember(c => c.Id, opt => opt.Ignore())
                //.ReverseMap();
        }
    }
}
