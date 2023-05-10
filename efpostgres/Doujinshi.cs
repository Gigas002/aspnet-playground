using System;
using System.Collections.Generic;

namespace efpostgres;

public partial class Doujinshi
{
    public long Id { get; set; }

    public long CreationId { get; set; }

    public string Title { get; set; } = null!;

    public string[]? AlternativeTitles { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? AgeRating { get; set; }

    public string[]? AvailableAt { get; set; }

    public string[]? Genres { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public string[]? RelatedTo { get; set; }

    public string[]? Languages { get; set; }

    public string? Censorship { get; set; }

    public string? Picture { get; set; }

    public int? Length { get; set; }

    public int? Volumes { get; set; }

    public int? Chapters { get; set; }

    public bool? HasImages { get; set; }

    public bool? IsColored { get; set; }

    public virtual Creation Creation { get; set; } = null!;
}
