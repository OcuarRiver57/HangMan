namespace HangMan.Models
{
    public class FilteredWordsModel
    {
        public string Search { get; set; } = "";

        public string CategorySearch { get; set; } = "";
        
        public List<WordModel> Words { get; set; } = new List<WordModel>();
    }
}
