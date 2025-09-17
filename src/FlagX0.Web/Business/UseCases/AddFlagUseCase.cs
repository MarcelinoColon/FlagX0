using FlagX0.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FlagX0.Web.Business.UseCases
{
    public class AddFlagUseCase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AddFlagUseCase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Execute(string flagName, string UserId) 
        {
            bool flagAlreadyExist = await _applicationDbContext.Flags
                .AnyAsync(a => a.Name.Equals(flagName, StringComparison.InvariantCultureIgnoreCase) && a.UserId == UserId);

            return true;
        }
    }
}
