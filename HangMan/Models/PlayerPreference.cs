namespace HangMan.Models
{
    public class PlayerPreference
    {
        public int Id { get; set; }
        public string CustomWord { get; set; } = "";
        public int MaxWordLength { get; set; } = 0;
        public int MinWordLength { get; set; } = 0;
        public int Health { get; set; } = 0;
    }
}
