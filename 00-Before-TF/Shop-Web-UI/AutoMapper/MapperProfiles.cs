using Shop_Web_UI.Models;
using Shop_Web_UI.DTOs;
using AutoMapper;

namespace AccountsManager_UI_Web.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<AccountModel, Account> ()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();


        }
    }
}
