using Cash.Domain.Helpers;

namespace Cash.Domain.Services.Impl
{
    public class TransliterationService : ITransliterationService
    {
        public string Transliterate(string source) => Transliteration.Front(source);
    }
}