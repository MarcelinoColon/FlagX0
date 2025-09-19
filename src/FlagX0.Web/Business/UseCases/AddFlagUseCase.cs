using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlagX0.Web.Business.UseCases
{
    public class AddFlagUseCase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddFlagUseCase(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Execute(string flagName, bool isActive) 
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            FlagEntity entity = new()
            {
                Name = flagName,
                UserId = userId,
                Value = isActive
            };

            var response = await _applicationDbContext.Flags.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
