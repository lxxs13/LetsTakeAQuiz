using System;
using System.Collections.Generic;

namespace GeekQuiz.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string Question1 { get; set; } = null!;

    public string CorrectAnswer { get; set; } = null!;

    public string? OptionA { get; set; }

    public string? OptionB { get; set; }

    public string? OptionC { get; set; }

    public string? OptionD { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
