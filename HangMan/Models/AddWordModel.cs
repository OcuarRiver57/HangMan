using System.Data.Common;
using System.Runtime.CompilerServices;

namespace HangMan.Models
{
    public class AddWordModel
    {
        public AddWordModel()
        {
            this.Word = new();
            this.Categories = [];
        }

        public AddWordModel(List<CategoryModel> c, WordModel w)
        {
            this.Word = w;
            this.Categories = c;
        }

        public WordModel Word { get; set; }

        public List<CategoryModel> Categories { get; set; }
        public string SelectedCategoryName { get; set; } = string.Empty;

    }
}
