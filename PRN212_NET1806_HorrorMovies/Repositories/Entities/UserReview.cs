using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class UserReview
{
    public string UserId { get; set; } = null!;

    public float? Rating { get; set; }

    public bool? IsSuperReviewer { get; set; }

    public bool? HasSpoilers { get; set; }

    public bool? HasProfanity { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? MovieId { get; set; }

    public virtual Movie? Movie { get; set; }
}
