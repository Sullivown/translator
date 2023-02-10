namespace Translator.Models
{
    public class Calls
    {
        public int Id { get; set; }
        public string? OriginalText { get; set; }
        public string? TranslatorType { get; set; }
        public bool IsSuccessful { get; set; }
        public string? TranslatedText { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
