using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Movie
{
    public string MovieId { get; set; } = null!;

    public string? MovieTitle { get; set; }

    public int? MovieYear { get; set; }

    public string? MovieUrl { get; set; }

    public int? MovieRank { get; set; }

    public virtual ICollection<CriticReview> CriticReviews { get; set; } = new List<CriticReview>();

    public virtual ICollection<UserReview> UserReviews { get; set; } = new List<UserReview>();
}
