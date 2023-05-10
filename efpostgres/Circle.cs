using System;
using System.Collections.Generic;

namespace efpostgres;

public partial class Circle
{
    public long Id { get; set; }

    public string? Title { get; set; }

    public string? AlternativeTitles { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
