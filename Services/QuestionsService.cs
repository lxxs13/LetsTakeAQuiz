using GeekQuiz.Helpers;
using GeekQuiz.Interfaces;
using GeekQuiz.Models;
using GeekQuiz.Models.DTOs;

namespace GeekQuiz.Services
{
    public class QuestionsService : IQuestion
    {
        private readonly GeekQuizContext _geekQuizContext;

        public QuestionsService(GeekQuizContext geekQuizContext)
        {
            _geekQuizContext = geekQuizContext;
        }

        public ServiceResult<QuestionGridDTO> GetQuestions(int pageSize, int pageNumber, string? search, string? categoryId)
        {
            var query = _geekQuizContext.Questions.AsQueryable();

            if (pageNumber < 1) pageNumber = 1;

            int skipAmount = (pageNumber - 1) * pageSize;

            if (categoryId != null && !categoryId.Trim().Equals(""))
            {
                var categoryIds = categoryId.Split(',', StringSplitOptions.RemoveEmptyEntries)
                   .Select(int.Parse)
                   .ToList();

                query = query.Where(e => e.CategoryId.HasValue && categoryIds.Contains(e.CategoryId.Value));
            }

            if (search != null && !search.Trim().Equals(""))
                query = query.Where(e => e.Question1.Contains(search.ToLower()));

            int total = query.Count();

            var questions = query.OrderBy(p => p.QuestionId) // Always order for consistent pagination
                                 .Skip(skipAmount)
                                 .Take(pageSize)
                                 .Select(e => new QuestionGrid
                                 {
                                     QuestionId = e.QuestionId,
                                     Question = e.Question1,
                                     CategoryName = e.Category.Name ?? "",
                                     PercentageCorrect = 0,
                                     PercentageIncorrect = 1,
                                 })
                                 .ToList();

            QuestionGridDTO dto = new QuestionGridDTO
            {
                Total = total,
                QuestionList = questions,
            };

            return ServiceResult<QuestionGridDTO>.Ok(dto);
        }

        public ServiceResult<QuestionDTO> GetQuestionList(int categoryId, int questionLength)
        {

            Category category = _geekQuizContext.Categories.Find(categoryId);

            if (category == null)
                return ServiceResult<QuestionDTO>.Fail("Categoría no encontrada");

            var query = _geekQuizContext.Questions.AsQueryable()
                .Where(question => question.CategoryId == categoryId)
                .OrderBy(question => Guid.NewGuid())
                .Take(questionLength)
                .ToList();

            if (query.Count == 0)
                return ServiceResult<QuestionDTO>.Fail("No se encontraron preguntas para la categoria seleccionada");

            var question = query.Select(question => new QuestionInfo
            {
                QuestionId = question.QuestionId,
                Question = question.Question1,
                OptionA = question.OptionA ?? "",
                OptionB = question.OptionB ?? "",
                OptionC = question.OptionC ?? "",
                OptionD = question.OptionD ?? "",
            }).ToList();

            var questionsList = new QuestionDTO
            {
                CategoryName = category.Name ?? "",
                CategoryId = category.CategoryId,
                Info = question
            };

            return ServiceResult<QuestionDTO>.Ok(questionsList);
        }

        public ServiceResult<QuizResultDTO> GetResult(QuizProcessDTO resultSubmiited)
        {
            int totalScore = 0;

            if (resultSubmiited == null)
                return ServiceResult<QuizResultDTO>.Fail("No se recibieron las respuestas");

            Player infoPlayer = _geekQuizContext.Players.Find(resultSubmiited.PlayerId);

            if(infoPlayer == null)
                return ServiceResult<QuizResultDTO>.Fail("No se encontró la información del jugador.");

            var results = resultSubmiited.answers.Select(a => a.QuestionId).ToList();

            var correctAnswers = _geekQuizContext.Questions
                .Where(q => results.Contains(q.QuestionId))
                .ToDictionary(q => q.QuestionId, q => q.CorrectAnswer);

            foreach (var answer in resultSubmiited.answers)
            {
                if(correctAnswers.TryGetValue(answer.QuestionId, out var correct)) {
                    if(string.Equals(correct.Trim(), answer.SelectedAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        totalScore += 1;
                    }
                }
            }

            int totalQuestions = resultSubmiited.answers.Count();
            int points = (totalScore * 100) / totalQuestions;

            var score = new Score
            {
                PlayerId = resultSubmiited.PlayerId,
                CategoryId = resultSubmiited.CategoryId,
                Points = points,
                PlayedAt = DateTime.Now
            };

            _geekQuizContext.Scores.Add(score);
            infoPlayer.TotalScore = infoPlayer.TotalScore + points;

            _geekQuizContext.SaveChangesAsync();

            QuizResultDTO resultDTO = new QuizResultDTO
            {
                TotalScore = points,
                CorrectAnswers = totalScore,
                TotalQuestions = totalQuestions,
            };

            return ServiceResult<QuizResultDTO>.Ok(resultDTO);
        }

        public ServiceResult<bool> CreateQuestion(QuestionInfo questionDto)
        {
            if (questionDto == null)
                return ServiceResult<bool>.Fail("No se recibió información acerca de la pregunta");

            try
            {
                Question question = new Question
                {
                    CategoryId = questionDto.CategoryId,
                    Question1 = questionDto.Question,
                    CorrectAnswer = "",
                    OptionA = questionDto.OptionA,
                    OptionB = questionDto.OptionB,
                    OptionC = questionDto.OptionC,
                    OptionD = questionDto.OptionD,
                };

                _geekQuizContext.Questions.Add(question);
                _geekQuizContext.SaveChanges();
            } catch (Exception e)
            {
                return ServiceResult<bool>.Fail("Ocurrió un error al guardar la información.");
            }
               
            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<bool> DeleteQuestion(int id)
        {
            if (!ExistQuestion(id))
                return ServiceResult<bool>.Fail("No existen preguntas con el ID ingresado");

            try
            {
                var question = _geekQuizContext.Questions.Find(id);
                _geekQuizContext.Remove<Question>(question);

                _geekQuizContext.SaveChanges();
            }
            catch (Exception e) { 
                return ServiceResult<bool>.Fail("Ocurrió un error al tratar de eliminar el registro.");
            }

            return ServiceResult<bool>.Ok(true);

        }

        public bool ExistQuestion(int id)
        {
            Question question = _geekQuizContext.Questions.Find(id);

            if (question == null)
                return false;
            else 
                return true;
        }
    }
}
