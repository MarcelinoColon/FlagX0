using FlagX0.Web.Business.UseCases;
using FlagX0.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlagX0.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FlagsController : Controller
    {
        private readonly AddFlagUseCase _addFlagUseCase;

        public FlagsController(AddFlagUseCase addFlagUseCase)
        {
            _addFlagUseCase = addFlagUseCase;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new FlagViewModel());
        }

        [HttpPost("create")]
        public async Task <IActionResult> AddFlagToDatabase(FlagViewModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isCreated = await _addFlagUseCase.Execute(request.Name, request.IsEnabled, userId);
        }
    }
}
