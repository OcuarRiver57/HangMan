namespace HangMan.Models
{
    public class Word
    {
        public int Id { get; set; }
        public required string Spelling { get; set; }
        public int Length
        {
            get
            {
                return this.Spelling.Length;
            }
        }
        public int VowelCount
        {
            get
            {
                char[] vowels = { 'a', 'e', 'i', 'o', 'u'};
                int count = 0;
                foreach (Char c in this.Spelling)
                    if (vowels.Contains(c))
                        count++;
                return count;
            }
        }
        public Category Category { get; set; }
    }
}
