namespace LocalisationSheet.Server.Domain.Entities
{
    public class Translation
    {
        public Guid KeyId { get; set; }
        public Key Key { get; set; } = null!;
        public Guid LanguageId { get; set; }
        public Language Language { get; set; } = null!;
        public string Value { get; set; } = string.Empty;
    }
}