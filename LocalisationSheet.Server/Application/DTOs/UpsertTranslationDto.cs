namespace LocalisationSheet.Server.Application.DTOs
{
    public record UpsertTranslationDto(Guid KeyId, Guid LanguageId, string? Value);
}