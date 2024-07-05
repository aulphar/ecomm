using AutoMapper;
using EcommerceAPI.Models;
using EcommerceAPI.Models.Dto;

namespace EcommerceAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingconfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Coupon, CouponDto>();
            config.CreateMap<CouponDto, Coupon>();

        });
        return mappingconfig;


    }


}