namespace HangMan.Models
{
    public class FilteredWords
    {
        public string Search { get; set; } = "";
        
        public List<Word> Words { get; set; } = new List<Word>();
    }
}
