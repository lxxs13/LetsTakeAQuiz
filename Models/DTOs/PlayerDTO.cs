namespace GeekQuiz.Models.DTOs
{
    public class PlayerDTO
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public string MostPlayedCategoryName { get; set; }
        public int TimesPlayed { get; set; }
    }
}
