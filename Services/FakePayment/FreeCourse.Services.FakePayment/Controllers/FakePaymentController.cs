using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FakePaymentController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Payment()
        {
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
