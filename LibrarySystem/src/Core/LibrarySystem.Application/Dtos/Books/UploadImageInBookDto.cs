using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Application.Dtos.Books;

public record UploadImageInBookDto
 (
    IFormFile image
    );
