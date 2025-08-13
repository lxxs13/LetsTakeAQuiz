using System;
using System.Collections.Generic;

namespace GeekQuiz.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public string Username { get; set; } = null!;

    public int TotalScore { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
