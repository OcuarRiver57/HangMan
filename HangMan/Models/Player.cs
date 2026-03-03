namespace HangMan.Models
{
    public class Player
    {
        public Player() { }
        public Player(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // encrypt somehow?
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
