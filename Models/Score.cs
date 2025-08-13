using System;
using System.Collections.Generic;

namespace GeekQuiz.Models;

public partial class Score
{
    public int ScoreId { get; set; }

    public int PlayerId { get; set; }

    public int CategoryId { get; set; }

    public int Points { get; set; }

    public DateTime PlayedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
