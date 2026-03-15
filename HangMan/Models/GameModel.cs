namespace HangMan.Models
{
    public class GameModel
    {        
        public string Word { get; set; } = string.Empty;
        
        public List<char> Answer
        {
            get
            {
                return Word.ToList();
            }
        }

        public int Health { get; set; } = 3;

        public List<char> CorrectGuesses { get; set; } = new List<char>();

        public List<char> IncorrectGuesses { get; set; } = new List<char>();

        public bool IsCustom { get; set; } = false;

        public bool IsWin
        {
            get
            {
                return Answer.OrderBy(c => c).SequenceEqual(CorrectGuesses.OrderBy(c => c));
            }
        }

        public string Category { get; set; } = string.Empty;

        public string DrugClassification { get; set; } = string.Empty;

        public bool DrugIsGeneric { get; set; } = false;
    }
}
