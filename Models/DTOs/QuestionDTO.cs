
namespace GeekQuiz.Models.DTOs
{
    public class QuestionDTO
    {
        public List<QuestionInfo> Info { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class QuestionInfo
    {
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string Question { get; set; }
        public string? CorrectAnswer { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
    }

    public class QuestionGridDTO
    {
        public int Total { get; set; }
        public List<QuestionGrid> QuestionList { get; set; }
    }

    public class QuestionGrid
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string CategoryName { get; set; }
        public int PercentageCorrect { get; set; }
        public int PercentageIncorrect { get; set; }
    }
}
