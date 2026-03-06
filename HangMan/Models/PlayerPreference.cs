namespace HangMan.Models
{
    public class PlayerPreference
    {
        public PlayerPreference() { }
        public PlayerPreference(List<Category> c)
        {
            Categories = c;
            //Category = c[0];
        }
        public int Id { get; set; }
        public string CustomWord { get; set; } = "";
        public int MaxWordLength { get; set; } = 0;
        public int MinWordLength { get; set; } = 0;
        public int Health { get; set; } = 0;
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}
