using GeekQuiz.Interfaces;
using GeekQuiz.Models;
using GeekQuiz.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GeekQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestion _questionService;

        public QuestionsController(IQuestion questionService)
        {
            _questionService = questionService;   
        }

        [HttpGet]
        public IActionResult GetQuestions([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string? search, [FromQuery] string? categoryId)
        {
            var result = _questionService.GetQuestions(pageSize, pageNumber, search, categoryId);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);

        }

        [HttpGet]
        public IActionResult GetQuestionList([FromQuery] int categoryId, [FromQuery] int questionLength)
        {
            var result = _questionService.GetQuestionList(categoryId, questionLength);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult GetResult([FromBody] QuizProcessDTO quiz)
        {
            var result = _questionService.GetResult(quiz);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult PostQuestion([FromBody] QuestionInfo questionInfo)
        {
            var result = _questionService.CreateQuestion(questionInfo);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id) 
        {
            var result = _questionService.DeleteQuestion(id);

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);

        }
    }
}
