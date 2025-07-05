using LocalisationSheet.Server.Domain.Entities;
using LocalisationSheet.Server.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(LocalizationDbContext context)
    {
        var languages = new[]
        {
            new Language { Code = "ru", Name = "Русский" },
            new Language { Code = "en", Name = "English" },
            new Language { Code = "tr", Name = "Türkçe" }
        };

        foreach (var lang in languages)
        {
            if (!context.Languages.Any(l => l.Code == lang.Code))
                context.Languages.Add(lang);
        }
        await context.SaveChangesAsync();

        var keys = new[]
        {
            new Key { Name = "Hello" },
            new Key { Name = "Save" },
            new Key { Name = "Cancel" }
        };

        foreach (var key in keys)
        {
            if (!context.Keys.Any(k => k.Name == key.Name))
                context.Keys.Add(key);
        }
        await context.SaveChangesAsync();

        var translations = new[]
        {
            new { Key = "Hello", Lang = "ru", Value = "Привет" },
            new { Key = "Hello", Lang = "en", Value = "Hello" },
            new { Key = "Hello", Lang = "tr", Value = "Merhaba" },

            new { Key = "Save", Lang = "ru", Value = "Сохранить" },
            new { Key = "Save", Lang = "en", Value = "Save" },
            new { Key = "Save", Lang = "tr", Value = "Kaydet" },

            new { Key = "Cancel", Lang = "ru", Value = "Отмена" },
            new { Key = "Cancel", Lang = "en", Value = "Cancel" },
            new { Key = "Cancel", Lang = "tr", Value = "İptal" },
        };

        foreach (var t in translations)
        {
            var key = context.Keys.FirstOrDefault(k => k.Name == t.Key);
            var lang = context.Languages.FirstOrDefault(l => l.Code == t.Lang);

            if (key != null && lang != null && !context.Translations.Any(tr => tr.KeyId == key.Id && tr.LanguageId == lang.Id))
            {
                context.Translations.Add(new Translation
                {
                    KeyId = key.Id,
                    LanguageId = lang.Id,
                    Value = t.Value
                });
            }
        }
        await context.SaveChangesAsync();
    }
}
