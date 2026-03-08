namespace HangMan.Models
{
    public class PlayerPreferenceViewModel
    {
        public PlayerPreferenceViewModel() { }

        public PlayerPreferenceViewModel(PlayerPreferenceModel p, List<CategoryModel> c)
        {
            this.Preferences = p;
            this.Categories = c;
        }
        public PlayerPreferenceModel Preferences { get; set; } = new PlayerPreferenceModel();

        public List<CategoryModel> Categories { get; set; }
    }
}
