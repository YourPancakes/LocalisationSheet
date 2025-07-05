namespace LocalisationSheet.Server.Domain.Entities
{
    public class Language
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ICollection<Translation> Translations { get; set; } = new List<Translation>();
    }
}