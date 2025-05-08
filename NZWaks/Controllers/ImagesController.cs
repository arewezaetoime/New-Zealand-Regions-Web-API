using Microsoft.AspNetCore.Mvc;
using NZWaks.API.Models.Domain;
using NZWaks.API.Models.DTO;
using NZWaks.API.Repositories;

namespace NZWaks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {

                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { "jpg", "jpeg", "png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Invalid file format. Only JPEG, JPG, and PNG files are allowed.");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size exceeds the limit of 10MB.");
            }
        }
    }
}
