using LibrarySystem.Application.Dtos.File;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Application.Interfaces.Services;

public interface ICloudinaryService
{
    Task<UploadImageDto> ImageUploadAsync(IFormFile file);
    Task DeleteImageAsync(string publicId);
}
