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

            this.CreateMap<AddressModel, Address>()
                .ReverseMap();

            this.CreateMap<BillingAddressModel, BillingAddress>()
                .ReverseMap();

            this.CreateMap<CreditCardModel, CreditCard>()
                .ReverseMap();

            this.CreateMap<DeliveryModel, Delivery>()
                .ReverseMap();

            this.CreateMap<InventoryCheckModel, InventoryCheck>()
                .ReverseMap();

            this.CreateMap<OrderHistoryModel, OrderHistory>()
                .ReverseMap();

            this.CreateMap<OrderModel, Order>()
                .ReverseMap();

            this.CreateMap<ProductModel, Product>()
                .ReverseMap();

            this.CreateMap<ReturnModel, Return>()
                 .ReverseMap();

            this.CreateMap<StockModel, Stock>()
                .ReverseMap();

            this.CreateMap<SupplierModel, Supplier>()
                .ReverseMap();

        }
    }
}
