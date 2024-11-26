using AutoMapper;
using EcommerceAPI.Models.CouponModels;
using EcommerceAPI.Models.Dtos;
using EcommerceAPI.Models.ProductModels;

namespace EcommerceAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingconfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Coupon, CouponDto>();
            config.CreateMap<CouponDto, Coupon>();
            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();

        });
        return mappingconfig;


    }


}