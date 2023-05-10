using System;
using System.Collections.Generic;

namespace efpostgres;

public partial class Creation
{
    public long Id { get; set; }

    public string CreationType { get; set; } = null!;

    public virtual ICollection<CreationRelation> CreationRelationOriginCreations { get; set; } = new List<CreationRelation>();

    public virtual ICollection<CreationRelation> CreationRelationRelatedCreations { get; set; } = new List<CreationRelation>();

    public virtual ICollection<Doujinshi> Doujinshis { get; set; } = new List<Doujinshi>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
