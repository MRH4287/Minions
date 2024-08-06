using DataAccess.Contracts;
using MAAM.Models;
using Microsoft.AspNetCore.Mvc;

namespace MAAM.Controllers
{
    [Route("api/{assetId}/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ICollectionRepository<Image> _imageRepository;

        public ImageController(ICollectionRepository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpGet("{workerId}")]
        public async Task<IActionResult> Image([FromRoute] string assetId, [FromRoute] string workerId)
        {
            var image = await _imageRepository.Get(assetId, workerId);

            if (image == null) {
                return NotFound();
            }

            return File(image.Data, image.ContentType);
        }

        [HttpPost("{workerId}")]
        public async Task<IActionResult> Image([FromRoute] string assetId, [FromRoute] string workerId, [FromForm] IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            using var stream = file.OpenReadStream();
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            var data = new Image
            {
                Id = workerId,
                Data = buffer,
                ContentType = file.ContentType
            };

            await _imageRepository.Save(assetId, data);

            return  new JsonResult($"/minions/api/{assetId}/image/{workerId}");
        }
    }
}
