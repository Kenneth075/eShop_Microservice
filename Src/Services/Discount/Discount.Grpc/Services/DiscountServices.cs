using Discount.Grpc.Data;
using Discount.Grpc.Model;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountServices(DiscountContext dbContext, ILogger<DiscountServices> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
                return new CouponModel { Id = 0, ProductName = "Product not found", Description = "No desc", Amount = 0 };

            var couponModel = coupon.Adapt<CouponModel>();

            logger.LogInformation($"Discount is retrieve for product name: {coupon.ProductName}, amount: {coupon.Amount}");

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request payload"));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Discount is successfully created. ProductName: {coupon.ProductName}");
            var modelModel = coupon.Adapt<CouponModel>();

            return modelModel;

        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request payload"));

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Discount is successfully Update. ProductName: {coupon.ProductName}");
            var modelModel = coupon.Adapt<CouponModel>();

            return modelModel;

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
 
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"{nameof(DeleteDiscount)}, productName {coupon.ProductName} deleted successfully");

            return new DeleteDiscountResponse { IsSucces = true };
        }
    }
}
