namespace LocalisationSheet.Server.Application.DTOs
{
    public class TranslationTableDto
    {
        public string KeyName { get; set; } = default!;
        public List<TranslationItemDto> Translations { get; set; } = new();
    }
}