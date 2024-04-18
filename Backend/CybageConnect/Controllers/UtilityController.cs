using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CybageConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        //Post: For uploading photo of the post and profile
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Please select a file to upload.");
                }

                //getting temporary path to store the file
                var filePath = Path.GetTempFileName();

                //storing file at temporary location
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //creadential for cloudinary library
                Account? account = new Account(
                        "dtj4wy21a",
                        "247958963948524",
                        "LNgHS_KUg9UZTFb4Ms05nFymJuE");

                //creating object
                Cloudinary cloudinary = new Cloudinary(account);

                //uploafing image at server and getting response with link
                var result = cloudinary.Upload(new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    PublicId = Guid.NewGuid().ToString()
                });
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "File not uploaded on server");
            }
        }
    }
}
