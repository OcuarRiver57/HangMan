using System.ComponentModel.DataAnnotations;

namespace HangMan.Models
{
    public class WordModel
    {
        public WordModel() { }

        public WordModel(string spelling, CategoryModel category)
        {
            this.Spelling = spelling;
            this.Category = category;
            
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        [StringLength(50)]
        public string Spelling { get; set; }

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

        public CategoryModel Category { get; set; }

        public string Link
        {
            get
            {
                if (this.Category.Name == "drug")
                    return $"https://www.drugs.com/{this.Spelling}.html";

                return "https://www.dictionary.com/browse/" + this.Spelling;
            }
        }

        public string DrugClassification { get; set; } = "Not a drug";

    }
}
