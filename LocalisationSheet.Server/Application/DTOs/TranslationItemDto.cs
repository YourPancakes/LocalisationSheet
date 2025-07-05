namespace LocalisationSheet.Server.Application.DTOs
{
    public class TranslationItemDto
    {
        public string Code { get; set; } = default!;
        public string? Value { get; set; }
    }
}