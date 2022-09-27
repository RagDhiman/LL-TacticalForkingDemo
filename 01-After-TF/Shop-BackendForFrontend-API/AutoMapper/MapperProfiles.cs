﻿using AutoMapper;
using Shop_BackendForFrontend_API.Domain;
using Shop_BackendForFrontend_API.Model;

namespace Shop_BackendForFrontend_API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<AccountModel, Account> ()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<AddressModel, Address>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<BillingAddressModel, BillingAddress>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CreditCardModel, CreditCard>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<DeliveryModel, Delivery>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<InventoryCheckModel, InventoryCheck>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<OrderModel, Order>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<OrderHistoryModel, OrderHistory>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<ProductModel, Product>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<ReturnModel, Return>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<StockModel, Stock>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SupplierModel, Supplier>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();


            
        }
    }
}
