namespace GeekQuiz.Models.DTOs
{
    public class QuizProcessDTO
    {
        public int PlayerId { get; set; }
        public int CategoryId { get; set; }
        public List<QuizAnswerDTO> answers { get; set; }
    }

    public class QuizAnswerDTO
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
    }

    public class QuizResultDTO
    {
        public int TotalScore { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
    }

}
