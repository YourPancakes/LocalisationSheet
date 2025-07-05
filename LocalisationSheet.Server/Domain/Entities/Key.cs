namespace LocalisationSheet.Server.Domain.Entities
{
    public class Key
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Translation> Translations { get; set; } = new List<Translation>();
    }
}