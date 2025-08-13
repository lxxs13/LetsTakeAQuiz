using GeekQuiz.Helpers;
using GeekQuiz.Models.DTOs;

namespace GeekQuiz.Interfaces
{
    public interface IQuestion
    {
        public ServiceResult<QuestionGridDTO> GetQuestions(int pageSize, int pageNumber, string? search, string? categoryId);
        public ServiceResult<QuestionDTO> GetQuestionList(int categoryId, int questionLength);
        public ServiceResult<QuizResultDTO> GetResult(QuizProcessDTO resultSubmiited);
        public ServiceResult<bool> CreateQuestion(QuestionInfo question);
        //public ServiceResult<QuestionDTO> UpdateClient(int id, QuestionDTO dto);
        public ServiceResult<bool> DeleteQuestion(int id);
        public bool ExistQuestion(int id);
    }
}
