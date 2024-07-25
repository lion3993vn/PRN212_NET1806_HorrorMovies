using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class CriticReview
{
    public int ReviewId { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? CriticName { get; set; }

    public bool? IsTopCritic { get; set; }

    public string? PublicationName { get; set; }

    public string? ReviewUrl { get; set; }

    public string? Quote { get; set; }

    public string? ScoreSentiment { get; set; }

    public string? MovieId { get; set; }

    public virtual Movie? Movie { get; set; }
}
