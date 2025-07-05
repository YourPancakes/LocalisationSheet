namespace LocalisationSheet.Server.Application.DTOs
{
    public record TranslationDto(Guid KeyId, Guid LanguageId, string? Value);
}