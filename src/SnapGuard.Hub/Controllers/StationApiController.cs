using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SnapGuard.Hub.Configurations;
using SnapGuard.Hub.DTOs.Requests;
using SnapGuard.Hub.DTOs.Responses;
using SnapGuard.Hub.Helpers;

namespace SnapGuard.Hub.Controllers;

[ApiController]
[Route("api/station")]
public class StationApiController(
    IOptions<PictureServerSettings> settings,
    ILogger<StationApiController> logger
) : ControllerBase
{
    public PictureServerSettings _settings = settings.Value;
    public ILogger<StationApiController> _logger = logger;

    [HttpPost("{stationId}/uploadPicture")]
    [ProducesResponseType<PictureUploadResponse>(200)]
    public async Task<IActionResult> UploadPicture(int stationId, PictureUploadRequest request)
    {
        // Verifica el tamaño del archivo
        if (request.Picture.Length == 0 || request.Picture.Length > 5 * 1024 * 1024)
            return BadRequest("Invalid picture size");

        // Valida que el archivo corresponda a una imagen válida
        using var pictureStream = request.Picture.OpenReadStream();
        string contentType = request.Picture.ContentType;
        bool isValid = await ImageValidator.Validate(contentType, pictureStream);

        if (!isValid)
            return BadRequest("Invalid picture format");

        // Asigna un nombre y una ruta para el archivo
        string fileName = $"{Guid.NewGuid()}.{ImageValidator.GetExtension(contentType)}";
        string filePath = $"{_settings.DirectoryPath}/{fileName}";

        // Crea un nuevo archivo en donde copiar el contenido de la imagen
        using var fileStream = System.IO.File.Create(filePath);
        await request.Picture.CopyToAsync(fileStream);
        await fileStream.FlushAsync();

        // Envia el identificador de la imagen como respuesta
        return Ok(new PictureUploadResponse
        {
            PictureId = Random.Shared.Next()
        });
    }
}
