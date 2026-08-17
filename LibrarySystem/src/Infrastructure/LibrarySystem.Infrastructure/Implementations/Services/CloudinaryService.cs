using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LibrarySystem.Application.Dtos.File;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Infrastructure.Implementations.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryService(ICloudinarySettings cloudinarySettings)
    {
        Account account = new Account(
            cloudinarySettings.Name,
            cloudinarySettings.Key,
            cloudinarySettings.Secret
            );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<UploadImageDto> ImageUploadAsync(IFormFile file)
    {
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        ImageUploadResult result = new();
        using (var stream = file.OpenReadStream()) {
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, stream)
            };

            result = await _cloudinary.UploadAsync(uploadParams);
        }

        return new UploadImageDto(result.PublicId, result.SecureUrl.ToString());
    }

    public async Task DeleteImageAsync(string publicId)
    {
        DeletionParams deletionParams = new(publicId);
        await _cloudinary.DestroyAsync(deletionParams);
    }
}
