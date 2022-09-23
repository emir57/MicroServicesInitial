using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            var stream = new FileStream(path, FileMode.Create);

            await photo.CopyToAsync(stream, cancellationToken);

            var returnPath = $"photos/{photo.FileName}";

            return Ok(returnPath);
        }
    }
}
