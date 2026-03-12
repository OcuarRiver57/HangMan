using Microsoft.AspNetCore.Identity;

namespace HangMan.Models
{
    public class PlayerModel : IdentityUser
    {
        public PlayerModel() : base() { }

        public int GamesPlayed { get; set; } = 0;

        public int GamesWon { get; set; } = 0;

        public int GamesLost { get; set; } = 0;

        public string LongestWord { get; set; } = "";

        public int MistakesWithWin {  get; set; } = 0;

        public int MistakesTotal { get; set; } = 0;

        public float WinLossRatio
        {
            get
            {
                return (float) this.GamesWon / this.GamesLost;
            }
        }
        public float WinMistakeRatio
        {
            get
            {
                return (float) this.MistakesWithWin / this.GamesWon;
            }
        }
    }
}
