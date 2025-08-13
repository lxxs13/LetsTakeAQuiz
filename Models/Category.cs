using System;
using System.Collections.Generic;

namespace GeekQuiz.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Deleted { get; set; }

    public bool Inactive { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
