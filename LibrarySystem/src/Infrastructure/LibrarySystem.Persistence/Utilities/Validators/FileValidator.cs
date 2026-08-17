using LibrarySystem.Domain.Entities;
using LibrarySystem.Persistence.Utilities.Enums;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Persistence.Utilities.Validators;

public static class FileValidator
{
    public static bool FileTypeValidator(this IFormFile file, string type) => file.ContentType.Contains(type);

    public static bool FileSizeValidator(this IFormFile file, int size, SizeType sizeType)
    {
        switch (sizeType)
        {
            case SizeType.KB:
                return file.Length < size * 1024;
            case SizeType.MB:
                return file.Length < size * 1024 * 1024;
            case SizeType.GB:
                return file.Length < size * 1024 * (1024 * 1024);
        }
        return false;
    }
}
