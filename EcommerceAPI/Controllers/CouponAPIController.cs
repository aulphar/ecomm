using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Models.CouponModels;
using EcommerceAPI.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto GetAllCoupons()
        {
            try
            {
                IEnumerable<Coupon> coupons = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto GetCouponById(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(u => u.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(coupon);


            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
                
            }
            return _response;
        }

        [HttpGet]
        [Route("getbycode/{couponcode}")]
        public ResponseDto GetCouponByCode(string couponcode)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(u => u.CouponCode == couponcode);
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpPost]
       // [Authorize(Roles = "ADMIN")]
        public ResponseDto CreateCoupon([FromBody] CouponDto model)
        {
            try
            {
                Coupon couponobj = _mapper.Map<Coupon>(model);
                _db.Coupons.Add(couponobj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(couponobj);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
      //  [Authorize(Roles = "ADMIN")]
        public ResponseDto DeleteCoupon(int id)
        {
            try
            {

                Coupon coupon = _db.Coupons.First(u => u.CouponId == id);
                _db.Remove(coupon);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        [HttpPut]
  //      [Authorize(Roles = "ADMIN")]
        public ResponseDto UpdateCoupon([FromBody] CouponDto model)
        {
            try
            {
                Coupon couponobj = _mapper.Map<Coupon>(model);
                _db.Coupons.Update(couponobj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(couponobj);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
        
            return _response;
        }

        [HttpPatch]
      //  [Authorize(Roles = "ADMIN")]
        public ResponseDto PartialUpdateCoupon([FromBody] CouponDto model)
        {
            try
            {
                Coupon couponobj = _mapper.Map<Coupon>(model);
                _db.Coupons.Update(couponobj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(couponobj);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
        
            return _response;
        }




    }
}
