
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangMan.Models
{
    public class PlayerPreferenceModel
    {
        public PlayerPreferenceModel() { }

        [Key]
        public string PlayerId { get; set; } = string.Empty;

        public string CustomWord { get; set; } = "";

        public int MaxWordLength { get; set; } = 0;

        public int MinWordLength { get; set; } = 0;

        public int Health { get; set; } = 0;

        public string Category { get; set; } = string.Empty;

    }
}
