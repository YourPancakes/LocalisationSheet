namespace LocalisationSheet.Server.Application.DTOs
{
    public record TranslationTableDto
    {
        public string KeyName { get; set; } = default!;
        public IReadOnlyList<TranslationItemDto> Translations; //List<TranslationItemDto> Translations { get; init; } = new();
    }
}